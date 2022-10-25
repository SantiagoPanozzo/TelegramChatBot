namespace Library;
using System.Collections.Generic;

/// <summary>  </summary>
public class SolicitudCatalog{

    public List<Solicitud> Solicitudes = new List<Solicitud>();

    /// <summary>  </summary>
    /// <param name="Oferta">  </param>
    /// <param name="empleador">  </param>
    public void AddSolicitud(OfertaDeServicio Oferta, Empleador empleador){

        Solicitudes.Add(new Solicitud(Oferta, empleador) );

    }

    /// <summary>  </summary>
    /// <param name="solicitud">  </param>
    public void RemoveSolicitud (Solicitud solicitud){

        Solicitudes.Remove(solicitud);
    }
}
