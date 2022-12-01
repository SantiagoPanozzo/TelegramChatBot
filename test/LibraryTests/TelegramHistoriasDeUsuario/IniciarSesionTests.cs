using Telegram.Bot.Types;
using Library;
using Library.BotHandlers;

namespace LibraryTests.TelegramHistoriasDeUsuario;

public class IniciarSesionTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [TearDown]
    public void Wipe()
    {
        Administrador root = new Administrador("root", "toor", "0130123", "abc@acb.cba");
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
    public void IniciarSesionUsuarioTest()
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        Empleador empleador = registryHandler.RegistrarEmpleador("ENombre", "EApellido", "ENick", "EPass", "1970 1 1",
            "1234567",
            "473555555", "empleador@dominio.com", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        IniciarSesionHandler iniciarSesionHandler = new(null);
        User tuser = new User();
        tuser.Id = 12345;
        Message mensaje1 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "Iniciar sesion";
        Message mensaje2 = new Message();
        mensaje2.From = tuser;
        mensaje2.Text = "ENick";
        Message mensaje3 = new Message();
        mensaje3.From = tuser;
        mensaje3.Text = "EPass";
        bool expected = true;

        // Act
        string response;
        iniciarSesionHandler.Handle(mensaje1, out response);
        iniciarSesionHandler.Handle(mensaje2, out response);
        iniciarSesionHandler.Handle(mensaje3, out response);
        bool result = HandlerHandler.CachedLogins[12345].Equals(empleador);

        // Assert
        Assert.That(expected.Equals(result));
    }
}