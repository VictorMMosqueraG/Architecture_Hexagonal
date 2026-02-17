# Architecture_Hexagonal

Descripción
-----------

Este repositorio contiene un esqueleto de proyecto en C# siguiendo la Arquitectura Hexagonal (también conocida como Puertos y Adaptadores). El objetivo de esta arquitectura es separar el núcleo de la aplicación (reglas de negocio y casos de uso) de las dependencias externas (UI, bases de datos, servicios HTTP, colas, etc.) mediante capas y contratos bien definidos.

Motivación y por qué elegir Hexagonal
-------------------------------------

- Separación clara de responsabilidades: el `core` define contratos (puertos) y modelos, sin referencias a infraestructuras concretas.
- Testabilidad: los casos de uso (`application`) se pueden probar in-memory inyectando adaptadores falsos (mocks/stubs).
- Flexibilidad y mantenibilidad: cambiar una infraestructura (por ejemplo, pasar de RabbitMQ a SQS) solo requiere implementar/adaptar un adaptador sin tocar la lógica de negocio.
- Facilita el desarrollo en equipos y la adopción de patrones como CQRS, Mediator y Validación centralizada.

Estructura del proyecto
-----------------------

- `api/` — Proyecto ASP.NET Core (Web API). Exposición HTTP, versionado y documentación (Swagger).
- `application/` — Casos de uso, comandos/queries, handlers (MediatR), validaciones y DTOs específicos de los casos de uso.
- `core/` — Entidades, interfaces (puertos), DTOs, constantes y contratos que representan el dominio y las abstracciones.
- `infrastructure/` — Implementaciones concretas (adaptadores) de persistencia, mensajería, wrappers HTTP, almacenamiento de objetos, monitorización y otros servicios externos.

Notas sobre puertos y adaptadores
--------------------------------

- Los **puertos** (interfaces) deben vivir en `core/` y describir lo que la aplicación necesita (por ejemplo `IUserRepository`, `IMessagePublisher`).
- Los **adaptadores** viven en `infrastructure/` y proveen las implementaciones concretas (`SqlUserRepository`, `RabbitMqPublisher`, `S3ObjectStore`).
- `application/` depende de `core/` y puede recibir los puertos mediante inyección de dependencias.
- `api/` depende de `application/` y orquesta solicitudes HTTP hacia los casos de uso.

Requisitos
----------

- .NET SDK: Recomendado `dotnet 9.0` (ver `TargetFramework` en los `.csproj`). Instalar desde https://dotnet.microsoft.com/
- `dotnet` CLI disponible en `PATH`.
- (Opcional) Docker para contenedores y despliegue.
- Variables de entorno y proveedor de secretos para credenciales (KeyVault, Vault, AWS Secrets Manager, etc.).

Instalación y ejecución rápida
-----------------------------

1. Clonar el repositorio.
2. Desde la raíz, restaurar paquetes y ejecutar el proyecto Web API:

```bash
cd api
dotnet restore
dotnet run
```

Por defecto la API arranca en `http://localhost:5000` o el puerto configurado por ASP.NET Core. Swagger estará disponible en entorno de desarrollo si está habilitado.

Compilación y pruebas
---------------------

- Para compilar todos los proyectos desde la raíz (si se añadiera una solución `.sln`):

```bash
dotnet build
```

- Recomendación: añadir un proyecto de tests (`tests/` con xUnit) que referencia `application` y `core` para tests unitarios de casos de uso.

Cómo añadir un nuevo puerto/adaptador
------------------------------------

1. Definir la interfaz en `core/` (por ejemplo `IEmailSender`).
2. Implementar la interfaz en `infrastructure/Adapters/Email/` (por ejemplo `SmtpEmailSender`).
3. Registrar la implementación en `infrastructure/DependencyInjection` (o en un módulo de composición) mediante `services.AddScoped<IEmailSender, SmtpEmailSender>();`.
4. Consumir el puerto desde `application` vía inyección en los handlers o servicios.

Configuración y secretos
------------------------

- Mantén la configuración por entorno (`appsettings.Development.json`, `appsettings.Production.json`) y no incluya credenciales en el repo.
- Usa un proveedor de secretos en `infrastructure` (Key Vault, Vault, SecretStore) para enlazar secretos a DTOs de configuración.

Observabilidad y salud
----------------------

- Agrega `Serilog` o similar para logging estructurado desde `infrastructure`.
- Añade `HealthChecks` en `api` y registra probes desde `infrastructure` (bases de datos, colas, storage).
- Considera métricas (Prometheus) y traces distribuidos (OpenTelemetry).

Buenas prácticas recomendadas
----------------------------

- Mantén `core` libre de dependencias externas.
- Prefiere DTOs de dominio en `core` y mapea a DTOs de transporte en `application`/`api`.
- Documenta los contratos de puertos con ejemplos y expectativas (por ejemplo, idempotencia, límites de tiempo).
- Añade CI que haga: restore, build, run tests, linting y generación de artefactos.

CI / CD y despliegue
--------------------

- Pipeline sugerido:
	- `dotnet restore` y `dotnet build`
	- `dotnet test` para el proyecto de tests
	- Construcción de imágenes Docker para `api`
	- Despliegue a staging y luego producción con migraciones controladas.

Plantillas y próximos pasos sugeridos
-----------------------------------

- Añadir un `tests/` con xUnit y mocks para `application`.
- Añadir un `docker/` o `Dockerfile` en `api/`.
- Añadir un archivo `architecture.md` que documente decisiones arquitectónicas y contratos importantes (puertos).

Contacto y contribución
-----------------------

Si quieres propoer cambios, abre un PR y describe la motivación y el impacto en la arquitectura.

Licencia
--------

Por defecto no hay licencia en este repositorio; añade un `LICENSE` si quieres permitir contribuciones externas.

*** Fin del documento de referencia para la arquitectura hexagonal ***
# Architecture_Hexagonal

Proyecto de ejemplo en C# con estructura inicial (consola).

Instrucciones rápidas:

```bash
cd src/Architecture.Hexagonal
dotnet restore
dotnet run
```

Estructura:
- src/Architecture.Hexagonal: proyecto principal
