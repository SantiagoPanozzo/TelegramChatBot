namespace LibraryTests;
using Library;

/// <summary> Tests de la clase <see cref="Categoria"> </summary>
public class CategoriaTests{
    [SetUp]
    public void Setup() {}

    [Test]
    public void  GetOfertaById() {

    
    //Arrange

    OfertasHandler ofertasHandler = OfertasHandler.GetInstance();
    RegistryHandler registryHandler = RegistryHandler.GetInstance();
    Administrador admin = registryHandler.RegistrarAdministrador("lolo", "1111","098268188","hello@gmail.com");
    Categoria categoria = ofertasHandler.CrearCategoria(admin, "categoria");
    Trabajador usuario = registryHandler.RegistrarTrabajador("Tito", "Pérez","toto", "mmm","01 02 2003", "36547821","47325698","ninguno@gmail.com",new Tuple<double, double>(2,3));
    OfertaDeServicio ofert = ofertasHandler.Ofertar(categoria.GetId(), usuario, "jardinero", "mantenimiento", 250);
    Categoria expected = categoria;

    // REVISAR CLASE CATEGORIA, TEMA WIPE (NO TIENE GET INSTANCE)
    // Act

    Categoria result = OfertaDeServicio.Wipe(admin);
    Categoria Cate = categoria.Get

     // Assert
    
    Assert.That(expected.Equals(result));

