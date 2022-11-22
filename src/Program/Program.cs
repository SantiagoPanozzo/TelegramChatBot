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
        while(true) {
            Updater.EnableAutoUpdate();
            TelegramBot.Main();
        }
    }
}
