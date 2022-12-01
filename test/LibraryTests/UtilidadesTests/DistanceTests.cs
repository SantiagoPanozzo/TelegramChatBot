namespace LibraryTests;
using Library.DistanceMatrix;
using DotNetEnv;

public class DistanceTests
{
    [SetUp]
    public void Setup() {
        DotNetEnv.Env.TraversePath().Load();
    }

    [Test]
    public void TestLocation()
    {
        var instance = Distance.GetInstance();
        int dist = instance.Calculate("Salto Uruguay", "Montevideo Uruguay").GetAwaiter().GetResult();
        int expected = 493;
        Assert.That(dist, Is.EqualTo(expected));
    }
    [Test]
    public void TestLocationWithComma()
    {
        var instance = Distance.GetInstance();
        int expected = 493;
        int dist = instance.Calculate("Salto, Uruguay", "Montevideo, Uruguay").GetAwaiter().GetResult();
        Assert.That(dist, Is.EqualTo(expected));
    }
}
