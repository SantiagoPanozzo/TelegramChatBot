namespace Library;
using System;
using System.Text;

/// <summary> Clase abstracta <see cref="Usuario"/> para que luego hereden <see cref="Administrador"/>, <see cref="Trabajador"/> y <see cref="Empleador"/> </summary>
public abstract class Usuario {
    
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string Cedula {get; set;}
    public string Telefono {get; set;}
    public Tuple<double,double> Ubicacion {get; set;} // TODO testear cuando tengamos una clase que herede de Usuario
    private List<Calificacion> Reputacion { get; set; }
    public string Correo { get; set; }
    private bool Activo { get; set; }
    private string Contraseña { get; set; }
    protected TipoDeUsuario Tipo { get; set; }

    /// <summary> Constructor de la clase </summary>
    /// <returns> Retorna tipo <see cref="Usuario.Usuario(string, string, string, DateTime, string, string, string, Tuple{double, double})"/> con sus respectivos atributos </returns>
    public Usuario(string nombre, string apellido, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double>  ubicacion) {
    
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Correo = correo;
        this.Ubicacion = ubicacion;
        this.Reputacion = new List<Calificacion>();
        this.Activo = true;
        this.SetContraseña(contraseña);
    }

    /// <summary> Checkea que tipo de usuario es, puede ser Administrador, Trabajador o Empleador </summary>
    /// <returns> Retorna el valor indicado, teniendo en cuenta el enum <see cref="TipoDeUsuario"/> 0 = Administrador, 1 = Trabajador, 2 = Empleador</returns>
    public TipoDeUsuario GetTipo() {
        return this.Tipo;
    }

    /// <summary> Método para obtener las calificaciones del usuario </summary>
    /// <returns> Retorna el promedio de las calificaciones de un usuario, cualquiera que sea  </returns>
    public Calificacion GetReputacion()
    {
        int x = 0;
        foreach (var calif in this.Reputacion)
        {
            x += (int)calif;
        }

        x /= this.Reputacion.Count;
        return (Calificacion)x;
    }

    /// <summary> Método para calificar un usuario </summary>
    public void Calificar(Calificacion Rate)
    {
        this.Reputacion.Add(Rate);
    }
    
    /// <summary> Método para obtener el contacto de un usuario </summary>
    /// <returns> Retorna un mensaje con los datos para contactar del usuario </returns>
    public string GetContacto() {
        StringBuilder contacto = new StringBuilder();
        contacto.Append($"Nombre: {this.Nombre}\n");
        contacto.Append($"Apellido: {this.Apellido}\n");
        contacto.Append($"Teléfono: {this.Telefono}");
        return contacto.ToString();
    }


    /// <summary> Checkea si el usuario está activo </summary>
    /// <returns> Retorna un valor de bool, True si está activo o False si no lo está </returns>
    public bool isActivo()
    {
        return this.Activo;
    }

    /// <summary> Método para desactivar un usuario </summary>
    public void Deactivate()
    {
        this.Activo = false;
    }

    /// <summary> Método para activar un usuario </summary>
    public void ReActivate()
    {
        this.Activo = true;
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
}
