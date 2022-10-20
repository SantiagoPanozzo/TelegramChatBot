namespace Library;
using System;

public class Registro {
    private List<Usuario> usuarios = new();

    public void AddUsuario(Usuario nuevoUsuario) {
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
}
