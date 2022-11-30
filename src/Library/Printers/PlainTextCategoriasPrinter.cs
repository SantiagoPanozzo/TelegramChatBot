namespace Library;

public class PlainTextCategoriaPrinter : ITextPrinter<Categoria>
{
    public string Print(List<Categoria> catalog)
    {
        string response = $"Categorías ({catalog.Count} en total)";

        foreach (var cat in catalog)
        {
            response += $"\n»» ID: {cat.GetId()} ║ Descripción: {cat.Descripcion}║ Estado: {cat.IsActive()}\n";
        }
        return response;
    }
}
