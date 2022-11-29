namespace Library;
public class PlainTextOfertasPrinter : ITextPrinter<OfertaDeServicio>
{
    public string Print(List<OfertaDeServicio> ofertas, Usuario user)
    {
        string response = "";

        foreach (var of in ofertas)
        {
            response += $"\n»» ID: {of.GetId()} ║ Descripción: {of.Descripcion} ║ Precio: {of.Precio}\n";
        }
        return response;
    }
}
