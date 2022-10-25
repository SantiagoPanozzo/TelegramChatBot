namespace Library;
using System.Collections.Generic;
public class SolicitudCatalog{

    public List<Solicitud> Solicitudes = new List<Solicitud>();

    public void AddSolicitud(OfertaDeServicio Oferta, Empleador empleador){

        Solicitudes.Add(new Solicitud(Oferta, empleador) );

    }
    public void RemoveSolicitud (Solicitud solicitud){

        Solicitudes.Remove(solicitud);
    }

}

