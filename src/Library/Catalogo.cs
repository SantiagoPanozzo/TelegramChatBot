namespace Library;
using System.Collections.Generic;
public class Catalogo{

    private List<Solicitud> solicitudes = new List<Solicitud>();

    public void addSolicitud(OfertaDeServicio Oferta, Empleador empleador){

        solicitudes.Add(new Solicitud(Oferta, empleador) );

    }

    public void removeSolicitud (Solicitud solicitud){

        solicitudes.Remove(solicitud);
    }


}

