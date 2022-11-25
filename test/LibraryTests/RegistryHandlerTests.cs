namespace LibraryTests;
using Library;

/// <summary> Tests de la clase <see cref="RegistryHandler"> </summary>
public class RegistryHandlerTests
{
    [SetUp]
    public void Setup() {}

    [Test]
    public void ValidCorreo() {
        RegistryHandler registryHandler = RegistryHandler.GetInstance();

        var result = registryHandler.VerificarCorreo("someone@somewhere.com");

        bool expected = true; 

        Assert.That(expected.Equals(result));
    }

    [Test]
    public void InvalidCorreo() {
        RegistryHandler registryHandler = RegistryHandler.GetInstance();

        var result = registryHandler.VerificarCorreo("fdsa@fdsa.");

        bool expected = false; 

        Assert.That(expected.Equals(result));
    }
}
