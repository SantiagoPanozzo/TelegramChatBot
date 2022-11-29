using Telegram.Bot.Types;
using System;
using Library.BotHandlers;

namespace Library;
/// <summary>
/// Se fija si el Telegram ID de la persona corresponde a un administrador y le pide su contraseña de administrador y la
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
   /// <summary> El estado del comando. </summary>
    public PanelState State { get; set; }
    protected Dictionary<string,string> TempPanelInfo = new();
    protected PlainTextCategoriaPrinter CatPrinter = new();
    protected PlainTextSolicitudPrinter SolPrinter = new();
    protected PlainTextOfertasPrinter OfPrinter = new();
    protected PlainTextUsuariosPrinter UsPrinter = new();
    protected ContratoHandler SolHandler = ContratoHandler.GetInstance();
    protected SolicitudCatalog SolCatalog = SolicitudCatalog.GetInstance();
    protected CategoriasCatalog CatCatalog = CategoriasCatalog.GetInstance();
    protected OfertasHandler OfCatalog = OfertasHandler.GetInstance();
    protected UsuariosCatalog UsCatalog = UsuariosCatalog.GetInstance();

    static Administrador admin = new("a","b","c","d");
    protected int catremover;
    protected int ofremover;
    protected int solremover;
    private Dictionary<long, PanelState> Posiciones = new Dictionary<long, PanelState>();

    /// <summary> Inicializa una nueva instancia de la clase <see cref="BuscarHandler"/>. </summary>
    /// <param name="next">Un buscador de direcciones.</param>
    /// <param name="next">El próximo "handler".</param>
    public PanelDeControlHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] {"admin","admin login","login admin","/admin"};
        this.State = PanelState.Start;
        this._id = Handlers.PanelDeControlHandler;
    }
    /// <summary>  </summary>
    /// <param name="message">  </param>
    /// <returns>  </returns>

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
        switch (state)
        {
            case PanelState.Start:
                response = $"Ingresa tu nombre de usuario";
                this.Posiciones[message.From.Id] = PanelState.Username;
                break;
            case PanelState.Username:
                this.TempPanelInfo["adminusername"]=message.Text;
                response = $"Ingresa tu contraseña";
                this.Posiciones[message.From.Id] = PanelState.Password;
                break;
            case PanelState.Password:
                this.TempPanelInfo["adminpassword"]=message.Text;
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
                    response=$"{CatPrinter.Print(CatCatalog.GetCategorias(), admin)}\n¿Deseas realizar otra acción?\n1)Eliminar Categoría\n2)Agregar Categoría\n3)Cancelar";
                    break;
                case "2":
                    this.Posiciones[message.From.Id] = PanelState.VerOfertas;
                    if(message.Text.Equals("3"))
                    {
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        response = "Volviendo al inicio";
                    }
                    response=$"{OfPrinter.Print(OfCatalog.GetOfertasIgnoreId(), admin)} \n¿Deseas realizar otra acción?\n1)Eliminar Oferta";
                    break;
                
                case "3":
                    this.Posiciones[message.From.Id] = PanelState.VerSolicitudes;
                    response=$"{SolPrinter.Print(SolHandler.GetSolicitudes(admin), admin )}\n¿Deseas realizar otra acción?\n1)Eliminar Solicitud";
                    break;
                case "4":
                    this.Posiciones[message.From.Id] = PanelState.VerUsuarios;
                    response=$"{UsPrinter.Print(UsCatalog.GetUsuariosIgnoreId(), admin) }\n¿Deseas realizar otra acción?\n1)Eliminar Usuario";
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
                    switch(state){
                        case PanelState.EliminarCategoria:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        catremover=Int32.Parse(message.Text);
                        CatCatalog.RemoveCategoria(admin, CatCatalog.GetCategoriaById(catremover));
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
                    switch(state){
                        case PanelState.AgregarCategoria:
                        if(message.Text.Equals("5"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        this.TempPanelInfo["addcategoria"]=message.Text;
                        CatCatalog.AddCategoria(admin, this.TempPanelInfo["addcategoria"] );
                        response=$"Categoria añadida";
                        break;
                    }
                    break;
                case "3":
                    this.Posiciones[message.From.Id] = PanelState.PanelStart;
                    response = "Volviendo al inicio";
                break;
            }
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
                    switch(state){
                        case PanelState.EliminarOferta:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        ofremover=Int32.Parse(message.Text);
                        OfCatalog.DarDeBajaOferta(admin, ofremover);
                        response=$"Oferta eliminada";
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
                    switch(state){
                        case PanelState.EliminarSolicitud:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        solremover=Int32.Parse(message.Text);
                        SolCatalog.RemoveSolicitud(SolHandler.GetSolicitud(solremover));
                        response=$"Solicitud eliminada";
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
                    switch(state){
                        case PanelState.EliminarUsuario:
                        if(message.Text.Equals("cancelar"))
                        {
                            this.Posiciones[message.From.Id] = PanelState.Panel;
                            response = "Volviendo al inicio";
                        }
                        this.TempPanelInfo["usuarioeliminado"]=message.Text;
                        UsCatalog.RemoveUsuario(admin, UsCatalog.GetUsuarioById(this.TempPanelInfo["usuarioeliminado"]));
                        response=$"Usuario eliminado";
                        break;
                    }
                    break;
            }
            break;
            }
        }
    protected bool LoginAdminChecker()
    {
    foreach (Usuario user in UsCatalog.GetUsuarios()){

    if (user.Nick.Equals(this.TempPanelInfo["adminusername"]) && user.VerifyContraseña(this.TempPanelInfo["adminpassword"])){return true;}
    }
    return false;

    }
    }      
