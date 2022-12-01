using Telegram.Bot.Types;
using Library;
using Library.BotHandlers;

namespace LibraryTests.TelegramHistoriasDeUsuario;

public class OfertarHandlerTests {
    [SetUp]
    public void Setup() {}
    
    [TearDown]
    public void Wipe() {
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
    public void TrabajadorOfertarTest() {
        // Arrange
        Trabajador trab = RegistryHandler.GetInstance().RegistrarTrabajador("nom", "ape", "nick", "con", "01 01 2001", "12121212", "tel", "correo@correo.com", new Tuple<double, double>(-30.423423, -31.2341231));
        User tuser = new User();
        tuser.Id = 1234;
        Message mensaje1 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "ofertar";
        Message mensaje2 = new Message();
        mensaje1.From = tuser;
        mensaje1.Text = "";
        Message mensaje3 = new Message();
        mensaje3.From = tuser;
        mensaje3.Text = "";
        Message mensaje4 = new Message();
        mensaje4.From = tuser;
        mensaje4.Text = "";
        Message mensaje5 = new Message();
        mensaje5.From = tuser;
        mensaje5.Text = "";
        Message mensaje6 = new Message();
        mensaje6.From = tuser;
        mensaje6.Text = "";
        Message mensaje7 = new Message();
    }
}