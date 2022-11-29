using Library.BotHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace Library.BotHandlers;

/// <summary> <see cref="IHandler"/> del patrón Chain of Responsibility que implementa lee la palabra categoria/s
/// y muestra al usuario una lista con las respectivas categorías. </summary>
public class VerCategoriasHandler : BaseHandler {

    /// <summary> Instancia del bot de Telegram. </summary>
    private TelegramBotClient bot;

    /// <summary> Inicializa una nueva instancia de la clase <see cref="CategoriasHandler"/>. Esta clase procesa el mensaje "categorias". </summary>
    /// <param name="next"> Próximo <see cref="IHandler"/>. </param>
    public VerCategoriasHandler(TelegramBotClient bot, BaseHandler next) : base(next) {
        this.Keywords = new string[] {};
        this._id = Handlers.CategoriasHandler;
    }

    /// <summary> Verifica que se pueda procesar el mensaje </summary>
    /// <param name="message"> Mensaje a procesar </param>
    /// <returns> true si puede procesar el mensaje, false en caso contrario </returns>
    protected override bool CanHandle(Message message) {
        if (HandlerHandler.ActiveHandler[message.From.Id].Equals(Handlers.BuscarHandler)) {
            return base.CanHandle(message);
        } else {
            return false;
        }
    }

    /// <summary> Procesa el mensaje "categorias" y retorna true; retorna false en caso contrario. </summary>
    /// <param name="message"> El mensaje a procesar. </param>
    /// <param name="response"> La respuesta al mensaje procesado .</param>
    /// <returns> true si el estado de <see cref="HandlerHandler"/> es el correcto, false en caso contrario. </returns>
    protected override void InternalHandle(Message message, out string response) {
        CategoriasCatalog catCatalog = CategoriasCatalog.GetInstance();
        PlainTextCategoriaPrinter catPrinter = new();
        response = catPrinter.Print(catCatalog.GetCategorias());
    }
}

