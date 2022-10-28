namespace Library;
using System;
using System.Text;

/// <summary> Clase empleador que hereda de <see cref="Usuario"/> </summary>
public class Empleador:Usuario,ICalificable{
    
    private List<Calificacion> Reputacion { get; set; }

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
    
    /// <summary> Método para calificar un usuario </summary>
    public void Calificar(Calificacion Rate)
    {
        this.Reputacion.Add(Rate);
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

}
