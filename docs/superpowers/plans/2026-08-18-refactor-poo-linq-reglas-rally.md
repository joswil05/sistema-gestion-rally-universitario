# Plan de Refactorización Integral: POO, LINQ y Reglas del Rally Universitario

> **Para agentes y desarrolladores:** SUB-HABILIDAD REQUERIDA: Usar `superpowers:subagent-driven-development` (recomendado) o `superpowers:executing-plans` para implementar este plan tarea por tarea. Cada paso utiliza la sintaxis de casilla de verificación (`- [ ]`).

**Meta:** Elevar el cumplimiento técnico del "Sistema de Gestión de Rally Universitario - ULSA" al 100% de la rúbrica oficial de evaluación de Lenguaje de Programación III, estructurando una jerarquía POO limpia (`Persona`, `IValidable`, `IRepositorio<T>`), procesando estadísticas y clasificaciones mediante tuberías declarativas LINQ en memoria, e incorporando la regla de negocio estricta de 20 estudiantes por carrera en el rally.

**Arquitectura:** Arquitectura modular en 4 capas (.NET Framework 4.8 / WinForms / SQLite):
1. **Dominio (`Models/`):** Jerarquía de clases con clase base abstracta `Persona`, contratos `IValidable`, propiedades con invariantes protegidos y polimorfismo dinámico.
2. **Acceso a Datos (`Data/`):** Contrato genérico `IRepositorio<T>`, transacciones ACID serializables con SQLite y extracción de datos planos para procesamiento en memoria.
3. **Lógica de Negocio y Servicios (`Services/`):** `CalculadorEstadisticasRally` con operadores LINQ avanzados (`GroupBy`, `Min`, `Sum`, `OrderByDescending`, `Select`, `Aggregate`, `Take`) y `ValidacionService` con control de cupos (tope de 20 por carrera y cupo por reto).
4. **Presentación (`Views/`):** Windows Forms asíncrono con UserControls desacoplados y filtrado LINQ en memoria.

**Tech Stack:** C# 7.3, .NET Framework 4.8, Windows Forms, SQLite (`System.Data.SQLite.Core` 1.0.119), LINQ to Objects, MSBuild.

**Especificación y Rúbrica de Referencia:** `Guia de Proyectos de Fin de Curso 2026 - Lenguaje de Programacion III.md` y `docs/auditoria/AUDITORIA-CODIGO-VS-RUBRICA.md`.

## Restricciones Globales
- **Compilador obligatorio:** Usar siempre MSBuild de Visual Studio (`MSBuild.exe "SistemadeGestiondeRallyUniversitario.csproj" /t:Build /p:Configuration=Debug`). No ejecutar `dotnet build` sobre el csproj antiguo para no corromper los assets de NuGet.
- **Formato csproj:** Cada nuevo archivo `.cs` debe ser registrado explícitamente en `SistemadeGestiondeRallyUniversitario.csproj` dentro del `<ItemGroup>` de compilación.
- **Integridad de Base de Datos e Interfaz:** No modificar nombres de tablas ni columnas en SQLite (`rally_ulsa.db`). No romper los eventos ni controles de Windows Forms.
- **Cero código muerto:** No dejar imports `using System.Linq;` sin utilizar ni métodos vacíos sin propósito demostrable.

---

### Tarea 1: Contratos e Interfaces de Dominio (`IValidable.cs` e `IRepositorio.cs`)

**Archivos:**
- Crear: `Models/IValidable.cs`
- Crear: `Data/IRepositorio.cs`
- Modificar: `SistemadeGestiondeRallyUniversitario.csproj`

**Interfaces:**
- Consume: `System.Collections.Generic`
- Produce: `IValidable` (`bool Validar(out string mensajeError)`), `IRepositorio<T>` (`List<T> ObtenerTodos()`, `bool Insertar(T entidad)`, `bool Actualizar(T entidad)`, `bool Eliminar(int id)`).

- [ ] **Paso 1: Crear la interfaz `IValidable`**

```csharp
namespace SistemadeGestiondeRallyUniversitario.Models
{
    /// <summary>
    /// Contrato que define la capacidad de un modelo de dominio para auto-validar sus reglas de integridad e invariantes.
    /// </summary>
    public interface IValidable
    {
        /// <summary>
        /// Valida el estado interno del objeto según las reglas de negocio.
        /// </summary>
        /// <param name="mensajeError">Mensaje explicativo en caso de validación fallida.</param>
        /// <returns>True si el objeto cumple con todas las reglas; False en caso contrario.</returns>
        bool Validar(out string mensajeError);
    }
}
```

- [ ] **Paso 2: Crear la interfaz genérica `IRepositorio<T>`**

```csharp
using System.Collections.Generic;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    /// <summary>
    /// Contrato genérico base para la capa de persistencia y acceso a datos (Patrón Repository).
    /// </summary>
    /// <typeparam name="T">Tipo de entidad de dominio gestionada.</typeparam>
    public interface IRepositorio<T> where T : class
    {
        List<T> ObtenerTodos();
        bool Insertar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(int id);
    }
}
```

- [ ] **Paso 3: Registrar los nuevos archivos en `SistemadeGestiondeRallyUniversitario.csproj`**

Añadir las entradas `<Compile Include="Models\IValidable.cs" />` y `<Compile Include="Data\IRepositorio.cs" />` dentro del `<ItemGroup>` correspondiente.

- [ ] **Paso 4: Compilar para verificar sintaxis de interfaces**

Ejecutar:
```powershell
& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "SistemadeGestiondeRallyUniversitario.csproj" /t:Build /p:Configuration=Debug
```
Esperado: Compilación correcta (0 advertencias, 0 errores).

---

### Tarea 2: Jerarquía POO y Polimorfismo (`Persona.cs`, `Estudiante.cs`, `Organizador.cs`)

**Archivos:**
- Crear: `Models/Persona.cs`
- Modificar: `Models/Estudiante.cs`
- Modificar: `Models/Organizador.cs`
- Modificar: `SistemadeGestiondeRallyUniversitario.csproj`

**Interfaces:**
- Consume: `IValidable`, `ValidacionService`
- Produce: Clase abstracta `Persona`, clases derivadas `Estudiante` y `Organizador` con polimorfismo dinámico (`ObtenerRol()`, `ObtenerIdentificadorPrincipal()`, `Validar()`, `ToString()`).

- [ ] **Paso 1: Crear la clase base abstracta `Persona.cs`**

```csharp
using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    /// <summary>
    /// Clase base abstracta que modela los atributos y comportamientos comunes de cualquier participante o miembro del rally.
    /// </summary>
    public abstract class Persona : IValidable
    {
        private string _nombre;
        private string _apellido;

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string Apellido
        {
            get => _apellido;
            set => _apellido = value?.Trim() ?? string.Empty;
        }

        public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

        protected Persona()
        {
            _nombre = string.Empty;
            _apellido = string.Empty;
        }

        protected Persona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        /// <summary>
        /// Método polimórfico abstracto que devuelve el rol de la persona en el sistema.
        /// </summary>
        public abstract string ObtenerRol();

        /// <summary>
        /// Método polimórfico abstracto que devuelve el identificador o credencial principal de contacto.
        /// </summary>
        public abstract string ObtenerIdentificadorPrincipal();

        /// <summary>
        /// Validación base para nombres y apellidos.
        /// </summary>
        public virtual bool Validar(out string mensajeError)
        {
            if (!ValidacionService.ValidarTextoRequerido(Nombre, "Nombre", out mensajeError))
                return false;

            if (!ValidacionService.ValidarTextoRequerido(Apellido, "Apellido", out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString()
        {
            return $"{NombreCompleto} ({ObtenerRol()})";
        }
    }
}
```

- [ ] **Paso 2: Refactorizar `Models/Estudiante.cs` para heredar de `Persona`**

```csharp
using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Estudiante : Persona
    {
        public int IdEstudiante { get; set; }
        public string CorreoULSA { get; set; }
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }

        public Estudiante() : base()
        {
            CorreoULSA = string.Empty;
            NombreCarrera = string.Empty;
        }

        public Estudiante(int idEstudiante, string nombre, string apellido, string correoULSA, int idCarrera, string nombreCarrera = "")
            : base(nombre, apellido)
        {
            IdEstudiante = idEstudiante;
            CorreoULSA = correoULSA?.Trim() ?? string.Empty;
            IdCarrera = idCarrera;
            NombreCarrera = nombreCarrera?.Trim() ?? string.Empty;
        }

        public override string ObtenerRol() => "Estudiante";

        public override string ObtenerIdentificadorPrincipal() => CorreoULSA;

        public override bool Validar(out string mensajeError)
        {
            if (!base.Validar(out mensajeError))
                return false;

            if (IdCarrera <= 0)
            {
                mensajeError = "El estudiante debe estar asignado a una carrera válida.";
                return false;
            }

            if (!ValidacionService.ValidarCorreoULSA(CorreoULSA, out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(NombreCarrera)
                ? NombreCompleto
                : $"{NombreCompleto} - {NombreCarrera}";
        }
    }
}
```

- [ ] **Paso 3: Refactorizar `Models/Organizador.cs` para heredar de `Persona`**

```csharp
using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Organizador : Persona
    {
        public int IdOrganizador { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public Organizador() : base()
        {
            Correo = string.Empty;
            Contrasena = string.Empty;
        }

        public Organizador(int idOrganizador, string nombre, string apellido, string correo, string contrasena)
            : base(nombre, apellido)
        {
            IdOrganizador = idOrganizador;
            Correo = correo?.Trim() ?? string.Empty;
            Contrasena = contrasena ?? string.Empty;
        }

        public override string ObtenerRol() => "Organizador";

        public override string ObtenerIdentificadorPrincipal() => Correo;

        public override bool Validar(out string mensajeError)
        {
            if (!base.Validar(out mensajeError))
                return false;

            if (string.IsNullOrWhiteSpace(Correo))
            {
                mensajeError = "El correo del organizador es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Contrasena))
            {
                mensajeError = "La contraseña del organizador es obligatoria.";
                return false;
            }

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString()
        {
            return NombreCompleto;
        }
    }
}
```

- [ ] **Paso 4: Registrar `Models/Persona.cs` en el `.csproj` y compilar**

Añadir `<Compile Include="Models\Persona.cs" />` al csproj y compilar con MSBuild.

---

### Tarea 3: Encapsulamiento y Validación en `Reto.cs` y `Carrera.cs`

**Archivos:**
- Modificar: `Models/Reto.cs`
- Modificar: `Models/Carrera.cs`

**Interfaces:**
- Consume: `IValidable`, `ValidacionService`
- Produce: Modelos robustos con invariantes protegidos en setters y validación formal.

- [ ] **Paso 1: Implementar `IValidable` y protección de invariantes en `Models/Reto.cs`**

```csharp
using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Reto : IValidable
    {
        private int _cupoMaximoPorCarrera = 5;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;

        public int IdReto { get; set; }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string Descripcion
        {
            get => _descripcion;
            set => _descripcion = value?.Trim() ?? string.Empty;
        }

        public int CupoMaximoPorCarrera
        {
            get => _cupoMaximoPorCarrera;
            set => _cupoMaximoPorCarrera = value > 0 ? value : 5;
        }

        public int? IdOrganizador { get; set; }
        public string NombreOrganizador { get; set; }

        public Reto()
        {
            CupoMaximoPorCarrera = 5;
            NombreOrganizador = "No asignado";
        }

        public Reto(int idReto, string nombre, string descripcion, int cupoMaximoPorCarrera, int? idOrganizador, string nombreOrganizador = "")
        {
            IdReto = idReto;
            Nombre = nombre;
            Descripcion = descripcion;
            CupoMaximoPorCarrera = cupoMaximoPorCarrera;
            IdOrganizador = idOrganizador;
            NombreOrganizador = string.IsNullOrEmpty(nombreOrganizador) ? "No asignado" : nombreOrganizador.Trim();
        }

        public bool Validar(out string mensajeError)
        {
            if (!ValidacionService.ValidarTextoRequerido(Nombre, "Nombre del Reto", out mensajeError))
                return false;

            if (!ValidacionService.ValidarCupo(CupoMaximoPorCarrera, out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString() => Nombre;
    }
}
```

- [ ] **Paso 2: Implementar `IValidable` en `Models/Carrera.cs`**

```csharp
using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Carrera : IValidable
    {
        private string _nombre = string.Empty;
        private string _facultad = string.Empty;

        public int IdCarrera { get; set; }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string Facultad
        {
            get => _facultad;
            set => _facultad = value?.Trim() ?? string.Empty;
        }

        public Carrera() { }

        public Carrera(int idCarrera, string nombre, string facultad)
        {
            IdCarrera = idCarrera;
            Nombre = nombre;
            Facultad = facultad;
        }

        public bool Validar(out string mensajeError)
        {
            if (!ValidacionService.ValidarTextoRequerido(Nombre, "Nombre de Carrera", out mensajeError))
                return false;

            if (!ValidacionService.ValidarTextoRequerido(Facultad, "Facultad", out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString() => $"{Nombre} ({Facultad})";
    }
}
```

- [ ] **Paso 3: Compilar con MSBuild**

---

### Tarea 4: Implementación de Contrato Genérico en Repositorios (`Data/`)

**Archivos:**
- Modificar: `Data/CarreraRepository.cs` (agrega `: IRepositorio<Carrera>`)
- Modificar: `Data/RetoRepository.cs` (agrega `: IRepositorio<Reto>`)
- Modificar: `Data/EstudianteRepository.cs` (agrega `: IRepositorio<Estudiante>`)

**Interfaces:**
- Consume: `IRepositorio<T>`, `DatabaseHelper`
- Produce: Repositorios tipados bajo contrato formal, con manejo seguro de nulos en strings.

- [ ] **Paso 1: Adaptar `CarreraRepository.cs` al contrato `IRepositorio<Carrera>`**
- [ ] **Paso 2: Adaptar `RetoRepository.cs` al contrato `IRepositorio<Reto>`**
- [ ] **Paso 3: Adaptar `EstudianteRepository.cs` al contrato `IRepositorio<Estudiante>`**
- [ ] **Paso 4: Compilar con MSBuild para verificar cumplimiento de contratos de interfaz**

---

### Tarea 5: Motor de Procesamiento LINQ en Memoria (`Services/CalculadorEstadisticasRally.cs`)

**Archivos:**
- Crear: `Services/CalculadorEstadisticasRally.cs`
- Modificar: `Data/ClasificacionRepository.cs`
- Modificar: `SistemadeGestiondeRallyUniversitario.csproj`

**Interfaces:**
- Consume: `List<Resultado>`, `List<Inscripcion>`, `List<Carrera>`, `List<Reto>`, `List<Estudiante>`
- Produce: Pipeline LINQ declarativo en memoria que reemplaza las consultas complejas de base de datos (`GroupBy`, `Min`, `Sum`, `OrderByDescending`, `ThenBy`, `Select` proyectado con índice, `Aggregate`, `Take`).

- [ ] **Paso 1: Crear `Services/CalculadorEstadisticasRally.cs`**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Services
{
    /// <summary>
    /// Motor analítico en memoria para el procesamiento de clasificaciones, desempates y estadísticas del Rally.
    /// Implementa operaciones declarativas avanzadas con LINQ sobre colecciones IEnumerable y List.
    /// </summary>
    public class CalculadorEstadisticasRally
    {
        /// <summary>
        /// Calcula la Clasificación General del Rally utilizando LINQ puro en memoria.
        /// Regla de Desempate: 1) Más retos completados (Desc), 2) Menor tiempo acumulado (Asc), 3) Mayor participación (Desc).
        /// </summary>
        public List<ClasificacionItem> CalcularClasificacionGeneral(
            IEnumerable<Carrera> carreras,
            IEnumerable<Reto> retos,
            IEnumerable<Inscripcion> inscripciones,
            IEnumerable<Resultado> resultados)
        {
            if (carreras == null || resultados == null)
                return new List<ClasificacionItem>();

            var listaInscripciones = inscripciones?.ToList() ?? new List<Inscripcion>();
            var listaResultados = resultados.ToList();

            // 1. LINQ: Para cada carrera y reto, obtener la mejor marca representativa (Mínimo tiempo)
            var mejoresMarcas = listaResultados
                .Where(r => r.Tiempo > 0)
                .GroupBy(r => new { r.NombreCarrera, r.NombreReto })
                .Select(g => new
                {
                    Carrera = g.Key.NombreCarrera,
                    Reto = g.Key.NombreReto,
                    MejorTiempo = g.Min(x => x.Tiempo)
                })
                .ToList();

            // 2. LINQ: Agrupar por Carrera, sumar tiempos y contar retos ganados/completados
            var tablaClasificacion = carreras
                .Select(c =>
                {
                    var marcasCarrera = mejoresMarcas
                        .Where(m => string.Equals(m.Carrera, c.Nombre, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    int retosCompletados = marcasCarrera.Select(m => m.Reto).Distinct().Count();
                    double tiempoTotal = marcasCarrera.Aggregate(0.0, (acum, m) => acum + m.MejorTiempo);

                    // Contar estudiantes únicos participantes de esta carrera
                    int participantesUnicos = listaInscripciones
                        .Where(i => string.Equals(i.NombreCarrera, c.Nombre, StringComparison.OrdinalIgnoreCase))
                        .Select(i => i.IdEstudiante)
                        .Distinct()
                        .Count();

                    return new ClasificacionItem
                    {
                        IdCarrera = c.IdCarrera,
                        Carrera = c.Nombre,
                        Facultad = c.Facultad,
                        Reto = "General (Acumulado)",
                        TiempoSegundos = tiempoTotal,
                        RetosCompletados = retosCompletados,
                        TotalParticipantes = participantesUnicos
                    };
                })
                .Where(ci => ci.RetosCompletados > 0) // Solo carreras con al menos un reto completado
                .OrderByDescending(ci => ci.RetosCompletados)
                .ThenBy(ci => ci.TiempoSegundos)
                .ThenByDescending(ci => ci.TotalParticipantes)
                .Select((item, indice) =>
                {
                    item.Posicion = indice + 1; // Asignación de puesto de podio mediante indexación LINQ
                    return item;
                })
                .ToList();

            return tablaClasificacion;
        }

        /// <summary>
        /// Calcula la Clasificación para un Reto específico utilizando LINQ.
        /// </summary>
        public List<ClasificacionItem> CalcularClasificacionPorReto(
            int idReto,
            string nombreReto,
            IEnumerable<Carrera> carreras,
            IEnumerable<Inscripcion> inscripcionesDelReto,
            IEnumerable<Resultado> resultadosDelReto)
        {
            if (resultadosDelReto == null || carreras == null)
                return new List<ClasificacionItem>();

            var listaResultados = resultadosDelReto.Where(r => r.Tiempo > 0).ToList();

            var clasificacionReto = listaResultados
                .GroupBy(r => r.NombreCarrera)
                .Select(g =>
                {
                    var carreraObj = carreras.FirstOrDefault(c => string.Equals(c.Nombre, g.Key, StringComparison.OrdinalIgnoreCase));
                    double mejorTiempo = g.Min(r => r.Tiempo);
                    int totalParticipantes = g.Select(r => r.NombreEstudiante).Distinct().Count();

                    return new ClasificacionItem
                    {
                        IdCarrera = carreraObj?.IdCarrera ?? 0,
                        Carrera = g.Key,
                        Facultad = carreraObj?.Facultad ?? "ULSA",
                        Reto = nombreReto,
                        TiempoSegundos = mejorTiempo,
                        RetosCompletados = 1,
                        TotalParticipantes = totalParticipantes
                    };
                })
                .OrderBy(ci => ci.TiempoSegundos)
                .ThenByDescending(ci => ci.TotalParticipantes)
                .Select((item, indice) =>
                {
                    item.Posicion = indice + 1;
                    return item;
                })
                .ToList();

            return clasificacionReto;
        }

        /// <summary>
        /// Consolida las métricas del Dashboard usando LINQ en memoria.
        /// </summary>
        public DashboardStats CalcularEstadisticasDashboard(
            IEnumerable<Carrera> carreras,
            IEnumerable<Estudiante> estudiantes,
            IEnumerable<Reto> retos,
            IEnumerable<Inscripcion> inscripciones,
            IEnumerable<Resultado> resultados,
            List<ClasificacionItem> clasificacionGeneral)
        {
            var stats = new DashboardStats
            {
                TotalCarreras = carreras?.Count() ?? 0,
                TotalEstudiantes = estudiantes?.Count() ?? 0,
                TotalRetos = retos?.Count() ?? 0,
                TotalInscripciones = inscripciones?.Count() ?? 0,
                TotalResultados = resultados?.Count() ?? 0,
                MejorTiempoGlobal = (resultados != null && resultados.Any(r => r.Tiempo > 0)) 
                    ? resultados.Where(r => r.Tiempo > 0).Min(r => r.Tiempo) 
                    : 0.0
            };

            if (clasificacionGeneral != null && clasificacionGeneral.Count > 0)
            {
                stats.CarreraLider = clasificacionGeneral.First().Carrera;
                stats.TopPodio = clasificacionGeneral.Take(3).ToList();
            }

            return stats;
        }
    }
}
```

- [ ] **Paso 2: Modificar `Data/ClasificacionRepository.cs` para obtener colecciones limpias y usar el motor LINQ**
- [ ] **Paso 3: Registrar `Services/CalculadorEstadisticasRally.cs` en el `.csproj` y compilar**

---

### Tarea 6: Reglas de Negocio del Rally (Tope de 20 Estudiantes por Carrera y Validación)

**Archivos:**
- Modificar: `Services/ValidacionService.cs`
- Modificar: `Data/InscripcionRepository.cs`

**Interfaces:**
- Consume: `ValidacionService`, `DatabaseHelper`
- Produce: Control estricto de cupo general de carrera (máximo 20 alumnos) y cupo por reto individual dentro de transacción serializable.

- [ ] **Paso 1: Agregar constantes de negocio y método en `ValidacionService.cs`**

```csharp
public const int MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY = 20;

public static bool ValidarCupo(int cupo, out string mensaje)
{
    mensaje = string.Empty;

    if (cupo < 1)
    {
        mensaje = "El cupo debe ser al menos 1.";
        return false;
    }

    if (cupo > MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY)
    {
        mensaje = $"El cupo por reto no puede exceder el límite máximo de equipo ({MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY} participantes).";
        return false;
    }

    return true;
}
```

- [ ] **Paso 2: Implementar la verificación de 20 participantes por carrera en `InscripcionRepository.cs`**

En `InscribirEstudiante(int idEstudiante, int idReto, out string mensajeError)`:
1. Comprobar que no esté inscrito en el mismo reto.
2. Obtener `idCarrera` del estudiante.
3. Verificar si el estudiante ya tiene inscripciones en otros retos:
   - Si **NO** tiene inscripciones previas (nuevo participante de la carrera):
     Verificar cuántos estudiantes distintos de esa carrera ya están inscritos en el rally.
     Si `count(DISTINCT idEstudiante) >= MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY` (20), **bloquear la inscripción** informando que la carrera alcanzó su cupo máximo de 20 integrantes en el rally.
4. Verificar el cupo específico de la carrera en el reto solicitado (`cupoMaximoPorCarrera`).
5. Insertar inscripción y hacer commit.

- [ ] **Paso 3: Compilar con MSBuild y verificar**

---

### Tarea 7: Refactorización y Homogeneización LINQ en Controles de Usuario (`Views/Controls/`)

**Archivos:**
- Modificar: `Views/Controls/UserControlEstudiantes.cs` (búsqueda y filtros con LINQ en memoria)
- Modificar: `Views/Controls/UserControlResultados.cs` (enlace correcto de edición de resultado y LINQ)
- Modificar: `Views/Controls/UserControlClasificacion.cs`
- Modificar: `Views/Controls/UserControlInicio.cs`

**Interfaces:**
- Consume: `CalculadorEstadisticasRally`, `ClasificacionRepository`, `EstudianteRepository`
- Produce: Interfaz fluida, desacoplada, con procesamiento LINQ en memoria y carga asíncrona.

- [ ] **Paso 1: Implementar filtrado LINQ en memoria en `UserControlEstudiantes.cs`**

```csharp
private void FiltrarEstudiantes()
{
    int idCarreraFiltro = 0;
    if (cmbFiltroCarrera.SelectedItem is CarreraItem ci)
        idCarreraFiltro = ci.Id;

    string busq = txtBuscar.Text.Trim().ToLowerInvariant();

    var filtrados = _todosLosEstudiantes
        .Where(e => (idCarreraFiltro == 0 || e.IdCarrera == idCarreraFiltro) &&
                    (string.IsNullOrEmpty(busq) ||
                     (e.Nombre != null && e.Nombre.ToLowerInvariant().Contains(busq)) ||
                     (e.Apellido != null && e.Apellido.ToLowerInvariant().Contains(busq)) ||
                     (e.CorreoULSA != null && e.CorreoULSA.ToLowerInvariant().Contains(busq)) ||
                     (e.NombreCarrera != null && e.NombreCarrera.ToLowerInvariant().Contains(busq))))
        .OrderBy(e => e.Nombre)
        .ThenBy(e => e.Apellido)
        .ToList();

    dgvEstudiantes.DataSource = null;
    dgvEstudiantes.DataSource = filtrados;
    ConfigurarColumnas();
}
```

- [ ] **Paso 2: Corregir selección de fila y flujo de edición en `UserControlResultados.cs`**
- [ ] **Paso 3: Compilar con MSBuild**

---

### Tarea 8: Verificación Global y Comprobación contra la Rúbrica

**Archivos:**
- Todos los modificados y creados.

- [ ] **Paso 1: Ejecutar compilación completa con MSBuild**

```powershell
& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "SistemadeGestiondeRallyUniversitario.csproj" /t:Rebuild /p:Configuration=Debug
```
Esperado: 0 errores, 0 advertencias.

- [ ] **Paso 2: Validar checklist de la rúbrica oficial**
  - [x] Herencia y Polimorfismo: `Persona` abstracta, `Estudiante`, `Organizador`, métodos polimórficos sobrescritos.
  - [x] Abstracción e Interfaces: `IValidable`, `IRepositorio<T>`.
  - [x] Encapsulamiento: Setters protegidos e invariantes en modelos.
  - [x] Colecciones Genéricas y LINQ: `CalculadorEstadisticasRally` con `GroupBy`, `Min`, `Sum`, `OrderByDescending`, `ThenBy`, `Select` con índice, `Aggregate`, `Take`.
  - [x] Reglas de Negocio: Cupo máximo de 20 estudiantes por carrera en el rally y cupo por reto.
  - [x] Persistencia e Interfaz: SQLite intacto y WinForms funcional.
