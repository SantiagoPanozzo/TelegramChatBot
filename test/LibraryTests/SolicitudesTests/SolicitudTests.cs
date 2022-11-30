using Library;
using Library.Excepciones;
using System;
namespace LibraryTests;

/// <summary> Tests de la clase <see cref="Solicitud">. </summary>
public class SolicitudTests {
    [SetUp]
    public void Setup() {}

    [TearDown]
    /// <summary> Al terminar un test borra todas las instancias de singleton. </summary>
    public void Wipe() {
        Administrador root = new Administrador("ROOT", "TOOR", "34023", "aaa@bbb.ccc");
        Categoria.Wipe(root);
        SolicitudCatalog.Wipe(root);
        ContratoHandler.Wipe(root);
        OfertaDeServicio.Wipe(root);
        OfertasHandler.Wipe(root);
        RegistryHandler.Wipe(root);
        Solicitud.Wipe(root);
        SolicitudCatalog.Wipe(root);
        UsuariosCatalog.Wipe(root);
    }

    [Test]
    /// <summary> Test para obtener una solicitud por id. </summary>
    public void ObtenerIdSolicitudTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1) y solicitud (s2)
        // Son parámetros necesarios para poder crear una nueva solicitud
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = new Solicitud(o1, e1);
        int expected = 1; // Valor de id dado a una solicitud en el constructor

        // Act
        int result = s1.GetId();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para obtener el trabajador de una solicitud. </summary>
    public void ObtenerOfertanteDeUnaSolicitudTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1) y solicitud (s2)
        // Son parámetros necesarios para poder crear una nueva solicitud
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = new Solicitud(o1, e1);
        string expected = "nick";

        // Act
        string result = s1.GetEmpleador(); // El método GetEmpleador() retorna el nicknanme del trabajador (t1)
        
        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para obtener el la ubicación de una solicitud. </summary>
    public void ObtenerUbicacionDeUnaSolicitudTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1) y solicitud (s2)
        // Son parámetros necesarios para poder crear una nueva solicitud
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = new Solicitud(o1, e1);
        Tuple<double, double> expected = new Tuple<double, double>(-31.389425985682045, -57.959432913914476);

        // Act
        Tuple<double, double> result = s1.GetUbicacion(); // El método GetUbicacion() retorna en formato de tupla las coordenadas de la solicitud
        
        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para dar una solicitud como iniciada. </summary>
    public void IniciarTrabajoTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = false;

        // Act
        s1.RecibirRespuesta(Aceptacion.Aceptada); // Este método interiormente llama a IniciarTrabajo(), 
                                                  // que lo que hace es cambiar el estado de disponibilidad a false             
        bool result = s1.Oferta.Disponible;       // Valor booleano de la oferta luego de ser aceptada, se espera que sea false  

        // Assert
        Assert.That(result.Equals(expected));

    }

    [Test]
    /// <summary> Test para cuando un trabajador que no realizó una solicitud desea calificarla. </summary>
    public void CalificacionPorTrabajadorQueNoEsTest() {
        //Assert
        Assert.Throws<UsuarioIncorrectoException>(ErrorCalificacionTrabajador);
    }

    public void ErrorCalificacionEmpleador() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t2 = new Trabajador("no", "ap", "nickInvalido", "co", DateTime.Now, "ce", "te", "cor", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        
        // Act
        s1.CalificarEmpleador(t2, Calificacion.MuyBueno);
    }

    [Test]
    /// <summary> Test para cuando un empleador que no es parte de solicitud desea calificarla. </summary>
    public void CalificacionPorEmpleadorQueNoEsTest() {
        //Assert
        Assert.Throws<UsuarioIncorrectoException>(ErrorCalificacionTrabajador);
    }

    public void ErrorCalificacionTrabajador() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Empleador e2 = new Empleador("nom", "ape", "nickInvalido", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        
        // Act
        s1.CalificarTrabajador(e2, Calificacion.MuyBueno);
    }

    [Test]
    /// <summary> Test para obtener la calificación de un empleador. </summary>
    public void ObtenerCalificacionEmpleadorTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        Calificacion expected = Calificacion.MuyBueno; // Valor del enum Calificación, es equivalente a int = 4;

        // Act
        s1.CalificarEmpleador(t1, Calificacion.MuyBueno);
        Calificacion result = s1.GetEmpleadorRate();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para obtener la calificación de un trabajador. </summary>
    public void ObtenerCalificacionTrabajadorTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        Calificacion expected = Calificacion.Bueno; // Valor del enum Calificación, es equivalente a int = 3;

        // Act
        s1.CalificarTrabajador(e1, Calificacion.Bueno);
        Calificacion result = s1.GetTrabajadorRate();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para verificar que un empleador queda calificado. </summary>
    public void EmpleadorYaCalificadoTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = true; // Valor booleano esperado luego de calificar una oferta

        // Act
        s1.CalificarEmpleador(t1, Calificacion.MuyBueno);
        bool result = s1.IsEmpleadorRated();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para verificar que una oferta quede calificada. </summary>
    public void OfertaYaCalificadaTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = true; // Valor booleano esperado luego de calificar una oferta

        // Act
        s1.CalificarTrabajador(e1, Calificacion.MuyBueno);
        bool result = s1.Oferta.IsRated();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando se quiere calificar un trabajador ya calificado </summary>
    public void CalificarTrabajadorYaCalificadoTest() {
        // Assert
        Assert.Throws<YaCalificadoException>(ErrorDobleCalificacionTrabajador);
    }

    public void ErrorDobleCalificacionTrabajador() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        
        // Act
        s1.CalificarTrabajador(e1, Calificacion.MuyBueno);
        s1.CalificarTrabajador(e1, Calificacion.Sobresaliente);
    }

    [Test]
    /// <summary> Test para cuando se quiere calificar un empleador ya calificado </summary>
    public void CalificarEmpleadorYaCalificadoTest() {
        Assert.Throws<YaCalificadoException>(ErrorDobleCalificacionEmpleador);
    }

    public void ErrorDobleCalificacionEmpleador() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        
        // Act
        s1.CalificarEmpleador(t1, Calificacion.Deficiente);
        Calificacion a = s1.GetEmpleadorRate();
        s1.CalificarEmpleador(t1, Calificacion.Bueno);
    }

    [Test]
    /// <summary> Test para cuando se da de baja una solicitud </summary>
    public void AdminDarDeBajaUsuarioTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = false;

        // Act
        s1.DarDeBaja(a1); 
        bool result = s1.IsActive();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un administrador reactiva una solicitud </summary>
    public void AdminReactivarUsuarioTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = true;

        // Act
        s1.DarDeBaja(a1); // Acá el valor para IsActive() es false, se ve en AdminDarDeBajaUsuarioTest()
        s1.Reactivar(a1);
        bool result = s1.IsActive();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un trabajador puede ser autocalificado con la calificación neutra </summary>
    /// <remarks> Updater.FastForward() simula una fecha que no es para la actualización de la clase,
    /// para que pueda ser autocalificado deben pasar 30 días, y en este caso se adelantan 30 días.
    /// Es por eso que el trabajador puede ser autocalificado </remarks>
    public void AutocalificacionTrabajadorTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = true;

        // Act
        s1.CalificarEmpleador(t1, Calificacion.Regular);
        Updater.FastForward(new TimeSpan(days: 31, 0, 0, 0));
        bool result = s1.Oferta.IsRated();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un empleador puede ser autocalificado con la calificación neutra </summary>
    /// <remarks> Updater.FastForward() simula una fecha que no es para la actualización de la clase,
    /// para que pueda ser autocalificado deben pasar 30 días, y en este caso se adelantan 30 días.
    /// Es por eso que el empleador puede ser autocalificado </remarks>
    public void AutocalificacionEmpleadorTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        bool expected = true;

        // Act
        s1.CalificarEmpleador(t1, Calificacion.Regular);
        Updater.FastForward(new TimeSpan(days: 31, 0, 0, 0));
        bool result = s1.IsRated();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un empleador no puede ser autocalificado con la calificación neutra </summary>
    /// <remarks> Updater.FastForward() simula una fecha que no es para la actualización de la clase,
    /// para que pueda ser autocalificado deben pasar 30 días, y en este caso se adelantan 15 días.
    /// Es por eso que el empleador no puede ser autocalificado </remarks>
    public void NoSePuedeAutocalificacionEmpleadorTest() {

        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Updater.Update();
        bool expected = true;

        // Act
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        Updater.FastForward(new TimeSpan(days: 31, 0, 0, 0));
        bool result = s1.Oferta.IsRated();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para conocer la calificación de un empleador autocalificado </summary>
    public void ValorAutocalificacionEmpleadorTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        Calificacion expected = Calificacion.Bueno;

        // Act
        Updater.FastForward(new TimeSpan(days: 31, 0, 0, 0));
        Calificacion result = s1.GetEmpleadorRate();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para conocer la calificación de un empleador autocalificado </summary>
    public void ValorAutocalificacionTrabajadorTest() {
        // Arrange
        // Se crean nuevas instancias de: solicitud (s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1), solicitud (s2) y catálogo de solicitudes (sc)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog sc = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("nick", "con", "tel", "corr");
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s1 = sc.AddSolicitud(o1, e1);
        Calificacion expected = Calificacion.Bueno;

        // Act
        Updater.FastForward(new TimeSpan(days: 31, 0, 0, 0));
        Calificacion result = s1.Oferta.GetCalificacion();

        // Assert
        Assert.That(result.Equals(expected));
    }
}
