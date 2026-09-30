Documentación del Proyecto: Sistema de Registro de Incidencias (SistemaTickets)
1. Objetivo y Alcance del Proyecto
    El presente proyecto tiene como objetivo principal proporcionar una plataforma eficiente para el registro y gestión de incidencias de equipos de cómputo. Está diseñado específicamente para facilitar la labor del personal técnico, permitiendo un registro ágil y centralizado cuando un cliente requiere la reparación o mantenimiento de su equipo.
    
    Actualmente, el sistema está concebido para uso exclusivo e interno del personal técnico.

2. Roles y Gestión de Acceso
    El sistema opera bajo un modelo de acceso de privilegio único.

    Administrador / Técnico: Es el único rol existente en el sistema. Solamente el personal autorizado bajo este rol tiene la capacidad de acceder a la plataforma y realizar el registro de las incidencias.

    Autenticación: La validación de credenciales y el control de acceso se gestionan mediante la integración con los servicios de Supabase.

3. Arquitectura y Base de Datos
    El proyecto se encuentra en una fase inicial de desarrollo.

    Base de Datos: En la actualidad, Supabase se utiliza de manera exclusiva para el servicio de autenticación de usuarios. El esquema de base de datos relacional para almacenar las entidades del sistema (como los registros de los tickets, detalles de clientes o categorías de incidencias) se encuentra en fase de planeación y diseño, por lo que aún no ha sido implementado.

    Diseño Frontend: La interfaz de usuario está construida empleando el framework de diseño Tailwind CSS. La compilación de los estilos se gestiona a través del ecosistema de Node.js, reflejado en los archivos de configuración incluidos en el proyecto.

4. Estructura del Código y Directorios
    Para facilitar la comprensión del proyecto a nivel de código para futuros desarrolladores, a continuación se detalla la estructura principal de directorios y archivos:

    .devcontainer/: Contiene la configuración para el uso de contenedores de desarrollo en Visual Studio Code, garantizando entornos consistentes.

    .github/workflows/: Aloja los scripts de integración continua (CI) para la compilación y prueba automática del código mediante GitHub Actions (dotnet-ci.yml).

    Pages/: Directorio principal de la interfaz de usuario, desarrollado bajo el patrón ASP.NET Core Razor Pages.

    Index.cshtml, Privacy.cshtml, Error.cshtml: Páginas estandarizadas de inicio, políticas y manejo de errores.

    IncidenciasPages/principal.cshtml: Página que contiene la interfaz principal para el registro de las incidencias de los equipos de cómputo.

    Shared/: Contiene componentes compartidos como el diseño maestro (_Layout.cshtml) y scripts de validación.

    Services/: Directorio destinado a la lógica de negocio y la integración con proveedores externos. Destaca el archivo SupabaseAuthService.cs, encargado de establecer la comunicación con la API de Supabase para la autenticación.

    wwwroot/: Directorio raíz para archivos estáticos (públicos).

    css/: Incluye los archivos base de estilos (app.css) y el archivo final compilado por Tailwind CSS (output.css).

    lib/: Contiene bibliotecas de terceros integradas por defecto (como Bootstrap y jQuery, aunque el diseño principal delegue en Tailwind).

    supabase/: Directorio de configuración local para el entorno de Supabase (config.toml).

    Archivos de Configuración Raíz:

    Program.cs: Punto de entrada principal de la aplicación .NET, donde se configuran los servicios y el flujo de peticiones HTTP.

    appsettings.json: Archivo de configuración para variables de entorno y cadenas de conexión.

    package.json / package-lock.json: Archivos de manifiesto de Node.js, utilizados exclusivamente para gestionar las dependencias de Tailwind CSS.

    docker-compose.yml: Archivo de orquestación de servicios Docker.

5. Requisitos e Instalación
    El proyecto ha sido diseñado bajo una filosofía de despliegue mediante contenedores. La meta a futuro es alojar la imagen de la aplicación en un repositorio en la nube.

    Por lo tanto, para los desarrolladores que deseen ejecutar o probar el proyecto localmente, el único requisito estricto es tener instalada la versión más reciente de Docker.

    No es estrictamente necesario instalar el SDK de .NET ni Node.js de manera local, ya que el archivo docker-compose.yml y la infraestructura de contenedores se encargarán de proveer el entorno de ejecución necesario y las dependencias requeridas para compilar tanto el código backend como el frontend.

6. Despliegue (Deployment) y Estado Actual
    El proyecto se encuentra en una fase temprana (desarrollo inicial). Las instrucciones particulares para su despliegue en entornos de producción (Producción / Staging) no han sido definidas aún, ya que la infraestructura final de alojamiento será evaluada en fases posteriores del ciclo de vida del software.

    Si considera que es necesario profundizar en alguna sección específica, como detallar algún servicio del código backend en particular, quedo a su entera disposición para expandir el documento.