namespace LibraryTests;
using Library;

/// <summary> Tests de la clase <see cref="OfertaDeServicio"> </summary>
public class OfertaDeServicioTests
{
    [SetUp]
    public void Setup() { }
    
    [TearDown]
    public void Wipe()
    {
        Administrador root = new Administrador("", "", "", "");
        Categoria.Wipe(root);
        CategoriasCatalog.Wipe(root);
        ContratoHandler.Wipe(root);
        OfertaDeServicio.Wipe(root);
        OfertasHandler.Wipe(root);
        RegistryHandler.Wipe(root);
        Solicitud.Wipe(root);
        SolicitudCatalog.Wipe(root);
        UsuariosCatalog.Wipe(root);
    }

    [Test]
    public void DarDeBajaOfertaActiva()
    // Test de dar de baja una oferta
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("", "", "", "", DateTime.Now, "", "", "", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("", "", "", "");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "", "", 10);
        bool expected = false;
        
        // Act
        ofertaDeServicio.DarDeBaja(administrador);
        bool result = ofertaDeServicio.IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
    [Test]
    public void DarDeBajaOfertaDesactivada()
    // Test de dar de baja una oferta que ya ha sido desactivada
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("", "", "", "", DateTime.Now, "", "", "", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("", "", "", "");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "", "", 10);
        ofertaDeServicio.DarDeBaja(administrador);
        bool expected = false;
        
        // Act
        ofertaDeServicio.DarDeBaja(administrador);
        bool result = ofertaDeServicio.IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
    [Test]
    public void ReactivarOfertaDesactivada()
    // Test de volver a activar una oferta que fue desactivada
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("", "", "", "", DateTime.Now, "", "", "", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("", "", "", "");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "", "", 10);
        ofertaDeServicio.DarDeBaja(administrador);
        bool expected = true;
        
        // Act
        ofertaDeServicio.Reactivar(administrador);
        bool result = ofertaDeServicio.IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
    [Test]
    public void ReactivarOfertaActiva()
    // Test de reactivar una oferta ya está activa
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("", "", "", "", DateTime.Now, "", "", "", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("", "", "", "");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "", "", 10);
        bool expected = true;
        
        // Act
        ofertaDeServicio.Reactivar(administrador);
        bool result = ofertaDeServicio.IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
}
