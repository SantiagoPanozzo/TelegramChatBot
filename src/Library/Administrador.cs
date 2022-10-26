namespace Library;

public class Administrador : Usuario
{
    public void Autorizar(OfertaDeServicio oferta)
    {
        //Falta intermediario para las ofertas?
    }

    public Administrador(string nombre, string apellido, string contraseña, DateTime fechaNacimiento, string cedula, string telefono, string correo,
        Tuple<double, double> ubicacion) : base(nombre, apellido, contraseña, fechaNacimiento, cedula, telefono, correo, ubicacion)
    {
        this.Tipo = TipoDeUsuario.Administrador;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Correo = correo;
        this.Ubicacion = ubicacion;
        this.SetContraseña(contraseña);
    }
}