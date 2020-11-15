# ASP.NET Core 3.1 Web Api

Api web desarrollada en ASP.Net Core 3.1 siguiente los tutoriales de la documentación de Microsoft:
[https://docs.microsoft.com/es-es/aspnet/core/web-api/?view=aspnetcore-3.1]
[https://docs.microsoft.com/es-es/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code]

## Crear proyecto web api usando la plantilla por defecto

Para crear un proyecto de tipo web api utilizando la plantilla por defecto, abrimos un terminal *powershell* o *shell* e introducimos la siguiente instrucción:

    dotnet new webapi -o TodoApi

Además, añadimos el fichero *.gitignore* para evitar subir ficheros innecesarios al repositorio de control de código (carpetas bin, obj, etc.). Para ello lo que hacemos es ejecutar la siguiente instrucción en el terminal:

    dotnet new gitignore
