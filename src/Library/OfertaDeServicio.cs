namespace Library;

public class OfertaDeServicio
{
    public Trabajador Ofertante { get; set; }
    public string Descripcion { get; set; }
    public string Empleo { get; set; }
    public double Precio { get; set; }
    public Calificacion Rate { get; set; }
    public bool Disponible { get; set; }
    public int Id; // TODO implementar IDs, placeholder

    public OfertaDeServicio(Trabajador ofertante, string descripcion, string empleo, double precio)
    {
        this.Ofertante = ofertante;
        this.Descripcion = descripcion;
        this.Empleo = empleo;
        this.Precio = precio; 
    }

    public void RateMe(Calificacion rate)
    { // TODO test
        this.Rate = rate;
        this.Ofertante.Calificar(rate);
    }

    public Calificacion getCalificacion()
    {
        return this.Rate;
    }
    
}