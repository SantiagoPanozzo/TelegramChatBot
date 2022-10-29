namespace Library;

public interface IDesactivable
{
    public void DarDeBaja(Usuario user);
    public void Reactivar(Usuario user);
    public bool IsActive();
}