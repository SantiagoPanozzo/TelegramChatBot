namespace Library;
using System;

/// <summary> Clase para manejar el catalogo </summary>
public class ContratoHandler{
    public SolicitudCatalog Catalogo = new SolicitudCatalog();

    /// <summary>  </summary>
    /// <param name="oferta">  </param>
    /// <param name="emp">  </param>
    public void SolicitarTrabajador(OfertaDeServicio oferta, Empleador emp){
        Catalogo.AddSolicitud(oferta, emp);
    }

    /// <summary>  </summary>
    /// <param name="solicitud">  </param>
    public void AceptarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Aceptada);
    }

    /// <summary>  </summary>
    /// <param name="solicitud">  </param>
    public void RechazarSolicitud(Solicitud solicitud){
        solicitud.RecibirRespuesta(Aceptacion.Rechazada);

    }
}
