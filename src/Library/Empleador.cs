namespace Library;
using System;
using System.Text;

public class Empleador:Usuario{

    public Empleador(string nombre, string apellido, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion): base(nombre, apellido, contraseña, fechaNacimiento, cedula, telefono, correo, ubicacion)
    {
        this.Tipo = TipoDeUsuario.Empleador;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Ubicacion = ubicacion;
        this.Correo = correo;
        this.SetContraseña(contraseña);
    }
}
