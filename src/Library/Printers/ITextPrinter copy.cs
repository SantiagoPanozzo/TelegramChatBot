namespace Library;

public interface ITextPrinter<T, U> {
    public string Print(List<T> catalog, U user);
}
