namespace LibraryTests;
using Library;

/// <summary> Tests de la clase <see cref="SolicitudCatalog"> </summary>
public class SolicitudCatalogTests {
    [SetUp]
    public void Setup() {}

    [TearDown]
    /// <summary> Al terminar un test borra todas las instancias de singleton </summary>
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
    /// <summary> Verificación de que si se crea una nueva instancia de <see cref="SolicitudCatalog"/> 
    /// es en realidad igual a la que ya estaba creada. </summary>
    public void CategoriaCatalogSingletonTest() {
        // Arrange
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        SolicitudCatalog s2 = SolicitudCatalog.GetInstance();
        bool expected = true;

        // Act
        var result = (s1 == s2);

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Verificación de que el método Wipe() borra la instancia </summary>
    public void WipearCategoriaCatalog() {
        // Arrange
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        Administrador a1 = new Administrador("nick", "contraseña", "telefono", "correo");
        bool expected = false;

        // Act 
        SolicitudCatalog.Wipe(a1);
        SolicitudCatalog s2 = SolicitudCatalog.GetInstance(); // Se crea acá una instancia posterior al Wipe()
                                                              // para verificar que la instancia fue eliminada.  
        bool result = (s1 == s2);

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    public void AgregarSolicitud() {
        SolicitudCatalog s1 = SolicitudCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", Tuple<30, 30>);
    }
}
