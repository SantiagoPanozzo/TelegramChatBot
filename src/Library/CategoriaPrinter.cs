namespace Library;

public class CategoriaPrinter : IPrinter<Categoria> {

    public void PrintCatalog(List<Categoria> categorias) {
        Console.WriteLine($"Categorías ({categorias.Count} en total) \n");
        foreach (var cat in categorias) {
            Console.WriteLine($"»» Descripción: {cat.Descripcion}\t ║ ID: {cat.GetId()}\n");
        }
    }
}
