namespace Library;

/// <summary> Clase de manejo del catalogo de categorías de las ofertas </summary>
public class CategoriasCatalog
{
    protected List<Categoria> Categorias;

    /// <summary> Constructor de la clase, inicia la lista de las categorías </summary>
    public CategoriasCatalog()
    {
        this.Categorias = new List<Categoria>();
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

    //TODO
    /// <summary> Método para obtener información de las categorías </summary>
    /// <param name="descripcion">  </param>
    /// <returns> Retorna una variable de tipo <see cref="Categoria"/> </returns>
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
    /// <param name="user"> Verificación del tipo de usuario, en caso de que sea <see cref="Administrador"/> se podrá agregar </param>
    /// <param name="descripcion"> //TODO </param>
    public Categoria AddCategoria(Usuario user, string descripcion)
    {
        // TODO implementar que solo funcione si el usuario es admin
        Categoria nuevaCategoria = new Categoria(descripcion);
        this.Categorias.Add(nuevaCategoria);
        return nuevaCategoria;
    }
    public void RemoveCategoria(Usuario user, Categoria categoria)
    {
        // TODO implementar que solo funcione si el usuario es admin
        this.Categorias.Remove(categoria);
    }
}
