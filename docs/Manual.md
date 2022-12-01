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

***

# Historias de usuario:
Para verificar que se pueden realizar las historias de usuario se deben ingresar los siguientes comandos una vez loggeado:

* Cómo administrador, quiero poder indicar categorías sobre las cuales se realizarán las ofertas de servicios para que de
esa forma, los trabajadores puedan clasificarlos:

  1) "admin"

  2) "1"

  3) "2"

  4) Ingresar la descripción de la categoría

* Como administrador, quiero poder dar de baja ofertas de servicios, avisando al oferente para que de esa forma, pueda evitar ofertas inadecuadas.**

    1) "admin"

    2) "2"

    3) "1" 

    4) Ingresar ID de la oferta

* Como trabajador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto para que de esa forma, pueda proveer información de contacto a quienes quieran contratar mis servicios.**

    1) "registrar"

    2) Completar datos

* Como trabajador, quiero poder hacer ofertas de servicios; mi oferta indicará en qué categoría quiero publicar, tendrá una descripción del servicio ofertado, y un precio para que de esa forma, mis ofertas sean ofrecidas a quienes quieren contratar servicios.**

    1) "ofertar"

    2) Ingresar ID de categoria

    3) Completar datos

* Como empleador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto para que de esa forma, pueda proveer información de contacto a los trabajadores que quiero contratar. *Como empleador, quiero buscar ofertas de trabajo, opcionalmente filtrando por categoría para que de esa forma, pueda contratar un servicio.**

    1) "registrar"

    2) Completar datos

* Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma ascendente de distancia a mi ubicación, es decir, las más cercanas primero para que de esa forma, pueda poder contratar un servicio.**

    1) "buscar"

    2) "2"

* Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma descendente por reputación, es decir, las de mejor reputación primero para que de esa forma, pueda contratar un servicio.**

    1) "buscar"

    2) "3"

    3) Ingresar reputacion a filtrar

* Como empleador, quiero poder contactar a un trabajador para que de esa forma pueda, contratar una oferta de servicio determinada.**

    1) implementar

* Como trabajador, quiero poder calificar a un empleador; el empleador me tiene que calificar a mí también, si no me califica en un mes, la calificación será neutral, para que de esa forma pueda definir la reputación de mi empleador.**

    1) "Ver solicitudes"

    2) "2"

    3) Ingresar ID de la solicitud

    4) "1"

    5) Ingresar calificacion

* Como empleador, quiero poder calificar a un trabajador; el trabajador me tiene que calificar a mí también, si no me califica en un mes, la calificación será neutral, para que de esa forma, pueda definir la reputación del trabajador.**

    1) "Ver solicitudes"

    2) "2"

    3) Ingresar ID de la solicitud

    4) "1"

    5) Ingresar calificación

* Como trabajador, quiero poder saber la reputación del empleador que me contacte para que de esa forma, poder decidir sobre su solicitud de contratación.

    1) Implementar