using Nito.AsyncEx;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace Library.BotHandlers;
using Telegram.Bot.Types;

public class DefaultHandler: BaseHandler
{
    private TelegramBotClient bot;
    public DefaultHandler(BaseHandler next, TelegramBotClient bot) : base(next) {
        this.Keywords = new string[] {"default"};
        this.bot = bot;
    }

    /// <summary>  </summary>
    /// <param name="message">  </param>
    /// <returns>  </returns>
    protected override bool CanHandle(Message message)
    {
        return true;
    }

    protected override void InternalHandle(Message message, out string response)
    {
        response = "????????? q";
        switch (message.Text)
        {
            case "tu madre":
                response = "tiene una polla";
                break;
            case "que":
                response = "so";
                break;
            case "faker":
                response =
                    "GIGACHAD Who is Faker? For the blind, He is the vision. For the hungry, He is the chef. For the thirsty, He is the water. If Faker thinks, I agree. If Faker speaks, I’m listening. If Faker has one fan, it is me. If Faker has no fans, I don’t exist. GIGACHAD";
                break;
            case "puto":
                response = "tu vieja";
                break;
            case "comandos":
                response = "Usa /info";
                break;
            case "gracias":
                response = "de nada";
                break;
            case "free bird":
                AsyncContext.Run(() => SendImage(message));
                response = string.Empty;
                break;
            default:
                response = "???????? q decis mogolico";
                break;
        }
    }
    
     private async Task SendImage(Message message)
     {
         // Can be null during testing
            if (bot != null)
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

                const string filePath = @"../../../../../Assets/freebird.mp4";
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

                await bot.SendVideoAsync(
                    chatId: message.Chat.Id,
                    video: new InputOnlineFile(fileStream, fileName),
                    caption: "Bot creado por Santiago Panozzo, Tomás Esteves, Facundo Martinez, Mateo Rodriguez y Alejandra Benitez, gracias por usarlo"
                );
            }
        }
}