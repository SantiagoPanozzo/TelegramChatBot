namespace Library.Excepciones;

[Serializable]
public class YaCalificadoException : Exception
{
    public YaCalificadoException() : base() { }
    public YaCalificadoException(string message) : base(message) { }
    public YaCalificadoException(string message, Exception inner) : base(message, inner) { }
}