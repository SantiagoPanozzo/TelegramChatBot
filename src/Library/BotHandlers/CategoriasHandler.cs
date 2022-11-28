using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.BotHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Nito.AsyncEx;

namespace Library;

/// <summary> <see cref="IHandler"/> del patrón Chain of Responsibility que implementa lee la palabra categoria/s
/// y muestra al usuario una lista con las respectivas categorías. </summary>
public class CategoriasHandler : BaseHandler {

    /// <summary> Instancia del bot de Telegram. </summary>
    private TelegramBotClient bot;

    /// <summary> Inicializa una nueva instancia de la clase <see cref="CategoriasHandler"/>. Esta clase procesa el mensaje "categorias". </summary>
    /// <param name="next"> Próximo <see cref="IHandler"/>. </param>
    public CategoriasHandler(TelegramBotClient bot, BaseHandler next) : base(next) {
        this.Keywords = new string[] {"categorias", "/categorias"};
        this._id = Handlers.CategoriasHandler;
    }

    /// <summary> Procesa el mensaje "categorias" y retorna true; retorna false en caso contrario. </summary>
    /// <param name="message"> El mensaje a procesar. </param>
    /// <param name="response"> La respuesta al mensaje procesado .</param>
    /// <returns> true si el mensaje fue procesado; false en caso contrario. </returns>
    protected override void InternalHandle(Message message, out string response) {
        response = "Categorias: --a-a-a--a-a-a-a-";
    }
}
