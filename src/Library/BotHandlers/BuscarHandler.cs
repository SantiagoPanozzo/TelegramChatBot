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
    switch (this.State)
    {
        case BuscarState.Start:
            return base.CanHandle(message);
        case BuscarState.Filtro:
            return true;
        case BuscarState.Distancia:
            return true;
        case BuscarState.Categoria:
            return true;
        case BuscarState.Reputacion:
            return true;
        case BuscarState.Solicitar:
            return true;
        default:
            break;
    }
    return this.Keywords.Any(s => message.Text.Equals(s, StringComparison.InvariantCultureIgnoreCase));
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

    if (!this.Posiciones.ContainsKey(message.From.Id)) {
        this.Posiciones.Add(message.From.Id, BuscarState.Start);
    }

    response = "Error desconocido";
    
    BuscarState state = this.Posiciones[message.From.Id];

    switch(state) {
        case BuscarState.Start:
            this.Posiciones[message.From.Id] = BuscarState.Filtro;
            response = $"Filtrar por:\n1) Categoria\n2) Distancia\n3) Reputación {this.Posiciones[message.From.Id]}";
            break;
        case BuscarState.Filtro:
            switch(message.Text) {
                case "1":
                    this.Posiciones[message.From.Id] = BuscarState.Categoria;
                    response = "categoria";
                    break;
                case "2":
                    this.Posiciones[message.From.Id] = BuscarState.Distancia;
                    response = "distancia";
                    break;
                case "3":
                    this.Posiciones[message.From.Id] = BuscarState.Reputacion;
                    response = "reputacion";
                    break;
                case "volver":
                    this.Posiciones[message.From.Id] = BuscarState.Filtro;
                    break;
                default:
                    response = "Verifique que el estado ingresado sea correcto";
                    this.Posiciones[message.From.Id] = BuscarState.Filtro;
                    response = $"Filtrar por:\n1) Categoria\n2) Distancia\n3) Reputación {this.Posiciones[message.From.Id]}";
                    break;
            }
            Console.WriteLine(response);
            break;
        case BuscarState.Categoria:
            this.Posiciones[message.From.Id] = BuscarState.VerOferta;
            this.Oferta = message.Text;
            response = "categoria";  //TODO falta implementar printer
            break;
        case BuscarState.Distancia:
            this.Posiciones[message.From.Id] = BuscarState.VerOferta;
            this.Oferta = message.Text;
            response = "distancia";  //TODO falta implementar printer
            break;
        case BuscarState.Reputacion:
            this.Posiciones[message.From.Id] = BuscarState.VerOferta;
            this.Oferta = message.Text;
            response = "reputacion";  //TODO falta implementar printer
            break;
        case BuscarState.VerOferta:
            response = $"ver oferta {this.Oferta}, desea solicitarla?";  //TODO ver esto
            break;
        case BuscarState.Solicitar:
            response = "Ok";
            this.Posiciones[message.From.Id] = BuscarState.Filtro;
            response = $"Filtrar por:\n1) Categoria\n2) Distancia\n3) Reputación {this.Posiciones[message.From.Id]}";
            break;
        default:
            response = "a";
            break;
        }
    }
}
