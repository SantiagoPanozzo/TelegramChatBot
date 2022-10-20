namespace Library;

public class Solicitud {
    public OfertaDeServicio Oferta { get; set;}
    public Aceptacion Aceptada { get; set;}
    public DateTime FechaAceptada { get; set;}
    public Empleador Emp { get; set;}

    public Solicitud(OfertaDeServicio oferta, Empleador emp) {
        this.Oferta = oferta;
        this.Emp = emp;
    }

    public void IniciarTrabajo() {
        this.FechaAceptada = DateTime.Now;
        Oferta.Disponible = false;
    }

    public void Calificar(Calificacion rate) {
        Oferta.RateMe(rate);
    }

    public void RecibirRespuesta(Aceptacion siono) {
        this.Aceptada = siono;
    }

    public bool IsRated()
    {
        return !(Oferta.GetCalificacion().Equals(Calificacion.NoCalificado));
    }
}
