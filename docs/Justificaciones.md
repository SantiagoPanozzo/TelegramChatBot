# Justificaciones

## Utilización de patrones:

### Patrones y principios utilizados:

* **Chain of responsibility**: Utilizamos este patrón para todos los Handlers dentro de la carpeta 'src/Library/BotHandlers'. 
Se sigue el orden establecido en la variable `firstHandler`. Cada Handler del bot hereda de `BaseHandler` y recibe como 
parámetro en su constructor el "siguiente Handler". El funcionamiento es tal que el primer Handler intentará gestionar 
qué hacer con el mensaje recibido del usuario (evaluado en su método CanHandle()),  si no puede gestionarlo va a pasarselo 
al `IHandler` que le pasamos como parámetro en su constructor. Seguirá esta cadena hasta que uno de los Handlers pueda 
gestionar el mensaje o se llegue al último (el cual tendrá como siguiente Handler `null`).
* **Singleton**: Utilizamos el patrón Singleton en todas las clases de las que solo queríamos que hubiera una instancia. 
Esto es importante para las que denominamos "Handler" dentro de 'src/Library' (no confundir con los Handlers mencionados en 
Chain of Responsibility), ya que estas clases son nuestra "base de datos" junto con las denominadas "catalog" y no nos convenía 
tener más de una instancia de cada clase ya que no sabríamos a cual acceder para ver y modificar los datos.
* **DIP y LSP**: Utilizamos varias veces el principio de inversión de dependencias (DIP) y el LSP, más que nada este último 
creando varias interfaces, algunas de las cuales utilizan el tipo genérico, para que el programa fuera más expansible. De 
esta forma, podemos utilizar esas interfaces como intermedio en los métodos, para poder aceptar una variable de cualquier 
tipo siempre y cuando ese tipo implemente una interfaz específica que le fuerce a poder realizar las acciones que necesitamos.
* **Creator**: Respetamos el patrón creator creando instancias de las clases dentro del código de las clases con las que 
estuvieran más relacionadas. Por ejemplo, la clase encargada de crear una OfertaDeServicio es la clase OfertasHandler ya 
que es la que interactúa con el usuario y verifica que la información ingresada sea la correcta.
* **Expert y SRP**: Nos aseguramos de que nuestras clases cumplieran los principios Expert y SRP siendo expertas en la 
información que necesitan para cumplir con su única responsabilidad.

Nos pareció crucial el uso de patrones y principios para crear un código más reutilizable y en general más entendible. 
Por ejemplo, si no fuera por el uso de interfaces se nos habría hecho muy dificil integrar y agrupar nuevas clases que 
realizaran tareas similares. Lo mismo con el singleton como mencionamos, que tener varias instancias de nuestras clases
"bases de datos" sería contraproducente en varios sentidos.

***

## Otras decisiones tomadas:

### Registro:
Utilizamos un sistema de registro e inicio de sesión en nuestro bot a pesar de que no era un requerimiento considerando
el uso de Telegram, el cual ya tiene un registro propio que podríamos haber empleado. Nuestra decisión se basó en el
hecho de que sería molesto, tanto para el testeo como para el uso general del programa, el tener un usuario ligado a una
cuenta de Telegram. Por ejemplo, para testear una interacción entre un empleador y trabajador tendríamos que utilizar
dos dispositivos con cuentas distintas para generar esa interacción. Utilizando nuestra implementación una misma cuenta
de Telegram puede registrar más de un usuario y, uno a la vez, iniciar sesión con ellos. De esta forma, alguien puede
pedirle prestado su dispositivo a otra persona y simplemente iniciar sesión con su cuenta, o tener una cuenta como
empleador y otra como trabajador, o incluso en el caso de los administradores estos pueden tener su cuenta de administrador
y además una cuenta normal como trabajador o empleador.

### API de Ubicación:
Utilizamos la API DistanceMatrix de Google Maps ya que fue la que encontramos tiempo antes de que nos fuera presentada
la recomendada por la Universidad. Esta API requiere el uso de Google Cloud por lo que su uso está limitado a la cantidad
de datos que nos permite este servicio por mes. Para este proyecto creemos que esto es más que suficiente y nos da unas
herramientas bastante útiles para calcular la distancia entre dos puntos a un nivel bastante preciso y confiable.

### Administrador:
Los administradores los hicimos hardcodeados ya que creemos que a una escala reducida no hay necesidad de que haya una
gran cantidad de administradores, por lo cual es bastante realista que la persona u organización que utilice el bot se
pueda dar a la tarea a añadir manualmente a cada administrador. Por lo cual, solo se podrá iniciar sesión como administrador
utilizando una cuenta ya existente autorizada.

### Updater:
Para poder gestionar los tiempos requeridos por el programa (por ejemplo, que cuando un usuario califique a otro habrá
un plazo de un mes para que el usuario calificado haga lo mismo), creamos una clase estática Updater. Esta clase se
encarga de periódicamente "avisarle" a todas las clases que requieran llevar una cuenta del tiempo (IActualizables) la
fecha actual. Updater tiene un método asíncrono para iniciar actualizaciones automáticas cada X cantidad de tiempo
establecida como variable de tipo TimeSpan dentro de la clase; esto inicia timers con esa cantidad de tiempo que, cuando
finalizada, le pasa a todos los IActualizable la fecha actual dada por DateTime.Now de forma que estos realicen sus tareas
acorde a la fecha. El punto es tener una sola clase con un método asíncrono que se encargue actualizar a las otras, para
para que estas otras no tengan que tener, por ejemplo, un timer que cuente 30 días (el cual se detendría si se detiene
el programa). Con un solo timer periódico que solo actualice la fecha, y una comparación en las clases actualizables de
si ya llegó la fecha que están esperando, nos ahorramos estos problemas.

### Panel de Control:
Separamos las acciones que puede realizar el administrador en un Handler de Telegram exclusivo para este, al cual solo
puede entrar un administrador con un ID de Telegram autorizado, y en el cual podrá ver la información de todos los usuarios
y todos los datos de ofertas, categorías y solicitudes existentes para poder darlos de baja si es necesario. De esta forma
el resto de usuarios tendrán sus métodos que mostrarán solo la información pertinente a ellos y sin este control exclusivo
de los administradores.

### Singleton Wipes:
Ya que implementamos el patrón Singleton en varias de nuestras clases, esto causó problemas inesperados a la hora de
testear, ya que dentro de un mismo archivo de Test se mantenía la instancia a través de cada test individual, lo cual
no queremos que pase si deseamos que cada Test esté aislado como situaciones distintas. Para esto implementamos un método
Wipe() en cada Singleton que actúa como un "anti-singleton" y se encarga de borrar la instancia para poder crear una
nueva en blanco la próxima vez que se llame al método de GetInstance(), efectivamente rompiendo el Singleton. Es por
esto que este método solo lo puede utilizar un administrador que tenga acceso directo al programa, ya que se debería
utilizar únicamente en casos de prueba para no romper con el patrón.