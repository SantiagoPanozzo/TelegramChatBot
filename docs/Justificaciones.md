# Justificaciones:

***

## Utilización de patrones:

### Patrones utilizados:

* **Chain of responsibility**: Utilizamos este patrón para todos los Handlers dentro de la carpeta 'src/Library/BotHandlers'. Se sigue el orden establecido en la variable `firstHandler`. Cada Handler del bot hereda de `BaseHandler` y recibe como parámetro en su constructor el "siguiente Handler". El funcionamiento es tal que el primer Handler intentará gestionar qué hacer con el mensaje recibido del usuario (evaluado en su método CanHandle()),  si no puede gestionarlo va a pasarselo al `IHandler` que le pasamos como parámetro en su constructor. Seguirá esta cadena hasta que uno de los Handlers pueda gestionar el mensaje o se llegue al último (el cual tendrá como siguiente Handler `null`).
* **Singleton**: Utilizamos el patrón Singleton en todas las clases de las que solo queríamos que hubiera una instancia. Esto es importante para las que denominamos "Handler" dentro de 'src/Library' (no confundir con los Handlers mencionados en Chain of Responsibility), ya que estas clases son nuestra "base de datos" junto con las denominadas "catalog" y no nos convenía tener más de una instancia de cada clase ya que no sabríamos a cual acceder para ver y modificar los datos.
* **DIP y LSP**: Utilizamos varias veces el principio de inversión de dependencias (DIP) y el LSP, más que nada este último creando varias interfaces, algunas de las cuales utilizan el tipo genérico, para que el programa fuera más expansible. De esta forma, podemos utilizar esas interfaces como intermedio en los métodos, para poder aceptar una variable de cualquier tipo siempre y cuando ese tipo implemente una interfaz específica que le fuerce a poder realizar las acciones que necesitamos.
* **Creator**: Respetamos el patrón creator creando instancias de las clases dentro del código de las clases con las que estuvieran más relacionadas. Por ejemplo, la clase encargada de crear una OfertaDeServicio es la clase OfertasHandler ya que es la que interactúa con el usuario y verifica que la información ingresada sea la correcta.
* **Expert y SRP**: Nos aseguramos de que nuestras clases cumplieran los principios Expert y SRP siendo expertas en la información que necesitan para cumplir con su única responsabilidad.