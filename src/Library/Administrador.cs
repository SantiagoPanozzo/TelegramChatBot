namespace Library;

public class Administrador : Usuario
{
    public Administrador(string nick, string contraseña, string telefono, string correo)
    {
        this.Tipo = TipoDeUsuario.Administrador;
        this.Nick = nick;
        this.Telefono = telefono;
        this.Correo = correo;
        this.SetContraseña(contraseña);
    }
}