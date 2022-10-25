namespace Library;

public class OfertasHandler{
    
    public CategoriasCatalog catalog = new CategoriasCatalog();
    public void Ofertar(string CategoryDesc,Trabajador ofertante, string descripcion, string empleo, double precio){
        //Por patron Creator se crea instancia de oferta de servicio en esta clase      
        Categoria Category = this.catalog.GetCategoria(CategoryDesc);
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);
        Category.AddOferta(Oferta);
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
    
}