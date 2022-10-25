namespace Library;
using System;
using System.Text;

public abstract class Usuario {
    
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string Cedula {get; set;}
    public string Telefono {get; set;}
    public Tuple<double,double>  Ubicacion {get; set;} // TODO testear cuando tengamos una clase que herede de Usuario
    private List<Calificacion> Reputacion { get; set; }
    public string Correo { get; set; }
    private bool Activo { get; set; }
    private string Contraseña { get; set; }
    protected TipoDeUsuario Tipo { get; set; }
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

    public TipoDeUsuario GetTipo()
    {
        return this.Tipo;
    }
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

    public void Calificar(Calificacion Rate)
    {
        this.Reputacion.Add(Rate);
    }
    
    public string GetContacto()
    {
        StringBuilder contacto = new StringBuilder();
        contacto.Append($"Nombre: {this.Nombre}\n");
        contacto.Append($"Apellido: {this.Apellido}\n");
        contacto.Append($"Teléfono: {this.Telefono}");
        return contacto.ToString();
    }

    public bool isActivo()
    {
        return this.Activo;
    }

    public void Deactivate()
    {
        this.Activo = false;
    }

    public void ReActivate()
    {
        this.Activo = true;
    }

    protected void SetContraseña(string contraseña)
    {
        // Este metodo solo existe por modularidad
        // Sugerencia: implementar un sistema mejor.
        this.Contraseña = contraseña;
    }

    public bool VerifyContraseña(string contraseña)
    {
        // Idem de SetContraseña
        return this.Contraseña.Equals(contraseña);
    }
}
