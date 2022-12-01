using Nito.AsyncEx;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
namespace Library.BotHandlers;

/// <summary> <see cref="IHandler"/> "default", procesa una respuesta a cualquier mensaje que no lo haga otro handler. </summary>
public class DefaultHandler : BaseHandler
{
    /// <summary> Instancia del bot de Telegram. </summary>
    private TelegramBotClient bot;

    /// <summary> Inicializa una nueva instancia de la clase <see cref="DefaultHandler"/>, 
    /// procesa todo mensaje que no pueda procesar otro <see cref="IHandler"/>. </summary>
    /// <param name="next"> Próximo <see cref="IHandler"/>. </param>
    /// <param name="bot"> Bot de Telegram utilizado. </param>
    public DefaultHandler(BaseHandler next, TelegramBotClient bot) : base(next) {
        this.Keywords = new string[] {"default"};
        this.bot = bot;
        _id = Handlers.DefaultHandler;
    }

    /// <summary> Verifica que se pueda procesar el mensaje </summary>
    /// <param name="message"> Mensaje a procesar. </param>.
    /// <returns> Siempre devuelve true, es la idea para que procese cualquier mensaje. </returns>
    protected override bool CanHandle(Message message)
    {
        return true;
    }

    /// <summary> Procesamiento de los mensajes. </summary>
    /// <param name="message"> Mensaje a procesar. </param>
    /// <param name="response"> Respuesta al mensaje. </param>
    protected override void InternalHandle(Message message, out string response)
    {
        response = "????????? q";
        message.Text = message.Text.ToLower();
        switch (message.Text)
        {
            case "que":
                response = "so";
                break;
            case "faker":
                response =
                    "GIGACHAD Who is Faker? For the blind, He is the vision. For the hungry, He is the chef. For the thirsty, He is the water. If Faker thinks, I agree. If Faker speaks, I’m listening. If Faker has one fan, it is me. If Faker has no fans, I don’t exist. GIGACHAD";
                break;
            case "comandos":
                response = "Usa /info";
                break;
            case "gracias":
                response = "de nada";
                break;
            case "free bird":
                AsyncContext.Run(() => SendVideo(message));
                response = string.Empty;
                break;
            
            default:
                response = "Mensaje desconocido, para conocer todos los comandos ingrese \"info\" o ejecute el comando /info";
                break;
        }
    }
    
     private async Task SendVideo(Message message)
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
