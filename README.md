<h1 align="center"> BD Domotica </h1>
<p align="center">
  <img src="https://et12.edu.ar/imgs/et12.gif">
</p>

<h2 align="center"> E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín" </h2>

**Desarrolado por**:
* **Karen Mejia**: Encargada de hacer la BD, las clases en C#, Dapper, Tests y Grants. Y la implementacion de MVC

**Asignatura**:  PSR

**Nombre TP**: Domotica

**Curso**: 6to 7ma

**Año**:  2025

# Domotica
Este proyecto se basa en Base de Datos de Domotica,con el objetivo de uso cotidiano y comodidad. A través de esta BD, podrás controlar cualquier electrodoméstico que se encuentre en tu hogar, independientemente de tu ubicación; tendrás la comodidad de encenderlos o apagarlos sin estar presente en ella, podrás ver el consumo de dicho electrodoméstico para saber cuanto gastaste mensualmente, podrás ver todos los electrodomésticos que esten en tu casa en un formato de lista, y un dato no menor podrás tener más de dos casas desde tu mismo usuario y poder acceder a ellas en simultaneo.

## Comenzando 

Clonar el repositorio github, desde Github Desktop o ejecutar en la terminal o CMD:

```
https://github.com/Katt4722/BD_Domotica
```

## Pre-requisitos 

- .NET 9 (SDK .NET 9.0.X) - [Descargar](https://dotnet.microsoft.com/es-es/download/dotnet/9.0)
- Visual Studio Code - [Descargar](https://code.visualstudio.com/#alt-downloads)
- Git - [Descargar](https://git-scm.com/downloads)
- MySQL - [Descargar](https://dev.mysql.com/downloads/mysql/)
- Dapper - Micro ORM para .NET

## Pasos para iniciar el proyecto 

_Para iniciar el proyecto primero debe desplegar la base de datos y para eso tiene que hacer segundo click en la carpeta scripts bd_
_y presionar en terminal integrado, le aparecera una terminal donde tiene que poner lo siguiente:_

```
mysql -u tuUsuario -p 
:tuContraseña
```
_Luego dirigirse a la carpeta src y dentro de la carpeta Biblioteca.Persistencia.Dapper.Test

1. Crear `appSettings.json`: nombre del archivo json que tiene que estar en la misma carpeta.
El contenido del archivo tiene que ser:  
  ```json
  {
  "ConnectionStrings": {
    "MySQL": "server=localhost;database=tuBD;user=tuUsuarioBD;password=tuPass"
  }
  }
  ```

Para desplegar el proyecto, sigue los siguientes pasos:


1. **Abrir el proyecto**:
     ```
   - Abre el proyecto en Visual Studio Code ejecutando:
     ```



2. **Configurar la base de datos**:
   - Asegúrate de tener MySQL instalado y en funcionamiento.
   - Crea una base de datos llamada `5to_Domotica` en tu servidor MySQL.
   - Navega a la carpeta `scripts bd` dentro del proyecto:
     ```
     cd scripts bd
     ```
   - Ejecuta el siguiente comando para importar los scripts SQL necesarios:
     ```
     mysql -u UsuarioMySQL -p
     ```
   - Luego, ingresa la contraseña de tu usuario y ejecuta:
     ```
     install ddl.sql
     ```

3. **Ejecutar el proyecto**:
   - Regresa al directorio raíz del proyecto:
     ```
     cd ..
     ```
   - Ejecuta el proyecto utilizando el siguiente comando:
     ```
     dotnet run
     ```
    - Ejecuta el proyecto pero permitiendo que las maquinas de tu red lan puedan interactuar con el proyecto: 
     ```
      dotnet run --urls "http://0.0.0.0:5000"
     ```

4. **Probar el proyecto**:
   - Para ejecutar las pruebas unitarias, utiliza el siguiente comando:
     ```
     dotnet test
     ```

5. **Acceder a la aplicación**:
   - Una vez que el proyecto esté en ejecución, podrás acceder a la aplicación a través de tu navegador en la dirección que se indique en la terminal (generalmente `http://localhost:5000` o similar).


## Herramientas

El proyecto fue construido utilizando las siguientes herramientas y versiones:

* .NET 8 (SDK .NET 8.0.105)
* Visual Studio Code
* SQL Server
* Dapper (versión 2.1.35)
* Entity Framework Core
* MySQL (versión 8.0 o superior)
* XUnit (para pruebas unitarias)
* ASP.NET Core MVC (para el manejo del patrón Modelo-Vista-Controlador (MVC))
* Autenticacion basada en cookie (Utiliza el middleware con cookies para manejar la sesión del usuario y almacenar la información de autenticación de manera segura.)
