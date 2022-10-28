using System.Security.Authentication;

namespace Library;

/// <summary> Clase para manejar el catálogo de ofertas </summary>
public class OfertasHandler{
    
    private CategoriasCatalog catalog = new CategoriasCatalog();

    /// <summary> Constructor de la clase </summary>
    /// <param name="CategoryDesc"> Descripción de la categoría </param>
    /// <param name="ofertante"> Usuario que oferta su trabajo </param>
    /// <param name="descripcion"> Descripción de la oferta </param>
    /// <param name="empleo"> Rubro de la oferta </param>
    /// <param name="precio"> Precio de la oferta </param>
    /// <returns> Devuelve la oferta de tipo <see cref="OfertaDeServicio"/> </returns>
    public OfertaDeServicio Ofertar(string CategoryDesc, Trabajador ofertante, string descripcion, string empleo, double precio){
        //Por patron Creator se crea instancia de oferta de servicio en esta clase      
        Categoria Category = this.catalog.GetCategoria(CategoryDesc);
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);
        Category.AddOferta(Oferta);
        return Oferta;
    }

    /// <summary> Método para dar de baja una <see cref="OfertaDeServicio"/> </summary>
    /// <param name="user"> Usuario que llama al método </param>
    /// <param name="id"> Id de la oferta </param>
    public void DarDeBajaOferta(Usuario user, int id)
    {
        OfertaDeServicio oferta = GetOfertaById(id);
        oferta.DarDeBaja(user);
    }

    /// <summary> Método para obtener la lista de categorías </summary>
    /// <returns> Devuelve el catálogo de <see cref="Categoria"/> </returns>
    public List<Categoria> GetCategorias()
    {
        return this.catalog.GetCategorias();
    }

    /// <summary>  </summary>
    /// <param name="user">  </param>
    /// <param name="descripcion">  </param>
    /// <returns>  </returns>
    public Categoria CrearCategoria(Usuario user, string descripcion)
    {
        // TODO test que esto solo funcione si el usuario es admin
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            return this.catalog.AddCategoria(user, descripcion);
        }

        throw (new AuthenticationException("Solo un administrador puede crear categorías"));
    }
    
    public void EliminarCategoria(Usuario user, Categoria categoria)
    {
        // implementar que solo funcione si el usuario es admin
        this.catalog.RemoveCategoria(user, categoria);
    }

    public List<OfertaDeServicio> GetOfertas(int categoriaId)
    {
        return GetCategoriaById(categoriaId).getOfertas();
        throw (new ArgumentException("El id ingresado no coincide con ninguna categoria"));

    }
    public OfertaDeServicio GetOfertaById(int id)
    {
        return this.catalog.GetOfertaById(id);
        throw (new ArgumentException("El id ingresado no coincide con ninguna oferta de servicio"));
    }

    public Categoria GetCategoriaById(int id)
    {
        return this.catalog.GetCategoriaById(id);
        throw (new ArgumentException("El id ingresado no coincide con ninguna categoria"));

    }
}
