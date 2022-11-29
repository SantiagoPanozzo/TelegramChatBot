namespace Library;

/// <summary> Interfaz para mostrar datos en el bot de Telegram. </summary>
/// <typeparam name="T"> Tipo genérico, se debe indicar que se quiere imprimir en el método. </typeparam>
public interface ITextPrinter<T> {
    public string Print(List<T> catalog);
}
