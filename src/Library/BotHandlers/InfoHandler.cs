using Library.BotHandlers;
using Telegram.Bot.Types;

namespace Library;

/// <summary> Un "handler" del patrón Chain of Responsibility que lee el comando "info". </summary>
public class InfoHandler : BaseHandler {
    /// <summary> Inicializa una nueva instancia de la clase <see cref="InfoHandler"/>. Esta clase procesa el mensaje "info". </summary>
    /// <param name="next">El próximo "handler".</param>
    public InfoHandler(BaseHandler next) : base(next)
    {
        this._id = Handlers.InfoHandler;
        this.Keywords = new string[] {"/info", "info"};
    }

    /// <summary> Procesa el mensaje "info" y retorna true; retorna false en caso contrario. </summary>
    /// <param name="message"> El mensaje a procesar. </param>
    /// <param name="response"> La respuesta al mensaje procesado. </param>
    /// <returns> true si el mensaje fue procesado; false en caso contrario. </returns>
    protected override void InternalHandle(Message message, out string response) {
        response = "A continuación te dejamos una lista con los comandos y sus acciones:\n» Buscar\n» Categorias\n» Info\n» Login\n» Panel de control\n» Registrar\n» Start";
    }
}
