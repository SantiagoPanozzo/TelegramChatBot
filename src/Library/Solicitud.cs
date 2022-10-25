namespace Library;

public class Solicitud {
    public OfertaDeServicio Oferta { get; set;}
    public Aceptacion Aceptada { get; set;}
    public DateTime FechaAceptada { get; set;}
    public Empleador Emp { get; set;}
    private DateTime FechaLimiteCalificar { get; set; }
    public TimeSpan TiempoMaximoCalificar = new TimeSpan(30, 0, 0, 0);

    public Solicitud(OfertaDeServicio oferta, Empleador emp) {
        this.Oferta = oferta;
        this.Emp = emp;
    }

    public void IniciarTrabajo() {
        this.FechaAceptada = DateTime.Now;
        this.FechaLimiteCalificar = FechaAceptada.Add(TiempoMaximoCalificar);
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

    public void Update()
    {
        if (!this.IsRated())
        {
            // Si la oferta no fue calificada aún y ya pasó la fecha limite, calificamos como neutro (bueno)
            if (CanBeAutoRated())
            {
                this.Calificar(Calificacion.Bueno);
            }
        }
    }

    public bool CanBeAutoRated()
    {
        // la fecha limite de calificacion se compara con la fecha actual
        // retornamos true si la fecha limite es anterior (-1) a la fecha actual
        return this.FechaLimiteCalificar.CompareTo(DateTime.Now).Equals(-1);
    }
}
