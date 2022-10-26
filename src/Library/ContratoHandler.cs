namespace Library;
using System;

/// <summary> Clase para manejar el catalogo </summary>
public class ContratoHandler {
    public SolicitudCatalog Catalogo = new SolicitudCatalog();

    /// <summary> Método que crea una solicitud de trabajo </summary>
    /// <param name="oferta"> Oferta en cuestión </param>
    /// <param name="emp"> Empleador que va a realizar la solicitud </param>
    public void SolicitarTrabajador(OfertaDeServicio oferta, Empleador emp){
        Catalogo.AddSolicitud(oferta, emp);
    }

    /// <summary> Método para aceptar una solicitud </summary>
    /// <param name="solicitud"> Variable de tipo <see cref="Solicitud"> para aceptar </param>
    public void AceptarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Aceptada);
    }

    /// <summary> Método para rechazar una solicitud </summary>
    /// <param name="solicitud"> Variable de tipo <see cref="Solicitud"> para rechazar </param>
    public void RechazarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Rechazada);
    }
}
