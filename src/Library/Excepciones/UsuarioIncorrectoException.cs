namespace Library.Excepciones;

[Serializable]
public class UsuarioIncorrectoException : Exception
{
    public UsuarioIncorrectoException() : base() { }
    public UsuarioIncorrectoException(string message) : base(message) { }
    public UsuarioIncorrectoException(string message, Exception inner) : base(message, inner) { }
}
