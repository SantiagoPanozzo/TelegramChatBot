namespace Library;

/// <summary> Clase <see cref="Administrador"/> que hereda de <see cref="Usuario"/> </summary>
public class Administrador : Usuario {
    /// <summary> Constructor de la clase <see cref="Administrador"/> </summary>
    /// <param name="nick"> Username del administrador </param>
    /// <param name="contraseña"> Contraseña del administrador </param>
    /// <param name="telefono"> Teléfono del administrador </param>
    /// <param name="correo"> Correo electrónico del administrador </param>
    public Administrador(string nick, string contraseña, string telefono, string correo) {
        this.Tipo = TipoDeUsuario.Administrador;
        this.Nick = nick;
        this.Telefono = telefono;
        this.Correo = correo;
        this.SetContraseña(contraseña);

    }
}
