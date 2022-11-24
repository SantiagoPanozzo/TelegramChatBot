using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Nito.AsyncEx;
namespace Library.Registro;

public class RegistrarHandler : BaseHandler
{
    // string nombre, string apellido, string nick, string contraseña, string fechaNacimiento,
    // string cedula, string telefono, string correo, Tuple<double,double> ubicacion
    public enum RegistrarState
    {
        Start,
        LecturaRol,
        LecturaNombre,
        LecturaApellido,
        LecturaNick,
        LecturaContraseña,
        LecturaFechaNacimiento,
        LecturaCedula,
        LecturaTelefono,
        LecturaCorreo,
        LecturaUbicacion,
        Confirmar,
        Fin,
    }
    
    public TipoDeUsuario TempTipo { get; set; }
    public Dictionary<string, string> TempInfo = new Dictionary<string, string>();

    public RegistrarState State { get; set; }
    
    private Dictionary<long, RegistrarState> Posiciones = new Dictionary<long, RegistrarState>();

    // Elegir tipo trabajador/empleador
    public RegistrarHandler(BaseHandler next) : base(next) {
        this.Keywords = new string[] {"registrar", "/registrar" };
        this.State = RegistrarState.Start;
    }

    protected override bool CanHandle(Message message)
    {
        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones[message.From.Id] = RegistrarState.Start;
        }

        switch (this.Posiciones[message.From.Id])
        {
            case RegistrarState.Start:
                return base.CanHandle(message);
            default:
                return true;
        }
    }
    
    // volver a bienvenida
    protected override void InternalHandle(Message message, out string response) {
        
        if (message == null || message.From == null || message.Text == null) {
            throw new Exception("No se recibió un mensaje");
        }
        
        switch (this.Posiciones[message.From.Id])
        {
            case RegistrarState.Start:
                response = "Selecciona tu rol:\n1) Trabajador \n2) Empleador\n 3) Regresar al inicio";
                this.State = RegistrarState.LecturaRol;
                break;
            case RegistrarState.LecturaRol:
                response = "Ingresa tu(s) nombre(s), escribe cancelar para volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaNombre;
                switch (message.Text)
                {
                    case "1":
                        this.TempTipo = TipoDeUsuario.Trabajador;
                        break;
                    case "2":
                        this.TempTipo = TipoDeUsuario.Empleador;
                        break;
                    case "3":
                        response = "Regresando al menu";
                        this.State = RegistrarState.Start;
                        break;
                    default:
                        response = "Error, intentalo de nuevo";
                        this.Posiciones[message.From.Id] = RegistrarState.LecturaRol;
                        break;
                }

                break;
            case RegistrarState.LecturaNombre:
                this.TempInfo["nombre"] = message.Text;
                response = "Ingresa tu(s) apellido(s), escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaApellido;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                        this.Posiciones[message.From.Id] = RegistrarState.Start;
                        response = "Volviendo al inicio"; // TODO idem
                        //this.TempInfo = new Dictionary<string, string>();
                        //this.TempTipo = null;
                }

                break;
            case RegistrarState.LecturaApellido:
                this.TempInfo["apellido"] = message.Text;
                response = "Ingresa un nombre de usuario, escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaNick;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.LecturaNick:
                this.TempInfo["nick"] = message.Text;
                response = "Ingresa una contraseña, escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaContraseña;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.LecturaFechaNacimiento:
                this.TempInfo["fechaNacimiento"] = message.Text;
                response = "Ingresa tu cedula (con puntos y guion), escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaCedula;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.LecturaCedula:
                this.TempInfo["cedula"] = message.Text;
                response = "Ingresa un teléfono para que puedan contactarte (será privado hasta que aceptes hablar con" +
                           "otro usuario), escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaTelefono;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.LecturaTelefono:
                this.TempInfo["telefono"] = message.Text;
                response = "Ingresa un correo para que puedan contactarte (será privado hasta que aceptes hablar con" +
                           "otro usuario), escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.LecturaCorreo;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.LecturaUbicacion:
                this.TempInfo["correo"] = message.Text;
                response = "Ingresa tu dirección (calle y numero), escribe cancelar parar volver al inicio";
                this.Posiciones[message.From.Id] = RegistrarState.Confirmar;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.Confirmar:
                this.TempInfo["ubicacion"] = message.Text;
                response = "Ingresaste la siguiente información: blabalbalabl\n ¿Guardar tu usuario? (Si/No)";
                this.Posiciones[message.From.Id] = RegistrarState.Fin;
                if(message.Text.ToLower().Equals("cancelar"))
                {
                    this.Posiciones[message.From.Id] = RegistrarState.Start;
                    response = "Volviendo al inicio"; // TODO idem
                    //this.TempInfo = new Dictionary<string, string>();
                    //this.TempTipo = null;
                }

                break;
            case RegistrarState.Fin:
                // TODO guardar aca la info
                this.Posiciones[message.From.Id] = RegistrarState.Start;
                break;
        }
        
        
        response = "Selecciona tu rol (Trabajador/Empleador)";
        Console.WriteLine(message.Text);
    }
}