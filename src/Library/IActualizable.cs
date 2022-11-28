namespace Library;

/// <summary> Interfaz para actualizar una fecha .</summary>
/// <!-- Utilizamos Polymorphism para poder actualizar con facilidad las clases que lo necesiten mediante el método Update(). -->
public interface IActualizable
{
    public void Update(DateTime fechaActual);
}