namespace Library.Excepciones;

[Serializable]
public class AccionInnecesariaException : Exception
{
    public AccionInnecesariaException() : base() { }
    public AccionInnecesariaException(string message) : base(message) { }
    public AccionInnecesariaException(string message, Exception inner) : base(message, inner) { }
}