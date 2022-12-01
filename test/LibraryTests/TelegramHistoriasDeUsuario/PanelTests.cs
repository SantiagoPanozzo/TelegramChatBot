using Telegram.Bot.Types;
using Library;
using Library.BotHandlers;

namespace LibraryTests.TelegramHistoriasDeUsuario;

public class PanelTests
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
    public void BorrarCategoria()
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("Admin", "toor", "473555555", "admin@dominio.com");
        PanelDeControlHandler panelDeControlHandler = new(null);
        CategoriasCatalog catalog = CategoriasCatalog.GetInstance();
        catalog.AddCategoria(admin, "blabla");
        User tuser = new User();
        tuser.Id = 123;
        Message mensaje1 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "admin";
        Message mensaje2 = new Message();
        mensaje2.From = tuser;
        mensaje2.Text = "Admin";
        Message mensaje3 = new Message();
        mensaje3.From = tuser;
        mensaje3.Text = "toor";
        Message mensaje4 = new Message();
        mensaje4.From = tuser;
        mensaje4.Text = "1";
        Message mensaje5 = new Message();
        mensaje5.From = tuser;
        mensaje5.Text = "1";
        Message mensaje6 = new Message();
        mensaje6.From = tuser;
        mensaje6.Text = "1";
        bool expected = false;
        
        // Act
        string response;
        panelDeControlHandler.Handle(mensaje1, out response);
        panelDeControlHandler.Handle(mensaje2, out response);
        panelDeControlHandler.Handle(mensaje3, out response);
        panelDeControlHandler.Handle(mensaje4, out response);
        panelDeControlHandler.Handle(mensaje5, out response);
        panelDeControlHandler.Handle(mensaje6, out response);
        bool result = catalog.GetCategoriaById(1).IsActive();
        
        // Assert
        Assert.That(expected.Equals(result));
    }
    
    [Test]
    public void CrearCategoria()
    {
        // Arrange
        RegistryHandler registryHandler = RegistryHandler.GetInstance();
        Administrador admin = registryHandler.RegistrarAdministrador("Admin", "toor", "473555555", "admin@dominio.com");
        PanelDeControlHandler panelDeControlHandler = new(null);
        CategoriasCatalog catalog = CategoriasCatalog.GetInstance();
        User tuser = new User();
        tuser.Id = 123;
        Message mensaje1 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "admin";
        Message mensaje2 = new Message();
        mensaje2.From = tuser;
        mensaje2.Text = "Admin";
        Message mensaje3 = new Message();
        mensaje3.From = tuser;
        mensaje3.Text = "toor";
        Message mensaje4 = new Message();
        mensaje4.From = tuser;
        mensaje4.Text = "1";
        Message mensaje5 = new Message();
        mensaje5.From = tuser;
        mensaje5.Text = "2";
        Message mensaje6 = new Message();
        mensaje6.From = tuser;
        mensaje6.Text = "categoria";
        bool expected = true;
        
        // Act
        string response;
        panelDeControlHandler.Handle(mensaje1, out response);
        panelDeControlHandler.Handle(mensaje2, out response);
        panelDeControlHandler.Handle(mensaje3, out response);
        panelDeControlHandler.Handle(mensaje4, out response);
        panelDeControlHandler.Handle(mensaje5, out response);
        panelDeControlHandler.Handle(mensaje6, out response);
        bool result = catalog.GetCategoriaById(1).Descripcion.Equals("categoria");
        
        // Assert
        Assert.That(expected.Equals(result));
    }
}
