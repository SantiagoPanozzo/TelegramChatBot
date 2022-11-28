namespace Library;
using System;

/// <summary> Interfáz para mostrar datos por pantalla. </summary>
public interface IConsolePrinter<T>{
    void Print(List<T> catalog, Usuario user);
}
