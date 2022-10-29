namespace Library;

/// <summary> Clase que representa una oferta de sevicio </summary>
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
    }

    /// <summary> Método para obtener id de <see cref="OfertaDeServicio"/> </summary>
    /// <returns> Devuelve el id de la <see cref="OfertaDeServicio"/> </returns>
    public int GetId()
    {
        return this._id;
    }

    public string GetUsuario()
    {
        return this.Ofertante.Nick;
    }

    public Tuple<double, double> GetUbicacion()
    {
        return this.Ubicacion;
    }

    public string GetContacto()
    {
        return Ofertante.GetContacto();
    }

    /// <summary> Método para calificar la oferta en cuestión </summary>
    /// <param name="rate"> Valor de <see cref="Calificacion"/> </param>
    public void RateMe(Calificacion rate)
    { // TODO test
        this.Rate = rate;
        this.Ofertante.Calificar(rate);
    }

    /// <summary> Método para obtener una calificación </summary>
    /// <returns> Devuelve una <see cref="Calificacion"/> según sea indicada </returns>
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
