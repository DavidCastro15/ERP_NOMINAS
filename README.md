# ERP de Nóminas - Azucarera San José de Abajo

Este proyecto es un sistema ERP de Nóminas desarrollado en C# (Windows Forms) con .NET Framework 4.7.2. Su objetivo principal es encargarse del cálculo de varios tipos de nóminas, integrando el control de asistencia, el manejo de múltiples conceptos de pago (percepciones) y descuentos (deducciones), así como la generación de reportes y procesos de nómina.

## Arquitectura del Proyecto

La solución (`ERP_NOMINAS.sln`) está estructurada en tres proyectos principales para mantener el código modular y escalable:

1. **ERP_LOGIN**
   Módulo encargado de la autenticación de los usuarios. Valida las credenciales y, en caso de éxito, lanza el módulo principal transfiriendo los datos de sesión (token/paquete de sesión) de forma segura.

2. **ERP_NOMINAS**
   Es el módulo principal donde ocurre toda la lógica de negocio y operativa del ERP. Aquí se centralizan las interfaces de usuario para:
   - **Catálogos Maestros:** Gestión de empleados, departamentos, categorías, ciclos, días festivos, materiales, equipos, salarios integrados y usos.
   - **Control de Asistencia:** Registro de asistencia, listas de asistencia y manejo de turnos adicionales.
   - **Percepciones (No Automáticas):** Registro de destajos, compensaciones, gratificaciones, premios por asistencia/puntualidad, vales de despensa, apoyo de transporte, subsidios, topos y más.
   - **Deducciones (No Automáticas):** Administración de pensiones alimenticias, retenciones de INFONAVIT, ajustes de ISR, ISR variable, descuentos globales, omisiones de ahorro y descuentos varios.
   - **Proceso de Nómina:** Módulos para aplicar deducciones en bloque y llevar a cabo los cálculos de la nómina (Ej. proceso para empleados Sindicalizados en Zafra, Reparación, etc.).

3. **ERP_SHARED**
   Proyecto de biblioteca de clases compartida. Contiene:
   - Manejo y modelos de la Sesión del usuario y permisos.
   - Lógica de conexión a la base de datos SQL Server (`ConnectorSql`).
   - Funciones globales útiles para todo el sistema.
   - Constantes de la aplicación (`AppConstants.cs`).

## Tecnologías Utilizadas

- **Lenguaje:** C#
- **Framework:** .NET Framework 4.7.2
- **Interfaz de Usuario:** Windows Forms (WinForms)
- **Base de Datos:** SQL Server
- **Reportes y Exportación:** Microsoft Report Viewer, ClosedXML, DocumentFormat.OpenXml, CsvHelper.

## Instalación y Configuración

1. **Clonar el repositorio.**
2. **Restaurar paquetes NuGet:**
   Abre la solución `ERP_NOMINAS.sln` en Visual Studio y restaura los paquetes NuGet (ClosedXML, CsvHelper, Newtonsoft.Json, Microsoft.SqlServer.Types, etc.).
3. **Configuración de Base de Datos:**
   Revisa el archivo `ERP_SHARED/AppConstants.cs` o los archivos `App.config` para ajustar la IP del servidor (`ServerIp`), el usuario (`User`) y la contraseña de conexión a tu servidor SQL local o de producción.
   En modo de desarrollo, el sistema puede arrancar en modo *Bypass* (usuario ficticio local) según la lógica definida en el `Program.cs` de `ERP_NOMINAS`.
4. **Ejecución:**
   - Puedes compilar y establecer `ERP_LOGIN` como el proyecto de inicio para simular el flujo completo de autenticación.
   - Opcionalmente, para pruebas rápidas de interfaz, puedes establecer `ERP_NOMINAS` como el proyecto de inicio (el sistema autoconfigurará un usuario de prueba en caso de no recibir parámetros de inicio).

## Características Destacadas

* **Arquitectura modular** que separa la autorización, la interfaz pesada de cálculos y las utilidades de base de datos.
* Soporte para **Múltiples Plantillas y Ciclos** (como Zafra y Reparación).
* **Manejo exhaustivo de conceptos salariales**, integrando cálculos complejos de impuestos y subsidios (tarifas diarias, semanales y mensuales).
* Exportación de información y carga mediante archivos de hojas de cálculo usando la librería OpenXml y ExcelDataReader.

---
*Desarrollado para la gestión integral de nóminas, diseñado para facilitar, auditar y optimizar los procesos contables y de recursos humanos.*