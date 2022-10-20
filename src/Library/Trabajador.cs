namespace Library;

public class Trabajador:Usuario
{
    public Trabajador(string nombre, string apellido, DateTime fechaNacimiento, string cedula, string telefono, Tuple<string,string>  ubicacion) : base(nombre, apellido, fechaNacimiento, cedula, telefono, ubicacion)
    { // TODO chequear validez del uso de :base()
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Telefono = telefono;
        this.Ubicacion = ubicacion;
    }

    public void Ofertarse(Categoria categoria, string descripcion, string empleo, double precio)
    // rabajador ofertante, string descripcion, string empleo, double precio
    { // TODO cambiar esto a una clase intermediaria?
    // wip
        categoria.AddOferta(new OfertaDeServicio(this, descripcion, empleo, precio));
    }
    
}