namespace Library;
using System;
using System.Text;

public class Empleador:Usuario{

    public Empleador(string Nombre, string Apellido, DateTime FechaNacimiento, string Cedula, string Telefono, Tuple<string,string>  Ubicacion, double Reputacion, string Correo) {

        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.FechaNacimiento = FechaNacimiento;
        this.Cedula = Cedula;
        this.Telefono = Telefono;
        this.Ubicacion = Ubicacion;
        this.Reputacion = Reputacion;
        this.Correo = Correo;
    }
}
