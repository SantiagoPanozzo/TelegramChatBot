using System.Security.Authentication;

namespace Library;

/// <summary> Clase para manejar el catálogo de ofertas </summary>
public class OfertasHandler{
    // TODO refactorizar catalog a "_catalog"
    private CategoriasCatalog catalog = CategoriasCatalog.GetInstance();
    
    private static OfertasHandler? _instance;

    private static OfertasHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new OfertasHandler();
            }

            return _instance;
        }
    }

    private OfertasHandler(){}
    public static OfertasHandler GetInstance()
    {
        return OfertasHandler.Instance;
    }

    /// <summary> Constructor de la clase </summary>
    /// <param name="CategoryDesc"> Descripción de la categoría </param>
    /// <param name="ofertante"> Usuario que oferta su trabajo </param>
    /// <param name="descripcion"> Descripción de la oferta </param>
    /// <param name="empleo"> Rubro de la oferta </param>
    /// <param name="precio"> Precio de la oferta </param>
    /// <returns> Devuelve la oferta de tipo <see cref="OfertaDeServicio"/> </returns>
    public OfertaDeServicio Ofertar(string CategoryDesc, Trabajador ofertante, string descripcion, string empleo, double precio){
        /// <remarks> Por patron Creator se crea instancia de oferta de servicio en esta clase </remarks>      
        Categoria Category = this.catalog.GetCategoria(CategoryDesc);
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);
        Category.AddOferta(Oferta);
        return Oferta;
    }

    /// <summary> Método para dar de baja una <see cref="OfertaDeServicio"/> </summary>
    /// <param name="user"> Identificación de <see cref="TipoDeUsuario"/> que implementa el método </param>
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

    /// <summary> Método para crear una categoria </summary>
    /// <param name="user"> Identificación de <see cref="TipoDeUsuario"/> que implementa el método </param>
    /// <param name="descripcion"> Descripcioón de la <see cref="Categoria"/> </param>
    /// <returns> Devuelve el catálogo actualizado con la nueva categoría </returns>
    public Categoria CrearCategoria(Usuario user, string descripcion)
    {
        // TODO test que esto solo funcione si el usuario es admin
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            return this.catalog.AddCategoria(user, descripcion);
        }

        throw (new AuthenticationException("Solo un administrador puede crear categorías"));
    }
    
    /// <summary> Método para eliminar una categoría </summary>
    /// <param name="user"> Identificación de <see cref="TipoDeUsuario"/> que implementa el método </param>
    /// <param name="categoria"> <see cref="Categoria"/> que se desea eliminar </param>
    public void EliminarCategoria(Usuario user, Categoria categoria)
    {
        //TODO implementar que solo funcione si el usuario es admin
        this.catalog.RemoveCategoria(user, categoria);
    }

    /// <summary> Método para obtener <see cref="OfertaDeServicio"/> </summary>
    /// <param name="categoriaId"> Categoría filtro de la búsqueda </param>
    /// <returns> Devuelve todas las <see cref="OfertaDeServicio"/> que coincida con la categoría ingresada </returns>
    public List<OfertaDeServicio> GetOfertas(int categoriaId)
    {
        return GetCategoriaById(categoriaId).getOfertas();
        throw (new ArgumentException("El id ingresado no coincide con ninguna categoria"));

    }

    /// <summary> Método para obtener una <see cref="OfertaDeServicio"/> por id </summary>
    /// <param name="id"> Id de la <see cref="OfertaDeServicio"/> que se busca </param>
    /// <returns> Devuelve la <see cref="OfertaDeServicio"/> que coincida con el id ingresado </returns>
    public OfertaDeServicio GetOfertaById(int id)
    {
        return this.catalog.GetOfertaById(id);
        throw (new ArgumentException("El id ingresado no coincide con ninguna oferta de servicio"));
    }

    /// <summary> Método para obtener una <see cref="Categoria"/> por id </summary>
    /// <param name="id"> Id de la <see cref="Categoria"/> que se busca </param>
    /// <returns> Devuelve la <see cref="Categoria"/> que coincida con el id ingresado </returns>
    public Categoria GetCategoriaById(int id)
    {
        return this.catalog.GetCategoriaById(id);
        throw (new ArgumentException("El id ingresado no coincide con ninguna categoria"));
    }
}
