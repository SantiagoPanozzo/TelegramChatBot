using Library.Excepciones;

namespace Library;
using System;
using System.Text.RegularExpressions;

/// <summary> Clase para manejar el registro. </summary>
/// <!-- Utilizamos patrón singleton ya que solo necesitamos una misma instancia de esta clase, si hubieran más
/// se mezclarían los elementos de la misma y no sabríamos a cual instancia acceder para hacer el registro y
/// de usuarios. -->
public class RegistryHandler
{
    private UsuariosCatalog usuarios;
    
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

    static Regex ValidEmailRegex = RegexValidarEmail();
    
    /// <summary> Método para borrar los datos de la clase. </summary>
    /// <param name="user"> Tipo de usuario que llama al método. </param>
    public static void Wipe(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            RegistryHandler._instance = null;
        }
        else
        {
            throw (new ElevacionException("Solo un administrador puede utilizar el método Wipe() de RegistryHandler"));
        }
    }
    
    /// <summary> Constructor de tipo Singleton de la clase. </summary>
    private RegistryHandler()
    {
        this.usuarios = UsuariosCatalog.GetInstance();
    }

    /// <summary> Método para obtener la instancia. </summary>
    /// <returns> Devuelve la instancia. </returns>
    public static RegistryHandler GetInstance()
    {
        return RegistryHandler.Instance;
    }

    /// <summary> Método para registrar un trabajador. </summary>
    /// <param name="nombre"> Nombre del trabajador. </param> 
    /// <param name="apellido"> Apellido del trabajador. </param> 
    /// <param name="contraseña"> Contraseña del trabajador. </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del trabajador. </param> 
    /// <param name="cedula"> Cédula del trabajador. </param> 
    /// <param name="telefono"> Teléfono del trabajador. </param> 
    /// <param name="correo"> Correo electrónico del trabajador. </param> 
    /// <param name="ubicacion"> Ubicación del trabajador. </param>
    /// <returns> Devuelve la instancia de <see cref="Trabajador"/> creada. </returns>
    /// <!-- Utilizamos el patrón Creator para crear instancias de las clases que heredan de Usuario dentro del registro,
    /// no hay otra clase que deba crear usuarios más que esta, ya que los métodos de Registrar comprueban primero que
    /// todos los datos sean válidos y luego almacena en esta misma clase las instancias creadas. Es la única que va a
    /// interactuar directamente con los usuarios. Las demás clases que necesiten a los usuarios interactuarán con esta. -->
    public Trabajador RegistrarTrabajador(string nombre, string apellido, string nick, string contraseña, string fechaNacimiento, 
                                          string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula) && VerificarNick(nick))
        {
            Trabajador nuevoTrabajador = (Trabajador)usuarios.AddUsuario( TipoDeUsuario.Trabajador, nombre,  apellido,  nick,  contraseña,  nacimiento, 
                 cedula,  telefono,  correo,  ubicacion);
            return nuevoTrabajador;
        }
        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));
    }

    /// <summary> Método para registrar un empleador. </summary>
    /// <param name="nombre"> Nombre del empleador. </param> 
    /// <param name="apellido"> Apellido del empleador. </param> 
    /// <param name="contraseña"> Contraseña del empleador. </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del empleador. </param> 
    /// <param name="cedula"> Cédula del empleador. </param> 
    /// <param name="telefono"> Teléfono del empleador. </param> 
    /// <param name="correo"> Correo electrónico del empleador. </param> 
    /// <param name="ubicacion"> Ubicación del empleador. </param>
    /// <returns> Devuelve la instancia de <see cref="Empleador"/> creada. </returns>
    public Empleador RegistrarEmpleador(string nombre, string apellido, string nick, string contraseña, string fechaNacimiento, 
                                        string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        DateTime nacimiento = DateTime.Parse(fechaNacimiento);
        if (VerificarCorreo(correo) && VerificarCedula(cedula) && VerificarNick(nick))
        {
            Empleador nuevoEmpleador = (Empleador)usuarios.AddUsuario( TipoDeUsuario.Empleador, nombre,  apellido,  nick,  contraseña,  nacimiento, 
                cedula,  telefono,  correo,  ubicacion);
            return nuevoEmpleador;
        }

        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));

    }

    /// <summary> Método para registrar un administrador. </summary>
    /// <param name="nick"> Nick del administrador. </param>
    /// <param name="contraseña"> Contraseña del administrador. </param>
    /// <param name="telefono"> Celéfono del administrador. </param>
    /// <param name="correo"> Correo del administrador. </param>
    /// <returns> Devuelve la instancia de <see cref="Trabajador"/> creada. </returns>
    public Administrador RegistrarAdministrador(string nick, string contraseña, string telefono, string correo)
    {
        if (VerificarCorreo(correo) && VerificarNick(nick))
        {
            Administrador nuevoAdministrador = (Administrador)usuarios.AddAdminstrador(nick, contraseña, telefono, correo);
            return nuevoAdministrador;
        }
        throw (new ArgumentException("Alguno de los valores introducidos no fue válido"));
    }

    /// <summary> Método para verificar el nickname de un <see cref="Usuario"/>. </summary>
    /// <param name="nick"> Nickname del <see cref="Usuario"/>. </param>
    /// <returns> Devuelve true si no existe otro <see cref="Usuario"/> con ese nick, de lo contrario devuelve false. </returns>
    public bool VerificarNick(string nick)
    {
        foreach (Usuario usuario in usuarios.GetUsuarios())
        {
            if (usuario.Nick.Equals(nick)) return false;
        }
        return true;
    }
    
    /// <summary> Referencia: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx. </summary>
    private static Regex RegexValidarEmail() {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
    }

    /// <summary> Método para verificar un correo. </summary>
    /// <param name="correo"> Correo para verificar. </param>
    /// <returns> Devuelve true si el correo es válido, en caso contrario false. </returns>
    public bool VerificarCorreo(string correo) {
        bool isValid = ValidEmailRegex.IsMatch(correo);
        return isValid;
    }

    /// <summary> Método para verificar la cédula de un <see cref="Usuario"/>. </summary>
    /// <param name="cedula"> Cédula del <see cref="Usuario"/>. </param>
    /// <returns> Devuelve true si el formato es válido, de lo contrario devuelve false. </returns>
    public bool VerificarCedula(string cedula)
    {
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
    
    /// <summary> Método para eliminar un <see cref="Usuario"/>. </summary>
    /// <param name="usuario"> <see cref="Usuario"/> que se desea eliminar. </param>
    public void RemoveUsuario(Usuario admin, Usuario usuarioEliminar)
    {
        if (!admin.GetTipo().Equals(TipoDeUsuario.Administrador))
            throw (new ElevacionException("Solo un administrador puede utilizar el método RemoveUsuario() de RegistryHandler"));
        
        if (!usuarios.GetUsuarios().Contains(usuarioEliminar)) {
            throw new ArgumentNullException("El usuario ingresado no existe");
        }
        else {
            usuarios.RemoveUsuario(admin,usuarioEliminar);
        }
    }

    /// <summary> Método para obtener los datos de un <see cref="Usuario"/>. </summary>
    /// <param name="nombre"> Nombre del usuario. </param>
    /// <param name="apellido"> Apellido del usuario. </param>
    /// <param name="contraseña"> Contraseña del usuario. </param>
    /// <returns> Devuelve el <see cref="Usuario"/> que coincida con los parámetros dados. </returns>
    public Usuario GetUsuario(string nickname, string contraseña)
    {
        foreach (Usuario user in usuarios.GetUsuarios())
        {
            if (user.Nick.Equals(nickname) && user.VerifyContraseña(contraseña))
            {
                return user;
             }
        }
        throw (new ArgumentException("Los datos introducidos no coinciden con ningun usuario"));
    }

    /// <summary> Método para obtener reputación de un trabajador o empleador. </summary>
    /// <param name="nickname"> Nickname del usuario. </param>
    /// <returns> Devuelve la reputación. </returns>
    public Calificacion GetReputacion(string nickname)
    {
        foreach (Usuario user in usuarios.GetUsuarios())
        {
            if(user.Nick.Equals(nickname))
            {
                if (user is Trabajador) return ((Trabajador)user).GetReputacion();
                if (user is Empleador) return ((Empleador)user).GetReputacion();
            }
        }
        throw (new("Usuario no encontrado"));
    }
    
    /// <summary> Método para obtener la información pública de un usuario. </summary>
    /// <param name="nickname"> Nickname del usuario. </param>
    /// <returns> Devuelve la información del usuario. </returns>
    public Dictionary<string, string> GetUserInfo(string nickname)
    {
        Usuario user = GetUser(nickname);
        return user.GetPublicInfo();
    }

    /// <summary> Método para obtener el contacto de un usuario. </summary>
    /// <param name="nickname"> Nickname del usuario. </param>
    /// <returns> Devuelve el contacto del usuario. </returns>
    public Dictionary<string, string> GetUserContact(string nickname)
    {
        Usuario user = GetUser(nickname);
        return user.GetContacto();
    }

    /// <summary> Método para obtener una instancia de un usuario. </summary>
    /// <param name="nickname"> Nickname del usuario. </param>
    /// <returns> Devuelve la instancia del usuario en caso de que exista. </returns>
    private Usuario GetUser(string nickname)
    {
        Usuario? user = null;
        try {
            foreach (Usuario usuario in usuarios.GetUsuarios()) {
                if (usuario.Nick.Equals(nickname)) {
                    user = usuario;
                }
            }
        }
        catch(NullReferenceException) {
            throw (new Exception("No se encontró el usuario"));
        }
        return user;
    }

    /// <summary> Método para obtener la lista de trabajadores. </summary>
    /// <returns> Devuelve lista de trabajadores. </returns>
    public List<string> GetTrabajadores()
    {
        List<string> trabajadores = new();
        foreach (Usuario usuario in usuarios.GetUsuarios())
        {
            if (usuario.GetTipo().Equals(TipoDeUsuario.Trabajador))
            {
                trabajadores.Add(usuario.Nick);
            }
        }

        return trabajadores;
    }
    
    /// <summary> Método para obtener la lista de empleadores. </summary>
    /// <returns> Devuelve lista de empleadores. </returns>
    public List<string> GetEmpleadores()
    {
        List<string> empleadores = new();
        foreach (Usuario usuario in usuarios.GetUsuarios())
        {
            if (usuario.GetTipo().Equals(TipoDeUsuario.Trabajador))
            {
                empleadores.Add(usuario.Nick);
            }
        }

        return empleadores;
    }

    /// <summary> Método para obtener lista de usuarios calificables. </summary>
    /// <returns> Devuelve lista. </returns>
    private List<ICalificable> NonAdmins()
    {
        List<ICalificable> nonAdmins = new();
        foreach (Usuario user in usuarios.GetUsuarios())
        {
            if (user is ICalificable)
            {
                nonAdmins.Add((ICalificable)user);
            }
        }
        return NonAdmins();
    }
}
