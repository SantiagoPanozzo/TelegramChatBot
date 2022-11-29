using Telegram.Bot.Types;

namespace Library.BotHandlers;

/// <summary> Permite a un <see cref="Trabajador"/> crear una oferta de servicio, mediante lo cual se le pedirá la categoría en la
/// cual crear la oferta, y luego la información de la misma. </summary>
public class OfertarHandler : BaseHandler
{
    public enum OfertarStates 
    {
        Start,
        AskCategory,
        AskDescription,
        AskJobType,
        AskPrice,
        Fin
    }
    protected Dictionary<long, OfertarStates> posiciones = new();
    protected Dictionary<long, Dictionary<string, string>> tempInfo = new();
    public OfertarHandler(BaseHandler next): base(next)
    {
        Keywords = new string[] {"ofertar", "/ofertar"};
        _id = Handlers.OfertarHandler;
    }
    protected override bool CanHandle(Message message)
    {
        if (!posiciones.ContainsKey(message.From.Id)) posiciones[message.From.Id] = OfertarStates.Start;

        return (posiciones[message.From.Id] == OfertarStates.Start) ? base.CanHandle(message) : true;
    }

    protected override void InternalHandle(Message message, out string response)
    {
        if (message == null || string.IsNullOrEmpty(message.Text) || message.From is null)
        {
            throw new Exception("Mensaje u origen del mismo no existentes");
        }

        response = "Error desconocido";

        OfertarStates state = posiciones[message.From.Id];

        Trabajador user;
        if (HandlerHandler.CachedLogins.ContainsKey(message.From.Id)) user = (Trabajador)HandlerHandler.CachedLogins[message.From.Id]; else return;

        response = "El usuario loggeado debe ser un Trabajador para poder ofertar un servicio.";

        if (user.GetTipo() == TipoDeUsuario.Trabajador){
            switch (state)
            {
                case OfertarStates.Start:
                    posiciones[message.From.Id] = OfertarStates.AskCategory;
                    response = "Ingrese la ID de la categoría, si no la conoce, puede visualizar las categorías disponibles con /vercategorias";
                    break;
                case OfertarStates.AskCategory:
                    posiciones[message.From.Id] = OfertarStates.AskDescription;
                    tempInfo[message.From.Id].Add("Category", message.Text);
                    response = "Ingrese una descripción para la oferta";
                    break;
                case OfertarStates.AskDescription:
                    posiciones[message.From.Id] = OfertarStates.AskJobType;
                    tempInfo[message.From.Id].Add("Description", message.Text);
                    response = "Ingrese el tipo de empleo";
                    break;
                case OfertarStates.AskJobType:
                    posiciones[message.From.Id] = OfertarStates.AskPrice;
                    tempInfo[message.From.Id].Add("Empleo", message.Text);
                    response = "Ingrese el precio de su oferta";
                    break;
                case OfertarStates.AskPrice:
                    posiciones[message.From.Id] = OfertarStates.Fin;
                    tempInfo[message.From.Id].Add("Price", message.Text);
                    response = "Oferta realizada";
                    break;
                case OfertarStates.Fin:
                    posiciones[message.From.Id] = OfertarStates.Start;
                    var inst = OfertasHandler.GetInstance();
                    var catId = Int32.Parse(tempInfo[message.From.Id]["Category"]);
                    var desc = tempInfo[message.From.Id]["Description"];
                    var job = tempInfo[message.From.Id]["Empleo"];
                    var price = double.Parse(tempInfo[message.From.Id]["Price"]);
                    inst.Ofertar(catId, user, desc, job, price);
                    break;
                default:
                    response = "Error desconocido (switch)";
                    break;
            }   
        }
    }
}