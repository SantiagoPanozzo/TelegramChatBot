namespace Library;
using System;
using System.Text;

/// <summary> Clase empleador que hereda de <see cref="Usuario"/> </summary>
public class Empleador:Usuario{

    /// <summary> Constructor de la clase <see cref="Empleador"/> </summary>
    /// <returns> Retorna tipo <see cref="Empleador.Empleador(string, string, string, DateTime, string, string, string, Tuple{double, double})"/> </returns>
    public Empleador(string nombre, string apellido, string nick, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo, Tuple<double,double> ubicacion)
    {
        this.Tipo = TipoDeUsuario.Empleador;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Nick = nick;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Ubicacion = ubicacion;
        this.Correo = correo;
        this.SetContraseña(contraseña);
    }
}
