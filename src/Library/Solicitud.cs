namespace Library;

public class Solicitud {
    public OfertaDeServicio Oferta {get; set;}
    public Aceptacion Aceptada {get; set;}
    public DateTime FechaAceptada {get; set;}
    public Empleador Emp {get; set;}

    public Solicitud(OfertaDeServicio oferta, Empleador emp) {
        this.Oferta = oferta;
        this.Emp = emp;
    }

    public enum Aceptacion {
        Pendiente,
        Aceptada,
        Rechazada
    }

    public void iniciarTrabajo() {
        this.FechaAceptada = DateTime.Now;
        Oferta.Disponible = false;
    }

    public void calificar(Calificacion rate) {
        Oferta.RateMe(rate);
    }

    public void recibirRespuesta(Aceptacion siono) {
        this.Aceptada = siono;
    }

    public bool isRated() {
        if (Oferta.getCalificacion != null ) {
            return true;
        }
        else {
            return false;
        }
    }
}
