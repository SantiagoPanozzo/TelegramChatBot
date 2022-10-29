namespace Library;
using System;

/// <summary> Clase para manejar el registro </summary>
public class RegistryHandler
{
    private List<Usuario> usuarios;
    
    private static RegistryHandler? _instance;

    private static RegistryHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RegistryHandler();
            }

            return _instance;
        }
    }
    
    private RegistryHandler()
    {
        this.usuarios = new();
    }

    public static RegistryHandler GetInstance()
    {
        return RegistryHandler.Instance;
    }

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
    /// <returns> Devuelve la instancia de <see cref="Trabajador"/> creada </returns>
    public Trabajador RegistrarTrabajador(string nombre, string apellido, string nick, string contraseña, string fechaNacimiento, 
                                          string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
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
    /// <param name="nombre"> Nombre del empleador </param> 
    /// <param name="apellido"> Apellido del empleador </param> 
    /// <param name="contraseña"> Contraseña del empleador </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del empleador </param> 
    /// <param name="cedula"> Cédula del empleador </param> 
    /// <param name="telefono"> Teléfono del empleador </param> 
    /// <param name="correo"> Correo electrónico del empleador </param> 
    /// <param name="ubicacion"> Ubicación del empleador </param>
    /// <returns> Devuelve la instancia de <see cref="Empleador"/> creada </returns>
    public Empleador RegistrarEmpleador(string nombre, string apellido, string nick, string contraseña, string fechaNacimiento, 
                                        string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula) && VerificarNick(nick))
        {
            Empleador nuevoEmpleador = new Empleador(nombre, apellido, nick, contraseña, nacimiento, cedula, telefono, correo, ubicacion);
            return nuevoEmpleador;
        }

        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));

    }

    /// <summary>  </summary>
    /// <param name="nick"></param>
    /// <param name="contraseña"></param>
    /// <param name="telefono"></param>
    /// <param name="correo"></param>
    /// <returns> Devuelve la instancia de <see cref="Trabajador"/> creada </returns>
    public Administrador RegistrarAdministrador(string nick, string contraseña, string telefono, string correo)
    {
        if (VerificarCorreo(correo) && VerificarNick(nick))
        {
            Administrador nuevoAdministrador = new Administrador(nick, contraseña, telefono, correo);
            return nuevoAdministrador;
        }
        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));
    }

    /// <summary> Método para verificar el nickname de un <see cref="Usuario"/> </summary>
    /// <param name="nick"> Nickname del <see cref="Usuario"/> </param>
    /// <returns> Devuelve true si no existe otro <see cref="Usuario"/> con ese nick, de lo contrario devuelve false </returns>
    public bool VerificarNick(string nick)
    {
        foreach (Usuario usuario in usuarios)
        {
            if (usuario.Nick.Equals(nick)) return false;
        }
        return true;
    }
    
    /// <summary> Método para verificar el correo de un <see cref="Usuario"/> </summary>
    /// <param name="correo"> Correo del <see cref="Usuario"/> </param>
    /// <returns> Devuelve true si el formato del correo es válido, de lo contrario devuelve false </returns>
    public bool VerificarCorreo(string correo)
    { // TODO testear
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

    /// <summary> Método para verificar la cédula de un <see cref="Usuario"/> </summary>
    /// <param name="cedula"> Cédula del <see cref="Usuario"/> </param>
    /// <returns> Devuelve true si el formato es válido, de lo contrario devuelve false </returns>
    public bool VerificarCedula(string cedula)
    { // TODO testear
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
    
    /// <summary> Método para eliminar un <see cref="Usuario"/> </summary>
    /// <param name="usuario"> <see cref="Usuario"/> que se desea eliminar </param>
    public void RemoveUsuario(Usuario usuario) {
        if (!usuarios.Contains(usuario)) {
            throw new ArgumentNullException("El usuario ingresado no existe");
        }
        else {
            usuarios.Remove(usuario);
        }
    }

    /// <summary> Método para obtener los datos de un <see cref="Usuario"/> </summary>
    /// <param name="nombre"> Nombre del usuario </param>
    /// <param name="apellido"> Apellido del usuario </param>
    /// <param name="contraseña"> Contraseña del usuario </param>
    /// <returns> Devuelve el <see cref="Usuario"/> que coincida con los parámetros dados </returns>
    public Usuario GetUsuario(string nickname, string contraseña)
    {
        foreach (Usuario user in usuarios)
        {
            if (user.Nick.Equals(nickname) && user.VerifyContraseña(contraseña))
            {
                return user;
             }
        }
        throw (new ArgumentException("Los datos introducidos no coinciden con ningun usuario"));
    }
}
