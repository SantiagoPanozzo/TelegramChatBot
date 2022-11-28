namespace Library;

public interface ITextPrinter<T>
{
    public string Print(List<T> catalog, Usuario user);
}