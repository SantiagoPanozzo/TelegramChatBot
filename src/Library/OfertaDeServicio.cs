namespace Library;

public class OfertaDeServicio
{
    public Trabajador Ofertante { get; set; }
    public string Descripcion { get; set; }
    public string Empleo { get; set; }
    public double Precio { get; set; }
    private List<Calificacion> Rate { get; set; }
    public bool Disponible { get; set; }
    public int Id; // TODO implementar IDs, placeholder


    public enum Calificacion
    {
        Deficiente,
        Regular,
        Bueno,
        MuyBueno,
        Sobresaliente
    }

    public OfertaDeServicio(Trabajador ofertante, string descripcion, string empleo, double precio)
    {
        this.Ofertante = ofertante;
        this.Descripcion = descripcion;
        this.Empleo = empleo;
        this.Precio = precio; 
    }

    public void RateMe(int rate)
    { // TODO test
        this.Rate.Add((Calificacion)rate);
    }

    public Calificacion getCalificacion()
    {
        int x = 0;
        foreach (var calif in this.Rate)
        {
            x += (int)calif;
        }

        x /= this.Rate.Count;
        return (Calificacion)x;
    }
    
}