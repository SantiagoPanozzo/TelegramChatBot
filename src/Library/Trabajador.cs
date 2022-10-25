namespace Library;

public class Trabajador:Usuario
{
    public Trabajador(string nombre, string apellido, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo, Tuple<double, double>  ubicacion) : base(nombre, apellido, contraseña, fechaNacimiento, cedula, telefono,  correo, ubicacion)
    { // TODO chequear validez del uso de :base()
        this.Tipo = TipoDeUsuario.Trabajador;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Correo = correo;
        this.Ubicacion = ubicacion;
        this.SetContraseña(contraseña);
    }

    public Usuario GetContecto()
    {
        return this;
    }

}