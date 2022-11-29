namespace Library;

/// <summary> Interfaz para mostrar datos en el bot de Telegram. </summary>
/// <typeparam name="T"> Tipo genérico, se debe indicar que se quiere imprimir en el método. </typeparam>
public interface ITextPrinter<T, U>
{
    /// <summary> Arma texto en formato para mostrar por mensaje </summary>
    /// <param name="catalog"> Catálogo según el tipo que se indique </param>
    /// <param name="user"> <see cref="Usuario"/> que llama al método </param>
    /// <returns> Devuelve un texto en formato con lo que corresponda </returns>
    public string PrintAll(List<T> catalog, U user);
}