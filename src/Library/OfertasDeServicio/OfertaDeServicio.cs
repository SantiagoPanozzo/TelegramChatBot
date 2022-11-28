using Library.Excepciones;

namespace Library;

/// <summary> Clase que representa una oferta de servicio </summary>
public class OfertaDeServicio : IDesactivable

{
    private Trabajador Ofertante { get; set; }
    public string Descripcion { get; set; }
    public string Empleo { get; set; }
    public double Precio { get; set; }
    public Calificacion Rate { get; set; }
    public bool Disponible { get; set; }
    private int _id;
    private static int Instancias { get; set; } = 0;
    private bool Activa { get; set; }
    private Tuple<double,double> Ubicacion { get; set; }

    /// <summary> Constructor de la clase </summary>
    /// <param name="ofertante"> Ofertante de la oferta </param>
    /// <param name="descripcion"> Descripción de la oferta </param>
    /// <param name="empleo"> Rubro del ofertante </param>
    /// <param name="precio"> Precio de la oferta </param>
    public OfertaDeServicio(Trabajador ofertante, string descripcion, string empleo, double precio)
    {
        this.Ofertante = ofertante;
        this.Descripcion = descripcion;
        this.Empleo = empleo;
        this.Precio = precio;
        OfertaDeServicio.Instancias++;
        this.Activa = true;
        this._id = Instancias;
        this.Ubicacion = Ofertante.Ubicacion;
        this.Rate = Calificacion.NoCalificado;
        this.Disponible = true;
    }
    
    /// <summary>Método para borrar los datos de la clase</summary>
    /// <param name="admin">tipo de usuario que llama al método</param>

    public static void Wipe(Administrador admin)
    {
        OfertaDeServicio.Instancias = 0;
    }

    /// <summary> Método para obtener id de <see cref="OfertaDeServicio"/> </summary>
    /// <returns> Devuelve el id de la <see cref="OfertaDeServicio"/> </returns>
    public int GetId()
    {
        return this._id;
    }

    /// <summary> Método para obtener NickName de Ofertante</summary>
    /// <returns> Devuelve el ofertante de la <see cref="OfertaDeServicio"/> </returns>
    public string GetOfertante()
    {
        return this.Ofertante.Nick;
    }

    /// <summary> Método para obtener la reputación del ofertante </summary>
    /// <returns> Devuelve la <see cref="Calificacion"/> del <see cref="Trabajador"/> ofertante </returns>
    public Calificacion GetReputacion()
    {
        return Ofertante.GetReputacion();
    }

    /// <summary> Método para obtener la ubicación de la oferta de servicio </summary>
    /// <returns> Devuelde la ubicación </returns>
    public Tuple<double, double> GetUbicacion()
    {
        return this.Ubicacion;
    }

    /// <summary> Método para obtener el contacto del ofertante </summary>
    /// <returns> Devuelve el contacto </returns>
    public Dictionary<string, string> GetContacto()
    {
        return Ofertante.GetContacto();
    }

    /// <summary> Método para verificar si fue calificada la oferta </summary>
    /// <returns> Deuelve true si fue calificado, en caso contrario será false </returns>
    public bool IsRated()
    {
        return (!this.Rate.Equals(Calificacion.NoCalificado));
    }

    /// <summary> Método para calificar la oferta en cuestión </summary>
    /// <param name="rate"> Valor de <see cref="Calificacion"/> </param>
    public void RateMe(Calificacion rate)
    {
        if(!this.IsRated())
        {
            this.Rate = rate;
            this.Ofertante.Calificar(rate);
        }
        else
        {
            throw (new YaCalificadoException("La oferta ya fue calificada"));
        }
    } 

    /// <summary> Método para obtener la calificación dada a la oferta tras ser finalizada </summary>
    /// <returns> Devuelve la <see cref="Calificacion"/> correspondiente de la oferta </returns>
    public Calificacion GetCalificacion()
    {
        return this.Rate;
    }
    
    /// <summary> Método para conocer el estado de la <see cref="OfertaDeServicio"/> </summary>
    /// <returns> Devuelve el estado de <see cref="OfertaDeServicio"/>, true si está activa, false si no está activa  </returns>
    public bool IsActive()
    {
        return this.Activa;
    }
    
    /// <summary> Método para dar de baja un <see cref="Usuario"/> </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> que se dará de baja </param>
    public void DarDeBaja(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            if (this.Activa) this.Activa = false;
            else throw (new AccionInnecesariaException("Esta oferta ya fue dada de baja"));
        }
        else if (user.GetTipo().Equals(TipoDeUsuario.Trabajador))
        {
            if (user.Nick.Equals(Ofertante.Nick))
            {
                if (this.Activa) this.Activa = false;
                else throw (new AccionInnecesariaException("Esta oferta ya fue dada de baja"));
            }
            else
            {
                throw (new UsuarioIncorrectoException(
                    "Solo un administrador o el Trabajador que creó la oferta puede utilzar el método DarDeBaja()"));
            }
        }
        else
        {
            throw (new ElevacionException("Solo un administrador o el Trabajador que creó la oferta puede utilizar el método DarDeBaja()"));
        }
        
    }

    /// <summary> Método para reactivar un <see cref="Usuario"/> </summary>
    /// <param name="user"> Tipo de <see cref="Usuario"/> que se reactivará </param>
    public void Reactivar(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            if (!this.Activa) this.Activa = true;
            else throw (new AccionInnecesariaException("Esta oferta ya esta activa"));
        }
        else if (user.GetTipo().Equals(TipoDeUsuario.Trabajador) && user.Nick.Equals(Ofertante.Nick))
        {
            if (!this.Activa) this.Activa = false;
            else throw (new AccionInnecesariaException("Esta oferta ya está activa"));
        }
        else
        {
            throw (new ElevacionException("Solo un administrador o un usuario autorizado puede utilizar el método Reactivar() de OfertaDeServicio"));
        }
        
    }
}
