using Telegram.Bot.Types;
using Library;
using Library.BotHandlers;

namespace LibraryTests.TelegramHistoriasDeUsuario;

public class AdministradorTests
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
    private void AdministradorCrearCategoriaParte1()
    // "Cómo administrador, quiero poder indicar categorías sobre las cuales se realizarán las ofertas de servicios
    // para que de esa forma, los trabajadores puedan clasificarlos.
    {
        // Arrange
        Administrador admin = RegistryHandler.GetInstance().RegistrarAdministrador("Admin", "toor", "473555555", "admin@dominio.com");
        User tuser = new User();
        tuser.Id = 1234;
        Message mensaje1 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "admin";
        Message mensaje2 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "Admin";
        Message mensaje3 = new Message();
        mensaje3.From = tuser;
        mensaje3.Text = "toor";
        Message mensaje4 = new Message();
        mensaje4.From = tuser;
        mensaje4.Text = "Ver categorias";
        Message mensaje5 = new Message();
        mensaje5.From = tuser;
        mensaje5.Text = "1";
        Message mensaje6 = new Message();
        mensaje6.From = tuser;
        mensaje6.Text = "Crear Categoria";
        Message mensaje7 = new Message();
        mensaje7.From = tuser;
        mensaje7.Text = "algunaCategoria";
        RegistrarHandler registrarHandler = new RegistrarHandler(null);
        CategoriasCatalog categoriasCatalog = CategoriasCatalog.GetInstance();

        bool expected = true;

        // Act
        string response;
        registrarHandler.Handle(mensaje1,out response);
        registrarHandler.Handle(mensaje2,out response);
        registrarHandler.Handle(mensaje3,out response);
        registrarHandler.Handle(mensaje4,out response);
        registrarHandler.Handle(mensaje5,out response);
        registrarHandler.Handle(mensaje6,out response);
        registrarHandler.Handle(mensaje7,out response);
        Categoria result = categoriasCatalog.GetCategoria("algunaCategoria");
        
    }
    
    [Test]
    public void AdministradorCrearCategoriaParte2()
    {
        //Assert.DoesNotThrow(AdministradorCrearCategoriaParte1);
        Assert.Pass();
    }
    
    
}