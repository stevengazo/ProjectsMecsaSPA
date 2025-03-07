using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using System;
using Telegram.Bot.Polling;

public class TelegramService
{
    private readonly string _botToken = "";
    private readonly TelegramBotClient _botClient;
    private readonly Telegram.Bot.Types.User me;
    private readonly CancellationTokenSource cts;

    public TelegramService()
    {
        // Crea el bot cliente y lo configura
        _botClient = new TelegramBotClient(_botToken);
        me = _botClient.GetMe().Result;

        // Inicia un token de cancelación para detener el bot
        cts = new CancellationTokenSource();

        // Muestra información del bot
        Console.WriteLine("---------------------------");
        Console.WriteLine("Bot ID: " + me.Id);
        Console.WriteLine("Bot Nombre: " + me.FirstName);
        Console.WriteLine("---------------------------");

        // Muestra el nombre de usuario del bot y espera la entrada del usuario para finalizar
        Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");

        // Configura el evento para manejar los mensajes
        _botClient.OnMessage += OnMessage;
        _botClient.OnError += OnError;


    }

    // Maneja los mensajes recibidos por el bot
    private async Task OnMessage(Message msg, UpdateType type)
    {
        if (msg.Text is null) return; // solo manejamos mensajes de texto
        Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");

        // Responde al mensaje recibido
        await _botClient.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
    }

    // Método para detener el bot cuando se presione Enter
    public void StopBot()
    {
        Console.ReadLine();  // Espera por la entrada del usuario
        cts.Cancel();        // Detiene el bot
    }

    // method to handle errors in polling or in your OnMessage/OnUpdate code
    async Task OnError(Exception exception, HandleErrorSource source)
    {
        Console.WriteLine(exception); // just dump the exception to the console
    }
}
