namespace LibraryTests;
using Library;
using Library.Excepciones;

/// <summary> Tests de la clase <see cref="CategoriaCatalog"> </summary>
public class CategoriaCatalogTests {
    [SetUp]
    public void SetUp() {}

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
        CategoriasCatalog.Wipe(root);
    }

    [Test]
    /// <summary> Test de que si se crea una nueva instancia de <see cref="CategoriasCatalog"/> 
    /// es en realidad igual a la que ya estaba creada. </summary>
    public void CategoriasCatalogSingletonTest() {
        // Arrange
        // Se crean nuevas instancias de catálogo de Categoriases (c1) y (c2)
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        CategoriasCatalog c2 = CategoriasCatalog.GetInstance();
        bool expected = true; // Valor esperado al comparar dos instancias de los catálogos de Categoriases

        // Act
        var result = (c1 == c2); // Se compara si el objeto c1 es igual a c2

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test de que el método Wipe() borra la instancia. </summary>
    public void WipearCategoriasCatalogTest() {
        // Arrange
        // Se crean nuevas instancias de catálogo de Categoriases(c1) y administrador (a1)
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        Administrador a1 = new Administrador("nick", "contraseña", "telefono", "correo");
        bool expected = false; // Valor esperado al comparar dos instancias de los catálogos de Categoriases

        // Act 
        CategoriasCatalog.Wipe(a1);
        CategoriasCatalog c2 = CategoriasCatalog.GetInstance(); // Se crea acá una instancia posterior al Wipe()
                                                                // para verificar que la instancia fue eliminada.  
        bool result = (c1 == c2);                               // Se compara si el objeto c1 es igual a c2

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un admin agrega una categoría </summary>
    public void AdminAgregarCategoriaTest() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        Administrador a1 = new Administrador("n", "con", "tel", "corr");
        Categoria c2 = c1.AddCategoria(a1, "desc");
        bool expected = true;

        // Act
        bool result = c1.GetCategorias().Contains(c2);

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un usuario no admin agrega una categoría </summary>
    public void NoAdminAgregarCategoriaTest() {
        // Assert
        Assert.Throws<ElevacionException>(ErrorAgregarCategoria);
    }

    public void ErrorAgregarCategoria() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Categoria c2 = c1.AddCategoria(e1, "desc");

        // Act
        Categoria c3 = c1.AddCategoria(e1, "desc");
    }

    [Test]
    /// <summary> Test para cuando se quiere obtener una oferta por su valor de id </summary>
    public void ObtenerOfertaPorIdTest() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        OfertasHandler o = OfertasHandler.GetInstance();
        Empleador e1 = new Empleador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador a1 = new Administrador("n", "con", "tel", "corr");
        Categoria c2 = c1.AddCategoria(a1, "cortar el pasto a domicilio");
        OfertaDeServicio o1 = o.Ofertar(c2.GetId(), t1, "cortar pasto", "empleo", 100);
        int id = 1; // Valor de id generado en el constructor de OfertaDeServicio
        OfertaDeServicio expected = o1;

        // Act
        OfertaDeServicio result = c1.GetOfertaById(id); 

        // Assert
        Assert.That(result.Equals(expected)); 
    }

    [Test]
    /// <summary> Test para cuando se quiere obtener una categoría por su descripción </summary>
    public void ObtenerCategoriaPorDescripcionTest() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        Administrador a1 = new Administrador("n", "con", "tel", "corr");
        Categoria c2 = c1.AddCategoria(a1, "cortar el pasto a domicilio");
        Categoria expected = c2;

        // Act
        Categoria result = c1.GetCategoria("cortar el pasto a domicilio");

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando se elimina una categoría </summary>
    public void AdminEliminarCategoriaTest() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        Administrador a1 = new Administrador("n", "con", "tel", "corr");
        Categoria c2 = c1.AddCategoria(a1, "cortar el pasto a domicilio");
        bool expected = false;

        // Act
        c1.RemoveCategoria(a1, c2);
        bool result = c2.IsActive();

        // Assert
        Assert.That(result.Equals(expected));
    }

    [Test]
    /// <summary> Test para cuando un no admin quiere eliminar una categoría </summary>
    public void NoAdminEliminarCategoriaTest() {
        // Assert
        Assert.Throws<ElevacionException>(ErrorEliminarCategoria);
    }
    
    public void ErrorEliminarCategoria() {
        // Arrange
        CategoriasCatalog c1 = CategoriasCatalog.GetInstance();
        Administrador a1 = new Administrador("n", "con", "tel", "corr");
        Trabajador t1 = new Trabajador("nom", "ape", "nick", "con", DateTime.Now, "ced", "tel", "corr", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Categoria c2 = c1.AddCategoria(a1, "cortar el pasto a domicilio");

        // Act
        c1.RemoveCategoria(t1, c2);
    }
}
