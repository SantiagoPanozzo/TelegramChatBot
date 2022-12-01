namespace LibraryTests;
using Library;
using DotNetEnv;

public class AddressValidationTests
{
    [SetUp]
    public void Setup() {
        DotNetEnv.Env.TraversePath().Load();
    }

    [Test]
    public void TestLocation()
    {
        var expected = true;
        var actual = AddressValidation.Process("Artigas 1251, Salto, Salto");
        Assert.That(expected, Is.EqualTo(actual));
    }
}
