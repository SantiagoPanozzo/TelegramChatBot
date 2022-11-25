namespace Library.BotHandlers;

public static class HandlerHandler
{
    public static Dictionary<long, Usuario> CachedLogins = new Dictionary<long, Usuario>();
    public static Dictionary<long, Handlers> ActiveHandler = new Dictionary<long, Handlers>();
}

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