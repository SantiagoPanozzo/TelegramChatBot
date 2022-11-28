namespace Library;

/// <summary> Interfaz para calificar entre <see cref="Trabajador"/>/es y <see cref="Empleador"/>/es. </summary>
/// <!-- Utilizamos polymorphism para que las clases calificables puedan ser calificadas y se pueda obtener su
/// calificación independiende de la manera en la que se implemente en cada clase. -->
public interface ICalificable {
    public Calificacion GetReputacion();
    public void Calificar(Calificacion Rate);

}
