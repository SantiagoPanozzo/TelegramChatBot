namespace Library;

/// <summary> Clase <see cref="Solicitud"/> para iniciar una <see cref="OfertaDeServicio"/> </summary>
public class Solicitud : IDesactivable,IActualizable{
    public OfertaDeServicio Oferta { get; set;}
    public Aceptacion Aceptada { get; set;}
    public DateTime FechaAceptada { get; set;}
    private Empleador Emp { get; set;} 
    public string Trab { get; set; }
    private DateTime FechaLimiteTrabajador { get; set; }
    private DateTime FechaLimiteEmpleador { get; set; }
    public TimeSpan TiempoMaximoCalificar = new (30, 0, 0, 0);
    private static int Instancias { get; set; } = 0;
    private readonly int _id;
    private bool Activa;
    private Calificacion EmpleadorRate { get; set; }

    /// <summary> Constructor de la clase <see cref="Solicitud"/> </summary>
    /// <returns> Retorna tipo <see cref="Solicitud.Solicitud(OfertaDeServicio, Empleador)"/> </returns>
    public Solicitud(OfertaDeServicio oferta, Empleador emp) {
        this.Oferta = oferta;
        this.Emp = emp;
        Solicitud.Instancias++;
        this._id = Instancias;
        this.Trab = oferta.GetOfertante();
        this.Activa = true;
        EmpleadorRate = Calificacion.NoCalificado;
    }

    /// <summary> Método para obtener el id de una <see cref="Solicitud"/> </summary>
    /// <returns> Devuelve el id de <see cref="Solicitud"/> </returns>
    public int GetId()
    {
        return this._id;
    }

    /// <summary> Método para obtener <see cref="Empleador"/> que busca la <see cref="Solicitud"/> </summary>
    /// <returns> Devuelve el <see cref="Empleador"/> de una <see cref="Solicitud"/> </returns>
    public string GetEmpleador()
    {
        return this.Emp.Nick;
    }

    /*
    LEGACY
     
    /// <summary> Método para obtener <see cref="Trabajador"/> que busca la <see cref="Solicitud"/> </summary>
    /// <returns> Devuelve el <see cref="Trabajador"/> de una <see cref="Solicitud"/> </returns>
    public Trabajador GetTrabajador()
    {
        return this.Trab;
    } */

    /// <summary> Método que inicia un trabajo, settea la fecha que fue aceptada y la máxima para calificar
    /// También cambia la disponibilidad de la oferta </summary>
    public void IniciarTrabajo() {
        this.FechaAceptada = DateTime.Now;
        Oferta.Disponible = false;
    }

    /// <summary> Método para calificar una oferta </summary>
    /// <param name="rate"> Es un valor del enum <see cref="Calificacion"/> </param>
    public void CalificarTrabajador(Empleador user, Calificacion rate) {
        if(user.Nick.Equals(Emp.Nick) && !IsRated())
        {
            Oferta.RateMe(rate);
            this.FechaLimiteTrabajador = FechaAceptada.Add(TiempoMaximoCalificar);
        }
    }
    
    public void CalificarEmpleador(Trabajador user, Calificacion rate)
    {
        if(user.Nick.Equals(Trab) && !IsEmpleadorRated())
        {
            EmpleadorRate = rate;
            Emp.Calificar(rate);
            this.FechaLimiteEmpleador = FechaAceptada.Add(TiempoMaximoCalificar);
        }
    }
    
    public Calificacion GetEmpleadorRate()
    {
        return EmpleadorRate;
    }

    public Calificacion GetTrabajadorRate()
    {
        return Oferta.GetCalificacion();
    }
    
    /// <summary> Método para conocer el estado de una oferta </summary>
    /// <param name="siono"> Toma un valor del enum <see cref="Aceptacion"/> </param>
    public void RecibirRespuesta(Aceptacion siONo) {
        this.Aceptada = siONo;
        if (this.Aceptada.Equals(Aceptacion.Aceptada))
        {
            IniciarTrabajo();
        }
    }

    /// <summary> Método para conocer si una oferta fue calificada </summary>
    /// <returns> Retorna True si la oferta está calificada o False si no lo está </returns>
    public bool IsRated() {
        return !(Oferta.GetCalificacion().Equals(Calificacion.NoCalificado));
    }

    public bool IsEmpleadorRated()
    {
        return (!this.EmpleadorRate.Equals(Calificacion.NoCalificado));
    }
    
    public bool IsActive()
    {
        return this.Activa;
    }

    public void DarDeBaja(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.Activa = false;
        }
    }
    
    public void Reactivar(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.Activa = true;
        }
    }

    /// <summary> Método para obtener la ubicación de una oferta </summary>
    /// <returns> Devuelve en formato de coordenadas la ubicación de la oferta </returns>
    public Tuple<double, double> GetUbicacion()
    {
        return this.Oferta.GetUbicacion();
    }

    /// <summary> Método para actualizar una calificación en caso que se haga dentro del plazo.
    /// Si excede la fecha límite se califica de forma neutral </summary>
    public void Update(DateTime fechaActual) {
        /// <remarks> Si la oferta no fue calificada aún y ya pasó la fecha limite, 
        /// calificamos como neutro <see cref="Calificacion.Bueno"> </remarks>
        if (!this.IsRated() && CanTrabajadorBeAutoRated(fechaActual))
        {
            Oferta.RateMe(Calificacion.Bueno);
        }
        if (!this.IsEmpleadorRated() && CanEmpleadorBeAutoRated(fechaActual))
        {
            this.Emp.Calificar(Calificacion.Bueno);
            this.EmpleadorRate = Calificacion.Bueno;
        }
    }

    /// <summary> Compara la fecha actual con la fecha límite para calificar </summary>
    /// <returns> Devuelve true si ya pasó un mes (30 días) desde que se hizo la <see cref="Solicitud">, de lo contrario devuelve false </returns>
    public bool CanTrabajadorBeAutoRated(DateTime fechaActual) {
        return this.FechaLimiteTrabajador.CompareTo(fechaActual).Equals(-1);
    }
    
    public bool CanEmpleadorBeAutoRated(DateTime fechaActual) {
        return this.FechaLimiteEmpleador.CompareTo(fechaActual).Equals(-1);
    }
}
