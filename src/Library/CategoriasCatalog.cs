namespace Library;

/// <summary> Clase para manejar el catálogo de categorías </summary>
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
        // TODO implementar que solo funcione si el usuario es admin
        Categoria nuevaCategoria = new Categoria(descripcion);
        this.Categorias.Add(nuevaCategoria);
        return nuevaCategoria;
    }

    /// <summary> Método para eliminar una categoría </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> </param>
    /// <param name="categoria"> <see cref="Categoria"/> que se desea eliminar </param>
    public void RemoveCategoria(Usuario user, Categoria categoria)
    {
        // TODO implementar que solo funcione si el usuario es admin
        this.Categorias.Remove(categoria);
    }
}
