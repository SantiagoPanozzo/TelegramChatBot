namespace Library;

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

    public OfertaDeServicio(Trabajador ofertante, string descripcion, string empleo, double precio)
    {
        this.Ofertante = ofertante;
        this.Descripcion = descripcion;
        this.Empleo = empleo;
        this.Precio = precio;
        OfertaDeServicio.Instancias++;
        this._id = Instancias;
    }

    public int GetId()
    {
        return this._id;
    }

    public void RateMe(Calificacion rate)
    { // TODO test
        this.Rate = rate;
        this.Ofertante.Calificar(rate);
    }

    public Calificacion GetCalificacion()
    {
        return this.Rate;
    }
    

}