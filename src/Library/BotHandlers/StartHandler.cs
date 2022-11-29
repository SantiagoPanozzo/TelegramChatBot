using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Nito.AsyncEx;
namespace Library.BotHandlers;

/// <summary> Un "handler" del patrón Chain of Responsibility que lee la palabra "start", o el comando /start,
/// que es lo que se manda si o si al iniciar el chat con el bot en Telegram. </summary>
public class StartHandler : BaseHandler {
    /// <summary> Instancia del Bot de Telegram </summary>
    private TelegramBotClient bot;
    
    /// <summary>  Inicializa una nueva instancia de la clase <see cref="StartHandler"/>. Esta clase lee el comando /start </summary>
    /// <param name="next">El próximo "handler".</param>
    public StartHandler(BaseHandler next) : base(next) {
        this.Keywords = new string[] {"start", "/start"};
        this._id = Handlers.StartHandler;
    }

    /// <summary> Procesa el mensaje "Categorias" y retorna true; retorna false en caso contrario. </summary>
    /// <param name="message"> El mensaje a procesar. </param>
    /// <param name="response"> La respuesta al mensaje procesado. </param>
    /// <returns> true si el mensaje fue procesado; false en caso contrario. </returns>
    protected override void InternalHandle(Message message, out string response) {
        response = "Para ver todos los comandos ingrese la palabra \"info\", o ejecute el comando /info";
    }
}
