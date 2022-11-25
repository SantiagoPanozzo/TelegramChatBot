namespace Library;

/// <summary>Clase para mostrar por pantalla las categorías</summary>

public class CategoriaPrinter : IPrinter<Categoria> {


/// <summary> Método que imprime el texto </summary>
/// <param name="categorias"> Lista de categorías </param>
/// <param name="user"> Tipo de usuario que llama al método </param>
    public void PrintCatalog(List<Categoria> categorias, Usuario user) {
        Console.WriteLine($"Categorías ({categorias.Count} en total)\n");
        foreach (var cat in categorias) {
            Console.WriteLine($"»» ID: {cat.GetId()} ║ Descripción: {cat.Descripcion}");
        }
    }
}
