using Telegram.Bot.Types;
using System;
using Library.BotHandlers;
using Library.Excepciones;

namespace Library;
/// <summary> Solicita al usuario su Nick y su Contraseña y si coinciden con la base de datos pasa a <see cref="InicioHandler"/>. </summary>
public class IniciarSesionHandler : BaseHandler
{
    private RegistryHandler RegistryHandler { get { return RegistryHandler.GetInstance(); } }
    /// <summary> Enum para indicar el estado de <see cref="IniciarSesionHandler"/> </summary>
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

    private string _tempLogin = String.Empty;

    /// <summary> Diccionario que guarda el estado en el <see cref="IHandler"/> según el ID de Telegram. </summary>
    /// <typeparam name="long"> ID de usuario de Telegram. </typeparam>
    /// <typeparam name="LoginState"> Estado del <see cref="IHandler"/>. </typeparam>
    private Dictionary<long, LoginState> Posiciones = new Dictionary<long, LoginState>();

    /// <summary> Inicializa una nueva instancia de la clase <see cref="IniciarSesionHandler"/>. </summary>
    /// <param name="next"> El próximo <see cref="IHandler"/>. </param>
    public IniciarSesionHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] { "iniciar", "login", "/login", "iniciar sesion", "iniciar sesión", "cerrar sesion", "cerrar sesión", "logout"};
        this.State = LoginState.Start;
        this._id= Handlers.IniciarSesionHandler;
    }
    
    /// <summary> Verifica que se pueda procesar el mensaje </summary>
    /// <param name="message"> Mensaje a procesar </param>
    /// <returns> true si puede procesar el mensaje, false en caso contrario </returns>
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

    /// <summary> Procesamiento de los mensajes. </summary>
    /// <param name="message"> Mensaje a procesar </param>
    /// <param name="response"> Respuesta al mensaje procesado </param>
    protected override void InternalHandle(Message message, out string response)
    {
        if (message == null || message.From == null || message.Text == null)
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
                if (message.Text.ToLower().Equals("volver"))
                {
                    this.Posiciones[message.From.Id] = LoginState.Start;
                    response = "Volviendo a inicio";
                    break;
                }
                if (!RegistryHandler.VerificarNick(message.Text))
                {
                    _tempLogin = message.Text;
                    response = $"Ingresa tu contraseña";
                    this.Posiciones[message.From.Id] = LoginState.Password;
                }
                else response = "No existe un usuario correspondiente al nombre de usuario ingresado, vuelve a intentarlo o escribe \"volver\" para volver al inicio.";
                break;
            case LoginState.Password:
                if (message.Text.ToLower().Equals("volver"))
                {
                    this.Posiciones[message.From.Id] = LoginState.Start;
                    response = "Volviendo a inicio";
                    break;
                }
                Usuario user;
                try
                {
                    user = RegistryHandler.GetUsuario(_tempLogin, message.Text);
                }
                catch (FalloDeAutenticacionException e)
                {
                    Console.WriteLine(e);
                    response = "Contraseña incorrecta, vuelve a intentarlo o escribe \"volver\" para regresar al inicio.";
                    break;
                }
                HandlerHandler.CachedLogins[message.From.Id] = user;
                response = "Sesion iniciada correctamente";
                this.Posiciones[message.From.Id] = LoginState.Success;
                break;
            case LoginState.Success:
                if (message.Text.Equals("cerrar sesion") || message.Text.Equals("cerrar sesión") ||
                    message.Text.Equals("logout"))
                {
                    HandlerHandler.CachedLogins.Remove(message.From.Id);
                    response = "Sesión cerrada correctamente, volviendo a inicio";
                    this.Posiciones[message.From.Id] = LoginState.Start;
                }
                break;
            default:
                response = "Error desconocido, /login para volver a logearte";
                this.Posiciones[message.From.Id] = LoginState.Start;
                break;
            
        }
    }
}
