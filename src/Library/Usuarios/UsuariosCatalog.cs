namespace Library;

/// <summary>Método para el manejo del catálogo del usuario</summary>
/// <!-- Utilizamos patrón singleton ya que solo necesitamos una misma instancia de esta clase, si hubieran más
/// se mezclarían los elementos de la misma y no sabríamos a cual instancia acceder para "almacenar" los usuarios -->
public class UsuariosCatalog {
    protected List<Usuario> Usuarios;

    private static UsuariosCatalog? _instance;

    private static UsuariosCatalog Instance
    {
        get
        {
            if (_instance == null)  
            {
                _instance = new UsuariosCatalog();
            }

            return _instance;
        }
    }

    /// <summary> Método para borrar los datos de la clase </summary>
    /// <param name="user"> Tipo de usuario que llama al método </param>

    public static void Wipe(Administrador user)
    {
        UsuariosCatalog._instance = null;
    }

    /// <summary> Constructor de la clase, inicia la lista de los usuarios </summary>
    private UsuariosCatalog()
    {
        this.Usuarios = new List<Usuario>();
    }

    public static UsuariosCatalog GetInstance()
    {
        return UsuariosCatalog.Instance;
    }

    /// <summary> Método para conocer los usuarios </summary>
    /// <returns> Retorna la lista de usuarios </returns>
    public List<Usuario> GetUsuarios()
    {
        return this.Usuarios;
    }


    /// <summary> Método para obtener <see cref="Usuario"/> por id </summary>
    /// <param name="id"> Valor del id que se quiere filtrar </param>
    /// <returns> Devuelve los <see cref="Usuario"/> existentes </returns>
    public List<Usuario> GetUsuariosIgnoreId()
    {
        var users = GetUsuarios();
        List<Usuario> final = new();
        foreach (var user in users)
        {
            final.Add(GetUsuarioById(user.Nick));
        }
        return final;
    }
    /// <summary> Método para obtener <see cref="OfertaDeServicio"/> por id </summary>
    /// <param name="nick"> Valor del id que se quiere filtrar </param>
    /// <returns> Devuelve la <see cref="OfertaDeServicio"/> filtrada por el id dado </returns>
    public Usuario GetUsuarioById(string nick)
    {
        foreach (Usuario usuario in Usuarios)
        {
            if (usuario.Nick.Equals(nick)) return usuario;
        }

        throw (new("No se encontró la oferta"));

    }

    /// <summary> Método que agrega un usuario </summary>
    /// <param name="tipo"> Tipo de usuario </param>
    /// <param name="nombre"> Nombre del usuario </param>
    /// <param name="apellido"> Apellido del usuario </param>
    /// <param name="nick"> Nickname del usuario </param>
    /// <param name="contraseña"> Contraseña del usuario </param>
    /// <param name="fechaNacimiento"> Fecha de nacimiento del usuario </param>
    /// <param name="cedula"> Cédula del usuario </param>
    /// <param name="telefono"> Teléfono del usuario </param>
    /// <param name="correo"> Correo del usuario </param>
    /// <param name="ubicacion"> Ubicación del usuario </param>
    /// <returns> Devuelve la instancia creada del usuario </returns>

    public Usuario AddUsuario(TipoDeUsuario tipo, string nombre, string apellido, string nick, string contraseña,
        DateTime fechaNacimiento,
        string cedula, string telefono, string correo, Tuple<double, double> ubicacion)

    {
        Usuario nuevoUsuario;
        switch (tipo)
        {
            case TipoDeUsuario.Trabajador:
                nuevoUsuario = new Trabajador(nombre, apellido, nick, contraseña, fechaNacimiento, cedula, telefono,
                    correo, ubicacion);
                break;
            case TipoDeUsuario.Empleador:
                nuevoUsuario = new Empleador(nombre, apellido, nick, contraseña, fechaNacimiento, cedula, telefono,
                    correo,
                    ubicacion);
                break;
            default:
                throw new("Tipo de usuario no encontrado, quizás quisiste utilizar AddAdministrador()?");
        }

        this.Usuarios.Add(nuevoUsuario);
        return nuevoUsuario;
    }

    /// <summary> Método para agregar un administrador </summary>
    /// <param name="nick"> Nickname del administrador </param>
    /// <param name="contraseña"> Contraseña del administrador </param>
    /// <param name="telefono"> Teléfono del administrador </param>
    /// <param name="correo"> Correo del administrador </param>
    /// <returns> Devuelve la instancia creada del administrador </returns>
    public Usuario AddAdminstrador(string nick, string contraseña, string telefono, string correo)
    { 
        Usuario nuevoUsuario = new Administrador(nick, contraseña, telefono, correo);
        return nuevoUsuario;
    }

    
    /// <summary> Método para eliminar un usuario </summary>
    /// <param name="admin"> Tipo de usuario que llama el método </param>
    /// <param name="usuarioEliminar"> Usuario a eliminar </param>
    public void RemoveUsuario(Usuario admin, Usuario usuarioEliminar)
    {
        if (usuarioEliminar.IsActive()) usuarioEliminar.DarDeBaja(admin);
    }
}
