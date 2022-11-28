namespace LibraryTests;
using Library;

/// <summary> Tests de la clase <see cref="Administrador"> </summary>
public class AdministradorTests {
    [SetUp]
    public void Setup() {}

    [Test]
    /// <summary> Test para verificar que todos los datos del <see cref="Administrador"> hayan sido ingresados </summary>
    public void ConstructorCompleto() {
        // Arrange
        Administrador a1 = new Administrador("nick", "123", "099", "abc@gmail.com");
        string expectedN = "nick";
        string expectedC = "123";
        string expectedT = "099";
        string expectedCo = "abc@gmail.com";
        TipoDeUsuario expectedTi = TipoDeUsuario.Administrador;
        bool expectedA = true;

        // Act
        string resultN = a1.Nick;
        string resultC = a1.Contraseña;
        string resultT = a1.Telefono;
        string resultCo = a1.Correo;
        TipoDeUsuario resultTi = a1.GetTipo();
        bool resultA = a1.IsActive();
        
        // Assert
        Assert.That(expectedN.Equals(a1.Nick));
        Assert.That(expectedC.Equals(a1.Contraseña));
        Assert.That(expectedT.Equals(a1.Telefono));
        Assert.That(expectedCo.Equals(a1.Correo));
        Assert.That(expectedTi.Equals(a1.GetTipo()));
        Assert.That(expectedA.Equals(a1.IsActive()));
    }

    [Test]
    /// <summary> Test para verificar que si no se ingresa el nickname se lee la excepción </summary>
    public void NoNickname () {
        // Assert
        Assert.Catch<Exception>(NullNickname);
    }

    public void NullNickname() {
        // Arrange
        Administrador a1 = new Administrador("", "123", "099", "abc@gmail.com");
    }

    [Test]
    /// <summary> Test para verificar que si no se ingresa una contraseña se lee la excepción </summary>
    public void NoContraseña () {
        // Assert
        Assert.Catch<Exception>(NullContraseña);
    }

    public void NullContraseña() {
        // Arrange
        Administrador a1 = new Administrador("nick", "", "099", "abc@gmail.com");
    }

    [Test]
    /// <summary> Test para verificar que si no se ingresa un número de teléfono se lee la excepción </summary>
    public void NoTelefono () {
        // Assert
        Assert.Catch<Exception>(NullTelefono);
    }

    public void NullTelefono() {
        // Arrange
        Administrador a1 = new Administrador("nick", "123", "", "abc@gmail.com");
    }

    [Test]
    /// <summary> Test para verificar que si no se ingresa un correo se lee la excepción </summary>
    public void NoCorreo () {
        // Assert
        Assert.Catch<Exception>(NullCorreo);
    }

    public void NullCorreo() {
        // Arrange
        Administrador a1 = new Administrador("nick", "123", "099", "");
    }

    /* [Test]
    public void CorreoValido() {
        // Arrange
        Administrador a1 = new Administrador("nick", "123", "099", "abc@gmail.com");
        RegistryHandler r1 = RegistryHandler.GetInstance();

        // Act
        var v = r1.VerificarCorreo(a1.Correo);
        bool expected = true;

        // Assert
        Assert.That(expected.Equals(v));
    }

    [Test]
    public void CorreoSinArroba() {
        // Arrange
        Administrador a1 = new Administrador("nick", "123", "099", "abcgmail.com");
        RegistryHandler r1 = RegistryHandler.GetInstance();

        // Act
        var v = r1.VerificarCorreo(a1.Correo);
        bool expected = false;

        // Assert
        Assert.That(expected.Equals(v));
    } */
}
