using Telegram.Bot.Types;
namespace Library.BotHandlers;

/// <summary> Muestra al administrador una lista de cada <see cref="Usuario"/> registrado y le da la opción de dar de baja a alguno. </summary>
public class VerUsuariosHandler : BaseHandler
{
    private RegistryHandler _regHandlerInstance = RegistryHandler.GetInstance();
    public enum VerUsuarioStates
    {
        Start,
        Filtro,
        Mostrar,
        AskUsuario,
        DarDeBaja
    }
    private Dictionary<long, VerUsuarioStates> _posiciones = new();
    public VerUsuariosHandler(BaseHandler next) : base(next)
    {
        Keywords = new string[] {"ver usuarios", "/verusuarios", "verusuarios"};
        _id = Handlers.VerUsuarioHandler;
    }

    protected override bool CanHandle(Message message)
    {

        if (!_posiciones.ContainsKey(message.From.Id)) _posiciones[message.From.Id] = VerUsuarioStates.Start;

        return (_posiciones[message.From.Id] == VerUsuarioStates.Start) ? base.CanHandle(message) : true;
    }

    protected override void InternalHandle(Message message, out string response)
    {
        if (message == null || message.From == null || message.Text == null) {
            throw new Exception("No se recibió un mensaje");
        }

        HandlerHandler.CachedLogins[message.From.Id] = RegistryHandler.GetInstance().RegistrarAdministrador("asd", "asd", "092319952", "fac@gmail.com");
        bool isLogged = HandlerHandler.CachedLogins.ContainsKey(message.From.Id);

        response = "El usuario no ha iniciado sesión. Inicie sesión con /login";
        
        if (isLogged)
        {

            bool isAdmin = TipoDeUsuario.Administrador == HandlerHandler.CachedLogins[message.From.Id].GetTipo();
            
            if (true)
            {
                var admin = HandlerHandler.CachedLogins[message.From.Id];

                response = "El usuario debe de ser un administrador para acceder a este comando";

                var userState = _posiciones[message.From.Id];

                switch(userState)
                {
                    case VerUsuarioStates.Start:
                        _posiciones[message.From.Id] = VerUsuarioStates.Filtro;
                        response = "Elija alguna de las siguientes opciones:\n"
                                +   "1 >> Mostrar todos los usuarios\n"
                                +   "2 >> Dar de baja a un usuario\n";
                        break;

                    case VerUsuarioStates.Filtro:
                        switch (message.Text)
                        {
                            case "1":

                                var trabajadoresList = _regHandlerInstance.GetTrabajadores();
                                var empleadoresList = _regHandlerInstance.GetEmpleadores();
                                int tCount = trabajadoresList.Count;
                                int eCount = empleadoresList.Count;
                                response = $"Usuarios ({tCount + eCount}): {tCount} trabajadores y {eCount} empleadores";

                                //Por LSP
                                ITextPrinter<string> printer;

                                printer = new PlainTextUsersEmpleadoresPrinter();
                                response += $"{printer.Print(empleadoresList)}";

                                printer = new PlainTextUsersTrabajadoresPrinter();
                                response += $"{printer.Print(trabajadoresList)}";

                                _posiciones[message.From.Id] = VerUsuarioStates.Start;

                                break;

                        case "2":
                            response = "¿Qué usuario quiere remover? Inserte su nick:";
                            _posiciones[message.From.Id] = VerUsuarioStates.DarDeBaja;
                            break;
                        case "volver":
                            _posiciones[message.From.Id] = VerUsuarioStates.Start;
                            break;
                        default:
                            response = "debug default";
                            _posiciones[message.From.Id] =  VerUsuarioStates.Filtro;
                            break;
                        }
                        break;

                    case VerUsuarioStates.DarDeBaja:
                        var username = message.Text;
                        var user = _regHandlerInstance.GetUserForAdmin(admin, username);
                        _regHandlerInstance.RemoveUsuario(admin, user);

                        response = $"Usuario de nombre {user.Nombre} y apellido {user.Apellido} eliminado";

                        _posiciones[message.From.Id] = VerUsuarioStates.Start;

                        break;
                }
            }
        }
    }
    private void Cancel(Message message)
    {
        if (message.Text.ToLower() == "volver") _posiciones[message.From.Id] = VerUsuarioStates.Start;
        return;
    }
}
