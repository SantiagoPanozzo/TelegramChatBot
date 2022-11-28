namespace Library;

/// <summary> Clase para mostrar por pantalla las categorías. </summary>
public class ConsoleCategoriaPrinter : IConsolePrinter<Categoria> {


/// <summary> Método que imprime el texto. </summary>
/// <param name="categorias"> Lista de categorías. </param>
/// <param name="user"> Tipo de usuario que llama al método. </param>
    public void Print(List<Categoria> categorias, Usuario user) {
        Console.WriteLine($"Categorías ({categorias.Count} en total)\n");
        foreach (var cat in categorias) {
            Console.WriteLine($"»» ID: {cat.GetId()} ║ Descripción: {cat.Descripcion}");
        }
    }
}
