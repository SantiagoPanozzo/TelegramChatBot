namespace Library;

public class CategoriasCatalog
{
    protected List<Categoria> Categorias;

    public CategoriasCatalog()
    {
        this.Categorias = new List<Categoria>();
    }

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
    public void AddCategoria(Usuario user, string descripcion)
    {
        // TODO implementar que solo funcione si el usuario es admin
        this.Categorias.Add(new Categoria(descripcion));
    }
    public void RemoveCategoria(Usuario user, Categoria categoria)
    {
        // TODO implementar que solo funcione si el usuario es admin
        this.Categorias.Remove(categoria);
    }
}