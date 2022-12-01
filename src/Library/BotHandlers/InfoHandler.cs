using System.Text;
using Library.BotHandlers;
using Telegram.Bot.Types;
namespace Library.BotHandlers;

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
    protected override void InternalHandle(Message message, out string response)
    {
        if (message == null || message.From == null || message.Text == null)
        {
            throw new Exception("No se recibió un mensaje");
        }
        StringBuilder respuesta = new StringBuilder();
        respuesta.Append("A continuación te dejamos una lista con los comandos y sus acciones:\n");
        if (!HandlerHandler.CachedLogins.ContainsKey(message.From.Id))
        {
            respuesta.Append("» Registrar\n");
            respuesta.Append("» Iniciar sesión\n");
        }
        else
        {
            respuesta.Append("» Cerrar sesión\n» Buscar\n» Categorias\n» Info\n» Panel de control\n» Start\n» Ver solicitudes\n» Ver info\n» Ver ofertas\n» Ver usuarios ");
        }

        response = respuesta.ToString();
    }
}
