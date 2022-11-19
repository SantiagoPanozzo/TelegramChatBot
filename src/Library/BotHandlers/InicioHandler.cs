namespace Library.BotHandlers;

/// <summary>
/// Dependiendo del <see cref="TipoDeUsuario"/> del <see cref="Usuario"/> muestra distintas opciones.
/// Para un <see cref="Trabajador"/> muestra <see cref="OfertarServicioHandler"/>, <see cref="VerOfertasHandler"/>,
/// <see cref="VerInfoHandler"/> y <see cref="VerSolicitudesHandler"/>.
/// Para un <see cref="Empleador"/> muestra <see cref="VerContratosHandler"/>, <see cref="VerInfoHandler"/> y
/// y <see cref="BuscarHandler"/>
/// </summary>
public class InicioHandler
{
    // Dependiendo del tipo muestra opciones
    // trabajador muestra Ofertar Servicio, Ver mis ofertas, Ver mi informacion, Ver mis solicitudes pendientes
    // empleador muestra Ver mis contratos, Ver mi informacion, Buscar
}