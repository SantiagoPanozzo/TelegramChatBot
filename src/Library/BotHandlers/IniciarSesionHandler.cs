using Telegram.Bot.Types;
using System;
using Library.BotHandlers;

namespace Library;
/// <summary>
/// Solicita al usuario su Nick y su Contraseña y si coinciden con la base de datos procede a <see cref="InicioHandler"/>
/// </summary>
public class IniciarSesionHandler : BaseHandler
{
    /// <summary> Indica los filtros </summary>
    public enum LoginState
    {
        Start,
        Username,
        Password,
        Success,
        Error
    }
    /// <summary> El estado del comando. </summary>
    public LoginState State { get; set; }

    private Dictionary<long, LoginState> Posiciones = new Dictionary<long, LoginState>();

    /// <summary> Inicializa una nueva instancia de la clase <see cref="BuscarHandler"/>. </summary>
    /// <param name="next">Un buscador de direcciones.</param>
    /// <param name="next">El próximo "handler".</param>
    public IniciarSesionHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] { "iniciar", "login", "/login", "iniciar sesion", "iniciar sesión"};
        this.State = LoginState.Start;
        this._id= Handlers.IniciarSesionHandler;
    }
    /// <summary>  </summary>
    /// <param name="message">  </param>
    /// <returns>  </returns>

    protected override bool CanHandle(Message message)
    {
        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones[message.From.Id] = LoginState.Start;
        }
        switch (this.Posiciones[message.From.Id])
        {
            case LoginState.Start:
                return base.CanHandle(message);
            default:
                return true;
        }
    }

    protected override void InternalHandle(Message message, out string response)
    {
        if (message == null || message.From == null)
        {
            throw new Exception("No se recibió un mensaje");
        }

        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones.Add(message.From.Id, LoginState.Start);
        }

        response = "Error desconocido";

        LoginState state = this.Posiciones[message.From.Id];

        switch (state)
        {
            case LoginState.Start:
                response = $"Ingresa tu nombre de usuario";
                this.Posiciones[message.From.Id] = LoginState.Username;
                break;
            case LoginState.Username:
                response = $"Ingresa tu contraseña";
                this.Posiciones[message.From.Id] = LoginState.Password;
                break;
            case LoginState.Password:
                switch (message.Text)
                {
                    case "contraseña correcta":
                        response = "Iniciando sesion...";
                        this.Posiciones[message.From.Id] = LoginState.Success;
                        // this.Posiciones[message.From.Id] = InicioState.Start;
                        break;
                    case "!contraseña correcta":
                        response = $"Nombre de usuario o contraseña incorrecta, vuelve a intentarlo\n\nIngresa tu nombre de usuario";
                        this.Posiciones[message.From.Id] = LoginState.Username;
                        break;
                }
            break;
            default:
                response = "Error desconocido, /login para volver a logearte";
                this.Posiciones[message.From.Id] = LoginState.Start;
                break;
        }
    }
}
