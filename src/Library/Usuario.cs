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
    public string Correo { get; set; }


    public Usuario(string Nombre, string Apellido, DateTime FechaNacimiento, string Cedula, string Telefono, Tuple<string,string>  Ubicacion, double Reputacion, string Correo) {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.FechaNacimiento = FechaNacimiento;
        this.Cedula = Cedula;
        this.Telefono = Telefono;
        this.Ubicacion = Ubicacion;
        this.Reputacion = Reputacion;
        this.Correo = Correo;
    }
    
    public double GetReputacion(){
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
