namespace Library;

/// <summary>  </summary>
public class OfertaDeServicio
{
    public Trabajador Ofertante { get; set; }
    public string Descripcion { get; set; }
    public string Empleo { get; set; }
    public double Precio { get; set; }
    public Calificacion Rate { get; set; }
    public bool Disponible { get; set; }
    private int _id; // TODO implementar IDs, placeholder
    private static int Instancias { get; set; } = 0;

    /// <summary>  </summary>
    /// <param name="ofertante">  </param>
    /// <param name="descripcion">  </param>
    /// <param name="empleo">  </param>
    /// <param name="precio">  </param>
    public OfertaDeServicio(Trabajador ofertante, string descripcion, string empleo, double precio)
    {
        this.Ofertante = ofertante;
        this.Descripcion = descripcion;
        this.Empleo = empleo;
        this.Precio = precio;
        OfertaDeServicio.Instancias++;
        this._id = Instancias;
    }

    /// <summary>  </summary>
    /// <returns>  </returns>
    public int GetId()
    {
        return this._id;
    }

    /// <summary>  </summary>
    /// <param name="rate">  </param>
    public void RateMe(Calificacion rate)
    { // TODO test
        this.Rate = rate;
        this.Ofertante.Calificar(rate);
    }

    /// <summary>  </summary>
    /// <returns>  </returns>
    public Calificacion GetCalificacion()
    {
        return this.Rate;
    }
}
