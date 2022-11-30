namespace Library;
public class PlainTextUsersTrabajadoresPrinter : ITextPrinter<string>
{
    public string Print(List<string> users)
    {
        string result = "Trabajadores:";

        foreach (var usuario in users)
        {
            result += $"\nNick: {usuario}";
        }
        return result;
    }
}
