# Pruebas de validación — Sistema de Gestión de Rally Universitario

Suite de pruebas automatizadas (xUnit) que valida la lógica de negocio del sistema:
reglas de inscripción y cupos, cálculo de clasificaciones/podio, seguridad de
contraseñas, validaciones de modelos y persistencia real contra SQLite.

## Cómo ejecutarla

Requiere el SDK de .NET (probado con .NET SDK 10). No requiere Visual Studio.

```bash
cd Tests/SistemaRallyTests
dotnet test
```

Para ver el detalle de cada prueba:

```bash
dotnet test --logger "console;verbosity=detailed"
```

## Cómo está armada

- El proyecto **no duplica** el código: compila directamente los archivos reales de
  `../../Models`, `../../Services` y `../../Data` (ver `SistemaRallyTests.csproj`),
  así que las pruebas corren contra la lógica de producción tal cual, no contra una copia.
- No referencia `System.Windows.Forms`: Models/Services/Data no dependen de la UI,
  así que el proyecto de pruebas es liviano y no necesita las `Views/`.
- Las pruebas de `Data/` usan una base SQLite real y aislada (se resuelve dentro de
  `bin/Debug/net48/`, nunca toca el `rally_ulsa.db` del proyecto principal). Se
  recrea desde cero al inicio de cada ejecución mediante `DatabaseFixture` y todas
  esas pruebas comparten la colección `"Base de datos SQLite"` para correr en
  secuencia (evita contención sobre el único archivo `.db`).
- Cada prueba de integración genera sus propios datos con nombres únicos (sufijo GUID)
  para no interferir entre sí, sin necesidad de truncar tablas entre pruebas.

## Qué cubre

| Archivo | Qué valida |
|---|---|
| `ValidacionServiceTests.cs` | Reglas de formato de correo, rango de tiempo (0–7200s), texto requerido, cupo (1–20) |
| `SeguridadHelperTests.cs` | Hashing SHA-256, compatibilidad con contraseñas legadas en texto plano, y una **vulnerabilidad de seguridad confirmada** (ver más abajo) |
| `ModelosValidacionTests.cs` | `Validar()` de `Estudiante`, `Organizador`, `Carrera`, `Reto` |
| `ClasificacionItemTests.cs` | Formato de tiempo `mm:ss.cc` / `hh:mm:ss.cc` |
| `CalculadorEstadisticasRallyTests.cs` | Motor LINQ de clasificación general, por reto, dashboard y estado de equipos (desempates, filtros, umbrales) |
| `Data/InscripcionRepositoryTests.cs` | Cupo por reto+carrera, cupo global de 20 por carrera en el rally, duplicados, transacciones |
| `Data/ResultadoRepositoryTests.cs` | Upsert de resultados, rango de tiempo, inscripción inexistente |
| `Data/CrudRepositoriesTests.cs` | CRUD de Carrera/Estudiante/Reto, autenticación de Organizador |
| `Data/DatabaseHelperYCascadaTests.cs` | Idempotencia de `InicializarBaseDatos`, `PRAGMA foreign_keys`, cascadas de borrado |
| `Data/ClasificacionRepositoryTests.cs` | Flujo de extremo a extremo contra SQLite real |

## Hallazgos de esta ronda de pruebas

**Corregidos** (bug real o mejora de bajo riesgo, con prueba de regresión):
- `Data/RetoRepository.cs` y `Data/ResultadoRepository.cs` mostraban `""` en vez de
  `"No asignado"` / `"Sistema"` cuando no había organizador asignado. Causa: el patrón
  `reader["col"]?.ToString() ?? "valor"` no detecta `DBNull.Value` (no es un `null` de
  C#), y `DBNull.Value.ToString()` devuelve `""`, así que el `??` nunca se activaba.
- `Models/Reto.CupoMaximoPorCarrera` sustituía silenciosamente cualquier valor `<= 0`
  por `5`, lo que hacía inalcanzable la regla "cupo mínimo 1" de
  `ValidacionService.ValidarCupo` a través del modelo. Se quitó la sustitución (tanto en
  el setter como en el mismo patrón redundante de `RetoRepository.Insertar/Actualizar`);
  ahora `Validar()` sí puede rechazar un cupo inválido antes de guardar. Sin cambio de
  comportamiento visible para el usuario: el `NumericUpDown` de `FormEditorReto` ya
  impedía cargar un valor `< 1`.
- El parámetro `retos` de `CalcularClasificacionGeneral` no se usaba en absoluto dentro
  del método. Se eliminó de la firma, y de paso `ClasificacionRepository.ObtenerClasificacionGeneral()`
  dejó de hacer una consulta `SELECT` a `Reto` que no aportaba nada al resultado.

**Reportado, pendiente de decisión** (cambia comportamiento de seguridad, no se tocó sin confirmar):
- `Services/SeguridadHelper.ValidarContrasena` compara primero en texto plano antes de
  comparar por hash. Quien obtenga el hash almacenado (p. ej. leyendo `rally_ulsa.db`)
  puede autenticarse enviando ese mismo hash como "contraseña", sin conocer la
  contraseña real (estilo *pass-the-hash*). Ver la prueba
  `ValidarContrasena_VULNERABILIDAD_CONFIRMADA_...` en `SeguridadHelperTests.cs`.
  Deprioritizado por el equipo por ahora.

**Documentado, no es un bug — decisión de alcance pendiente**:
- No existe ninguna forma en la app (ni repositorio ni UI) de crear un segundo
  `Organizador` ni de cambiar la contraseña del admin sembrado (`admin@ulsa.edu` / `1234`).
  `OrganizadorRepository` solo tiene `Autenticar` y `ObtenerTodos`; no hay `Insertar` ni
  `Actualizar`, y ninguna pantalla lo permite. Corregirlo es agregar una funcionalidad
  nueva (pantalla + repositorio), no ajustar una existente, así que no se implementó sin
  antes confirmarlo.
