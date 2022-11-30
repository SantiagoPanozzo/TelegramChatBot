# Manual de Usuario

En este apartado daremos las instrucciones para el correo uso del bot:

***

# Ejecución del programa:
Para poder utilizar el bot es necesario ejecutar el programa en un dispositivo con DotNet Runtime 6 o superior instalado.
Con esto hecho basta con ejecutar el proyecto "Program" dentro de la carpeta "src/Program" con el comando `dotnet run`
desde dentro de dicha carpeta, o el comando `dotnet run --project ./src/Program`. Dicho proyecto contiene únicamente el
código para iniciar un loop de forma tal que el programa no se detenga hasta que se ingrese la palabra `salir` por consola
o se cierre el proceso que ejecuta el programa.

***

# Utilización del bot por Telegram:

## Inicio:
Para poder comunicarnos con el bot es necesario agregarlo como contacto en Telegram, esto se puede lograr a través del 
siguiente link: [link al bot](http://t.me/Equipo21Bot).

## Comandos iniciales:
Al iniciar el bot se nos presentan únicamente tres comandos, "/info", "/login" y "/registrarse":
* **"Info"**:  muestra los posibles comandos que se pueden utilizar en el momento.
* **"Login"**: permite a un usuario ya registrado iniciar sesión con su cuenta (también se lo puede llamar 
escribiendo "iniciar sesion")
* **"Registrarse"**: permite crear una cuenta nueva en el bot.

Existe también un último comando secreto llamado "Panel de control" que permite a los administradores gestionar
los datos almacenados en el bot, pero solo pueden acceder aquellos administradores cuya cuenta ya exista en el bot,
debido a que no se puede registrar una cuenta administradora desde el chat.

## Funciones del bot:
Una vez el usuario haya iniciado la sesión, tendrá la posibilidad de realizar distinas acciones dependiendo de su rol,
algunas de las cuales son:

* Buscar ofertas de servicio (comando "/buscar")
* Ver sus propias ofertas de servicio (comando "/ver ofertas")
* Ver sus propias solicitudes (comando "/ver solicitudes)
* Ver su propia información (comando "/ver info")

La posibilidad que tiene cada rol de acceder a estas opciones son tal que:


|        Rol        |      Buscar       |    Ver ofertas    |  Ver solicitudes  |     Ver Info      |
|-------------------|-------------------|-------------------|-------------------|-------------------|
|     Trabajador    |        No         |        Si         |        Si*        |        Si         |
|      Empleador    |        Si         |       No**        |        Si         |        Si         |
|    Administrador  |      No***        |      Si***        |      Si***        |       No***       |

* \* Los trabajadores pueden ver todas las solicitudes que los empleadores hayan hecho para las ofertas del
propio trabajador, pero no pueden ver otras solicitudes que no los tengan a ellos como "objetivo".
* \*\* Los empleadores no tienen la opción de ver ofertas como tal, ya que ellos no crean ofertas, sin embargo,
disponen del buscador que les permite ver las ofertas creadas por los demás para así poder solicitarlas.
* \*\*\* Las opciones de los administradores parecen limitadas ya que, similar al empleador con el comando de
ver ofertas, no pueden directamente utilizar estos métodos reservados para los otros usuarios; sin embargo, si
que disponen de su propia forma de hacer algo similar mediante el Panel de Control, donde si pueden ver toda
esta información de todos los usuarios y además modificarla.

