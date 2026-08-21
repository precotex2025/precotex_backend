# CLAUDE.md - Guía de Desarrollo Backend (Precotex)

Guía rápida de arquitectura, reglas y convenciones para el backend en .NET 8.

---

## 1. Stack Tecnológico
* **Framework:** .NET 8 (`net8.0`) - ASP.NET Core Web API (Controllers clásicos).
* **Acceso a Datos:** **Dapper** (v2.1.35) + `System.Data.SqlClient`.
* **Base de Datos:** SQL Server (Acceso **exclusivamente** mediante Stored Procedures).
* **Documentación:** Swagger (Swashbuckle 7.3.1), habilitado solo en `Development`.
* **Restricción Clave:** **NO usar Entity Framework**, sin migraciones, sin tests unitarios, sin `[Authorize]` directo.

---

## 2. Arquitectura de 4 Capas
Flujo lineal de dependencias:
`Api` ➔ `Service` ➔ `Data` ➔ `Entity`

1. **`ic.backend.precotex.web.Api`**: Controllers, DTOs de entrada (`Parameters/`) y DI (`Extensions/`).
2. **`ic.backend.precotex.web.Service`**: Lógica de negocio y armado de respuestas (`ServiceResponse`).
3. **`ic.backend.precotex.web.Data`**: Repositorios Dapper, llamada a Stored Procedures.
4. **`ic.backend.precotex.web.Entity`**: Modelos POCO que mapean los resultados SQL.

> **Estructura de carpetas:**
> * Interfaces van en: `Service/Services/Implementacion/` y `Data/Repositories/Implementation/`.
> * Clases concretas van en la carpeta del módulo respectivo.

---

## 3. Pasos para Crear un Nuevo Endpoint
Para cada funcionalidad se crean/modifican estos archivos en orden:
1. **Entity:** Crear modelo POJO en `Entity/Entities/[Modulo]/`.
2. **Data (Interface & Repo):** Crear interfaz en `Implementation/` e implementar en `Data/Repositories/[Modulo]/` usando SP.
3. **Service (Interface & Service):** Crear interfaz en `Implementacion/` e implementar en `Service/Services/[Modulo]/` retornando `ServiceResponse<T>`.
4. **Api (Controller & Parameter):** Crear controller en `Api/Controllers/[Modulo]/` y DTO en `Parameters/`.
5. **Inyección de Dependencias (OBLIGATORIO):**
   * Registrar servicio en: `Api/Extensions/ServiceExtensions.cs` (`AddScoped`).
   * Registrar repositorio en: `Api/Extensions/RepositoryExtensions.cs` (`AddScoped`).

> Si se omite el paso 5 el proyecto compila igual, pero el endpoint falla en tiempo de ejecución.

---

## 4. Contrato de Respuesta
Wrappers ubicados en `Service/common/` y `Entity/common/`:

| Wrapper | Uso | Campos |
|---|---|---|
| `ServiceResponse<T>` | Un solo registro | `Success`, `CodeResult`, `Message`, `Element`, `CodeTransacc` |
| `ServiceResponseList<T>` | Listados | `Success`, `CodeResult`, `Message`, `Elements`, `TotalElements` |
| `ServiceResponseTransacSQL` | Retorno de SP de escritura | `nCod`, `sMsj` |
| `dtoGeneral` | Combos | `Codigo`, `Descripcion` |

Reglas por capa:
* **Repositorio:** `QueryAsync<T>` con `commandType: CommandType.StoredProcedure`; captura `SqlException` y relanza.
* **Servicio:** siempre retorna un wrapper. Si el SP no devuelve filas: `Success = true` con `Message = "No existe información"`. Captura `SqlException` y `Exception` sin relanzar.
* **Controller:** `CodeResult = 200` + `Ok(result)` si `Success`; `CodeResult = 400` + `BadRequest(result)` en caso contrario.

---

## 5. Reglas y Restricciones Estrictas
* **Base de Datos (ACCESO TOTALMENTE PROHIBIDO):**
  * **NUNCA conectarse ni interactuar directamente con la base de datos.**
  * Prohibido ejecutar consultas, `SELECT`, `ping`, `sqlcmd`, scripts o inspecciones directas a la BD bajo ningún motivo (ni desarrollo ni producción).
  * El trabajo es 100% sobre el código C# del backend.
  * Nunca escribir SQL inline (`SELECT ... FROM` en C#). El código solo invoca Stored Procedures existentes mediante Dapper.
  * Si se requiere un SP nuevo o modificar uno existente: describir qué debe hacer y entregarlo al equipo; no ejecutarlo.
* **Sin ORMs:** Prohibido agregar o usar Entity Framework Core, LINQ-to-SQL o cualquier otro ORM.
* **Control de Cambios:**
  * No modificar código fuera del módulo asignado.
  * Mantener el estilo existente del módulo que se está editando.
  * No renombrar clases, interfaces ni carpetas existentes.
  * No agregar paquetes NuGet ni cambiar la versión de .NET sin autorización.
  * No subir credenciales ni `appsettings.json` al repositorio.
* **Git:**
  * No trabajar directo en `main` ni `develop`.
  * Crear ramas con prefijo: `dev-[nombre]-[tarea]`.
  * Integrar siempre mediante Pull Request hacia `develop`.
  * No ejecutar `commit` ni `push` sin autorización explícita.

---
