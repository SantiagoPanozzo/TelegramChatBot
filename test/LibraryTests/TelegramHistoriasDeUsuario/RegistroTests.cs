using Library;
using Library.BotHandlers;
using Telegram.Bot.Types;

namespace LibraryTests.TelegramHistoriasDeUsuario;

public class RegistroTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RegistrarTrabajador()
    {
        // Como trabajador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto
        // para que de esa forma, pueda proveer información de contacto a quienes quieran contratar mis servicios.
        { 
            // Arrange
            User tuser = new User();
            tuser.Id = 1234;
            Message mensaje1 = new Message();
            mensaje1.From = tuser;
            mensaje1.Text = "Registrar";
            Message mensaje2 = new Message();
            mensaje2.From = tuser;
            mensaje2.Text = "1";
            Message mensaje3 = new Message();
            mensaje3.From = tuser;
            mensaje3.Text = "Pepe";
            Message mensaje4 = new Message();
            mensaje4.From = tuser;
            mensaje4.Text = "Gomez";
            Message mensaje5 = new Message();
            mensaje5.From = tuser;
            mensaje5.Text = "pepe123";
            Message mensaje6 = new Message();
            mensaje6.From = tuser;
            mensaje6.Text = "contraseña123";
            Message mensaje7 = new Message();
            mensaje7.From = tuser;
            mensaje7.Text = "01 02 2000";
            Message mensaje8 = new Message();
            mensaje8.From = tuser;
            mensaje8.Text = "1.234.567-8";
            Message mensaje9 = new Message();
            mensaje9.From = tuser;
            mensaje9.Text = "55555555";
            Message mensaje10 = new Message();
            mensaje10.From = tuser;
            mensaje10.Text = "pepeneitor@gmail.com";
            Message mensaje11 = new Message();
            mensaje11.From = tuser;
            mensaje11.Text = "calle 1 2";
            Message mensaje12 = new Message();
            mensaje12.From = tuser;
            mensaje12.Text = "si";
            RegistryHandler registryHandler = RegistryHandler.GetInstance();
            RegistrarHandler registrarHandler = new(null);
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
            registrarHandler.Handle(mensaje8,out response);
            registrarHandler.Handle(mensaje9,out response);
            registrarHandler.Handle(mensaje10,out response);
            registrarHandler.Handle(mensaje11,out response);
            registrarHandler.Handle(mensaje12,out response);
            Usuario miUsuario = registryHandler.GetUsuario("pepe123", "contraseña123");
            bool result = (miUsuario is Trabajador);
        
            // Assert
            Assert.That(result.Equals(expected));
        }
    }
    [Test]
    public void RegistrarEmpleador()
    {
        // Como empleador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto
        // para que de esa forma, pueda proveer información de contacto a los trabajadores que quiero contratar.
        { 
            // Arrange
            User tuser = new User();
            tuser.Id = 12345;
            Message mensaje1 = new Message();
            mensaje1.From = tuser;
            mensaje1.Text = "Registrar";
            Message mensaje2 = new Message();
            mensaje2.From = tuser;
            mensaje2.Text = "2";
            Message mensaje3 = new Message();
            mensaje3.From = tuser;
            mensaje3.Text = "Pepe";
            Message mensaje4 = new Message();
            mensaje4.From = tuser;
            mensaje4.Text = "Gomez";
            Message mensaje5 = new Message();
            mensaje5.From = tuser;
            mensaje5.Text = "pepe1234";
            Message mensaje6 = new Message();
            mensaje6.From = tuser;
            mensaje6.Text = "contraseña123";
            Message mensaje7 = new Message();
            mensaje7.From = tuser;
            mensaje7.Text = "01 02 2000";
            Message mensaje8 = new Message();
            mensaje8.From = tuser;
            mensaje8.Text = "1.234.567-8";
            Message mensaje9 = new Message();
            mensaje9.From = tuser;
            mensaje9.Text = "55555555";
            Message mensaje10 = new Message();
            mensaje10.From = tuser;
            mensaje10.Text = "pepeneitor@gmail.com";
            Message mensaje11 = new Message();
            mensaje11.From = tuser;
            mensaje11.Text = "calle 1 2";
            Message mensaje12 = new Message();
            mensaje12.From = tuser;
            mensaje12.Text = "si";
            RegistryHandler registryHandler = RegistryHandler.GetInstance();
            RegistrarHandler registrarHandler = new(null);
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
            registrarHandler.Handle(mensaje8,out response);
            registrarHandler.Handle(mensaje9,out response);
            registrarHandler.Handle(mensaje10,out response);
            registrarHandler.Handle(mensaje11,out response);
            registrarHandler.Handle(mensaje12,out response);
            Usuario miUsuario = registryHandler.GetUsuario("pepe1234", "contraseña123");
            bool result = (miUsuario is Empleador);
        
            // Assert
            Assert.That(result.Equals(expected));
        }
    }
}