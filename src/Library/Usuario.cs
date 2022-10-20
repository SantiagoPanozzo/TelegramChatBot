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
    private List<Calificacion> Reputacion { get; set; }

    public Usuario(string nombre, string apellido, DateTime fechaNacimiento, string cedula, string telefono, Tuple<string,string>  ubicacion) {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Ubicacion = ubicacion;
    }
    
    public Calificacion getReputacion()
    {
        int x = 0;
        foreach (var calif in this.Reputacion)
        {
            x += (int)calif;
        }

        x /= this.Reputacion.Count;
        return (Calificacion)x;
    }

    public void Calificar(Calificacion Rate){
        this.Reputacion.Add(Rate);
    }

    public string GetContacto() {
        StringBuilder contacto = new StringBuilder();
        contacto.Append($"Nombre: {this.Nombre}\n");
        contacto.Append($"Apellido: {this.Apellido}\n");
        contacto.Append($"Teléfono: {this.Telefono}");
        return contacto.ToString();
    }
}
