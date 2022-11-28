namespace Library;
using System;

/// <summary> Interfáz para mostrar datos por pantalla </summary>
public interface IPrinter<T, U>{
    U Print(List<T> catalog, Usuario user);
}