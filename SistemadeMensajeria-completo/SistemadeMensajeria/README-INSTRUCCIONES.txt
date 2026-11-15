SISTEMA DE MENSAJERIA - GUIA DE INICIO

Requisitos:
- Visual Studio actualizado compatible con .NET 10
- .NET 10 SDK
- SQL Server LocalDB (incluido normalmente con Visual Studio) o SQL Server.

Abrir:
1. Extraer la carpeta SistemadeMensajeria.
2. Abrir SistemadeMensajeria.slnx en Visual Studio.
3. Si Visual Studio no reconoce .slnx, abrir la carpeta y usar la CLI con los comandos indicados abajo.

Base de datos:
La cadena inicial usa SQL Server LocalDB:
Server=(localdb)\MSSQLLocalDB;Database=SistemadeMensajeriaDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True

Si usa otra instancia de SQL Server, cambie ConnectionStrings:Mensajeria en
SistemadeMensajeria.API/appsettings.json.

Desde una terminal ubicada en la carpeta que contiene el .slnx:
  dotnet restore SistemadeMensajeria.slnx
  dotnet build SistemadeMensajeria.slnx

Para crear la base de datos con EF Core:
  dotnet tool install --global dotnet-ef --version 10.0.12
  dotnet ef migrations add Inicial --project SistemadeMensajeria.Infrastructure --startup-project SistemadeMensajeria.API --output-dir AccesoDatos/Migrations
  dotnet ef database update --project SistemadeMensajeria.Infrastructure --startup-project SistemadeMensajeria.API

Para ejecutar:
  dotnet run --project SistemadeMensajeria.API

Swagger:
https://localhost:64463/swagger (o la URL mostrada por dotnet run).

API:
GET/POST/PUT/DELETE /api/clientes
GET/POST/PUT/DELETE /api/mensajeros
GET/POST/PUT/DELETE /api/direcciones
GET/POST/PUT/DELETE /api/envios

NOTAS:
- Este proyecto es una reconstrucción nueva basada en la estructura y requisitos disponibles.
- Los ZIP recibidos no contenían los archivos .cs originales; por eso no es posible afirmar que este código sea el código original recuperado.
- Las tablas se crean al ejecutar las instrucciones de migración.
