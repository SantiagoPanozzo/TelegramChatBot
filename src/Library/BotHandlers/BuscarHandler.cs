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
    VerOferta
}

/// <summary> El estado del comando. </summary>
public BuscarState state { get; set; }

private Dictionary<long, BuscarState> Posiciones = new Dictionary<long, BuscarState>();

/// <summary> Inicializa una nueva instancia de la clase <see cref="BuscarHandler"/>. </summary>
/// <param name="next">Un buscador de direcciones.</param>
/// <param name="next">El próximo "handler".</param>
public BuscarHandler(BaseHandler next) : base(next) {
    this.Keywords = new string[] {"buscar", "/buscar"};
    this.state = BuscarState.Start;
}

/// <summary>  </summary>
/// <param name="message">  </param>
/// <returns>  </returns>
protected override bool CanHandle(Message message) {
    if (this.state ==  BuscarState.Start) {
        return base.CanHandle(message);
    }
    else if (this.state ==  BuscarState.Filtro) {
        return base.CanHandle(message);
    }
    else {
        return true;
    }
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
            response = $"Filtrar por:\n1) Categoria\n2) Distancia\n3) Reputación";
            break;
        case BuscarState.Filtro:
            switch(message.Text) {
                case "1":
                    this.Posiciones[message.From.Id] = BuscarState.Categoria;
                    response = "categoria";
                    break;
                case "2":
                    this.Posiciones[message.From.Id] = BuscarState.Distancia;
                    break;
                case "3":
                    this.Posiciones[message.From.Id] = BuscarState.Reputacion;
                    break;
                default:
                    response = "Verifique que el estado ingresado sea correcto";
                    break;
            }
            break;
        case BuscarState.Categoria:
            this.Posiciones[message.From.Id] = BuscarState.VerOferta;
            response = "categoria";  //TODO falta implementar printer
            break;
        case BuscarState.Distancia:
            this.Posiciones[message.From.Id] = BuscarState.VerOferta;
            response = "distancia";  //TODO falta implementar printer
            break;
        case BuscarState.Reputacion:
            this.Posiciones[message.From.Id] = BuscarState.VerOferta;
            response = "reputacion";  //TODO falta implementar printer
            break;
        case BuscarState.VerOferta:
            response = "ver oferta";  //TODO ver esto
            break;
        default:
            response = "a";
            break;
        }
    }
}
