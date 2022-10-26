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
    public Trabajador RegistrarTrabajador(string nombre, string apellido, string contraseña, string fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula))
        {
            Trabajador nuevoTrabajador = ((Trabajador)AddUsuario(TipoDeUsuario.Trabajador, nombre, apellido, contraseña, nacimiento, cedula,
                telefono, correo, ubicacion));
            return nuevoTrabajador;
        }
        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));
    }

    /// <summary> Método para registrar un empleador </summary>
    /// <param name="nombre"> Nombre del usuario </param> <param name="apellido"> Apellido del usuario </param> <param name="contraseña"> Contraseña del usuario </param> <param name="fechaNacimiento"> Fecha de nacimiento del usuario </param> <param name="cedula"> Cédula del usuario </param> <param name="telefono"> Teléfono del usuario </param> <param name="correo"> Correo electrónico del usuario </param> <param name="ubicacion"> Ubicación //TODO(ver como) del usuario </param>
    /// <returns></returns>
    public Empleador RegistrarEmpleador(string nombre, string apellido, string contraseña, string fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula))
        {
            Empleador nuevoEmpleador = ((Empleador)AddUsuario(TipoDeUsuario.Empleador, nombre, apellido, contraseña, nacimiento, cedula,
                telefono, correo, ubicacion));
            return nuevoEmpleador;
        }

        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));

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
                if (6 < cedula.Length && cedula.Length < 8)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary> Método para agregar un usuario </summary>
    /// <param name="nombre"> Nombre del usuario </param> 
    /// <param name="apellido"> Apellido del usuario </param> 
    /// <param name="contraseña"> Contraseña del usuario </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del usuario </param> 
    /// <param name="cedula"> Cédula del usuario </param> 
    /// <param name="telefono"> Teléfono del usuario </param> 
    /// <param name="correo"> Correo electrónico del usuario </param> 
    /// <param name="ubicacion"> Ubicación //TODO(ver como) del usuario </param>
    /// <returns>  </returns>
    public Usuario AddUsuario(TipoDeUsuario tipo, string nombre, string apellido, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion) {
        Usuario nuevoUsuario;
        switch (tipo)
        {
            case TipoDeUsuario.Trabajador: 
                nuevoUsuario = new Trabajador(nombre, apellido, contraseña, fechaNacimiento, cedula, telefono, correo, ubicacion);
                break;
            case TipoDeUsuario.Empleador:
                nuevoUsuario = new Empleador(nombre, apellido, contraseña, fechaNacimiento, cedula, telefono, correo, ubicacion);
                break;
            default:
                throw (new AggregateException("Error al crear el usuario"));
        }
        if (usuarios.Contains(nuevoUsuario)) {
            throw new Exception("El usuario ya fue ingresado");
        }
        else {
            usuarios.Add(nuevoUsuario);
            return nuevoUsuario;
        }
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
