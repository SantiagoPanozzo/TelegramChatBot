namespace Library.Excepciones;

[Serializable]
public class FalloDeAutenticacionException : Exception
{
    public FalloDeAutenticacionException() : base() { }
    public FalloDeAutenticacionException(string message) : base(message) { }
    public FalloDeAutenticacionException(string message, Exception inner) : base(message, inner) { }
}