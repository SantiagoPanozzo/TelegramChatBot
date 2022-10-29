// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Library;
using System;

class Program {
    public static void Main() {
        /* CategoriaPrinter catPrinter = new();
        List<Categoria> a = new();

        Categoria c1 = new("Hogar");
        Categoria c2 = new("Salud");
        Categoria c3 = new("Yoquese");

        a.Add(c1);
        a.Add(c2);
        a.Add(c3);

        catPrinter.PrintCatalog(a); */
        Administrador a1 = new("nick", "contra", "tel", "a@b.c");

        //OfertaDeServicioPrinter ofePrinter = new();
        ContratoHandler ch = new(); 

        Trabajador t1 = new("Ihojan", "Werlyb", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t2 = new("Cosplay de", "Irelia", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));

        OfertaDeServicio o1 = new(t1, "ricas cosas", "cosas ricas", 69.420);
        OfertaDeServicio o2 = new(t2, "my name is gustavo", "but u can call me gus", 69.420);

        Empleador e1 = new("Paquito", "Paco", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Empleador e2 = new("Sigo?", "Cont", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));

        Solicitud s1 = new(o1, e1);
        Solicitud s2 = new(o2, e2);

        ch.Catalogo.AddSolicitud(o1, e1);
        ch.Catalogo.AddSolicitud(o2, e2);

        o1.DarDeBaja(a1);

        //ofePrinter.PrintCatalog(ch.Catalogo);
    }
}
