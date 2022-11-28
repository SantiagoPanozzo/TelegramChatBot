namespace Library;
using System.Collections.Generic;

/// <summary> Clase para para manejar el catálogo de solicitudes. </summary>
/// /// <!-- Utilizamos patrón singleton ya que solo necesitamos una misma instancia de esta clase, si hubieran más
/// se mezclarían los elementos de la misma y no sabríamos a cual instancia acceder para obtener las solicitudes. -->

public class SolicitudCatalog
{

    public List<Solicitud> Solicitudes;
    
    private static SolicitudCatalog? _instance;

    private static SolicitudCatalog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SolicitudCatalog();
            }

            return _instance;
        }
    }
    
    /// <summary> Método para borrar los datos de la clase. </summary>
    /// <param name="user"> Tipo de usuario que llama al método. </param>
    public static void Wipe(Usuario user)
    {
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            SolicitudCatalog._instance = null;
        }
    }
    
    /// <summary> Constructor de tipo singleton de la clase, inicia la lista de solicitudes. </summary>
    private SolicitudCatalog()
    {
        this.Solicitudes = new List<Solicitud>();
    }

    /// <summary> Método para obtener la instancia de la clase. </summary>
    /// <returns> Devuelve la instancia a crear. </returns>
    public static SolicitudCatalog GetInstance()
    {
        return SolicitudCatalog.Instance;
    }
    
    /// <summary> Método para agregar una <see cref="Solicitud"> al catálogo. </summary>
    /// <param name="Oferta"> <see cref="OfertaDeServicio"> que se busca. </param>
    /// <param name="empleador"> <see cref="Empleador"> que realiza la <see cref="Solicitud">. </param>
    /// <!-- Utilizamos creator ya que al ser esta clase el catálogo que almacenará instancias de solicitud, es la
    /// clase que más sentido tiene que cree estas instancias, es la que trabajará de manera más directa con las
    /// solicitudes. -->
    public Solicitud AddSolicitud(OfertaDeServicio Oferta, Empleador empleador)
    {
        foreach (Solicitud solicitud in Solicitudes)
        {
            if (solicitud.Oferta.Equals(Oferta)) throw (new("Esa oferta no está disponible"));
        }
        Solicitud nuevaSolicitud = new Solicitud(Oferta, empleador);
        Solicitudes.Add(nuevaSolicitud);
        return nuevaSolicitud;
    }

    /// <summary> Método para eliminar una <see cref="Solicitud">. </summary>
    /// <param name="solicitud"> <see cref="Solicitud"> que se desea eliminar. </param>
    public void RemoveSolicitud (Solicitud solicitud){
        Solicitudes.Remove(solicitud);
    }
}
