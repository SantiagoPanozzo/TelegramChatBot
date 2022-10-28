namespace Library;

/// <summary> Interfaz para calificar entre <see cref="Trabajador"/>/es y <see cref="Empleador"/>/es </summary>
public interface ICalificable {
    public Calificacion GetReputacion();
    public void Calificar(Calificacion Rate);

}
