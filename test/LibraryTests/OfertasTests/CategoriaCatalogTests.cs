namespace LibraryTests;
using Library;

/// <summary> Tests de la clase <see cref="CategoriaCatalog"> </summary>
public class CategoriaCatalogTests
{
    [SetUp]
    public void Setup() {}

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
    public void CategoriaCatalogSingletonTest() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        CategoriasCatalog c2 = CategoriasCatalog.GetInstance();
        bool expected = true;

        // Act
        var result = c1 == c2;

        // Assert
        Assert.That(result.Equals(expected));
    }
}
