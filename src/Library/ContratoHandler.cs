namespace Library;
using System;
public class ContratoHandler{
    SolicitudCatalog catalogo = new SolicitudCatalog();
    public void SolicitarTrabajador(Trabajador trabajador){
        Solicitud solicitud = new Solicitud();
    }

    public void AceptarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(1);
    }
    public void RechazarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(2);

    }
}