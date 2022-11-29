using Telegram.Bot;
using Telegram.Bot.Types;
using Library;
namespace Library.BotHandlers;

/// <summary>
/// Presenta al trabajador cada <see cref="OfertaDeServicio"/> vigente ligada al mismo y le permite calificar al
/// <see cref="Empleador"/> correspondiente. En caso de un <see cref="Administrador"/> le muestra todas las existentes
/// y le permite dar de baja alguna.
/// </summary>
public class VerOfertasHandler : BaseHandler {

    /// <summary> Inicializa una nueva instancia de la clase <see cref="CategoriasHandler"/>. Esta clase procesa el mensaje "categorias". </summary>
    /// <param name="next"> Próximo <see cref="IHandler"/>. </param>
    public VerOfertasHandler(BaseHandler next) : base(next) {
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
        OfertasHandler ofCatalog = OfertasHandler.GetInstance();
        PlainTextOfertasPrinter ofPrinter = new();
        response = ofPrinter.Print(ofCatalog.GetOfertas()); //TODO xd
    }
}
