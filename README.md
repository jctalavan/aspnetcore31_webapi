# ASP.NET Core 3.1 Web Api

Api web desarrollada en ASP.Net Core 3.1 siguiente los tutoriales de la documentación de Microsoft:
[https://docs.microsoft.com/es-es/aspnet/core/web-api/?view=aspnetcore-3.1]
[https://docs.microsoft.com/es-es/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code]

## Crear proyecto web api usando la plantilla por defecto

Para crear un proyecto de tipo web api utilizando la plantilla por defecto, abrimos un terminal *powershell* o *shell* e introducimos la siguiente instrucción:

    dotnet new webapi -o TodoApi

Además, añadimos el fichero *.gitignore* para evitar subir ficheros innecesarios al repositorio de control de código (carpetas bin, obj, etc.). Para ello lo que hacemos es ejecutar la siguiente instrucción en el terminal:

    dotnet new gitignore

## Incorporación de un contexto de base de datos

El *contexto de base de datos* es la clase principal que coordina la funcionalidad de Entity Framework para un modelo de datos.

Esta clase se crea derivándola de la clase *Microsoft.EntityFrameworkCore.DbContext*.

Para poder trabajar con Entity Framework en nuestro proyecto ejecutamos en un terminal las siguientes instrucciones:

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.InMemory

Con estas intrucciones nos descargamos de Nuget los paquetes necesarios para poder trabajar con Entity Framework en nuestro proyecto.

## Registro del contexto de base de datos

En ASP.NET Core, los servicios (como el contexto de la base de datos) deben registrarse con el contenedor de inserción de dependencias (DI).

El contenedor de inserción de dependencias permite proporcionar los distintos servicios configurados a los controladores. Con esto, podremos acceder a nuestro contexto de base de datos desde nuestros controladores.

Esta configuración se realiza en la clase *Startup.cs*.
