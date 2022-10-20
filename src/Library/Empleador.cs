namespace Library;
using System;
using System.Text;

public class Empleador:Usuario{

    public Empleador(string Nombre, string Apellido, DateTime FechaNacimiento, string Cedula, string Telefono, Tuple<string,string>  Ubicacion, double Reputacion, string Correo) {
        // public string Correo { get; set; } se bugea todo (?

        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.FechaNacimiento = FechaNacimiento;
        this.Cedula = Cedula;
        this.Telefono = Telefono;
        this.Ubicacion = Ubicacion;
        this.Reputacion = Reputacion;
        this.Correo = Correo;
    }
    
    public override double GetReputacion(){
        return this.Reputacion;
    }

    public override string GetContacto(){
        StringBuilder contactoempleador = new StringBuilder();
        contactoempleador.Append($"Nombre: {this.Nombre}\n");
        contactoempleador.Append($"Apellido: {this.Apellido}\n");
        contactoempleador.Append($"Teléfono: {this.Telefono}");
        return contactoempleador.ToString();
    }
}
