using Library;

namespace LibraryTests;

public class UpdaterTests
{
    [Test]
    public void CheckWorking()
    {
        // Arrange
        Updater.EnableAutoUpdate();
        Console.ReadKey();
    }
}