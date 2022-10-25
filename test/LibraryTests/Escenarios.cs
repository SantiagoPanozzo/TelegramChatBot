namespace LibraryTests;
using Library;

public class Escenarios
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Caso1()
    // "Cómo administrador, quiero poder indicar categorías sobre las cuales se realizarán las ofertas de servicios
    // para que de esa forma, los trabajadoras puedan clasificarlos.
    { // TODO falta clase admin
        // Arrange
        
        
        // Act
        
        
        // Assert
        
    }

    [Test]
    public void Caso2()
    // Como administrador, quiero poder dar de baja ofertas de servicios, avisando al oferente para que de esa forma,
    // pueda evitar ofertas inadecudas.
    { // TODO falta clase admin
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso3()
    // Como trabajador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto
    // para que de esa forma, pueda proveer información de contacto a quienes quieran contratar mis servicios.
    {
        // Arrange
        RegistryHandler registryHandler = new RegistryHandler();
        bool Expected = true;
        
        // Act
        Usuario miUsuario = registryHandler.RegistrarTrabajador("Manolo","Manolete", "1234",
            "2001 3 14","1234567","099555555",
            "manoloreal@gmail.com",new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        bool Result = (miUsuario is Trabajador);
        
        // Assert
        Assert.That(Result.Equals(Expected));
        
    }

    [Test]
    public void Caso4()
    // Como trabajador, quiero poder hacer ofertas de servicios; mi oferta indicará en qué categoría quiero publicar,
    // tendrá una descripción del servicio ofertado, y un precio para que de esa forma, mis ofertas sean ofrecidas a
    // quienes quieren contratar servicios.
    {
        // Arrange
        OfertasHandler ofertasHandler = new();
        RegistryHandler registryHandler = new();
        CategoriasCatalog categoriasCatalog = new();
        Usuario miUsuario = registryHandler.RegistrarTrabajador("Manolo","Manolete", "1234",
            "2001 3 14","1234567","099555555",
            "manoloreal@gmail.com",new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        
        // TODO cambiar a un usuario admin para agregar la categoria
        categoriasCatalog.AddCategoria(miUsuario,"Tareas del hogar");

        // Act
        OfertaDeServicio oferta = ofertasHandler.Ofertar("Tareas del hogar",(Trabajador)miUsuario,"El mejor limpiador de Salto","Limpiador",9000);
        OfertaDeServicio Expected = oferta;
        OfertaDeServicio Result = ofertasHandler.GetOfertaByID(oferta.GetId());
        
        // Assert
        Assert.That(Expected.Equals(Result));

    }

    [Test]
    public void Caso5()
    // Como empleador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto
    // para que de esa forma, pueda proveer información de contacto a los trabajadores que quiero contratar.
    {
        // Arrange
        RegistryHandler registryHandler = new RegistryHandler();
        bool Expected = true;
        
        // Act
        Usuario miUsuario = registryHandler.RegistrarEmpleador("Señor Manolo","Manolete", "1234",
            "1970 3 14","1234567","099555555",
            "mistermanoloreal@gmail.com",new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        bool Result = (miUsuario is Empleador);
        
        // Assert
        Assert.That(Result.Equals(Expected));
    }

    [Test]
    public void Caso6()
    // Como empleador, quiero buscar ofertas de trabajo, opcionalmente filtrando por categoría para que de esa forma,
    // pueda contratar un servicio.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso7()
    // Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma ascendente de
    // distancia a mi ubicación, es decir, las más cercanas primero para que de esa forma, pueda poder contratar un
    // servicio.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso8()
    // Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma descendente por
    // reputación, es decir, las de mejor reputación primero para que de esa forma, pueda contratar un servicio.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso9()
    // Como empleador, quiero poder contactar a un trabajador para que de esa forma pueda, contratar una oferta de
    // servicios determinada.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso10()
    // Como trabajador, quiero poder calificar a un empleador; el empleador me tiene que calificar a mi también, si no
    // me califica en un mes, la calificación será neutral, para que de esa forma pueda definir la reputación de mi
    // empleador.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso11()
    // Como empleador, quiero poder calificar a un trabajador; el trabajador me tiene que calificar a mi también, si no
    // me califica en un mes, la calificación será neutral, para que de esa forma, pueda definir la reputaión del
    // trabajador.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }

    [Test]
    public void Caso12()
    // Como trabajador, quiero poder saber la reputación de un empleador que me contacte para que de esa forma, poder
    // decidir sobre su solicitud de contratación.
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

    }
}