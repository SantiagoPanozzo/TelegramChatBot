# Proyecto ChatBot Programación II
### Implementación de un chatbot para ayudar a la gente encontrar trabajo y/o servicios

## Integrantes del equipo:
Santiago Panozzo - Facundo Martínez - Tomás Esteves - Mateo Rodríguez - Alejandra Benítez

## Consigna
¿Cómo podemos nosotros, estudiantes del curso de Programación II ayudar a las personas a encontrar trabajo? 
En base a las necesidades del usuario que el chat bot le responda con datos de quienes otorgan ese servicio. 
Ejemplo jardineros, pintores, chapistas y otros servicios. Conectando quienes necesitan un servicio y quienes lo ofrecen. 
Crear dentro del chat box algo similar a reputación, es decir un jardinero que hizo 3 trabajos ya tiene su clasificación para darle más seguridad a quien lo contrata. 
También podrían incorporarse sistemas de pagos a través de la "aplicación" lo cual generaría una mayor solidez de la estructura previniendo estafas y hurtos.

## Escenarios
Aquí veremos una explicación general e informal de las funciones del Software (nuestro programa), escrita desde la perspectiva del usuario final. Su propósito es articular cómo el Software proporcionará una función de valor al cliente.

1) Cómo administrador, quiero poder indicar categorías sobre las cuales se realizarán las ofertas de servicios para que de esa forma, los trabajadores puedan clasificarlos.
2) Como administrador, quiero poder dar de baja ofertas de servicios, avisando al oferente para que de esa forma, pueda evitar ofertas inadecuadas.
3) Como trabajador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto para que de esa forma, pueda proveer información de contacto a quienes quieran contratar mis servicios.
4) Como trabajador, quiero poder hacer ofertas de servicios; mi oferta indicará en qué categoría quiero publicar, tendrá una descripción del servicio ofertado, y un precio para que de esa forma, mis ofertas sean ofrecidas a quienes quieren contratar servicios.
5) Como empleador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto para que de esa forma, pueda proveer información de contacto a los trabajadores que quiero contratar.
6) Como empleador, quiero buscar ofertas de trabajo, opcionalmente filtrando por categoría para que de esa forma, pueda contratar un servicio.
7) Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma ascendente de distancia a mi ubicación, es decir, las más cercanas primero para que de esa forma, pueda poder contratar un servicio.
8) Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma descendente por reputación, es decir, las de mejor reputación primero para que de esa forma, pueda contratar un servicio.
9) Como empleador, quiero poder contactar a un trabajador para que de esa forma pueda, contratar una oferta de servicio determinada.
10) Como trabajador, quiero poder calificar a un empleador; el empleador me tiene que calificar a mí también, si no me califica en un mes, la calificación será neutral, para que de esa forma pueda definir la reputación de mi empleador.
11) Como empleador, quiero poder calificar a un trabajador; el trabajador me tiene que calificar a mí también, si no me califica en un mes, la calificación será neutral, para que de esa forma, pueda definir la reputación del trabajador.
12) Como trabajador, quiero poder saber la reputación del empleador que me contacte para que de esa forma, poder decidir sobre su solicitud de contratación.

***
## Reflexiones de Grupo
### Santiago Panozzo:
Me parece que el proyecto es interesante y fue bastante desafiante poder incorporar todas las cosas que se pedían para esta entrega. A su vez, fue interesante el ver situaciones en las que podíamos aplicar los patrones que estabamos trabajando
en clase para mejorar nuestro código; por ejemplo el singleton que nos sirvió para no tener que estar "buscando" las instancias de los catálogos cada vez que interactuabamos con ellos, sin crear uno nuevo.
Quizás algunas de las clases que hicimos podrían ser mejoradas, ya que aprendimos algunos patrones despúes de haberlas hecho y no tuvimos tiempo de volver atrás a cambiarlas. En general está siendo una experiencie enriquecedora en cuanto a
aprendizaje mediante trabajo.

### Mateo Rodríguez:
- Hubo buena colaboración grupal, se me hace una buena y gratificante experiencia ya que nos sirve para adentrarnos en el mundo de trabajar en conjunto con un equipo de desarrollo.
- Fue bueno emplear patrones y conceptos aprendidos en clase, y a su vez, nuestro propio conocimiento
- A mi parecer, lo mas dificil del proyecto fue lograr que mis clases funcionen en conjunto con las de mis compañeros y viceversa, ya sea en su funcionamiento o simplemente hacer que ellos entiendan mi codigo y yo el de ellos


### Facundo Martínez:

-  Bastante importante el concepto de programación asíncrona (entender await/async/Task, conceptos de threading y deadlock) que no se tocó
   en ningún momento, en especial para la interacción con APIs.
-  Gracias al proyecto, pudimos tocar nuevos temas, como lo son la programación asíncrona, el uso de cloud services (google cloud),
   APIs y Keys, bases de datos no relacionales (noSQL en este caso con MongoDB), y variables de entorno (puntos que para esta entrega en particular
   no se aplicaron completamente).
-  La parte más desafiante, en mi caso en particular, fue todo lo relacionado a la programación asíncrona, como un ejemplo,
   en una instancia me encontré con un deadlock en el que no fue hasta más adelante que entendí el por qué siquiera ocurría o qué era. Por otro lado la importancia de
   las variables de entorno, en un momento cometí el error de pushear mi commit al repositorio con una key privada, por lo que poco después
   tuve que deshabilitarla.

### Tomás Esteves:
Hasta el momento el proyecto está siendo una experiencia desafiante a la vez que enriquecedora. Enriquecedora en el sentido de que es una experiencia
totalmente nueva para mi y la mayoría de integrantes del equipo por el hecho de estar trabajando de la manera que los estamos haciendo, que viene a ser 
como una primer experiencia de un equipo de desarrollo el día de mañana. Y hasta el momento lo más complicado fue lograr una buena interpretación del
código de los demás integrantes para poder implementarlo con las demás clases y funciones. También por otro lado el hecho de intentar aplicar gran cantidad
de los conceptos aprendidos en clase, que en la está hecha la documentación de los que se utilizaron en la realización de nuestro proyecto.

En cuanto a la organización debo destacar que fue bastante buena, algunas aplicaciones nos ayudaron a manejarla de una mejor manera, como lo fue Jira
(software de administración de proyectos similar a Trello), donde nos fuimos dividiendo las tareas que eran necesarias y así tener un mejor control sobre 
el estado de las mismas, si están para realizar, en curso o ya fueron realizadas. Adjunto captura de pantalla del tablero.

![Jira](Assets/Jira.png)
En la captura se ven algunas de las asignaciones a las tareas. Donde cada ícono representa un distinto integrante del equipo.
(AB: Alejandra Benítez - TE: Tomás Esteves - Imagen: Facundo Martínez - SP: Santiago Panozzo - MR: Mateo Rodríguez)

### Alejandra Benítez:
La elaboración del proyecto se presentó como todo un desafío, pero a su vez una excelente oportunidad para aplicar los contenidos dados en clase.  Utilizar el Git cómo herramienta para realizar comits e ir trabajando cada uno en paralelo en el mismo proyecto resultó un ser recurso muy práctico y acertado. Se pudo además, aplicar diferentes patrones aprendidos y realización de test para verificar que el código funcione correctamente. En general es una excelente experiencia práctica de colaboración y apoyo grupal.

***
## Asignaciones:
Nos repartimos las tareas en general de la siguiente forma:

### Facundo Martinez:
* Clase administrador
* Refactorizar clase usuario (fixes en el constructor)
* Investigar sobre API para buscador por ubicación
* Investigar sobre formas de añadir seguridad a la información de los usuarios
* Investigar sobre bases de datos para implementar a futuro

### Mateo Rodriguez:
* Clase SearchHandler (buscador)
* Clase ContratoHandler
* Clase Trabajador
* Tests unitarios de las clases

### Tomás Esteves:
* Clase Usuario
* Clase Registro (RegistryHandler)
* Clase Solicitud
* Clase IPrinter y sus implementaciones
* Documentación XML y Doxygen de las clases

### Alejandra Benitez:
* Documentación XML de métodos
* Clase OfertasHandler
* Clase SolicitudCatalog
* Escenarios

### Santiago Panozzo:
* UML
* Sistema de ubicación por coordenadas
* Métodos para registrar
* Enums (Calificacion, TipoDeUsuario, Aceptada)
* Autenticación a través de TipoDeUsuario y sistema de nicks
* Escenarios faltantes
