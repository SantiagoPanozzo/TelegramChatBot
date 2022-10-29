namespace Library;
using System;

/// <summary> Clase para manejar el catalogo </summary>
public class ContratoHandler
{

    public SolicitudCatalog Catalogo;
    
    private static ContratoHandler? _instance;

    private static ContratoHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ContratoHandler();
            }

            return _instance;
        }
    }
    
    private ContratoHandler()
    {
        this.Catalogo = SolicitudCatalog.GetInstance();
    }

    public static ContratoHandler GetInstance()
    {
        return ContratoHandler.Instance;
    }

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

    /// <summary> Método para obtener una solicitud por id </summary>
    /// <param name="id"> Valor de id para filtrar <see cref="Solicitud"/> </param>
    /// <returns> Devuelve la <see cref="Solicitud"/> por valor de id </returns>
    public Solicitud GetSolicitud(int id)    {
        foreach (Solicitud solicitud in Catalogo.Solicitudes)
        {
            if (solicitud.GetId().Equals(id)) return solicitud;
        }
        throw (new Exception("No se encontró la solicitud"));
    }
    
    /// <summary> Método para mostrar solicitudes </summary>
    /// <param name="user"> Variable de tipo <see cref="Usuario"/> </param>
    /// <returns> Devuelve las solicitudes según que tipo de <see cref="Usuario"/> se </returns>
    public List<Solicitud> GetSolicitudes(Usuario user)
    {
        List<Solicitud> solicitudesDelUsuario = new();
        if (user.GetTipo().Equals(TipoDeUsuario.Administrador))
        {
            solicitudesDelUsuario = this.Catalogo.Solicitudes;
        }

        else if (user.GetTipo().Equals(TipoDeUsuario.Trabajador))
        {
            foreach (Solicitud solicitud in Catalogo.Solicitudes)
            {
                if(solicitud.Trab.Equals(user.Nick)) solicitudesDelUsuario.Add(solicitud);
            }
        }
        else if (user.GetTipo().Equals(TipoDeUsuario.Empleador))
        {
            foreach (Solicitud solicitud in Catalogo.Solicitudes)
            {
                if(solicitud.GetEmpleador().Equals(user.Nick)) solicitudesDelUsuario.Add(solicitud);
            }
        }
        else
        {
            throw (new("Error: tipo de usuario no definido"));
        }
        return solicitudesDelUsuario;
    }
}