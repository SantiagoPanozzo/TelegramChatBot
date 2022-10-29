namespace Library;

/// <summary> Clase para conocer y agregar categorias de ofertas de servicio </summary>
public class Categoria : IDesactivable
{
    public string Descripcion { get; set; }
    private List<OfertaDeServicio> Ofertas { get; set; } = new List<OfertaDeServicio>();
    private int Id { get; set; }
    private static int Instancias { get; set; } = 0;
    private bool Activa { get; set; }

    /// <summary> Constructor de la clase <see cref="Categoria"/> </summary>
    /// <param name="descripcion"> Descripción de <see cref="Categoria"/> </param>
    public Categoria(string descripcion)
    {
        Categoria.Instancias++;
        this.Id = Instancias;
        this.Descripcion = descripcion;
        this.Activa = true;

    }

    /// <summary> Método para obtener el ID de una <see cref="Categoria"/>  </summary>
    /// <returns> Devuelve el ID de <see cref="Categoria"/> </returns>
    public int GetId()
    {
        return this.Id;
    }

    /// <summary> Método para agregar una oferta en caso de que la misma no exista todavía en la lista </summary>
    /// <param name="oferta"> Variable de tipo <see cref="OfertaDeServicio"/>, es la que se desea agregar </param>
    public void AddOferta(OfertaDeServicio oferta)
    {
        if (Ofertas.Contains(oferta)) throw (new ArgumentException("Esa oferta ya se encuentra en la lista"));
        this.Ofertas.Add(oferta);
    }
    
    /// <summary> Método para quitar una oferta, en caso de que la misma no exista no hará nada </summary>
    /// <param name="idOferta"> Variable de tipo <see cref="int"/> que representa el Id de la oferta que se desea eliminar </param>
    public void RemoveOferta(int idOferta, Usuario user)
    {
        OfertaDeServicio oferta = GetOfertaById(idOferta);
        if (!Ofertas.Contains(oferta)) throw (new ArgumentException("Esa oferta no está en la lista"));
        oferta.DarDeBaja(user);
    }

    /// <summary> Método para conocer la lista de ofertas </summary>
    /// <returns> Retorna la lista con las ofertas agregadas </returns>
    public List<OfertaDeServicio> getOfertas()
    { // TODO refactorizar para que cumpla con Case (GetOfertas())
        return this.Ofertas;
    }

    /// <summary> Filtrar <see cref="OfertaDeServicio"/> por id </summary>
    /// <param name="id"> ID de la oferta que se quiere ver </param>
    /// <returns> Devuelve la oferta con el ID ingresado </returns>
    public OfertaDeServicio GetOfertaById(int id)
    {
        foreach (OfertaDeServicio ofertaDeServicio in Ofertas)
        {
            if (ofertaDeServicio.GetId().Equals(id)) return ofertaDeServicio;
        }
        throw (new Exception("No se encontró la oferta"));
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
            this.Activa = false;
        }
    }
    
}
