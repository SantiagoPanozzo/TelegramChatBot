using Library;

namespace LibraryTests;

public class UpdaterTests
{
    /*
    [Test]
    public void CheckWorking()
    {
        // Arrange
        Updater.EnableAutoUpdate();
        // Console.ReadKey();
        Assert.Pass();
    }
*/
    
    /// <summary>
    /// Test de que la <see cref="Updater.FechaActual"/> esté al día con la fecha real (apreciacion de 1 segundo).
    /// </summary>
    [Test]
    public void FechaActualCheck()
    {
        // Arrange
        Updater.Update();
        DateTime ahora = DateTime.Now;
        bool expected = true;

        // Act
        // Comparamos la fecha del año, horas, minutos y segundos del programa con los de la fecha real.
        bool result = (Updater.FechaActual.Date.Equals(ahora.Date)) && (Updater.FechaActual.Hour.Equals(ahora.Hour)) &&
                      (Updater.FechaActual.Minute.Equals(ahora.Minute)) && (Updater.FechaActual.Second.Equals(ahora.Second));
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
    /// <summary>
    /// Test de que el método <see cref="Updater.FakeUpdate"/> establezca en el reloj del programa la fecha pasada
    /// como parámetro.
    /// </summary>
    [Test]
    public void FakeUpdate()
    {
        // Arrange
        Updater.Update();
        DateTime fecha = new DateTime(2025, 10, 31);
        DateTime expected = fecha;

        // Act
        Updater.FakeUpdate(fecha);
        DateTime result = Updater.FechaActual;
        
        // Assert
        Assert.That(expected.Equals(result));
    }

    /// <summary>
    /// Test de que el método <see cref="Updater.FastForward"/> avance la <see cref="Updater.FechaActual"/> la cantidad
    /// de tiempo pasada como parámetro.
    /// </summary>
    [Test]
    public void FastForwardFechaActual()
    {
        // Arrange
        Updater.Update();
        TimeSpan aumento = new TimeSpan(10, 0, 0);
        DateTime expected = Updater.FechaActual.Add(aumento);
        
        // Act
        Updater.FastForward(aumento);
        DateTime result = Updater.FechaActual;
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
}