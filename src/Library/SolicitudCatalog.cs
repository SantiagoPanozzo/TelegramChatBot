namespace Library;
using System.Collections.Generic;

/// <summary> Clase para para manejar el catálogo de solicitudes </summary>
public class SolicitudCatalog{

    public List<Solicitud> Solicitudes = new List<Solicitud>();

    /// <summary> Método para agregar una <see cref="Solicitud"> al catálogo </summary>
    /// <param name="Oferta"> <see cref="OfertaDeServicio"> que se busca </param>
    /// <param name="empleador"> <see cref="Empleador"> que realiza la <see cref="Solicitud"> </param>
    public void AddSolicitud(OfertaDeServicio Oferta, Empleador empleador){

        Solicitudes.Add(new Solicitud(Oferta, empleador) );

    }

    /// <summary> Método para eliminar una <see cref="Solicitud"> </summary>
    /// <param name="solicitud"> <see cref="Solicitud"> que se desea eliminar </param>
    public void RemoveSolicitud (Solicitud solicitud){
        Solicitudes.Remove(solicitud);
    }
}
