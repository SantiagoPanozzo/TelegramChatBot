namespace Library;

/// <summary>  </summary>
public class OfertasHandler{
    
    public CategoriasCatalog catalog = new CategoriasCatalog();

    /// <summary>  </summary>
    /// <param name="CategoryDesc">  </param>
    /// <param name="ofertante">  </param>
    /// <param name="descripcion">  </param>
    /// <param name="empleo">  </param>
    /// <param name="precio">  </param>
    /// <returns>  </returns>
    public OfertaDeServicio Ofertar(string CategoryDesc,Trabajador ofertante, string descripcion, string empleo, double precio){
        //Por patron Creator se crea instancia de oferta de servicio en esta clase      
        Categoria Category = this.catalog.GetCategoria(CategoryDesc);
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);
        Category.AddOferta(Oferta);
        return Oferta;
    }

    public void CrearCategoria(Usuario user, string descripcion)
    {
        // TODO test que esto solo funcione si el usuario es admin
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.catalog.AddCategoria(user, descripcion);
        }
    }
    
    public void EliminarCategoria(Usuario user, Categoria categoria)
    {
        // implementar que solo funcione si el usuario es admin
        this.catalog.RemoveCategoria(user, categoria);
    }

    public OfertaDeServicio GetOfertaByID(int id)
    {
        foreach (Categoria categoria in catalog.GetCategorias())
        {
            foreach (OfertaDeServicio oferta in categoria.getOfertas())
            {
                if (oferta.GetId().Equals(id))
                {
                    return oferta;
                }
            }
        }
        throw (new ArgumentException("El id ingresado no coincide con ninguna oferta de servicio"));
    }
    
}