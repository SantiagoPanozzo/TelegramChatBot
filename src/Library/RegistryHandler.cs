namespace Library;
using System;

public class RegistryHandler {
    private List<Usuario> usuarios = new();

    public void AddUsuario(TipoDeUsuario tipo, string nombre, string apellido, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion) {
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
