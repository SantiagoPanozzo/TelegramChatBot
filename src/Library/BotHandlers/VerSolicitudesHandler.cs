using Telegram.Bot.Types;
using Library;
namespace Library.BotHandlers;

/// <summary> Muestra cada <see cref="Solicitud"/> no-finalizada que esté ligada al <see cref="Empleador"/> que tenga la sesión
/// iniciada y le permite calificar al <see cref="Trabajador"/> de la misma. En caso de un <see cref="Administrador"/>,
/// muestra todas las solicitudes en pie existentes en la base de datos y le permite dar de baja alguna. </summary>
public class VerSolicitudesHandler : BaseHandler
{
    public enum SolicitudState
    {
        Start,
        ElegidaEmp,
        ElegidaEmp2,
        ElegidaTrab,
        ElegidaTrab2,
        ContestarEmp,
        ContestarTrab,
        CalificarEmp,
        CalificarTrab,
        QueHacerEmp,
        QueHacerTrab,
        CalificarSolEmp,
        CalificarSolTrab
    }
    public TipoDeUsuario Tipo { get; set; }

    private PlainTextEmpleadorSolicitudPrinter empSolPrinter = new();
    private PlainTextTrabajadorSolicitudPrinter trabSolPrinter = new();
    private ContratoHandler solCatalog = ContratoHandler.GetInstance();
    private List <Solicitud> empList = new ();
    private List <Solicitud> trabList = new();
    private int solGetterEmp;
    private int solGetterTrab;
    private Calificacion calif { get; set; }
    private ContratoHandler conHandler = ContratoHandler.GetInstance();

    protected override bool CanHandle(Message message)
    {
        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones[message.From.Id] = SolicitudState.Start;
        }

        switch (this.Posiciones[message.From.Id])
        {
            case SolicitudState.Start:
                return base.CanHandle(message);
            default:
                return true;
        }
    }

    private Dictionary<long, SolicitudState> Posiciones = new Dictionary<long, SolicitudState>();

    public VerSolicitudesHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] {"Ver solicitudes", "/versolicitudes", "/ver solicitudes","solicitudes","/solicitudes"};
        this._id = Handlers.VerSolicitudesHandler;
    }

    /// <summary> Procesa el mensaje "Categorias" y retorna true; retorna false en caso contrario. </summary>
    /// <param name="message"> El mensaje a procesar. </param>
    /// <param name="response"> La respuesta al mensaje procesado. </param>
    /// <returns> true si el mensaje fue procesado; false en caso contrario. </returns>
    protected override void InternalHandle(Message message, out string response)
    {
        response="";
        Usuario user = HandlerHandler.CachedLogins[message.From.Id];
        switch (this.Posiciones[message.From.Id])
        {
            case SolicitudState.Start:
                response=$"¿Que deseas hacer?\n1)Ver mis solicitudes\n2)Calificar mis solicitudes";
                switch(message.Text){
                    case "1":
                        switch(Tipo)
                        {
                            case TipoDeUsuario.Empleador:
                                response = $"{empSolPrinter.Print(solCatalog.GetSolicitudes(user)) }\nIntroduzca el ID de la solicitud que quiere ver o ingrese \"volver\" para regresar.";
                                this.Posiciones[message.From.Id] = SolicitudState.ElegidaEmp;
                            break;
                            case TipoDeUsuario.Trabajador:
                                response = $"{trabSolPrinter.Print(solCatalog.GetSolicitudes(user)) }\nIntroduzca el ID de la solicitud que quiere ver o ingrese \"volver\" para regresar.";
                                this.Posiciones[message.From.Id] = SolicitudState.ElegidaTrab;
                            break;
                        }
                    break;
                    case "2":
                        switch(Tipo)
                        {
                            case TipoDeUsuario.Empleador:
                                response = $"{empSolPrinter.Print(solCatalog.GetSolicitudes(user)) }\nIntroduzca el ID de la solicitud que quiere ver o ingrese \"volver\" para regresar.";
                                this.Posiciones[message.From.Id] = SolicitudState.ElegidaEmp2;
                            break;
                            case TipoDeUsuario.Trabajador:
                                response = $"{trabSolPrinter.Print(solCatalog.GetSolicitudes(user)) }\nIntroduzca el ID de la solicitud que quiere ver o ingrese \"volver\" para regresar.";
                                this.Posiciones[message.From.Id] = SolicitudState.ElegidaTrab2;
                            break;
                        }
                        break;
                }
            break;
            
            case SolicitudState.ElegidaEmp:
                if (message.Text.Equals("volver"))
                {
                    response = "Volviendo al inicio";
                    this.Posiciones[message.From.Id] = SolicitudState.Start;
                    break;
                }
                solGetterEmp= Int32.Parse(message.Text);
                empList.Add(solCatalog.GetSolicitud(solGetterEmp));
                response = $"{empSolPrinter.Print(empList)} aca se muestra la solicitud\n1) Aceptarla\n2)Rechazarla\n3)Volver";
                this.Posiciones[message.From.Id] = SolicitudState.ContestarEmp;
                break;

            case SolicitudState.ElegidaTrab:
                if (message.Text.Equals("volver"))
                {
                    response = "Volviendo al inicio";
                    this.Posiciones[message.From.Id] = SolicitudState.Start;
                    break;
                }
                solGetterTrab= Int32.Parse(message.Text);
                trabList.Add(solCatalog.GetSolicitud(solGetterTrab));
                response = $"{empSolPrinter.Print(trabList)} aca se muestra la solicitud\n1) Aceptarla\n2)Rechazarla\n3)Volver";
                this.Posiciones[message.From.Id] = SolicitudState.ContestarTrab;
                break;
            case SolicitudState.ContestarEmp:
                switch (message.Text)
                {
                    case "1":
                        solCatalog.AceptarSolicitud(user, solCatalog.GetSolicitud(solGetterEmp));
                        response = "Solicitud aceptada, volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;

                    case "2":
                        solCatalog.RechazarSolicitud(user, solCatalog.GetSolicitud(solGetterEmp));
                        response = "Solicitud rechazada, volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;

                    case "3":
                        response = "Volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;

                    default:
                        response = "Vuelve a intentarlo";
                        break;
                }
                break;

            case SolicitudState.ContestarTrab:
                switch (message.Text)
                {
                    case "1":
                        solCatalog.AceptarSolicitud(user, solCatalog.GetSolicitud(solGetterTrab));
                        response = "Solicitud aceptada, volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;
                        
                    case "2":
                        solCatalog.RechazarSolicitud(user, solCatalog.GetSolicitud(solGetterTrab));
                        response = "Solicitud rechazada, volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;

                    case "3":
                        response = "Volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;

                    default:
                        response = "Vuelve a intentarlo";
                        break;
                }
                break;
           case SolicitudState.ElegidaEmp2:
                if (message.Text.Equals("volver"))
                {
                    response = "Volviendo al inicio";
                    this.Posiciones[message.From.Id] = SolicitudState.Start;
                    break;
                }
                solGetterEmp= Int32.Parse(message.Text);
                empList.Add(solCatalog.GetSolicitud(solGetterEmp));
                response = $"{empSolPrinter.Print(empList)} aca se muestra la solicitud\n1) Calificarla\n2) Volver";
                this.Posiciones[message.From.Id] = SolicitudState.QueHacerEmp;
                break;

            case SolicitudState.ElegidaTrab2:
                if (message.Text.Equals("volver"))
                {
                    response = "Volviendo al inicio";
                    this.Posiciones[message.From.Id] = SolicitudState.Start;
                    break;
                }
                solGetterTrab= Int32.Parse(message.Text);
                trabList.Add(solCatalog.GetSolicitud(solGetterTrab));
                response = $"{empSolPrinter.Print(trabList)} aca se muestra la solicitud\n1) Calificarla\n2) Volver";
                this.Posiciones[message.From.Id] = SolicitudState.QueHacerTrab;

                break;
            case SolicitudState.QueHacerEmp:
                switch(message.Text){
                    case "1":
                        response = $"Que calificación deseas darle?\n1)Deficiente\n2)Regular\n3)Bueno\n4)Muy Bueno\n5)Sobresaliente";
                        this.Posiciones[message.From.Id] = SolicitudState.CalificarSolEmp;
                        break;
                    case "2":
                        response = "Volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;
                }
                break;
            case SolicitudState.QueHacerTrab:
                switch(message.Text){
                    case "1":
                        response = $"Que calificación deseas darle?\n1)Deficiente\n2)Regular\n3)Bueno\n4)Muy Bueno\n5)Sobresaliente";
                        this.Posiciones[message.From.Id] = SolicitudState.CalificarSolTrab;
                        break;
                    case "2":
                        response = "Volviendo a inicio";
                        this.Posiciones[message.From.Id] = SolicitudState.Start;
                        HandlerHandler.ActiveHandler[message.From.Id] = Handlers.NoneHandler;
                        break;
                }
                break;
            case SolicitudState.CalificarSolEmp:
             if (new string[] {"1", "2", "3", "4", "5"}.Contains(message.Text)) calif = (Calificacion)(Int32.Parse(message.Text));
                conHandler.GetSolicitud(solGetterEmp).CalificarTrabajador((Empleador)user,calif);




                break;
            case SolicitudState.CalificarSolTrab:
             if (new string[] {"1", "2", "3", "4", "5"}.Contains(message.Text)) calif = (Calificacion)(Int32.Parse(message.Text));
                conHandler.GetSolicitud(solGetterTrab).CalificarEmpleador((Trabajador)user,calif);

                // if (message.Text.Equals("1")){calif=Calificacion.Deficiente;}
                // if (message.Text.Equals("2")){calif=Calificacion.Regular;}
                // if (message.Text.Equals("3")){calif=Calificacion.Bueno;}
                // if (message.Text.Equals("4")){calif=Calificacion.MuyBueno;}
                // if (message.Text.Equals("5")){calif=Calificacion.Sobresaliente;}
                break;
            default:
                response = "Asegurate que el estado ingresado sea correcto";
                break;
        }
    }
}
