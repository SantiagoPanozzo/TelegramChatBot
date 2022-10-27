namespace Library;
using System;

/// <summary>  </summary>
public class RegistryHandler {
    private List<Usuario> usuarios = new();

    // TODO documentar que el "fechaNacimiento" se tiene que introducir como "año,mes,dia". Idem para la cedula, correo, etc
    /// <summary> Método para registrar un trabajador </summary>
    /// <param name="nombre"> Nombre del usuario </param> 
    /// <param name="apellido"> Apellido del usuario </param> 
    /// <param name="contraseña"> Contraseña del usuario </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del usuario </param> 
    /// <param name="cedula"> Cédula del usuario </param> 
    /// <param name="telefono"> Teléfono del usuario </param> 
    /// <param name="correo"> Correo electrónico del usuario </param> 
    /// <param name="ubicacion"> Ubicación //TODO(ver como) del usuario </param>
    /// <returns>  </returns>
    public Trabajador RegistrarTrabajador(string nombre, string apellido, string nick, string contraseña, string fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula) && VerificarNick(nick))
        {
            Trabajador nuevoTrabajador = new Trabajador(nombre, apellido, nick, contraseña, nacimiento, cedula, telefono, correo, ubicacion);
            return nuevoTrabajador;
        }
        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));
    }

    /// <summary> Método para registrar un empleador </summary>
    /// <param name="nombre"> Nombre del usuario </param> <param name="apellido"> Apellido del usuario </param> <param name="contraseña"> Contraseña del usuario </param> <param name="fechaNacimiento"> Fecha de nacimiento del usuario </param> <param name="cedula"> Cédula del usuario </param> <param name="telefono"> Teléfono del usuario </param> <param name="correo"> Correo electrónico del usuario </param> <param name="ubicacion"> Ubicación //TODO(ver como) del usuario </param>
    /// <returns></returns>
    public Empleador RegistrarEmpleador(string nombre, string apellido, string nick, string contraseña, string fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula) && VerificarNick(nick))
        {
            Empleador nuevoEmpleador = new Empleador(nombre, apellido, nick, contraseña, nacimiento, cedula, telefono, correo, ubicacion);
            return nuevoEmpleador;
        }

        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));

    }
    public Administrador RegistrarAdministrador(string nick, string contraseña, string telefono, string correo)
    {
        if (VerificarCorreo(correo) && VerificarNick(nick))
        {
            Administrador nuevoAdministrador = new Administrador(nick, contraseña, telefono, correo);
            return nuevoAdministrador;
        }
        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));
    }

    public bool VerificarNick(string nick)
    {
        foreach (Usuario usuario in usuarios)
        {
            if (usuario.Nick.Equals(nick)) return false;
        }

        return true;
    }
    
    /// <summary>  </summary>
    /// <param name="correo">  </param>
    /// <returns>  </returns>
    public bool VerificarCorreo(string correo)
    { // TODO testear, no tengo mucha fe en esto
        bool arroba = false;
        bool punto = false;
        foreach (char caracter in correo)
        {
            if (caracter.Equals('@'))
            {
                if (arroba) return false;
                arroba = true;
            }
            if (caracter.Equals('.'))
            {
                if (arroba) punto = true;
            }
        }
        if (punto) return true;
        return false;
    }

    /// <summary>  </summary>
    /// <param name="cedula">  </param>
    /// <returns>  </returns>
    public bool VerificarCedula(string cedula)
    { // TODO testear, tampoco le tengo fe
        cedula = cedula.Replace(".", string.Empty);
        cedula = cedula.Replace("-", string.Empty);
        string validos = "0123456789";
        foreach (char caracteres in cedula)
        {
            if (validos.Contains(caracteres))
            {
                if (cedula.Length > 6 && cedula.Length <= 8)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    /// <summary>  </summary>
    /// <param name="usuario">  </param>
    public void RemoveUsuario(Usuario usuario) {
        if (!usuarios.Contains(usuario)) {
            throw new ArgumentNullException("El usuario ingresado no existe");
        }
        else {
            usuarios.Remove(usuario);
        }
    }

    /// <summary>  </summary>
    /// <param name="nombre">  </param>
    /// <param name="apellido">  </param>
    /// <param name="contraseña">  </param>
    /// <returns>  </returns>
    public Usuario GetUsuario(string nombre, string apellido, string contraseña)
    {
        foreach (Usuario user in usuarios)
        {
            if (user.Nombre.Equals(nombre) && user.Apellido.Equals(apellido))
            {
                if (user.VerifyContraseña(contraseña))
                {
                    return user;
                }
            }
        }
        throw (new ArgumentException("Los datos introducidos no coinciden con ningun usuario"));
    }
}
