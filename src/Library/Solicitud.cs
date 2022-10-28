namespace Library;

/// <summary> Clase <see cref="Solicitud"/> para iniciar una <see cref="OfertaDeServicio"/> </summary>
public class Solicitud {
    public OfertaDeServicio Oferta { get; set;}
    public Aceptacion Aceptada { get; set;}
    public DateTime FechaAceptada { get; set;}
    private Empleador Emp { get; set;}
    private Trabajador Trab { get; set; }
    private DateTime FechaLimiteCalificar { get; set; }
    public TimeSpan TiempoMaximoCalificar = new TimeSpan(30, 0, 0, 0);
    private static int Instancias { get; set; } = 0;
    private int _id; // TODO implementar IDs, placeholder

    /// <summary> Constructor de la clase <see cref="Solicitud"/> </summary>
    /// <returns> Retorna tipo <see cref="Solicitud.Solicitud(OfertaDeServicio, Empleador)"/> </returns>
    public Solicitud(OfertaDeServicio oferta, Empleador emp) {
        this.Oferta = oferta;
        this.Emp = emp;
        Solicitud.Instancias++;
        this._id = Instancias;
    }

    /// <summary> Método para obtener el id de una <see cref="Solicitud"/> </summary>
    /// <returns> Devuelve el id de <see cref="Solicitud"/> </returns>
    public int GetId()
    {
        return this._id;
    }

    /// <summary> Método para obtener <see cref="Empleador"/> que busca la <see cref="Solicitud"/> </summary>
    /// <returns> Devuelve el <see cref="Empleador"/> de una <see cref="Solicitud"/> </returns>
    public Empleador GetEmpleador()
    {
        return this.Emp;
    }

    /// <summary> Método para obtener <see cref="Trabajador"/> que busca la <see cref="Solicitud"/> </summary>
    /// <returns> Devuelve el <see cref="Trabajador"/> de una <see cref="Solicitud"/> </returns>
    public Trabajador GetTrabajador()
    {
        return this.Trab;
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

    /// <summary> Método para actualizar una calificación en caso que se haga dentro del plazo.
    /// Si excede la fecha límite se califica de forma neutral </summary>
    public void Update() {
        if (!this.IsRated())
        {
            /// <remarks> Si la oferta no fue calificada aún y ya pasó la fecha limite, 
            /// calificamos como neutro <see cref="Calificacion.Bueno"> </remarks>
            if (CanBeAutoRated())
            {
                this.Calificar(Calificacion.Bueno);
            }
        }
    }

    /// <summary> Compara la fecha actual con la fecha límite para calificar </summary>
    /// <returns> Devuelve true si ya pasó un mes (30 días) desde que se hizo la <see cref="Solicitud"> </returns>
    public bool CanBeAutoRated() {
        // la fecha limite de calificacion se compara con la fecha actual
        // retornamos true si la fecha limite es anterior (-1) a la fecha actual
        return this.FechaLimiteCalificar.CompareTo(DateTime.Now).Equals(-1);
    }
}
