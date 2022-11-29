using Telegram.Bot.Types;

namespace Library.BotHandlers;

/// <summary> Muestra toda la información correspondiente al <see cref="Usuario"/> que tiene la sesión iniciada, ya sea
/// <see cref="Trabajador"/> o <see cref="Administrador"/>. Le permite también darse de baja de la plataforma o
/// volver al <see cref="InicioHandler"/>. </summary>
public class VerInfoHandler : BaseHandler
{
    protected string id;
    public VerInfoHandler(BaseHandler next): base(next)
    {
        Keywords = new string[] {"ver info", "verinfo", "/verinfo" };
        id = "VerInfoHandler";
    }

    protected override bool CanHandle(Message message)
    {
        return base.CanHandle(message);
    }

    protected override void InternalHandle(Message message, out string response)
    {
        if (message == null || message.From == null || message.Text == null) {
            throw new Exception("No se recibió un mensaje");
        }

        bool isLogged = HandlerHandler.CachedLogins.ContainsKey(message.From.Id);
        
        if (isLogged)
        {
            Usuario user = HandlerHandler.CachedLogins[message.From.Id];
            ITextPrinter<Usuario, Usuario> printer = new PlainTextUserPrinter();

            //ITextPrinter<Usuario, Usuario> printer = new PlainTextUserInfoPrinter();
            //response = $"{printer.Print(user)}"; Mejor así

            List<Usuario> users = new();    //Esto se saca
            users.Add(user);                //Esto se saca

            response = $"{printer.PrintAll(users, user)}";  //Muy feo, arreglarlo mañana
            return;
        }
        response = "El usuario no ha iniciado sesión. Inicie sesión con /login";
    }
    
}
