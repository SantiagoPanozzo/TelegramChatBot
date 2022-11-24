using Telegram.Bot.Types;
using System;
namespace Library.BotHandlers;

/// <summary>
/// Dependiendo del <see cref="TipoDeUsuario"/> del <see cref="Usuario"/> muestra distintas opciones.
/// Para un <see cref="Trabajador"/> muestra <see cref="OfertarServicioHandler"/>, <see cref="VerOfertasHandler"/>,
/// <see cref="VerInfoHandler"/> y <see cref="VerSolicitudesHandler"/>.
/// Para un <see cref="Empleador"/> muestra <see cref="VerContratosHandler"/>, <see cref="VerInfoHandler"/> y
/// y <see cref="BuscarHandler"/>
/// </summary>
public class InicioHandler : BaseHandler {
    // Dependiendo del tipo muestra opciones
    // trabajador muestra Ofertar Servicio, Ver mis ofertas, Ver mi informacion, Ver mis solicitudes pendientes
    // empleador muestra Ver mis contratos, Ver mi informacion, Buscar
    public enum InicioState {
        Start,
        Empleador,
        Trabajador,
        Identificarse,
        TrabajadorQueHago,
        EmpleadorQueHago
    }

    /// <summary> El estado del comando. </summary>
    public InicioState State { get; set; }

    /// <summary> Inicializa una nueva instancia de la clase <see cref="InicioHandler"/>. Esta clase procesa el mensaje "Información". </summary>
    /// <param name="next">El próximo "handler".</param>
    public InicioHandler(BaseHandler next) : base(next) {
        this.Keywords = new string[] {"/inicio", "inicio"};
        this.State = InicioState.Start;
    }

    private Dictionary<long, InicioState> Posiciones = new Dictionary<long, InicioState>();

    /// <summary>  </summary>
    /// <param name="message">  </param>
    /// <returns>  </returns>
    protected override bool CanHandle(Message message) {
        if (!this.Posiciones.ContainsKey(message.From.Id)) {
            this.Posiciones[message.From.Id] = InicioState.Start;
        }
        switch (this.Posiciones[message.From.Id]) {
            case InicioState.Start:
                return base.CanHandle(message);
            default:
                return true;
        }
    }

    protected override void InternalHandle(Message message, out string response) {
        if (message == null || message.From == null) {
            throw new Exception("No se recibió un mensaje");
        }
        
        response = "Error desconocido";
        
        this.State = this.Posiciones[message.From.Id];

        switch(State) {
            case InicioState.Start:
                this.Posiciones[message.From.Id] = InicioState.Identificarse;
                response = "Identificarse como:\n1) Empleador\n2) Trabajador";
                break;

            case InicioState.Identificarse:
                switch(message.Text) {
                    case "1":
                        this.Posiciones[message.From.Id] = InicioState.Empleador;
                        response = "imaginate que aca hay una lista filtrada por categoria \nque oferta ver?";
                        break;
                    case "2":
                        this.Posiciones[message.From.Id] = InicioState.Trabajador;
                        response = "imaginate que aca hay una lista filtrada por distancia \nque oferta ver?";
                        break;
                    default:
                        response = "Verifique que la identificación sea correcta";
                        this.Posiciones[message.From.Id] = InicioState.Identificarse;
                        response = "Identificarse como:\n1) Empleador\n2) Trabajador";
                        break;
                }
                break;
            case InicioState.Trabajador:
                this.Posiciones[message.From.Id] = InicioState.TrabajadorQueHago;
                response = $"¿Que operación desea realizar?\n1) Ofertar un servicio\n2) Ver mis ofertas\n3) Ver mis datos personales\n4) Ver mis solicitudes pendientes";  //TODO falta implementar printer
                break;

                case InicioState.TrabajadorQueHago:
                    switch (message.Text) {
                        case "1":
                            response = "Eliga una categoría de las siguientes para ofertar\n (Lista de las ofertas) ";
                            switch (message.Text) {
                                case "":
                                    break;
                                default:
                                    response = "Asegurese de que la categoría seleccionada exista\nEliga una categoría de las siguientes para ofertar\n (Lista de las ofertas) ";
                                    break;
                            }
                            break;

                        case "2":
                            response = "";
                            break;

                        case "3":
                            response = "(Información acá)\n\n¿Desea realizar alguna operación más?\n1) Darse de baja\n2) No";
                            switch (message.Text) {
                                case "1":
                                    response = "Ingrese a continuación su contraseña\n» ";
                                    // TODO Verificación de contraseña
                                    break;
                                case "2":
                                    break;
                            }
                            break;

                        case "4":
                            response = "(Solicitudes pendientes)";
                            break;

                        default:
                            response = "Asegurese de que la opción elegida sea valida";
                            this.Posiciones[message.From.Id] = InicioState.TrabajadorQueHago;
                            break;
                    }
                    break;

            case InicioState.Empleador:
                this.Posiciones[message.From.Id] = InicioState.EmpleadorQueHago;
                response = $"¿Que operación desea realizar?\n1) Ver mis contratos\n2) Ver mis datos personales\n3) Buscar ofertas";
                break;

                case InicioState.EmpleadorQueHago:
                    switch (message.Text) {
                        case "1":
                            response = "(Lista de contratos)";  //TODO Mostrar lista de contratos
                            
                            break;

                        case "2":
                            response = "(Datos personales)"; //TODO Mostrar datos
                            break;

                        case "3":
                            response = "(Buscador ofertas)";
                            
                            break;

                        default:
                            response = "Asegurese de que la opción elegida sea valida";
                            this.Posiciones[message.From.Id] = InicioState.EmpleadorQueHago;
                            break;
                    }
                    break;
            }
        Console.WriteLine(response);
    }
}
