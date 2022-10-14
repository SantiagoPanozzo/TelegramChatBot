namespace Usuario;
using System;
using System.Text;

abstract class Usuario {
    
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public string Cedula {get; set;}
    public string Telefono {get; set;}
    public string Ubicacion {get; set;} //TODO hacer la tupla de la ubicacion
    public double Reputacion {get; set;}

    public Usuario(string Nombre, string Apellido, DateTime FechaNacimiento, string Cedula, string Telefono, string Ubicacion, double Reputacion) {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.FechaNacimiento = FechaNacimiento;
        this.Cedula = Cedula;
        this.Telefono = Telefono;
        this.Ubicacion = Ubicacion;
        this.Reputacion = Reputacion;
    }

    public double GetReputacion() {
        return this.Reputacion;
    }

    public string GetContacto() {
        StringBuilder contacto = new StringBuilder();
        contacto.Append("Nombre: " + this.Nombre);
        contacto.Append("Apellido: " + this.Apellido);
        contacto.Append("Teléfono: " + this.Telefono);
        return contacto.ToString();
    }
}
