namespace Library;

public class PlainTextUserPrinter : ITextPrinter<Usuario, Usuario>
{
    public string PrintAll(List<Usuario> catalog, Usuario user)
    {
        string result = "Usuarios:";
        foreach (var usuario in catalog)
        {
            result += $"\n {usuario.GetPublicInfo().ToString()}";
        }

        return result;
    }

    public string PrintOne(Usuario user)
    {
        return user.GetPublicInfo().ToString();
    }
}