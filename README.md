# JonesBank

## FRONTEND

- Dentro de la raíz del proyecto ejecutar npm install para restaurar node_modules.
- Ejecutar ng serve -o para levantar el proyecto.
- Para hacer login, utilizar este email: susAz@gmail.com y el pass: 1234 p.ej, hay dos usuarios registrados en la BD con el mismo password.


## BACKEND

- WebApi de .NET 8
- Cambiar el valor del Server en la cadena de conexión DefaultConnection por el nombre del servidor de base de datos SqlServer del equipo.
- En la ubicación del csproj, abrir consola powershell y ejecutar dotnet ef database update, esto creará la BD y insertará dos ususarios y cuatro cuentas.
- He implementado un sistema de autenticación con JWT, es necesario hacer login antes, para obtener el token, para luego hacer en cada petición http desde Angular,
o para utilizar en Postman, p.ej.
- He hecho pruebas unitarias al Controlador de Cuentas y a su repositorio.
