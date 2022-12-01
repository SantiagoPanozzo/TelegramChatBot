namespace LibraryTests;

using Library;
using Library.DistanceMatrix;
using DotNetEnv;

public class GeocodeTests
{
    [SetUp]
    public void Setup() {
        DotNetEnv.Env.TraversePath().Load();
    }

    [Test]
    public void TestLocation()
    {
        var expected = new Tuple<double, double>(24, 23);
        Tuple<double, double> result = Geocode.Process("Av. Esteban Gautrón 1287");
    }

}
