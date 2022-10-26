namespace Library;

/// <summary> Clase <see cref="Solicitud"/> para iniciar una <see cref="OfertaDeServicio"/> </summary>
public class Solicitud {
    public OfertaDeServicio Oferta { get; set;}
    public Aceptacion Aceptada { get; set;}
    public DateTime FechaAceptada { get; set;}
    public Empleador Emp { get; set;}
    private DateTime FechaLimiteCalificar { get; set; }
    public TimeSpan TiempoMaximoCalificar = new TimeSpan(30, 0, 0, 0);

    /// <summary> Constructor de la clase <see cref="Solicitud"/> </summary>
    /// <returns> Retorna tipo <see cref="Solicitud.Solicitud(OfertaDeServicio, Empleador)"/> </returns>
    public Solicitud(OfertaDeServicio oferta, Empleador emp) {
        this.Oferta = oferta;
        this.Emp = emp;
    }

    /// <summary> Método que inicia un trabajo, settea la fecha que fue aceptada y la máxima para calificar
    /// También cambia la disponibilidad de la oferta </summary>
    public void IniciarTrabajo() {
        this.FechaAceptada = DateTime.Now;
        this.FechaLimiteCalificar = FechaAceptada.Add(TiempoMaximoCalificar);
        Oferta.Disponible = false;
    }

    /// <summary> Método para calificar una oferta </summary>
    /// <param name="rate"> Es un valor del enum <see cref="Calificacion"/> </param>
    public void Calificar(Calificacion rate) {
        Oferta.RateMe(rate);
    }

    /// <summary> Método para conocer el estado de una oferta </summary>
    /// <param name="siono"> Toma un valor del enum <see cref="Aceptacion"/> </param>
    public void RecibirRespuesta(Aceptacion siONo) {
        this.Aceptada = siONo;
    }

    /// <summary> Método para conocer si una oferta fue calificada </summary>
    /// <returns> Retorna True si la oferta está calificada o False si no lo está </returns>
    public bool IsRated() {
        return !(Oferta.GetCalificacion().Equals(Calificacion.NoCalificado));
    }

    /// <summary> Método para actualizar una calificación en caso que se haga dentro del plazo. Si excede la fecha límite se califica de forma neutral </summary>
    public void Update() {
        if (!this.IsRated())
        {
            // Si la oferta no fue calificada aún y ya pasó la fecha limite, calificamos como neutro (bueno)
            if (CanBeAutoRated())
            {
                this.Calificar(Calificacion.Bueno);
            }
        }
    }

    /// <summary> //TODO </summary>
    /// <returns>  </returns>
    public bool CanBeAutoRated() {
        // la fecha limite de calificacion se compara con la fecha actual
        // retornamos true si la fecha limite es anterior (-1) a la fecha actual
        return this.FechaLimiteCalificar.CompareTo(DateTime.Now).Equals(-1);
    }
}
