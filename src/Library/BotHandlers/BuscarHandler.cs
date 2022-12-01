using Telegram.Bot;
using Telegram.Bot.Types;
using System.Linq;
namespace Library.BotHandlers;

/// <summary> Muestra una lista de <see cref="OfertaDeServicio"/> disponibles según su ID y el trabajo. El usuario puede seleccionar
/// un filtro del <see cref="BuscarHandler"/> para cambiar el orden en el que se muestran las ofertas. Puede elegir una
/// oferta en concreto escribiendo su ID y esto le dará toda la información de la misma, en lo cual podrá crear una solicitud
/// para esa oferta o volver a la lista. </summary>
public class BuscarHandler : BaseHandler {
    /// <summary> Indica los filtros </summary>
    public enum BuscarState {
        Start,
        Filtro,
        Categoria,
        Distancia,
        Reputacion,
        VerOferta,
        Solicitar,
    }

    private string Oferta { get; set; }


    /// <summary> El estado del comando. </summary>
    public BuscarState State { get; set; }

    /// <summary> Diccionario para ver el estado en un handler de un usuario según su ID. </summary>
    /// <typeparam name="long"> ID de usuario de Telegram. </typeparam>
    /// <typeparam name="BuscarState"> Estado en un <see cref="IHandler"/>. </typeparam>
    private Dictionary<long, BuscarState> Posiciones = new Dictionary<long, BuscarState>();
    private PlainTextOfertasPrinter ofPrinter = new();
    private PlainTextCategoriaPrinter catPrinter = new();
    private CategoriasCatalog catCatalog = CategoriasCatalog.GetInstance();
    private OfertasHandler ofHandler = OfertasHandler.GetInstance();
    private SearchHandler seaHandler = new SearchHandler();
    private int catId;
    private Calificacion calif { get; set; }

    /// <summary> Inicializa una nueva instancia de la clase <see cref="BuscarHandler"/>. </summary>
    /// <param name="next"> Un buscador de direcciones. </param>
    /// <param name="next"> El próximo "handler". </param>
    public BuscarHandler(BaseHandler next) : base(next) {
        this.Keywords = new string[] {"buscar", "/buscar"};
        this.State = BuscarState.Start;

    }

    /// <summary> Verifica si el mensaje puede ser procesado por el <see cref="IHandler"/>. </summary>
    /// <param name="message"> Mensaje a procesar. </param>
    /// <returns> true si puede procesar el mensaje, false en caso contrario. </returns>
    protected override bool CanHandle(Message message)
    {
        if (!this.Posiciones.ContainsKey(message.From.Id))
        {
            this.Posiciones[message.From.Id] = BuscarState.Start;
        }

        switch (this.Posiciones[message.From.Id])
        {
            case BuscarState.Start:
                return base.CanHandle(message);
            default:
                return true;
        }
    }

    /// <summary> Procesamiento del mensaje recibido. </summary>
    /// <param name="message"> Mensaje recibido. </param>
    /// <param name="response"> Respuesta al mensaje. </param>
    protected override void InternalHandle(Message message, out string response) {
        if (message == null || message.From == null) {
            throw new Exception("No se recibió un mensaje");
        }

        response = "Error desconocido";

        this.State = this.Posiciones[message.From.Id];

        switch(State) {
            case BuscarState.Start:
                this.Posiciones[message.From.Id] = BuscarState.Filtro;
                response = $"Filtrar por:\n1) Categoria\n2) Distancia\n3) Reputación";
                break;
            case BuscarState.Filtro:
                switch(message.Text) {
                    case "1":
                        this.Posiciones[message.From.Id] = BuscarState.Categoria;
                        response = $"{catPrinter.Print(catCatalog.GetCategorias())}\nEstas son las categorias, ingresa el ID de las ofertas que quieres ver en dicha categoria"; //TODO implementar printers
                        break;
                    case "2":
                        this.Posiciones[message.From.Id] = BuscarState.Distancia;
                        response = "imaginate que aca hay una lista filtrada por distancia \nque oferta ver?";
                        break;
                    case "3":
                        this.Posiciones[message.From.Id] = BuscarState.Reputacion;
                        response = "Por que reputación quieres buscar?\n1)Deficiente\n2)Regular\n3)Bueno\n4)Muy Bueno\n5)Sobresaliente";
                        break;
                    case "volver":
                        this.Posiciones[message.From.Id] = BuscarState.Filtro;
                        break;
                    default:
                        response = "Verifique que el estado ingresado sea correcto";
                        this.Posiciones[message.From.Id] = BuscarState.Filtro;
                        response = $"Filtrar por:\n1) Categoria\n2) Distancia\n3) Reputación";
                        break;
                }
                break;
            case BuscarState.Categoria:
                this.Posiciones[message.From.Id] = BuscarState.VerOferta;
                catId= Int32.Parse(message.Text);
                response = $"{ofPrinter.Print(seaHandler.FiltrarCategoria(ofHandler.GetCategoriaById(catId)) )}";  //TODO falta implementar printer
                break;
            case BuscarState.Distancia:
                this.Posiciones[message.From.Id] = BuscarState.VerOferta;
                this.Oferta = message.Text;
                response = $"ver oferta \"{this.Oferta}\", desea solicitarla?";  //TODO falta implementar printer
                break;
            case BuscarState.Reputacion:
                SearchHandler inst = new();
                var filtered = inst.FiltrarReputacion();
                List<OfertaDeServicio> filteredByCalif = new();
                ITextPrinter<OfertaDeServicio> printer = new PlainTextOfertasPrinter();
                var cal = Int32.Parse(message.Text);
                if (new string[]{"1", "2", "3", "4", "5"}.Contains(message.Text))
                {
                    filteredByCalif = filtered.Where(x => x.GetReputacion() == ((Calificacion)(cal))).ToList();
                }
                response = $"Ofertas por reputación iguales a {((Calificacion)(cal)).ToString()}:\n"
                + printer.Print(filteredByCalif);
                // response = $"{ofPrinter.Print(seaHandler.FiltrarReputacion())}"; //TODO fixear metodo
                break;
            case BuscarState.VerOferta:
                response = "ok";  //TODO ver esto
                this.Posiciones[message.From.Id] = BuscarState.Start;
                break;
            default:
                response = "a";
                break;
        }
        Console.WriteLine(response);
    }
}
