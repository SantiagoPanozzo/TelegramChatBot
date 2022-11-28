using Library.Excepciones;

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
        Administrador root = new Administrador("ROOT", "TOOR", "34023", "aaa@bbb.ccc");
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
            new Trabajador("aaaa", "aaa", "aslkfj", "1iwj", DateTime.Now, "52342343", "545646", "uncorreo@real.com", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("admin", "toor", "1234234", "aaa@bbb.ccc");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "trt", "sds", 10);
        bool expected = false;
        
        // Act
        ofertaDeServicio.DarDeBaja(administrador);
        bool result = ofertaDeServicio.IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
    private void DarDeBajaOfertaDesactivadaError()
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("estoy", "cansado", "deinventar", "cosas", DateTime.Now, "12344235", "123124", "correo@real.xfa", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("aaaa", "wefw", "243234", "uncorreo@valido.com");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "es", "untrabajo", 10);
        ofertaDeServicio.DarDeBaja(administrador);
        bool expected = false;
        
        // Act
        ofertaDeServicio.DarDeBaja(administrador);
        bool result = ofertaDeServicio.IsActive();
    }
    
    [Test]
    public void DarDeBajaOfertaDesactivada()
    // Test de dar de baja una oferta que ya ha sido desactivada
    {
        // Assert
        Assert.Throws<AccionInnecesariaException>(DarDeBajaOfertaDesactivadaError);
    }

    [Test]
    public void ReactivarOfertaDesactivada()
    // Test de volver a activar una oferta que fue desactivada
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("as", "bs", "cs", "ds", DateTime.Now, "1231243", "2342342", "aaa@bb.cc", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("admin", "nimda", "123124", "repro@el.adm");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "ofertado", "genial", 10);
        ofertaDeServicio.DarDeBaja(administrador);
        bool expected = true;
        
        // Act
        ofertaDeServicio.Reactivar(administrador);
        bool result = ofertaDeServicio.IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }

    private void ReactivarOfertaActivaErorr()
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("nopuede", "ser", "otro", "mas", DateTime.Now, "12356775", "1231231", "adasd@asd.ca", new Tuple<double, double>(10, 10));
        Administrador administrador = new Administrador("otroadmin", "si", "12314113", "queseael@ultimo.com");
        OfertaDeServicio ofertaDeServicio = new OfertaDeServicio(trabajador, "a", "b", 10);
        bool expected = true;
        
        // Act
        ofertaDeServicio.Reactivar(administrador);
        bool result = ofertaDeServicio.IsActive();
    }
    [Test]
    public void ReactivarOfertaActiva()
    // Test de reactivar una oferta ya está activa
    {
        // Assert
        Assert.Throws<AccionInnecesariaException>(ReactivarOfertaActivaErorr);
    }

    [Test]
    public void IdsCoinciden()
    // Test de que los Ids vayan aumentando de manera que sean todos únicos para cada oferta
    {
        // Arrange
        Trabajador trabajador =
            new Trabajador("noera", "el", "ultimo", "aaa", DateTime.Now, "1231242", "213212", "si@dale.com", new Tuple<double, double>(10, 10));
        int expected = 4;       
        
        // Act
        OfertaDeServicio oferta1 = new OfertaDeServicio(trabajador, "of", "se", 10);
        OfertaDeServicio oferta2 = new OfertaDeServicio(trabajador, "er", "rv", 10);
        OfertaDeServicio oferta3 = new OfertaDeServicio(trabajador, "ta", "ic", 10);
        OfertaDeServicio oferta4 = new OfertaDeServicio(trabajador, "de", "io", 10);
        int result = oferta4.GetId();

        // Assert
        Assert.That(expected.Equals(result));
    }
    
}
