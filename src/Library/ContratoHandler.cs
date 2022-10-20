namespace Library;
using System;
public class ContratoHandler{
    private SolicitudCatalog _catalogo = new SolicitudCatalog();
    public void SolicitarTrabajador(OfertaDeServicio oferta, Empleador emp){
        _catalogo.AddSolicitud(oferta, emp);
    }

    public void AceptarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Aceptada);
    }
    public void RechazarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Rechazada);

    }
}