namespace Library;

/// <summary> Clase <see cref="Trabajador"> que hereda de <see cref="Usuario">. </summary>
public class Trabajador : Usuario, ICalificable
{

    private List<Calificacion> Reputacion { get; set; }

    /// <summary> Constructor de la clase. </summary>
    /// <param name="nombre"> Nombre del usuario. </param> 
    /// <param name="apellido"> Apellido del usuario. </param> 
    /// <param name="contraseña"> Contraseña del usuario. </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del usuario. </param> 
    /// <param name="cedula"> Cédula del usuario. </param> 
    /// <param name="telefono"> Teléfono del usuario. </param> 
    /// <param name="correo"> Correo electrónico del usuario. </param> 
    /// <param name="ubicacion"> Ubicación del usuario. </param>
    /// <returns> Devuelve instancia de <see cref="Trabajador"> creada. </returns>    
    public Trabajador(string nombre, string apellido, string nick, string contraseña, DateTime fechaNacimiento, string cedula,
     string telefono, string correo, Tuple<double, double>  ubicacion)
    {
        this.Tipo = TipoDeUsuario.Trabajador;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Nick = nick;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Correo = correo;
        this.Ubicacion = ubicacion;
        this.SetContraseña(contraseña);
        this.Reputacion = new List<Calificacion>();
        this.Activo = true;
    }

    public override bool IsActive()
    {
        return this.Activo;
    }
    
    /// <summary> Método para calificar un usuario. </summary>
    public void Calificar(Calificacion Rate)
    {
        this.Reputacion.Add(Rate);
    }

    /// <summary> Método para obtener las calificaciones del usuario. </summary>
    /// <returns> Retorna el promedio de las calificaciones de un usuario, cualquiera que sea. </returns>
    public Calificacion GetReputacion()
    {
        if (this.Reputacion.Count() <= 0)
        {
            return Calificacion.NoCalificado;
        }
        int x = 0;
        foreach (var calif in this.Reputacion)
        {
            x += (int)calif;
        }

        x /= this.Reputacion.Count;
        return (Calificacion)x;
    }

    /// <summary> Método para obtener datos de un <see cref="Trabajador">. </summary>
    /// <returns> Devuelve los atributos dados de <see cref="Trabajador">. </returns>
    public Usuario GetContecto()
    {
        return this;
    }
}
