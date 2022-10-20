namespace Library;
using System.Collections.Generic;
public class SolicitudCatalog{

    private List<Solicitud> _solicitudes = new List<Solicitud>();

    public void AddSolicitud(OfertaDeServicio Oferta, Empleador empleador){

        _solicitudes.Add(new Solicitud(Oferta, empleador) );

    }
    public void RemoveSolicitud (Solicitud solicitud){

        _solicitudes.Remove(solicitud);
    }

}

