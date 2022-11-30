using Telegram.Bot.Types;
namespace Library.BotHandlers;

/// <summary> Se fija si el Telegram ID de la persona corresponde a un administrador y le pide su contraseña de administrador y la
/// contrasta con la base de datos. Hecho esto le muestra la opción de ir a <see cref="VerCategoriasHandler"/>, <see cref="VerOfertasHandler"/>
/// <see cref="VerSolicitudesHandler"/>, <see cref="VerUsuariosHandler"/>, o volver a <see cref="StartHandler"/>.
/// </summary>
public class PanelDeControlHandler : BaseHandler
{
    /// <summary> Indica los filtros </summary>
    public enum PanelState
    {
        Start,
        Username,
        Password,
        Success,
        PanelStart,
        Panel,
        VerCategorias,
        EliminarCategoria,
        AgregarCategoria,
        CrearCategoria,
        VerOfertas,
        VerOfertasID,
        EliminarOferta,
        VerSolicitudes,
        EliminarSolicitud,
        VerUsuarios,
        EliminarUsuario
    }

    /// <summary> Estado de <see cref="PanelDeControlHandler"/> </summary>
    public PanelState State { get; set; }

    protected Dictionary<string,string> tempPanelInfo = new();

    protected PlainTextCategoriaPrinter catPrinter = new();
    protected PlainTextOfertasPrinter ofPrinter = new();
    protected PlainTextUsuariosPrinter usPrinter = new();
    protected PlainTextAdminSolicitudPrinter solPrinter = new();

    protected ContratoHandler solHandler = ContratoHandler.GetInstance();
    protected SolicitudCatalog solCatalog = SolicitudCatalog.GetInstance();
    protected CategoriasCatalog catCatalog = CategoriasCatalog.GetInstance();
    protected OfertasHandler ofCatalog = OfertasHandler.GetInstance();
    protected UsuariosCatalog usCatalog = UsuariosCatalog.GetInstance();
    static RegistryHandler regHandler = RegistryHandler.GetInstance();

    Administrador admin = new("a","b","c","d");

    protected int catRemover;
    protected int ofRemover;
    protected int solRemover;

    /// <summary> Diccionario que guarda el estado en el <see cref="IHandler"/> según el ID de Telegram. </summary>
    /// <typeparam name="long"> ID de usuario de Telegram. </typeparam>
    /// <typeparam name="LoginState"> Estado del <see cref="IHandler"/>. </typeparam>
    private Dictionary<long, PanelState> Posiciones = new Dictionary<long, PanelState>();

    /// <summary> Inicializa una nueva instancia de la clase <see cref="PanelDeControlHandler"/>. </summary>
    /// <param name="next"> Próximo <see cref="IHandler"/> </param>
    public PanelDeControlHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] {"admin","admin login","login admin","/admin"};
        this.State = PanelState.Start;
        this._id = Handlers.PanelDeControlHandler;
    }

    /// <summary> Verifica que se pueda procesar el mensaje </summary>
    /// <param name="message"> Mensaje a procesar </param>
    /// <returns> true si puede procesar el mensaje, false en caso contrario </returns>
    protected override bool CanHandle(Message message)
    {
        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones[message.From.Id] = PanelState.Start;
        }
        switch (this.Posiciones[message.From.Id])
        {
            case PanelState.Start:
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
        if (message == null || message.From == null)
        {
            throw new Exception("No se recibió un mensaje");
        }

        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones.Add(message.From.Id, PanelState.Start);
        }

        response = "Error desconocido";

        PanelState state = this.Posiciones[message.From.Id];
        switch(this.Posiciones[message.From.Id])
        {
            case PanelState.Start:
                response = $"Ingresa tu nombre de usuario";
                this.Posiciones[message.From.Id] = PanelState.Username;
                break;
            case PanelState.Username:
                this.tempPanelInfo["adminusername"]=message.Text;
                response = $"Ingresa tu contraseña";
                this.Posiciones[message.From.Id] = PanelState.Password;
                break;
            case PanelState.Password:
                this.tempPanelInfo["adminpassword"]=message.Text;
                switch (LoginAdminChecker())
                {
                    case true:
                        response = $"Iniciando sesion...\n\nElige una acción:\n1) Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        if(message.Text.Equals("5"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        break;
                    case false:
                        response = $"Nombre de usuario o contraseña incorrecta, vuelve a intentarlo\n\nIngresa tu nombre de usuario";
                        this.Posiciones[message.From.Id] = PanelState.Username;
                        break;
                }
            break;
            // default:
            //     response = "Error desconocido, /admin para volver a logearte";
            //     this.Posiciones[message.From.Id] = PanelState.Start;
            //     break;
        case PanelState.PanelStart:
            this.Posiciones[message.From.Id] = PanelState.Panel;
            response = $"Elige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios";
            if(message.Text.Equals("5"))
            {
                this.Posiciones[message.From.Id] = PanelState.Panel;
                response = "Volviendo al inicio";
            }
            break;
        case PanelState.Panel:
            switch(message.Text) {
                case "1":
                    response=$"{catPrinter.Print(catCatalog.GetCategorias())}\n¿Deseas realizar otra acción?\n1)Eliminar Categoría\n2)Agregar Categoría\n3)Cancelar";
                    this.Posiciones[message.From.Id] = PanelState.VerCategorias;
                    break;
                case "2":
                    this.Posiciones[message.From.Id] = PanelState.VerOfertas;
                    if(message.Text.Equals("3"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    response=$"{ofPrinter.Print(ofCatalog.GetOfertasIgnoreId())} \n¿Deseas realizar otra acción?\n1)Eliminar Oferta\n2)Cancelar";
                    break;
                
                     case "3": 
                    this.Posiciones[message.From.Id] = PanelState.VerSolicitudes; 
                    response=$"{solPrinter.Print(solHandler.GetSolicitudes(admin))}\n¿Deseas realizar otra acción?\n1)Eliminar Solicitud\n2)Cancelar";
                    break;

                case "4":
                    this.Posiciones[message.From.Id] = PanelState.VerUsuarios;
                    response=$"{usPrinter.Print(usCatalog.GetUsuariosIgnoreId()) }\n¿Deseas realizar otra acción?\n1)Eliminar Usuario\n2)Cancelar";
                    break;
                case "5":
                    this.Posiciones[message.From.Id] = PanelState.Panel;
                    response = "Volviendo al inicio";
                    break;
                default:
                    response = "Verifique que el estado ingresado sea correcto";
                    break;
            }
            break;
        case PanelState.VerCategorias:
            switch(message.Text) {
                case "1":
                    response=$"¿Que categoría deseas eliminar?";
                    this.Posiciones[message.From.Id] = PanelState.EliminarCategoria;
                    switch(message.Text){
                        case "cancelar":
                        this.Posiciones[message.From.Id] = PanelState.PanelStart;
                        response = "Volviendo al inicio";
                        break;
                    }
                    break;

               case "2":
                    response=$"Ingresa la descripción de la categoría";
                    this.Posiciones[message.From.Id] = PanelState.AgregarCategoria;
                    break;
                case "3":
                    this.Posiciones[message.From.Id] = PanelState.Panel;
                    response = "Volviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios";
                    break;
                default:
                    this.Posiciones[message.From.Id] = PanelState.PanelStart;
                    response = "Verifique que el estado ingresado sea correcto";
            break;
            }
            break;
           default:
                this.Posiciones[message.From.Id] = PanelState.PanelStart;
                response = "Verifique que el estado ingresado sea correcto";
            break;
        case PanelState.EliminarCategoria:
            catRemover=Int32.Parse(message.Text);
            catCatalog.RemoveCategoria(regHandler.RegistrarAdministrador("mateo","mateo123","098765432","mateo@gmail.com"), catCatalog.GetCategoriaById(catRemover));
            response=$"Categoria Eliminada\n{catPrinter.Print(catCatalog.GetCategorias())}\nVoviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
            this.Posiciones[message.From.Id] = PanelState.Panel;

            break;
        case PanelState.AgregarCategoria:
            if(message.Text.Equals("5"))
            {
                this.Posiciones[message.From.Id] = PanelState.PanelStart;
                response = "Volviendo al inicio";
            }
            this.tempPanelInfo["addcategoria"]=message.Text;
            catCatalog.AddCategoria(admin, this.tempPanelInfo["addcategoria"] );
            response=$"Categoria añadida\n{catPrinter.Print(catCatalog.GetCategorias())}\nVoviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
            this.Posiciones[message.From.Id] = PanelState.Panel;

            break;
        case PanelState.VerOfertas:
            switch(message.Text) {
                case "1":
                    response=$"¿Que oferta deseas eliminar?";
                    this.Posiciones[message.From.Id] = PanelState.EliminarOferta;
                break;
                case "2":
                    response=$"Volviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
                   this.Posiciones[message.From.Id] = PanelState.Panel;
                break;
                default:
                    response = "Verifique que el estado ingresado sea correcto";
                    break;
            }
            break;

        case PanelState.VerSolicitudes:
            switch(message.Text) {
                case "1":
                    response=$"¿Que solicitud deseas eliminar?";
                    this.Posiciones[message.From.Id] = PanelState.EliminarSolicitud;
                    if(message.Text.Equals("cancelar"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    break;
                case "2":
                    response=$"Volviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
                   this.Posiciones[message.From.Id] = PanelState.Panel;
                break;
                default:
                    response = "Verifique que el estado ingresado sea correcto";
                    break;
            }
        break;
        case PanelState.EliminarOferta:
            ofRemover=Int32.Parse(message.Text);
            ofCatalog.DarDeBajaOferta(admin, ofRemover);
            response=$"Oferta eliminada\n{ofPrinter.Print(ofCatalog.GetOfertasIgnoreId())}\nVoviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
            this.Posiciones[message.From.Id] = PanelState.Panel;
            break;
        case PanelState.EliminarSolicitud:
            solRemover=Int32.Parse(message.Text);
            solCatalog.RemoveSolicitud(solHandler.GetSolicitud(solRemover));
            response=$"Solicitud eliminada\n{solPrinter.Print(solHandler.GetSolicitudes(admin))}\nVoviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
            this.Posiciones[message.From.Id] = PanelState.Panel;
            break;

        case PanelState.VerUsuarios:
            switch(message.Text) {
                case "1":
                    response=$"Ingresa el nick del usuario a eliminar";
                    this.Posiciones[message.From.Id] = PanelState.EliminarUsuario;
                    if(message.Text.Equals("cancelar"))
                {
                        this.Posiciones[message.From.Id] = PanelState.PanelStart;
                        response = "Volviendo al inicio";
                }
                break;
                case "2":
                    response=$"Volviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
                   this.Posiciones[message.From.Id] = PanelState.Panel;
                break;
                default:
                    this.Posiciones[message.From.Id] = PanelState.PanelStart;
                    response = "Verifique que el estado ingresado sea correcto";
                break;

            }
        break;

        case PanelState.EliminarUsuario:
            this.tempPanelInfo["usuarioeliminado"]=message.Text;
            regHandler.RemoveUsuario(admin, usCatalog.GetUsuarioById(this.tempPanelInfo["usuarioeliminado"]));
            response=$"Usuario eliminado\n{usPrinter.Print(usCatalog.GetUsuariosIgnoreId())}\nVoviendo al inicio\n\nElige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
            this.Posiciones[message.From.Id] = PanelState.Panel;
        break;

            }
        }

    protected bool LoginAdminChecker()
    {
    foreach (Usuario user in usCatalog.GetUsuarios()){

    if (user.Nick.Equals(this.tempPanelInfo["adminusername"]) && user.VerifyContraseña(this.tempPanelInfo["adminpassword"])){return true;}
    }
    return false;

    }
}      

