namespace LibraryTests;
using Library.DistanceMatrix;
using DotNetEnv;

public class DistanceTests
{
    [SetUp]
    public void Setup() {}

    [Test]
    public void TestLocation()
    {
        DotNetEnv.Env.TraversePath().Load();            // No me gusta tenerlo por acá tho, quizá wrappearlo en una clase? (por alguna razón no funcionaba tho?)
        var instance = Distance.GetInstance();
        int dist = instance.Calculate("Salto Uruguay", "Montevideo Uruguay").GetAwaiter().GetResult();
        int expected = 493;
        Assert.That(dist, Is.EqualTo(expected));
    }
    [Test]
    public void TestLocationWithComma()
    {
        DotNetEnv.Env.TraversePath().Load();
        var instance = Distance.GetInstance();
        int expected = 493;
        int dist = instance.Calculate("Salto, Uruguay", "Montevideo, Uruguay").GetAwaiter().GetResult();
        Assert.That(dist, Is.EqualTo(expected));
    }
}
