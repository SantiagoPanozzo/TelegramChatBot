namespace Library;

/// <summary> Clase que representa una oferta de servicio </summary>
public class OfertaDeServicio : IDesactivable

{
    private Trabajador Ofertante { get; set; }
    public string Descripcion { get; set; }
    public string Empleo { get; set; }
    public double Precio { get; set; }
    public Calificacion Rate { get; set; }
    public bool Disponible { get; set; }
    private int _id; // TODO implementar IDs, placeholder
    private static int Instancias { get; set; } = 0;
    private bool Activa { get; set; }
    private Tuple<double,double> Ubicacion { get; set; }

    /// <summary> Constructor de la clase </summary>
    /// <param name="ofertante"> Ofertante de la oferta </param>
    /// <param name="descripcion"> Descripción de la oferta </param>
    /// <param name="empleo"> Rubro del ofertante </param>
    /// <param name="precio"> Precio de la oferta </param>
    public OfertaDeServicio(Trabajador ofertante, string descripcion, string empleo, double precio)
    {
        this.Ofertante = ofertante;
        this.Descripcion = descripcion;
        this.Empleo = empleo;
        this.Precio = precio;
        OfertaDeServicio.Instancias++;
        this.Activa = true;
        this._id = Instancias;
        this.Ubicacion = Ofertante.Ubicacion;
        this.Rate = Calificacion.NoCalificado;
    }

    /// <summary> Método para obtener id de <see cref="OfertaDeServicio"/> </summary>
    /// <returns> Devuelve el id de la <see cref="OfertaDeServicio"/> </returns>
    public int GetId()
    {
        return this._id;
    }

    public string GetOfertante()
    {
        return this.Ofertante.Nick;
    }

    /// <summary> Método para obtener la reputación del ofertante </summary>
    /// <returns> Devuelve la <see cref="Calificacion"/> del <see cref="Trabajador"/> ofertante </returns>
    public Calificacion GetReputacion()
    {
        return Ofertante.GetReputacion();
    }

    public Tuple<double, double> GetUbicacion()
    {
        return this.Ubicacion;
    }

    public Dictionary<string, string> GetContacto()
    {
        return Ofertante.GetContacto();
    }

    /// <summary> Método para calificar la oferta en cuestión </summary>
    /// <param name="rate"> Valor de <see cref="Calificacion"/> </param>
    public void RateMe(Calificacion rate)
    { // TODO test
        if(!this.Rate.Equals(Calificacion.NoCalificado))
        {
            this.Rate = rate;
            this.Ofertante.Calificar(rate);
        }
    }

    /// <summary> Método para obtener la calificación dada a la oferta tras ser finalizada </summary>
    /// <returns> Devuelve la <see cref="Calificacion"/> correspondiente de la oferta </returns>
    public Calificacion GetCalificacion()
    {
        return this.Rate;
    }
    
    /// <summary> Método para conocer el estado de la <see cref="OfertaDeServicio"/> </summary>
    /// <returns> Devuelve el estado de <see cref="OfertaDeServicio"/>, true si está activa, false si no está activa  </returns>
    public bool IsActive()
    {
        return this.Activa;
    }
    
    /// <summary> Método para dar de baja un <see cref="Usuario"/> </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> que se dará de baja </param>
    public void DarDeBaja(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.Activa = false;
        }

        if (user.GetTipo().Equals(TipoDeUsuario.Trabajador))
        {
            if (user.Nick.Equals(Ofertante.Nick))
            {
                this.Activa = false;
            }
        }
    }

    /// <summary> Método para reactivar un <see cref="Usuario"/> </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> que se reactivará </param>
    public void Reactivar(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.Activa = true;
        }

        if (user.GetTipo().Equals(TipoDeUsuario.Trabajador))
        {
            if (user.Nick.Equals(Ofertante.Nick))
            {
                this.Activa = true;
            }
        }
    }
}
