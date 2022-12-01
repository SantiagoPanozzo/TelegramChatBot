namespace Library;
public class PlainTextOfertasPrinter : ITextPrinter<OfertaDeServicio>
{
    public string Print(List<OfertaDeServicio> ofertas)
    {
        string response = "";

        foreach (var of in ofertas)
        {
            response += $"\n»» ID: {of.GetId()} ║ Empleo: {of.Empleo} ║ Descripción: {of.Descripcion} ║ Precio: {of.Precio} || Reputación: {of.GetReputacion().ToString()}\n";
        }
        return response;
    }
}
