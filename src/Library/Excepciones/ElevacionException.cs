namespace Library.Excepciones;

/// <summary>
/// Representa una excepción por falta de 
/// </summary>

[Serializable]
public class ElevacionException : Exception
{
    public ElevacionException() : base() { }
    public ElevacionException(string message) : base(message) { }
    public ElevacionException(string message, Exception inner) : base(message, inner) { }
}
