using System;
namespace Library;

/// <summary> Clase <see cref="Administrador"/> que hereda de <see cref="Usuario"/>. </summary>
public class Administrador : Usuario {
    /// <summary> Crea una nueva instancia de la clase <see cref="Administrador"/>. </summary>
    /// <param name="nick"> Username del administrador. </param>
    /// <param name="contraseña"> Contraseña del administrador. </param>
    /// <param name="telefono"> Teléfono del administrador. </param>
    /// <param name="correo"> Correo electrónico del administrador. </param>
    public Administrador(string nick, string contraseña, string telefono, string correo) {
        this.Tipo = TipoDeUsuario.Administrador;
        this.Activo = true;

        this.Nick = nick;
        if (nick.Equals("")) {throw new ArgumentNullException("El campo no puede quedar en blanco"); }
        this.Telefono = telefono;
        if (telefono.Equals("")) { throw new ArgumentNullException("El campo no puede quedar en blanco"); }
        this.Correo = correo;
        if (correo.Equals("")) {throw new ArgumentNullException("El campo no puede quedar en blanco"); }
        this.SetContraseña(contraseña);
        if (contraseña.Equals("")) { throw new ArgumentNullException("El campo no puede quedar en blanco"); }
        
    }
}
