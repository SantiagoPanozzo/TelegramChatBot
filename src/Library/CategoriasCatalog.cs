namespace Library;

/// <summary> Clase para manejar el catálogo de categorías </summary>
public class CategoriasCatalog
{
    protected List<Categoria> Categorias;

    private static CategoriasCatalog? _instance;

    private static CategoriasCatalog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CategoriasCatalog();
            }

            return _instance;
        }
    }
    

    /// <summary>Método para borrar los datos de la clase</summary>
    /// <param name="user">tipo de usuario que llama al método</param>
    public static void Wipe(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            CategoriasCatalog._instance = null;
        }
    }
    
    /// <summary> Constructor de la clase, inicia la lista de las categorías </summary>
    private CategoriasCatalog()
    {
        this.Categorias = new List<Categoria>();
    }

    /// <summary>Método para obtener la instancia del catálogo de categorías</summary>
    /// <returns>devuelve la instancia creada del catálogo</returns>

    public static CategoriasCatalog GetInstance()
    {
        return CategoriasCatalog.Instance;
    }

    /// <summary> Método para conocer las categorias </summary>
    /// <returns> Retorna la lista de categorías </returns>
    public List<Categoria> GetCategorias()
    {
        return this.Categorias;
    }

    /// <summary> Método para obtener <see cref="OfertaDeServicio"/> por id </summary>
    /// <param name="id"> Valor del id que se quiere filtrar </param>
    /// <returns> Devuelve la <see cref="OfertaDeServicio"/> filtrada por el id dado </returns>
    public OfertaDeServicio GetOfertaById(int id)
    {
        foreach (Categoria categoria in Categorias)
        {
            return categoria.GetOfertaById(id);
        }

        throw (new("No se encontró la oferta"));

    }

    /// <summary> Método para obtener una categoría por descripción </summary>
    /// <param name="descripcion"> Descripción filtro para la busqueda </param>
    /// <returns> Devuelve <see cref="Categoria"/> según el filtro de la descripción </returns>
    public Categoria GetCategoria(string descripcion)
    {
        foreach (Categoria categoria in Categorias)
        {
            if (categoria.Descripcion.Equals(descripcion))
            {
                return categoria;
            }
        }
        throw (new ArgumentException("Los datos introducidos no corresponen a ninguna categoria existente"));
    }

    /// <summary> Método para obtener una categoría por id </summary>
    /// <param name="id"> Id filtro para la busqueda </param>
    /// <returns> Devuelve <see cref="Categoria"/> según el filtro de id </returns>
    public Categoria GetCategoriaById(int id)
    {
        foreach (Categoria categoria in Categorias)
        {
            if (categoria.GetId().Equals(id))
            {
                return categoria;
            }
        }
        throw (new ArgumentException("Los datos introducidos no corresponen a ninguna categoria existente"));
    }

    /// <summary> Método para agregar una nueva categoría al catálogo </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> </param>
    /// <param name="descripcion"> Descripción de la categoría </param>
    public Categoria AddCategoria(Usuario user, string descripcion)
    {
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            Categoria nuevaCategoria = new Categoria(descripcion);
            this.Categorias.Add(nuevaCategoria);
            return nuevaCategoria;
        }

        throw (new("Solo un administrador puede agregar categorías"));
    }

    /// <summary> Método para eliminar una categoría </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> </param>
    /// <param name="categoria"> <see cref="Categoria"/> que se desea eliminar </param>
    public void RemoveCategoria(Usuario user, Categoria categoria)
    {
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            categoria.DarDeBaja(user);
        }

        throw (new("Solo un administrador puede quitar categorías"));
    }
}
