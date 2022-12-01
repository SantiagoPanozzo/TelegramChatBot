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
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Library;
using Library.DistanceMatrix;

public class Program {
    public static void Main()
    {
        DotNetEnv.Env.TraversePath();
        RegistryHandler.GetInstance().RegistrarTrabajador("TESTNombre", "TESTApellido", "TEST", "TEST",
            "01 01 2000", "1234567", "555555555", "alguien@example.com",
            new Tuple<double, double>(-31.389425985682045, -57.959432913914476));
        Administrador admin = RegistryHandler.GetInstance().RegistrarAdministrador("Admin", "toor", "473555555", "admin@dominio.com");
        bool ejecutar = true;
        
        Updater.EnableAutoUpdate();
        while(ejecutar)
        {
            TelegramBot.Main();
            if (Console.ReadLine().Equals("salir")) ejecutar = false;
        }

    }
}
