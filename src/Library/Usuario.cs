namespace Library;
using System;
using System.Text;

public abstract class Usuario {
    
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string Cedula {get; set;}
    public string Telefono {get; set;}
    public Tuple<string,string>  Ubicacion {get; set;} // TODO testear cuando tengamos una clase que herede de Usuario
    public double Reputacion {get; set;}

    public Usuario(string nombre, string apellido, DateTime fechaNacimiento, string cedula, string telefono, Tuple<string,string>  ubicacion, double reputacion) {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Ubicacion = ubicacion;
        this.Reputacion = reputacion;
    }
    
    public double GetReputacion() {
        return this.Reputacion;
    }

    public string GetContacto() {
        StringBuilder contacto = new StringBuilder();
        contacto.Append($"Nombre: {this.Nombre}\n");
        contacto.Append($"Apellido: {this.Apellido}\n");
        contacto.Append($"Teléfono: {this.Telefono}");
        return contacto.ToString();
    }
}
