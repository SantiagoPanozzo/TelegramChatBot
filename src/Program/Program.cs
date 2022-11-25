// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
namespace Program;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Library;
using Library.DistanceMatrix;

public class Program {
    public static void Main() {
        
        while(true)
        {
            Updater.EnableAutoUpdate();
            TelegramBot.Main();
        }


        //Console.WriteLine(Distance.Calculate("Salto Uruguay", "Montevideo Uruguay"));
        /*
        Administrador admin = new("sd", "sd", "9231015", "sfa@sda.com");
        OfertasHandler handler = OfertasHandler.GetInstance();
        handler.CrearCategoria(admin, "a");
        handler.CrearCategoria(admin, "b");
         
        CategoriaPrinter catPrinter = new();
        List<Categoria> a = new();

        a.Add(c1);
        a.Add(c2);
        a.Add(c3);

        catPrinter.PrintCatalog(a);
        Administrador a1 = new("nick", "contra", "tel", "a@b.c");

        //OfertaDeServicioPrinter ofePrinter = new();
        ContratoHandler ch = new(); 


        Trabajador t1 = new("Ihojan", "Werlyb", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Trabajador t2 = new("Cosplay de", "Irelia", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));

        Empleador e1 = new("Paquito", "Paco", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Empleador e2 = new("Sigo?", "Cont", "hide on bush", "1234", new DateTime(2020,2,1), "11111111", "099", "a@b.c", new Tuple<double, double>(-31.389425985682045, -57.959432913914476));

        handler.Ofertar(1, t1, "asd", "asf", 24.6);
        handler.Ofertar(2, t2, "asd", "asf", 46);
        handler.Ofertar(1, t2, "sfa", "wqr", 99);
        Solicitud s1 = new(o1, e1);
        Solicitud s2 = new(o2, e2);

        ch.Catalogo.AddSolicitud(o1, e1);
        ch.Catalogo.AddSolicitud(o2, e2);

        o1.DarDeBaja(a1);

        ofePrinter.PrintCatalog(ch.Catalogo);
        
        */

    }
}
