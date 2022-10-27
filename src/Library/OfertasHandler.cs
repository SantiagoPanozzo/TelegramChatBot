using System.Security.Authentication;

namespace Library;

/// <summary>  </summary>
public class OfertasHandler{
    
    private CategoriasCatalog catalog = new CategoriasCatalog();

    /// <summary>  </summary>
    /// <param name="CategoryDesc">  </param>
    /// <param name="ofertante">  </param>
    /// <param name="descripcion">  </param>
    /// <param name="empleo">  </param>
    /// <param name="precio">  </param>
    /// <returns>  </returns>
    public OfertaDeServicio Ofertar(string CategoryDesc, Trabajador ofertante, string descripcion, string empleo, double precio){
        //Por patron Creator se crea instancia de oferta de servicio en esta clase      
        Categoria Category = this.catalog.GetCategoria(CategoryDesc);
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);
        Category.AddOferta(Oferta);
        return Oferta;
    }

    public void DarDeBajaOferta(Usuario user, int id)
    {
        OfertaDeServicio oferta = GetOfertaById(id);
        oferta.DarDeBaja(user);
    }

    public List<Categoria> GetCategorias()
    {
        return this.catalog.GetCategorias();
    }

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
