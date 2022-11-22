using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Nito.AsyncEx;
namespace Library.Registro;

public class RegistrarHandler : BaseHandler
{
    // Elegir tipo trabajador/empleador
    public RegistrarHandler(BaseHandler next) : base(next) {
        this.Keywords = new string[] {"registrar", "/registrar" };
        this._id = 2;
    }
    // volver a bienvenida
    protected override void InternalHandle(Message message, out string response) {
        response = "Selecciona tu rol (Trabajador/Empleador)";
        Console.WriteLine(message.Text);
    }
}