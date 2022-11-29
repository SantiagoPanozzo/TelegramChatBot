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
    PlainTextCategoriaPrinter CatPrinter = new();
    PlainTextSolicitudPrinter SolPrinter = new();
    PlainTextCategoriaPrinter OfPrinter = new();
    ContratoHandler SolCatalog = ContratoHandler.GetInstance();
    CategoriasCatalog CatCatalog = CategoriasCatalog.GetInstance();

    static Administrador admin = new("a","b","c","d");
    int catremove;

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
                switch (message.Text)
                {
                    case "contraseña correcta":
                        response = $"Iniciando sesion...\n\nElige una acción:\n1) Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios";
                        this.Posiciones[message.From.Id] = PanelState.Panel;
                        // this.Posiciones[message.From.Id] = InicioState.Start;
                        break;
                    case "!contraseña correcta":
                        response = $"Nombre de usuario o contraseña incorrecta, vuelve a intentarlo\n\nIngresa tu nombre de usuario";
                        this.Posiciones[message.From.Id] = PanelState.Username;
                        break;
                }
            break;
            default:
                response = "Error desconocido, /admin para volver a logearte";
                this.Posiciones[message.From.Id] = PanelState.Start;
                break;
        case PanelState.PanelStart:
            this.Posiciones[message.From.Id] = PanelState.Panel;
            response = $"Elige una acción:\n1) Ver Categorías\n2)Ver Ofertas\n3)Ver Solicitudes\n4)Ver Usuarios";
            break;
        case PanelState.Panel:
            switch(message.Text) {
                case "1":
                    this.Posiciones[message.From.Id] = PanelState.VerCategorias;
                    response=$"{CatPrinter.Print(CatCatalog.GetCategorias(), admin)}\n¿Deseas realizar otra acción?\n1)Eliminar Categoría\n2)Agregar Categoría";
                    break;
                case "2":
                    this.Posiciones[message.From.Id] = PanelState.VerOfertas;
                    response=$"\n¿Deseas realizar otra acción?\n1)Eliminar Oferta";
                    break;
                
                case "3":
                    this.Posiciones[message.From.Id] = PanelState.VerSolicitudes;
                    response=$"{SolPrinter.Print(SolCatalog.GetSolicitudes(admin), admin )}\n¿Deseas realizar otra acción?\n1)Eliminar Solicitud";
                    break;
                case "4":
                    this.Posiciones[message.From.Id] = PanelState.VerUsuarios;
                    response=$"-Lista de usuarios\n¿Deseas realizar otra acción?\n1)Eliminar Usuario";
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
                    switch(state){
                        case PanelState.EliminarCategoria:
                        catremove=Int32.Parse(message.Text);
                        CatCatalog.RemoveCategoria(admin, CatCatalog.GetCategoriaById(catremove) );
                        break;
                    }
                    break;
               case "2":
                    response=$"Ingresa la descripción de la categoría";
                    this.Posiciones[message.From.Id] = PanelState.AgregarCategoria;
                    switch(state){
                        case PanelState.AgregarCategoria:
                        this.TempPanelInfo["addcategoria"]=message.Text;
                        CatCatalog.AddCategoria(admin, this.TempPanelInfo["addcategoria"] );
                        break;
                    }
                    break;
            }
            break;
        case PanelState.VerOfertas:
            switch(message.Text) {
                case "1":
                    response=$"¿Que oferta deseas eliminar?";
                    this.Posiciones[message.From.Id] = PanelState.VerOfertas;
                    break;
            }
            break;
        case PanelState.VerSolicitudes:
            switch(message.Text) {
                case "1":
                    response=$"¿Que solicitud deseas eliminar?";
                    this.Posiciones[message.From.Id] = PanelState.VerSolicitudes;
                    break;
            }
            break;

        case PanelState.VerUsuarios:
            switch(message.Text) {
                case "1":
                    response=$"¿Que usuario deseas eliminar?";
                    this.Posiciones[message.From.Id] = PanelState.VerUsuarios;
                    break;
            }
            break;
            }
        Console.WriteLine(string.Join(", ", TempPanelInfo));

        }
    }      
