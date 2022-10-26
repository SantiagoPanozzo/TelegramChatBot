namespace Library;

public class Administrador : Usuario
{
    public void Autorizar(OfertaDeServicio oferta)
    {
        //Falta intermediario para las ofertas?
    }

    public Administrador(string nick, string contraseña, string telefono, string correo)
    {
        this.Tipo = TipoDeUsuario.Administrador;
        this.Nick = nick;
        this.Telefono = telefono;
        this.Correo = correo;
        this.SetContraseña(contraseña);
    }
}