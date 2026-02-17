# Architecture_Hexagonal
Este repositorio contiene un esqueleto de proyecto en C# siguiendo la Arquitectura Hexagonal (también conocida como Puertos y Adaptadores). El objetivo de esta arquitectura es separar el núcleo de la aplicación (reglas de negocio y casos de uso) de las dependencias externas (UI, bases de datos, servicios HTTP, colas, etc.) mediante capas y contratos bien definidos.

### Motivación y por qué elegir Hexagonal

- Separación clara de responsabilidades: el `core` define contratos (puertos) y modelos, sin referencias a infraestructuras concretas.
- Testabilidad: los casos de uso (`application`) se pueden probar in-memory inyectando adaptadores falsos (mocks/stubs).
- Flexibilidad y mantenibilidad: cambiar una infraestructura (por ejemplo, pasar de RabbitMQ a SQS) solo requiere implementar/adaptar un adaptador sin tocar la lógica de negocio.
- Facilita el desarrollo en equipos y la adopción de patrones como CQRS, Mediator y Validación centralizada.

### Estructura del proyecto

- `api/` — Proyecto ASP.NET Core (Web API). Exposición HTTP, versionado y documentación (Swagger).
- `application/` — Casos de uso, comandos/queries, handlers (MediatR), validaciones y DTOs específicos de los casos de uso.
- `core/` — Entidades, interfaces (puertos), DTOs, constantes y contratos que representan el dominio y las abstracciones.
- `infrastructure/` — Implementaciones concretas (adaptadores) de persistencia, mensajería, wrappers HTTP, almacenamiento de objetos, monitorización y otros servicios externos.

### Notas sobre puertos y adaptadores

- Los **puertos** (interfaces) deben vivir en `core/` y describir lo que la aplicación necesita (por ejemplo `IUserRepository`, `IMessagePublisher`).
- Los **adaptadores** viven en `infrastructure/` y proveen las implementaciones concretas (`SqlUserRepository`, `RabbitMqPublisher`, `S3ObjectStore`).
- `application/` depende de `core/` y puede recibir los puertos mediante inyección de dependencias.
- `api/` depende de `application/` y orquesta solicitudes HTTP hacia los casos de uso.

# Variables de Entorno

Para poder trabajar comodamente y de manera segura se requiere que se copie el archivo **.env.example** y se le ponga el nombre a **.env** y sean asignados los valores correspondientes, esto con el fin de no dejar datos sensibles al accesso facil, se podria manejar tambien mediante el uso de un gestor de secretos como pude ser Vault.

# Docker
El proyecto incluye una configuración de Docker Compose para levantar el entorno de persistencia necesario para el desarrollo local.

Para levantar los servicios
Ejecuta el siguiente comando desde la raíz del proyecto para iniciar la base de datos en segundo plano:

```bash
docker-compose up -d
```

# Requisitos

- .NET SDK: Recomendado `dotnet 9.0` (ver `TargetFramework` en los `.csproj`). Instalar desde https://dotnet.microsoft.com/
- `dotnet` CLI disponible en `PATH`.
- (Opcional) Docker para contenedores y despliegue.
- Variables de entorno y proveedor de secretos para credenciales (KeyVault, Vault, AWS Secrets Manager, etc.).
- Docker y Docker Compose instalados.					

# Instalación y ejecución rápida

1. Clonar el repositorio.
2. Desde la raíz, restaurar paquetes y ejecutar el proyecto Web API:

```bash
cd api
dotnet restore
dotnet run
```

Por defecto la API arranca en `http://localhost:5000` o el puerto configurado por ASP.NET Core. Swagger estará disponible en entorno de desarrollo si está habilitado.

# Estructura de Scripts

Docker ejecuta los archivos de `docker-entrypoint-initdb.d/` en **orden alfabético**.
Por eso cada carpeta usa prefijo numérico y los archivos también.

```
security/01_create_users.js     ← primero: crea usuarios
schema/01_clients.js            ← segundo: define colecciones
schema/02_invoices.js
schema/03_reminders_log.js
seed/01_clients.js              ← tercero: inserta datos de prueba
seed/02_invoices.js
migrations/                     ← se corren manualmente o en CI/CD
```

Los scripts de `docker-entrypoint-initdb.d/` solo se ejecutan
si el volumen de datos está **vacío**. Si ya existe data, se omiten.


### Usuarios creados

| Usuario           | Permisos            | Usado por              |
|-------------------|---------------------|------------------------|
| `admin`           | Root (del .env)     | Solo administración    |
| `billing_user`    | readWrite           | Backend C#             |
| `billing_readonly`| read                | Dashboard / reportes   |
| `billing_auditor` | read + reminders_log| Servicio de auditoría  |


