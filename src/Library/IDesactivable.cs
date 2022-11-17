namespace Library;

/// <summary>Interfaz para el manejo de actividad de un usuario, una categoría, etc.</summary>
/// <remarks>Por polymorphism agregamos esta interfaz que nos ayuda a poder desactivar una clase que la implemente,
/// de modo que no pueda ser usada a partir de ese momento, el cuerpo del método dependerá de la manera más
/// apropiada de desactivar cada clase desactivable.</remarks>
public interface IDesactivable
{
    public void DarDeBaja(Usuario user);
    public void Reactivar(Usuario user);
    public bool IsActive();
}
