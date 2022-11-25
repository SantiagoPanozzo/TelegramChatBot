namespace Library.BotHandlers;

public class HandlerHandler
{
    public Dictionary<long, Usuario> CachedLogins = new Dictionary<long, Usuario>();
    private Dictionary<long, string> ActiveHandler = new Dictionary<long, string>();
}