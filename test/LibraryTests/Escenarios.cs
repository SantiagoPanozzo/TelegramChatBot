namespace LibraryTests;
using Library;

/// <summary> Tests de los escenarios (casos de usuario) dados </summary>
public class Escenarios
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Caso1()
    // "Cómo administrador, quiero poder indicar categorías sobre las cuales se realizarán las ofertas de servicios
    // para que de esa forma, los trabajadores puedan clasificarlos.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("root", "toor", "1234", "abc@abc.com");
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        bool expected = true;

        // Act
        Categoria cat = ofertasHandler.CrearCategoria(admin, "Tareas del hogar");
        bool result = ofertasHandler.GetCategorias().Contains(cat);

        // Assert
        Assert.That(expected.Equals(result));

    }

    [Test]
    public void Caso2()
    // Como administrador, quiero poder dar de baja ofertas de servicios, avisando al oferente para que de esa forma,
    // pueda evitar ofertas inadecuadas.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("root", "toor", "1234", "abc@abc.com");
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        ofertasHandler.CrearCategoria(admin, "Tareas");
        Trabajador elpepe = registryHandler.RegistrarTrabajador("Pepe", "Pepe", "Elpepe", "elpepe", "2020,10,1", "12345678", "1234",
            "elpepe@elpepe.elpepe", new Tuple<double, double>(31,9393));
        OfertaDeServicio oferta = ofertasHandler.Ofertar("Tareas", elpepe, "un capo", "limpiador", 10);
        int id = oferta.GetId();
        bool expected = false;
        
        // Act
        ofertasHandler.DarDeBajaOferta(admin,id);
        bool result = ofertasHandler.GetOfertaById(id).IsActive();

        // Assert
        Assert.That(expected.Equals(result));

    }

    [Test]
    public void Caso3()
    // Como trabajador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto
    // para que de esa forma, pueda proveer información de contacto a quienes quieran contratar mis servicios.
    { 
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        bool Expected = true;
        
        // Act
        Usuario miUsuario = registryHandler.RegistrarTrabajador("Manolo","Manolete", "manoler","1234",
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
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        Usuario miUsuario = registryHandler.RegistrarTrabajador("Manolo","Manolete", "manoler","1234",
            "2001 3 14","1234567","099555555",
            "manoloreal@gmail.com",new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador admin = registryHandler.RegistrarAdministrador("elpepeAdmin", "1234", "1234", "god@dog.com");
        ofertasHandler.CrearCategoria(admin,"Tareas del hogar");

        // Act
        OfertaDeServicio oferta = ofertasHandler.Ofertar("Tareas del hogar",(Trabajador)miUsuario,"El mejor limpiador de Salto","Limpiador",9000);
        OfertaDeServicio expected = oferta;
        OfertaDeServicio result = ofertasHandler.GetOfertaById(oferta.GetId());
        
        // Assert
        Assert.That(expected.Equals(result));

    }

    [Test]
    public void Caso5()
    // Como empleador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto
    // para que de esa forma, pueda proveer información de contacto a los trabajadores que quiero contratar.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        bool expected = true;
        
        // Act
        Usuario miUsuario = registryHandler.RegistrarEmpleador("Señor Manolo","Manolete", "BigManoler","1234",
            "1970 3 14","1234567","099555555",
            "mistermanoloreal@gmail.com",new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        bool result = (miUsuario is Empleador);
        
        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    public void Caso6()
    // Como empleador, quiero buscar ofertas de trabajo, opcionalmente filtrando por categoría para que de esa forma,
    // pueda contratar un servicio.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        bool expected = true;

        // Act
        Administrador adm= registryHandler.RegistrarAdministrador("admin", "toor", "1234", "a@a.a");
        Categoria cat = ofertasHandler.CrearCategoria(adm, "categoria");
        bool result = true;

        // Assert
          Assert.That(result.Equals(expected));
    }

    [Test]
    public void Caso7()
    // Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma ascendente de
    // distancia a mi ubicación, es decir, las más cercanas primero para que de esa forma, pueda poder contratar un
    // servicio.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        bool result = true;
        // Act
    
        OfertaDeServicio ofertaDeServicio = ofertaDeServicio(ubicacion);
        bool ubicación = OfertaDeServicio.Ubicación;

        // Assert
          Assert.That(result.Equals(ubicación));
    }

    [Test]
    public void Caso8()
    // Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma descendente por
    // reputación, es decir, las de mejor reputación primero para que de esa forma, pueda contratar un servicio.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("admin", "toor", "1234", "a@a.a");
        bool result = true;
        
        // Act
        Categoria cat = ofertasHandler.CrearCategoria(admin, "categoria");
        
        // Assert
          Assert.That(result.Equals(expected));
    }

    [Test]
    public void Caso9()
    // Como empleador, quiero poder contactar a un trabajador para que de esa forma pueda, contratar una oferta de
    // servicio determinada.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        ContratoHandler contratoHandler = ContratoHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("admin", "toor", "1234", "a@a.a");
        Categoria cat = ofertasHandler.CrearCategoria(admin, "categoria");
        Trabajador pepe = registryHandler.RegistrarTrabajador("a", "a", "a", "a", "2020,2,2", "1234556", "12345", "a@a.a",
            new Tuple<double, double>(1, 1)); // TODO cambiar el sistema de categorias para que funcione con id en vez de descripcion
        OfertaDeServicio oferta = ofertasHandler.Ofertar("categoria", pepe ,"soy pro", "gamer", 10);
        Empleador mrbossman = registryHandler.RegistrarEmpleador("mr", "bossman", "eljefe", "lospoios", "2010,10,10",
            "1234567", "1234", "gus@lospoiosermanos.com", new Tuple<double, double>(10, 10));
        OfertaDeServicio expected = oferta;
        
        // Act
        contratoHandler.SolicitarTrabajador(oferta,mrbossman);
        OfertaDeServicio result = contratoHandler.GetSolicitudes(pepe).FirstOrDefault().Oferta;

        // Assert
        Assert.That(expected.Equals(result));

    }

    [Test]
    public void Caso10()
    // Como trabajador, quiero poder calificar a un empleador; el empleador me tiene que calificar a mí también, si no
    // me califica en un mes, la calificación será neutral, para que de esa forma pueda definir la reputación de mi
    // empleador.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        bool result = true;
        
        // Act
        
        
        // Assert
          Assert.That(result.Equals(expected));
    }

    [Test]
    public void Caso11()
    // Como empleador, quiero poder calificar a un trabajador; el trabajador me tiene que calificar a mí también, si no
    // me califica en un mes, la calificación será neutral, para que de esa forma, pueda definir la reputación del
    // trabajador.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("admin", "toor", "1234", "a@a.a");
        Calificacion
        // Act
        
        
        // Assert
          Assert.That(result.Equals(expected));
    }

    [Test]
    public void Caso12()
    // Como trabajador, quiero poder saber la reputación de un empleador que me contacte para que de esa forma, poder
    // decidir sobre su solicitud de contratación.
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        
        
        // Act
        
        
        // Assert
          Assert.That(result.Equals(expected));
    }
}