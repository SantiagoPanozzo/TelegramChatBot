namespace Library;
using System.Collections.Generic;

/// <summary> Clase para para manejar el catálogo de solicitudes </summary>
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
    
    private SolicitudCatalog()
    {
        this.Solicitudes = new List<Solicitud>();
    }

    public static SolicitudCatalog GetInstance()
    {
        return SolicitudCatalog.Instance;
    }
    
    /// <summary> Método para agregar una <see cref="Solicitud"> al catálogo </summary>
    /// <param name="Oferta"> <see cref="OfertaDeServicio"> que se busca </param>
    /// <param name="empleador"> <see cref="Empleador"> que realiza la <see cref="Solicitud"> </param>
    public Solicitud AddSolicitud(OfertaDeServicio Oferta, Empleador empleador)
    {
        Solicitud solicitud = new Solicitud(Oferta, empleador);
        Solicitudes.Add(solicitud);
        return solicitud;
    }

    /// <summary> Método para eliminar una <see cref="Solicitud"> </summary>
    /// <param name="solicitud"> <see cref="Solicitud"> que se desea eliminar </param>
    public void RemoveSolicitud (Solicitud solicitud){
        Solicitudes.Remove(solicitud);
    }
}
