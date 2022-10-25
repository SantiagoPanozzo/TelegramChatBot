namespace Library;
using System;

public class RegistryHandler {
    private List<Usuario> usuarios = new();

    // TODO documentar que el "fechaNacimiento" se tiene que introducir como "año,mes,dia". Idem para la cedula, correo, etc
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

    public void RemoveUsuario(Usuario usuario) {
        if (!usuarios.Contains(usuario)) {
            throw new ArgumentNullException("El usuario ingresado no existe");
        }
        else {
            usuarios.Remove(usuario);
        }
    }

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
