namespace Library;
public class PlainTextUsuariosPrinter : ITextPrinter<Usuario>
{
    public string Print(List<Usuario> users)
    {
        string response = "";

        foreach (var us in users)
        {
            response += $"\n»» ID: {us.Nick} ║ Tipo: {us.GetTipo()}\n";
        }
        return response;
    }
}
