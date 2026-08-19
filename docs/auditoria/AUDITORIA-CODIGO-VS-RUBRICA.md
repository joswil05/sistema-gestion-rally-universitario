# Auditoría Técnica y Reporte Comparativo: Código Actual vs. Rúbrica Oficial 2026

**Proyecto:** Sistema de Gestión de Rally Universitario - ULSA  
**Asignatura:** Lenguaje de Programación III (IV Año - Ing. en Mecatrónica y Sistemas de Control)  
**Docente Evaluador:** Ing. Freddy Alexander Mejía Quintana  
**Fecha de Auditoría:** 2026-08-18  
**Rol del Auditor:** Arquitecto de Software .NET y Auditor Técnico  
**Alcance Auditado:** 5.832 líneas C# / 23 archivos fuente (capas `Models/`, `Data/`, `Services/`, `Views/`)  
**Documento Oficial de Referencia:** `Guia de Proyectos de Fin de Curso 2026 - Lenguaje de Programacion III.md`

---

## 1. Resumen Ejecutivo y Estado de Línea Base

Se realizó una auditoría técnica profunda del código fuente del **Sistema de Gestión de Rally Universitario - ULSA** frente a los requerimientos y parámetros de evaluación de la **Guía Oficial de Proyectos de Fin de Curso 2026**.

### Diagnóstico General
El software presenta una **asimetría crítica entre la implementación real y los criterios que premia la rúbrica académica**:
1. **La fortaleza actual está en áreas de bajo peso de rúbrica:** Persistencia con SQLite (`DatabaseHelper`, transacciones, índices, claves foráneas) y diseño visual WinForms con navegación asíncrona están muy bien ejecutados.
2. **La debilidad crítica está en el núcleo de la asignatura:** Los pilares de Programación Orientada a Objetos (Herencia, Polimorfismo, Abstracción e Interfaces) y el procesamiento analítico mediante **LINQ y Colecciones Genéricas** se encuentran prácticamente ausentes o delegados a consultas SQL.

| Verificación Técnica | Estado Actual | Impacto en Evaluación |
|---|---|---|
| **Compilación MSBuild (VS 2022/2026)** | ✅ Correcta (0 errores, 0 warnings) | Permite refactorización segura y continua |
| **Framework y Lenguaje C#** | .NET Framework 4.8 / C# 7.3 | Compatible con LINQ completo, generics y OOP avanzada |
| **Persistencia SQLite** | `System.Data.SQLite.Core` v1.0.119 | Transaccional y parametrizada |
| **Jerarquía POO Propia** | ❌ 0 herencia de dominio, 0 interfaces | Incumplimiento directo de rúbrica POO (Criterio 2 y 3) |
| **Procesamiento LINQ en memoria** | ❌ Solo 2 `.Where()` triviales en toda la app | Incumplimiento de rúbrica LINQ y Colecciones (Criterio 5 y 6) |
| **Regla de 20 estudiantes por carrera** | ❌ Inexistente en la base de datos y código | Incumplimiento de regla de negocio crítica del rally |

---

## 2. Matriz Comparativa Oficial: Código Actual vs. Rúbrica de Evaluación

La evaluación oficial de la asignatura se divide en tres componentes principales de **10 puntos cada uno (Total: 30 pts / 100% de la nota del parcial)**:

### 2.1 Componente: Cumplimiento de Objetivos del Proyecto (10 Puntos)

| Criterio Oficial de Rúbrica | Pts Máx | Pts Actuales | Diagnóstico del Código Actual | Acción Correctiva de Refactorización | Pts Proyectados |
|---|---:|---:|---|---|---:|
| **Identificación de Req. Funcionales y No Funcionales** | 2.0 | **1.2** (60%) | Requerimientos de UI y persistencia cubiertos. Falta formalizar invariantes de dominio y el control de cupo general de carrera (máximo 20 alumnos). | Incorporar validación formal del tope de 20 participantes por carrera en `ValidacionService` e `InscripcionRepository`. | **2.0** (100%) |
| **Diagrama de Casos de Uso y Flujos Lógicos** | 2.0 | **1.4** (70%) | Flujos de captura de tiempo y registro de estudiantes operativos, pero con ambigüedades en selección de filas en resultados y filtrado heterogéneo. | Unificar flujos de interacción y corregir enlace de selección en grid de resultados. | **2.0** (100%) |
| **Diagrama y Arquitectura de Clases POO** | 2.0 | **0.4** (20%) | **Falta crítica:** No hay clase base abstracta, no hay contratos `interface`, propiedades anémicas sin invariantes, `ToString()` es el único polimorfismo. | Crear clase abstracta `Persona`, contratos `IValidable`, `IRepositorio<T>`, herencia en `Estudiante` y `Organizador`, métodos polimórficos (`ObtenerRol`, `Validar`). | **2.0** (100%) |
| **Diseño de Wireframes, Mockups e Interfaz GUI** | 2.0 | **1.8** (90%) | Interfaz moderna en Windows Forms, UserControls modulares, podio con estilos visuales oro/plata/bronce y carga asíncrona no bloqueante. | Mantener intacta la estética WinForms y enlazarla fluidamente a la nueva capa de servicios LINQ. | **2.0** (100%) |
| **Nivel de Cumplimiento Técnico (POO + LINQ + Colecciones)** | 2.0 | **0.5** (25%) | Cálculos estadísticos y podio delegados a SQL complejo (`WITH` CTEs) en lugar de utilizar transformaciones LINQ declarativas en C# sobre `List<T>`. | Reemplazar consultas SQL de agregación por pipeline LINQ (`GroupBy`, `Min`, `Select`, `OrderByDescending`, `Aggregate`) en memoria. | **2.0** (100%) |
| **SUBTOTAL OBJETIVOS TÉCNICOS** | **10.0** | **5.3 / 10** | **Nivel Deficiente / Regular en POO y LINQ** | **Aplicación del Plan de Refactorización Integral** | **10.0 / 10** |

---

### 2.2 Componente: Documento Técnico / Físico (10 Puntos)

| Sección del Informe Técnico | Pts Máx | Estado con Código Actual | Estado Proyectado tras Refactorización |
|---|---:|---|---|
| **Portada e Identificación Institucional** | - | Aceptable | Profesional completa con datos de ULSA y asignatura |
| **Introducción y Alcance (1 pt c/u)** | 2.0 | Regular: no refleja arquitectura orientada a objetos en C# | Excelente: describe arquitectura en capas, contratos e invariantes |
| **Objetivos SMART (1 pt)** | 1.0 | Aceptable | Objetivos medibles alineados a POO, LINQ y control de cupos |
| **Marco Teórico en APA v7 (1 pt)** | 1.0 | Parcial: falta sustento de contratos genéricos y expresiones lambda | Completo con definiciones formales de POO, LINQ y Generics |
| **Planteamiento del Problema (1 pt)** | 1.0 | Aceptable | Riguroso: automatización de tiempos, desempates y reglas de cupo |
| **Desarrollo (Requerimientos + UML + Mockups) (3 pts)** | 3.0 | Deficiente (1.2/3): El diagrama de clases no tendría herencia ni interfaces | Excelente (3.0/3): Diagrama UML completo con `Persona`, `IRepositorio<T>`, `IValidable`, `Estudiante`, `Organizador` |
| **Conclusiones, Recomendaciones y Referencias (2 pts)** | 2.0 | Regular: sin análisis de rendimiento LINQ vs BD | Sólidas conclusiones técnicas y bibliografía APA 7ma edición |
| **SUBTOTAL DOCUMENTO** | **10.0** | **5.5 / 10** | **10.0 / 10** |

---

### 2.3 Componente: Defensa Oral y Demostración en Vivo (10 Puntos)

| Categoría de Evaluación | Pts Máx | Situación Actual | Situación Proyectada con Refactorización |
|---|---:|---|---|
| **Dominio del Contenido POO y C#** | 2.0 | Vulnerable: Si el docente pregunta "¿dónde implementó polimorfismo y herencia?", la respuesta actual es nula. | Sólido: Demostración directa de jerarquía `Persona`, contratos `IValidable`, `IRepositorio<T>` y métodos virtuales/abstractos. |
| **Exposición y Demostración en Vivo** | 4.0 | Buena en UI, pero sin código C# representativo para explicar LINQ. | Fluida demostración del podio y estadísticas calculadas en tiempo real con LINQ. |
| **Soporte Visual y Coherencia UML vs Código** | 2.0 | Inconsistente si se presenta un diagrama con herencia que el código no tiene. | 100% de coherencia entre diagrama UML y archivos `.cs`. |
| **Trabajo en Equipo y Tiempo** | 2.0 | Cumplimiento estándar. | Dominio compartido de la arquitectura en capas. |
| **SUBTOTAL DEFENSA ORAL** | **10.0** | **6.0 / 10** | **10.0 / 10** |

---

## 3. Auditoría Detallada por Pilar Técnico

### 3.1 Pilar 1: Programación Orientada a Objetos (Abstracción, Encapsulamiento, Herencia, Polimorfismo)

#### A. Falta de Herencia y Duplicación de Código
* **Archivos:** `Models/Estudiante.cs:7-14` y `Models/Organizador.cs:7-13`.
* **Hallazgo:** Ambas clases definen de forma redundante `Nombre`, `Apellido` y la propiedad calculada `NombreCompleto => $"{Nombre} {Apellido}".Trim();`.
* **Solución Arquitectónica:** Extraer la clase base abstracta `Persona`:
  ```csharp
  public abstract class Persona : IValidable
  {
      public string Nombre { get; set; }
      public string Apellido { get; set; }
      public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
      
      public abstract string ObtenerRol();
      public abstract string ObtenerIdentificadorPrincipal();
      
      public virtual bool Validar(out string mensajeError)
      {
          // Validación base reutilizable
      }
  }
  ```

#### B. Ausencia de Polimorfismo Real
* **Hallazgo:** Los únicos `override` existentes en el proyecto son `ToString()` heredados de `System.Object`. No existe ningún método `abstract` ni `virtual` definido en el dominio del negocio.
* **Solución:** `Estudiante` y `Organizador` sobrescriben `ObtenerRol()`, `ObtenerIdentificadorPrincipal()` y `Validar()`, demostrando polimorfismo por despacho dinámico.

#### C. Inexistencia de Contratos e Interfaces
* **Hallazgo:** Ningún modelo ni repositorio implementa interfaces. `Data/CarreraRepository`, `Data/RetoRepository`, `Data/EstudianteRepository`, etc., comparten la misma firma CRUD sin contrato común.
* **Solución:**
  1. `Models/IValidable.cs`: Contrato para autovalidación de modelos.
  2. `Data/IRepositorio.cs`: Contrato genérico `IRepositorio<T> where T : class`.

#### D. Encapsulamiento Débil
* **Hallazgo:** Los modelos no protegen sus invariantes. `Reto.CupoMaximoPorCarrera` admite números negativos o superiores al límite permitido en tiempo de ejecución.
* **Solución:** Validaciones en setters / constructores y método `Validar(out string mensajeError)`.

---

### 3.2 Pilar 2: LINQ y Colecciones Genéricas (`List<T>`, `IEnumerable<T>`)

#### A. El Estado Actual del Código
* De las 5.832 líneas del proyecto, solo existen **dos** llamadas LINQ (`.Where().ToList()` básicos en `UserControlCarreras.cs:54` y `UserControlRetos.cs:80`).
* En `UserControlEstudiantes.cs:3` y `UserControlResultados.cs:3` el namespace `using System.Linq;` está importado como código muerto sin usar.

#### B. Migración de SQL a LINQ en Memoria
Actualmente, `Data/ClasificacionRepository.cs:20-49` utiliza una CTE (`WITH MejorTiempoPorCarreraPorReto`) en SQLite. Esta lógica debe realizarse en C# mediante una tubería LINQ declarativa:
```csharp
// 1. Obtener mejor tiempo por carrera por reto usando LINQ
var mejoresTiemposPorCarreraYReto = resultados
    .GroupBy(r => new { r.IdCarrera, r.IdReto })
    .Select(g => new {
        g.Key.IdCarrera,
        g.Key.IdReto,
        MejorTiempo = g.Min(x => x.Tiempo)
    }).ToList();

// 2. Agrupar por carrera y calcular métricas acumuladas
var clasificacion = carreras
    .Select(c => {
        var tiemposCarrera = mejoresTiemposPorCarreraYReto.Where(mt => mt.IdCarrera == c.IdCarrera).ToList();
        int retosCompletados = tiemposCarrera.Select(x => x.IdReto).Distinct().Count();
        double tiempoTotal = tiemposCarrera.Sum(x => x.MejorTiempo);
        int totalParticipantes = inscripciones
            .Where(i => i.IdCarrera == c.IdCarrera)
            .Select(i => i.IdEstudiante)
            .Distinct()
            .Count();

        return new ClasificacionItem {
            IdCarrera = c.IdCarrera,
            Carrera = c.Nombre,
            Facultad = c.Facultad,
            Reto = "General (Acumulado)",
            TiempoSegundos = tiempoTotal,
            RetosCompletados = retosCompletados,
            TotalParticipantes = totalParticipantes
        };
    })
    .Where(c => c.RetosCompletados > 0)
    .OrderByDescending(c => c.RetosCompletados)
    .ThenBy(c => c.TiempoSegundos)
    .ThenByDescending(c => c.TotalParticipantes)
    .Select((item, indice) => {
        item.Posicion = indice + 1;
        return item;
    })
    .ToList();
```

#### C. Búsqueda y Filtrado Unificados con LINQ
En `UserControlEstudiantes`, reemplazar el filtrado dinámico en SQL por filtrado en memoria sobre `List<Estudiante>` utilizando operadores LINQ (`Where`, `OrderBy`, `ThenBy`).

---

### 3.3 Pilar 3: Reglas de Negocio del Rally Universitario

#### A. Regla Crítica Faltante: Tope Máximo de 20 Estudiantes por Carrera
* **Regla Oficial:** Cada carrera puede registrar como máximo a 20 estudiantes distintos en todo el rally general.
* **Estado Actual:** No existe restricción en el código ni en la base de datos.
* **Implementación:**
  1. Definir constante `ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY = 20`.
  2. En `InscripcionRepository.InscribirEstudiante`:
     - Contar cuántos estudiantes **distintos** de la carrera ya tienen al menos 1 inscripción activa en cualquier reto.
     - Si el estudiante que se desea inscribir **no** tiene inscripciones previas (es nuevo participante para la carrera) y el total ya es $\ge 20$, **rechazar la inscripción** con mensaje claro.
     - Si el estudiante ya participaba en otro reto, **permitir la inscripción** (no consume una nueva plaza de los 20).
  3. Controlar el cupo específico por reto (`Reto.CupoMaximoPorCarrera`, por defecto 5 por carrera en cada estación).
  4. Ajustar `ValidacionService.ValidarCupo` para que el cupo por reto no supere el límite máximo de equipo (20).

---

## 4. Plan de Acción Inmediato

Para elevar la calificación del proyecto al **100% de la rúbrica (30/30 pts)**, se ejecutará el plan de refactorización estructurado en cuatro fases secuenciales:

1. **Fase 1 (POO & Contratos):** Creación de `IValidable`, `Persona`, herencia en `Estudiante`/`Organizador` y contrato genérico `IRepositorio<T>`.
2. **Fase 2 (LINQ & Servicios):** Implementación de `CalculadorEstadisticasRally` con operadores LINQ avanzados (`GroupBy`, `Min`, `Sum`, `OrderByDescending`, `Select`, `Aggregate`, `Take`) para podio y dashboard.
3. **Fase 3 (Reglas de Negocio):** Implementación del tope de 20 participantes por carrera y validaciones consistentes de modelos.
4. **Fase 4 (Integración UI & Verificación):** Actualización de UserControls, compilación limpia con MSBuild y pruebas de validación funcional.
