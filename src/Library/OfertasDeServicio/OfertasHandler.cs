using System.Security.Authentication;
using Library.Excepciones;
namespace Library;

/// <summary> </remarks> Clase para manejar el catálogo de ofertas 
/// Utilizamos patrón singleton ya que solo necesitamos una misma instancia de esta clase, si hubieran más
/// se mezclarían los elementos de la misma y no sabríamos a cual instancia acceder para interactuar con las ofertas </summary> </remarks>

public class OfertasHandler{
    
    private CategoriasCatalog _catalog { get { return CategoriasCatalog.GetInstance(); } }
    
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
    
    /// <summary> Método para borrar los datos de la clase. </summary>
    /// <param name="user"> Tipo de usuario que llama al método. </param>
    public static void Wipe(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            OfertasHandler._instance = null;
        }
        else
        {
            throw (new ElevacionException("Solo un administrador puede utilizar el método Wipe() de OfertasHandler"));
        }
    }

    /// <summary> Constructor de tipo Singleton de la clase. </summary>
    private OfertasHandler(){}

    /// <summary> Método para obtener la instancia de la clase. </summary>
    /// <returns> Devuelve la instancia. </returns>

    public static OfertasHandler GetInstance()
    {
        return OfertasHandler.Instance;
    }

    /// <summary> Constructor de la clase. </summary>
    /// <param name="CategoryDesc"> Descripción de la categoría. </param>
    /// <param name="ofertante"> Usuario que oferta su trabajo. </param>
    /// <param name="descripcion"> Descripción de la oferta. </param>
    /// <param name="empleo"> Rubro de la oferta. </param>
    /// <param name="precio"> Precio de la oferta. </param>
    /// <returns> Devuelve la oferta de tipo <see cref="OfertaDeServicio"/>. </returns>
    /// <!-- Por patron Creator se crea instancia de oferta de servicio en esta clase, ya que es la que va a
    /// interactuar más directamente con las mismas y que va a almacenarlas. -->

    public OfertaDeServicio Ofertar(int CategoryId, Trabajador ofertante, string descripcion, string empleo, double precio){
        Categoria Category = this._catalog.GetCategoriaById(CategoryId);
        OfertaDeServicio Oferta = new OfertaDeServicio(ofertante, descripcion, empleo, precio);
        Category.AddOferta(Oferta);
        return Oferta;
    }

    /// <summary> Método para dar de baja una <see cref="OfertaDeServicio"/>. </summary>
    /// <param name="user"> Identificación de <see cref="TipoDeUsuario"/> que implementa el método. </param>
    /// <param name="id"> Id de la oferta. </param>
    public void DarDeBajaOferta(Usuario user, int id)
    {
        OfertaDeServicio oferta = GetOfertaById(id);
        oferta.DarDeBaja(user);
    }

    /// <summary> Método para obtener la lista de categorías. </summary>
    /// <returns> Devuelve el catálogo de <see cref="Categoria"/>. </returns>
    public List<Categoria> GetCategorias()
    {
        return this._catalog.GetCategorias();
    }

    /// <summary> Método para crear una categoria. </summary>
    /// <param name="user"> Identificación de <see cref="TipoDeUsuario"/> que implementa el método. </param>
    /// <param name="descripcion"> Descripcioón de la <see cref="Categoria"/>. </param>
    /// <returns> Devuelve la nueva instancia de categoría creada. </returns>
    public Categoria CrearCategoria(Usuario user, string descripcion)
    {
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            return this._catalog.AddCategoria(user, descripcion);
        }

        else
        {
            throw (new ElevacionException("Solo un administrador puede utilizar el método CrearCategoria() de OfertasHandler"));
        }
    }
    
    /// <summary> Método para eliminar una categoría. </summary>
    /// <param name="user"> Identificación de <see cref="TipoDeUsuario"/> que implementa el método. </param>
    /// <param name="categoria"> <see cref="Categoria"/> que se desea eliminar. </param>
    public void EliminarCategoria(Usuario user, Categoria categoria)
    {
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this._catalog.RemoveCategoria(user, categoria);
        }

        else
        {
            throw (new ElevacionException("Solo un administrador puede utilizar el método EliminarCategoria() de OfertasHandler"));
        }
        
    }

    /// <summary> Método para obtener <see cref="OfertaDeServicio"/>. </summary>
    /// <param name="categoriaId"> Categoría filtro de la búsqueda. </param>
    /// <returns> Devuelve todas las <see cref="OfertaDeServicio"/> que coincida con la categoría ingresada. </returns>
    public List<OfertaDeServicio> GetOfertas(int categoriaId)
    {
        return GetCategoriaById(categoriaId).GetOfertas();
        throw (new ArgumentException("El id ingresado no coincide con ninguna categoria"));

    }
    /// <summary> Método para obtener una lista de todas las <see cref="OfertaDeServicio"/> </summary>
    /// <returns></returns>
    public List<OfertaDeServicio> GetOfertasIgnoreId()
    {
        var inst = CategoriasCatalog.GetInstance();
        var cats = inst.GetCategorias();
        List<OfertaDeServicio> final = new();
        foreach (var cat in cats)
        {
            final.AddRange(GetOfertas(cat.GetId()));
        }
        return final;
    }

    /// <summary> Método para obtener una <see cref="OfertaDeServicio"/> por id. </summary>
    /// <param name="id"> Id de la <see cref="OfertaDeServicio"/> que se busca. </param>
    /// <returns> Devuelve la <see cref="OfertaDeServicio"/> que coincida con el id ingresado. </returns>
    public OfertaDeServicio GetOfertaById(int id)
    {
        return this._catalog.GetOfertaById(id);
        throw (new ArgumentException("El id ingresado no coincide con ninguna oferta de servicio"));
    }

    /// <summary> Método para obtener una <see cref="Categoria"/> por id. </summary>
    /// <param name="id"> Id de la <see cref="Categoria"/> que se busca. </param>
    /// <returns> Devuelve la <see cref="Categoria"/> que coincida con el id ingresado. </returns>
    public Categoria GetCategoriaById(int id)
    {
        return this._catalog.GetCategoriaById(id);
        throw (new ArgumentException("El id ingresado no coincide con ninguna categoria"));
    }
}
