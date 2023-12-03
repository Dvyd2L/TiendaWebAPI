# WEB SERVICES. ACTIVIDAD 11

Instrucciones: Comenzar un proyecto Web API para solucionar un conjunto de necesidades específicas

## PARTE 1 ✅
	- Ejecutar el script proporcionado para generar las tablas de la base de datos MiTienda.

## PARTE 2 ✅
	- Crear un proyecto de tipo WebAPI.
	- Instalar los paquetes Nuget necesarios para utilizar Sql Server.
	- Integrar los modelos de datos de la base de datos creada.
	- Registrar la cadena de conexión de la base de datos en nuestro proyecto en el archivo json correspondiente.
	- Configurar en el Program el context de la base de datos para inyectarlo en los controllers.

## PARTE 3 ✅
	- Crear un controller para Dispositivos.
	- Desarrollar los siguientes endpoints en el controller que corresponda:
		- Agregar, modificar y eliminar un dispositivo. Al eliminar un dispositivo, solo permitir eliminar dispositivos descatalogados. Si se intenta eliminar un dispositivo que no está descatalogado, devolver un error 400.
		- Devolver los dispositivos de una marca que se pasará en la ruta.
		- Obtener una lista de dispositivos con la siguiente información construyendo una clase DTO a medida:
			- Id marca
			- Nombre marca
			- Nombre categoría
			- PromedioPrecio (Promedio del precio de los dispositivos de la marca)
			- CuentaDispositivos (Cuenta de dispositivos de la marca)
			- ListaDispositivos (Lista de los dispositivos de la marca):
				- Id dispositivo
				- Nombre dispositivo
				- Precio

## PARTE 4 ✅
	- Desarrollar un servicio con un método DescatalogarProducto que reciba un id de dispositivo. Hará lo siguiente:
		- Se localizará el dispositivo y se actualizará su propiedad Descatalogado a false.
	- Agregar un endpoint al controller de dispositivos que reciba un Id de dispositivo. Este método llamará a DescatalogarProducto para actualizar el dispositivo. Devolverá el dispositivo actualizado.

## PARTE 5 ✅
	- Desarrollar un middleware que registre en un archivo de texto la fecha y hora de cada petición, la IP desde la que se ha hecho la petición y el tipo de petición.

## PARTE 6 ✅
	- Desarrollar un filtro de excepción que responda a errores inesperados. Por cada petición, escribir en un archivo logErrores.txt, la IP de la petición, la fecha y hora, la ruta, el tipo de petición y el error que se ha producido.

## PARTE 7 ✅
	- Aplicar una CORS Policy al proyecto que acepte cualquier petición desde cualquier lugar.

## PARTE 8 ✅
	- Crear una tabla Usuarios con los siguientes campos:
		- Email (clave)
		- Password
		- Salt (varbinary(max)
	- Integrar la nueva tabla en el proyecto.
	- Desarrollar un servicio que genere y gestione el hash de un password.
	- Crear el controller de Usuarios con estos endpoints:
		- Register: Recibirá email y password y creará un usuario.
		- Login: Recibirá email y password y devolverá y:
			- Devolverá un 401 si no existe o el password es incorrecto.
			- Si las credenciales son correctas devolverá un objeto con el email del usuario y el token.
			- El token estará firmado con una clave de firma registrada en el archivo appsettings.
			- Incluir la generación de tokens en un servicio independiente.