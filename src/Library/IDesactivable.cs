namespace Library;

/// <summary>Interfaz para el manejo de actividad de un usuario, una categoría, etc.</summary>
public interface IDesactivable
{
    public void DarDeBaja(Usuario user);
    public void Reactivar(Usuario user);
    public bool IsActive();
}