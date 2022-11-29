namespace Library.BotHandlers;

/// <summary> Mantiene información de estados entre todos los <see cref="IHandler"/>. </summary>
public static class HandlerHandler
{
    public static Dictionary<long, Usuario> CachedLogins = new Dictionary<long, Usuario>();
    public static Dictionary<long, Handlers> ActiveHandler = new Dictionary<long, Handlers>();
}

/// <summary> Enum general para los <see cref="IHandler"/>. </summary>
public enum Handlers
{
    BuscarHandler,
    CategoriasHandler,
    DefaultHandler,
    InfoHandler,
    IniciarSesionHandler,
    InicioHandler,
    OfertarHandler,
    OfertarServicioHandler,
    PanelDeControlHandler,
    RegistrarHandler,
    StartHandler,
    VerCategoriasHandler,
    VerContratosHandler,
    VerInfoHandler,
    VerOfertasHandler,
    VerSolicitudesHandler,
    VerUsuarioHandler,
}