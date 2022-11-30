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

    //protected ITextPrinter<Solicitud> solPrinter = new PlainTextAdminSolicitudPrinter();
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
                // switch (LoginAdminChecker())
                switch (message.Text)

                {
                    // case true:
                    case "si":
                        response = $"Iniciando sesion...\n\nElige una acción:\n1) Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        if(message.Text.Equals("5"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        break;
                    // case false:
                    case "no":
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
            response = $"Elige una acción:\n1)Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios\n5)Cancelar";
            if(message.Text.Equals("5"))
            {
                this.Posiciones[message.From.Id] = PanelState.Panel;
                response = "Volviendo al inicio";
            }
            break;
        case PanelState.Panel:
            switch(message.Text) {
                case "1":
                    this.Posiciones[message.From.Id] = PanelState.VerCategorias;
                    response=$"{catPrinter.Print(catCatalog.GetCategorias())}\n¿Deseas realizar otra acción?\n1)Eliminar Categoría\n2)Agregar Categoría\n3)Cancelar";
                    break;
                case "2":
                    this.Posiciones[message.From.Id] = PanelState.VerOfertas;
                    if(message.Text.Equals("3"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    response=$"{ofPrinter.Print(ofCatalog.GetOfertasIgnoreId())} \n¿Deseas realizar otra acción?\n1)Eliminar Oferta";
                    break;
                
                /* case "3": //TODO ver los solPrinter
                    this.Posiciones[message.From.Id] = PanelState.VerSolicitudes; //TODO cambiar el printer
                    response=$"{solPrinter.Print(solHandler.GetSolicitudes(admin))}\n¿Deseas realizar otra acción?\n1)Eliminar Solicitud";
                    break;

                case "4":
                    this.Posiciones[message.From.Id] = PanelState.VerUsuarios;
                    response=$"{usPrinter.Print(usCatalog.GetUsuariosIgnoreId()) }\n¿Deseas realizar otra acción?\n1)Eliminar Usuario";
                    break;
                case "5":
                    this.Posiciones[message.From.Id] = PanelState.PanelStart;
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
                    response=$"¿Que categoría deseas eliminar? Escribe \"cancelar\" para volver al inicio";
                    this.Posiciones[message.From.Id] = PanelState.EliminarCategoria;
                    switch(message.Text){
                        case "cancelar":
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                        break;
                    }
                    if(message.Text.Equals("cancelar"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    switch(this.Posiciones[message.From.Id]){
                        case PanelState.EliminarCategoria:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        catRemover=Int32.Parse(message.Text);
                        catCatalog.RemoveCategoria(regHandler.RegistrarAdministrador("mateo","mateo123","098765432","mateo@gmail.com"), catCatalog.GetCategoriaById(catRemover));
                        response=$"Categoria Eliminada";
                        break;
                    }
                    break;
               case "2":
                    if(message.Text.Equals("cancelar"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    response=$"Ingresa la descripción de la categoría. Escribe \"cancelar\" para volver al inicio";
                    this.Posiciones[message.From.Id] = PanelState.AgregarCategoria;
                    if(message.Text.Equals("cancelar"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    switch(this.Posiciones[message.From.Id]){
                        case PanelState.AgregarCategoria:
                        if(message.Text.Equals("5"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        this.tempPanelInfo["addcategoria"]=message.Text;
                        catCatalog.AddCategoria(admin, this.tempPanelInfo["addcategoria"] );
                        response=$"Categoria añadida";
                        break;
                    }
                    break;
                case "3":
                    this.Posiciones[message.From.Id] = PanelState.PanelStart;
                    response = "Volviendo al inicio";
                break;
                default:
                this.Posiciones[message.From.Id] = PanelState.Panel;
                response = "Verifique que el estado ingresado sea correcto";
            break;
            }
            break;
           default:
                this.Posiciones[message.From.Id] = PanelState.Panel;
                response = "Verifique que el estado ingresado sea correcto";
            break;
        case PanelState.VerOfertas:
            switch(message.Text) {
                case "1":
                    response=$"¿Que oferta deseas eliminar? Escribe \"cancelar\" para volver al inicio";
                    this.Posiciones[message.From.Id] = PanelState.EliminarOferta;
                    if(message.Text.Equals("cancelar"))
                {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                }
                    switch(this.Posiciones[message.From.Id]){
                        case PanelState.EliminarOferta:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        ofRemover=Int32.Parse(message.Text);
                        ofCatalog.DarDeBajaOferta(admin, ofRemover);
                        response=$"Oferta eliminada";
                        break;
                    default:
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Verifique que el estado ingresado sea correcto";
                    break;
                    }
                    break;
            }
            break;

        case PanelState.VerSolicitudes:
            switch(message.Text) {
                case "1":
                    response=$"¿Que solicitud deseas eliminar? Escribe \"cancelar\" para volver al inicio";
                    this.Posiciones[message.From.Id] = PanelState.EliminarSolicitud;
                    if(message.Text.Equals("cancelar"))
                {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                }
                    switch(this.Posiciones[message.From.Id])
                    {
                        case PanelState.EliminarSolicitud:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        solRemover=Int32.Parse(message.Text);
                        solCatalog.RemoveSolicitud(solHandler.GetSolicitud(solRemover));
                        response=$"Solicitud eliminada";
                        break;
                    default:
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Verifique que el estado ingresado sea correcto";
                        break;
                    }
                        break;
            }
        break;
        case PanelState.VerUsuarios:
            switch(message.Text) {
                case "1":
                    response=$"Ingresa el nick del usuario a eliminar. Escribe \"cancelar\" para volver al inicio";
                    this.Posiciones[message.From.Id] = PanelState.EliminarUsuario;
                    if(message.Text.Equals("cancelar"))
                {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                }
                    switch(this.Posiciones[message.From.Id]){
                        case PanelState.EliminarUsuario:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        this.tempPanelInfo["usuarioeliminado"]=message.Text;
                        usCatalog.RemoveUsuario(admin, usCatalog.GetUsuarioById(this.tempPanelInfo["usuarioeliminado"]));
                        response=$"Usuario eliminado";
                        break;
                    }
                break;
                    default:
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Verifique que el estado ingresado sea correcto";
                    break;
            }
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

