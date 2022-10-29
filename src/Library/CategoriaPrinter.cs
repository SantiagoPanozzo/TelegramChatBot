namespace Library;

public class CategoriaPrinter : IPrinter<Categoria> {

    public void PrintCatalog(List<Categoria> categorias, Usuario user) {
        Console.WriteLine($"Categorías ({categorias.Count} en total)\n");
        foreach (var cat in categorias) {
            Console.WriteLine($"»» ID: {cat.GetId()} ║ Descripción: {cat.Descripcion}");
        }
    }
}
