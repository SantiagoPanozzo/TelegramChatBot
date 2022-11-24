using Telegram.Bot.Types;
using System;
namespace Library;

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

    private Dictionary<long, BuscarState> Posiciones = new Dictionary<long, BuscarState>();

    /// <summary> Inicializa una nueva instancia de la clase <see cref="BuscarHandler"/>. </summary>
    /// <param name="next">Un buscador de direcciones.</param>
    /// <param name="next">El próximo "handler".</param>
    public BuscarHandler(BaseHandler next) : base(next) {
        this.Keywords = new string[] {"buscar", "/buscar"};
        this.State = BuscarState.Start;
    }

    /// <summary>  </summary>
    /// <param name="message">  </param>
    /// <returns>  </returns>
    protected override bool CanHandle(Message message) {
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
        // return this.Keywords.Any(s => message.Text.Equals(s, StringComparison.InvariantCultureIgnoreCase));
        /*
        if (this.State ==  BuscarState.Start) {
            return base.CanHandle(message);
        }
        else if (this.State ==  BuscarState.Filtro)
        {
            return true;
        }
        else {
            return false;
        }
        */
    }

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
                        response = "imaginate que aca hay una lista filtrada por categoria \nque oferta ver?";
                        break;
                    case "2":
                        this.Posiciones[message.From.Id] = BuscarState.Distancia;
                        response = "imaginate que aca hay una lista filtrada por distancia \nque oferta ver?";
                        break;
                    case "3":
                        this.Posiciones[message.From.Id] = BuscarState.Reputacion;
                        response = "imaginate que aca hay una lista filtrada por reputacion \nque oferta ver?";
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
                this.Oferta = message.Text;
                response = $"ver oferta \"{this.Oferta}\", desea solicitarla?";  //TODO falta implementar printer
                break;
            case BuscarState.Distancia:
                this.Posiciones[message.From.Id] = BuscarState.VerOferta;
                this.Oferta = message.Text;
                response = $"ver oferta \"{this.Oferta}\", desea solicitarla?";  //TODO falta implementar printer
                break;
            case BuscarState.Reputacion:
                this.Posiciones[message.From.Id] = BuscarState.VerOferta;
                this.Oferta = message.Text;
                response = $"ver oferta \"{this.Oferta}\", desea solicitarla?";  //TODO falta implementar printer
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
