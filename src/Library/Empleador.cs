namespace Library;
using System;
using System.Text;

public class Empleador:Usuario{

    public Empleador(string nombre, string apellido, DateTime fechaNacimiento, string cedula, string telefono, Tuple<string,string> ubicacion, string correo): base(nombre, apellido, fechaNacimiento, cedula, telefono, ubicacion)
    {

        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Ubicacion = ubicacion;
        this.Correo = correo;
    }
}
