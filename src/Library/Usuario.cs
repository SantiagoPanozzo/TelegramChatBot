namespace Library;
using System;
using System.Text;

/// <summary> Clase abstracta <see cref="Usuario"/> 
/// para que luego hereden <see cref="Administrador"/>, <see cref="Trabajador"/> y <see cref="Empleador"/> </summary>
public abstract class Usuario : IDesactivable {
    
    public string Nick { get; set; }
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string Cedula {get; set;}
    public string Telefono {get; set;}
    public Tuple<double,double> Ubicacion {get; set;}
    public string Correo { get; set; }
    protected bool Activo { get; set; }
    private string Contraseña { get; set; }
    protected TipoDeUsuario Tipo { get; set; }

    /// <summary> Checkea que tipo de usuario es, puede ser Administrador, Trabajador o Empleador </summary>
    /// <returns> Retorna el valor indicado, teniendo en cuenta el enum <see cref="TipoDeUsuario"/> 
    /// 0 = Administrador, 1 = Trabajador, 2 = Empleador </returns>
    public TipoDeUsuario GetTipo() {
        return this.Tipo;
    }

    /// <summary> Método para obtener el contacto de un usuario </summary>
    /// <returns> Retorna un mensaje con los datos para contactar del usuario </returns>
    public Dictionary<string, string> GetContacto() {
        Dictionary<string, string> info = this.GetPublicInfo();
        info.Add("Telefono",this.Telefono);
        info.Add("Correo",this.Correo);
        return info;
    }

    public Dictionary<string, string> GetPublicInfo()
    {
        Dictionary<string, string> info = new Dictionary<string, string>();
        info.Add("Nick", this.Nombre);
        info.Add("Nombre", this.Nombre);
        info.Add("Apellido", this.Apellido);
        return info;
    }

    /// <summary> Método para settear la contraseña </summary>
    protected void SetContraseña(string contraseña)
    {
        // Este metodo solo existe por modularidad
        // Sugerencia: implementar un sistema mejor.
        this.Contraseña = contraseña;
    }

    /// <summary> Método verificar que la contraseña sea igual a la anteriormente ingresada al iniciar sesión </summary>
    public bool VerifyContraseña(string contraseña)
    {
        // Idem de SetContraseña
        return this.Contraseña.Equals(contraseña);
    }

    /// <summary> Checkea si el usuario está activo </summary>
    /// <returns> Retorna un valor de bool, True si está activo o False si no lo está </returns>
    public bool IsActive()
    {
        return this.Activo;
    }

    /// <summary> Método para desactivar un usuario </summary>
    public void DarDeBaja(Usuario user)
    {
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.Activo = false;
        }
    }

    /// <summary> Método para activar un usuario </summary>
    public void Reactivar(Usuario user)
    {
        if(user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            this.Activo = true;
        }
    }
}
