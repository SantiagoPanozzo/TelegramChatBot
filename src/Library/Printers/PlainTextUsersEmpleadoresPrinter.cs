namespace Library;
public class PlainTextUsersEmpleadoresPrinter : ITextPrinter<string>
{
    public string Print(List<string> users)
    {
        string result = "Empleadores:";

        foreach (var usuario in users)
        {
            result += $"\nNick: {usuario}";
        }
        return result;
    }
}
