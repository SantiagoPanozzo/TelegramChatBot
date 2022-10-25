namespace Library;
using System;
public class ContratoHandler{
    public SolicitudCatalog Catalogo = new SolicitudCatalog();
    public void SolicitarTrabajador(OfertaDeServicio oferta, Empleador emp){
        Catalogo.AddSolicitud(oferta, emp);
    }

    public void AceptarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Aceptada);
    }
    public void RechazarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Rechazada);

    }
}