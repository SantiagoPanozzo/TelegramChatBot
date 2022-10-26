namespace Library;

/// <summary>  </summary>
public class Trabajador:Usuario
{

    /// <summary>  </summary>
    /// <param name="nombre"> Nombre del usuario </param> 
    /// <param name="apellido"> Apellido del usuario </param> 
    /// <param name="contraseña"> Contraseña del usuario </param> 
    /// <param name="fechaNacimiento"> Fecha de nacimiento del usuario </param> 
    /// <param name="cedula"> Cédula del usuario </param> 
    /// <param name="telefono"> Teléfono del usuario </param> 
    /// <param name="correo"> Correo electrónico del usuario </param> 
    /// <param name="ubicacion"> Ubicación //TODO(ver como) del usuario </param>
    /// <returns>   </returns>
    public Trabajador(string nombre, string apellido, string nick, string contraseña, DateTime fechaNacimiento, string cedula,
     string telefono, string correo, Tuple<double, double>  ubicacion)
    { // TODO chequear validez del uso de :base()
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
    }

    /// <summary>  </summary>
    /// <returns>  </returns>
    public Usuario GetContecto()
    {
        return this;
    }
}
