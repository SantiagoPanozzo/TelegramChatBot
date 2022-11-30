using Library;
using Library.Excepciones;
using System.Linq;
using System;
namespace LibraryTests;

/// <summary> Tests de la clase <see cref="SolicitudCatalog">. </summary>
public class SolicitudCatalogTests {
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
    /// <summary> Test de que si se crea una nueva instancia de <see cref="SolicitudCatalog"/> 
    /// es en realidad igual a la que ya estaba creada. </summary>
    public void SolicitudCatalogSingletonTest() {
        // Arrange
        // Se crean nuevas instancias de catálogo de solicitudes (s1) y (s2)
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        SolicitudCatalog s2 = SolicitudCatalog.GetInstance();
        bool expected = true; // Valor esperado al comparar dos instancias de los catálogos de solicitudes

        // Act
        var result = (s1 == s2); // Se compara si el objeto s1 es igual a s2

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test de que el método Wipe() borra la instancia. </summary>
    public void WipearSolicitudCatalogTest() {
        // Arrange
        // Se crean nuevas instancias de catálogo de solicitudes(s1) y administrador (a1)
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        Administrador a1 = new Administrador("nick", "contraseña", "telefono", "correo");
        bool expected = false; // Valor esperado al comparar dos instancias de los catálogos de solicitudes

        // Act 
        SolicitudCatalog.Wipe(a1);
        SolicitudCatalog s2 = SolicitudCatalog.GetInstance(); // Se crea acá una instancia posterior al Wipe()
                                                              // para verificar que la instancia fue eliminada.  
        bool result = (s1 == s2);                             // Se compara si el objeto s1 es igual a s2

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test de que se agrega una solicitud al catálogo. </summary>
    public void AgregarSolicitudTest() {
        // Arrange
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();

        // Se crean nuevas instancias de: catálogo de solicitudes(s1), empleador (e1), trabajador (t1) y oferta de servicio (o1)
        // Son parámetros necesarios para poder crear una nueva solicitud
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        bool expected = true; // true porque se espera que el catálogo tenga la nueva solicitud

        // Act
        Solicitud solicitud = s1.AddSolicitud(o1, e1);     // Un empleador realiza una nueva solicitud
        bool result = s1.Solicitudes.Contains(solicitud);  // Se verifica que la nueva solicitud fue agregada

        //Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para eliminar una solicitud agregada al catálogo. </summary>
    public void EliminarSolicitudTest() {
        // Arrange
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();

        // Se crean nuevas instancias de: catálogo de solicitudes(s1), empleador (e1), trabajador (t1) y oferta de servicio (o1)
        // Son parámetros necesarios para poder crear una nueva solicitud
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        bool expected = false; // false porque se espera que en el catálogo no esté la solicitud realizada 

        // Act
        Solicitud solicitud = s1.AddSolicitud(o1, e1);     // Un empleador realiza una nueva solicitud
        s1.RemoveSolicitud(solicitud);                     // Se elimina la solicitud realizada
        bool result = s1.Solicitudes.Contains(solicitud);  // Se cuentan los elementos de la lista, se supone que en esta línea es 1

        //Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test de cuando se elimina una solicitud creada pero agregada al catálogo. </summary>
    public void EliminarSolicitudNoAgregadaTest() {
        //Assert
        Assert.Throws<AccionInnecesariaException>(ErrorEliminarSolicitud);
    }

    public void ErrorEliminarSolicitud() {
        // Arrange
        // Se crean nuevas instancias de: catálogo de solicitudes(s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1) y solicitud (s2)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s2 = new Solicitud(o1, e1);  

        // Act
        s1.RemoveSolicitud(s2); // Se elimina una solicitud no agregada
    }

    [Test]
    /// <summary> Test para cuando se quiere obtener una solicitud por un valor de id. </summary>
    public void ObtenerSolicitudPorIdTest() {
        // Arrange
        // Se crean nuevas instancias de: catálogo de solicitudes(s1), empleador (e1), trabajador (t1), 
        // oferta de servicio (o1) y solicitud (s2)
        // Son parámetros necesarios para poder crear una nueva solicitud
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        OfertaDeServicio o1 = new OfertaDeServicio(t1, "cortar el pasto a domicilio", "cortar pasto", 100.50);
        Solicitud s2 = s1.AddSolicitud(o1, e1);
        int id = 1;  // Valor de id generado en el constructor de la solicitud
        Solicitud expected = s2;

        // Act
        Solicitud result = s1.GetSolicitud(id);

        // Assert
        Assert.That(result.Equals(expected));
    }
}
