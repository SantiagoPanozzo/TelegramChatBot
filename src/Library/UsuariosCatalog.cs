namespace Library;

public class UsuariosCatalog
{
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

    public static void Wipe(Administrador user)
    {
        UsuariosCatalog._instance = null;
    }

    /// <summary> Constructor de la clase, inicia la lista de las categorías </summary>
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

    /// <summary> Método para obtener <see cref="OfertaDeServicio"/> por id </summary>
    /// <param name="id"> Valor del id que se quiere filtrar </param>
    /// <returns> Devuelve la <see cref="OfertaDeServicio"/> filtrada por el id dado </returns>
    public Usuario GetUsuarioById(string nick)
    {
        foreach (Usuario usuario in Usuarios)
        {
            if (usuario.Nick.Equals(nick)) return usuario;
        }

        throw (new("No se encontró la oferta"));

    }

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

    public Usuario AddAdminstrador(string nick, string contraseña, string telefono, string correo)
    { 
        Usuario nuevoUsuario = new Administrador(nick, contraseña, telefono, correo);
        return nuevoUsuario;
    }

    public void RemoveUsuario(Usuario admin, Usuario usuarioEliminar)
    {
        if (usuarioEliminar.IsActive()) usuarioEliminar.DarDeBaja(admin);
    }

}