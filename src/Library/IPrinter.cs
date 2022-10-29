namespace Library;
using System;

/// <summary> Interfáz para mostrar datos por pantalla </summary>
public interface IPrinter<T> {
    void PrintCatalog(List<T> catalog, Usuario user);
}
