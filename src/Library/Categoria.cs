namespace Library;

public class Categoria
{
    public string Descripcion { get; set; }
    private List<OfertaDeServicio> Ofertas { get; set; } = new List<OfertaDeServicio>();

    public Categoria(string descripcion)
    {
        this.Descripcion = descripcion;
        
    }

    public void AddOferta(OfertaDeServicio oferta)
    {
        if (Ofertas.Contains(oferta)) throw (new ArgumentException("Esa oferta ya se encuentra en la lista"));
        this.Ofertas.Add(oferta);
    }

    public List<OfertaDeServicio> getOfertas()
    {
        return this.Ofertas;
    }

    public void RemoveOferta(OfertaDeServicio oferta)
    {
        if (!Ofertas.Contains(oferta)) throw (new ArgumentException("Esa oferta no está en la lista"));
        Ofertas.Remove(oferta);
    }
    

}