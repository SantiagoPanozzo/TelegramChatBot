namespace Library;
public class PlainTextUsuariosPrinter : ITextPrinter<Usuario>
{
    public string Print(List<Usuario> users)
    {
        string result = "";

        foreach (var usuario in users)
        {
            var pubInfo = usuario.GetPublicInfo();
            var contactInfo = usuario.GetContacto();
            result += $"\nNick: {pubInfo["Nick"]}";
            result += $"\nTipo: {usuario.GetTipo()}";
            result += $"\nNombre: {pubInfo["Nombre"]}";
            result += $"\nApellido: {pubInfo["Apellido"]}";
            result += $"\nTeléfono: {contactInfo["Telefono"]}";
            result += $"\nCorreo: {contactInfo["Correo"]}";
            result += $"\nEstado: {usuario.IsActive()}";
        }
        return result;
    }
}
