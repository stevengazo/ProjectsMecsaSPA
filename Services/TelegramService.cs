using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

public class TelegramService
{
    private readonly string _botToken = "";
    private readonly TelegramBotClient _botClient;
    private readonly CancellationTokenSource _cts;

    public TelegramService()
    {
        _botClient = new TelegramBotClient(_botToken);
        _cts = new CancellationTokenSource();

        var me = _botClient.GetMeAsync().Result;
        Console.WriteLine("---------------------------");
        Console.WriteLine("Bot ID: " + me.Id);
        Console.WriteLine("Bot Name: " + me.FirstName);
        Console.WriteLine("Bot Username: " + me.Username);
        Console.WriteLine("---------------------------");

        StartReceiving();
    }

    private void StartReceiving()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: _cts.Token
        );

        Console.WriteLine("Bot is running... Press Enter to terminate.");
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.CallbackQuery != null)
        {
            await HandleCallbackQuery(update.CallbackQuery, cancellationToken);
        }
        else if (update.Message != null && update.Message.Text != null)
        {
            await HandleMessageAsync(update.Message, cancellationToken);
        }
    }

    private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        if (message.Text == "/menu")
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("Buscar proyecto", "search_project") },
                new[] { InlineKeyboardButton.WithCallbackData("Ingresar información", "enter_info") }
            });

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Selecciona una opción:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (message.Text.StartsWith("/"))
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Escribe /menu para ver las opciones disponibles.",
                cancellationToken: cancellationToken
            );
        }
        else
        {
            if (_userStates.TryGetValue(chatId, out var userState))
            {
                switch (userState)
                {
                    case UserState.WaitingForPhoneNumber:
                        await HandlePhoneNumberInput(chatId, message.Text, cancellationToken);
                        break;
                    case UserState.WaitingForName:
                        await HandleNameInput(chatId, message.Text, cancellationToken);
                        break;
                    case UserState.WaitingForIsTechnician:
                        await HandleIsTechnicianInput(chatId, message.Text, cancellationToken);
                        break;
                    case UserState.WaitingForSubscription:
                        await HandleSubscriptionInput(chatId, message.Text, cancellationToken);
                        break;
                }
            }
        }
    }

    private async Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var chatId = callbackQuery.Message.Chat.Id;
        var data = callbackQuery.Data;

        if (data == "search_project")
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Por favor, ingresa el número del proyecto:",
                cancellationToken: cancellationToken
            );
        }
        else if (data == "enter_info")
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Por favor, ingresa tu número de teléfono:",
                cancellationToken: cancellationToken
            );

            _userStates[chatId] = UserState.WaitingForPhoneNumber;
        }
    }

    private async Task HandlePhoneNumberInput(long chatId, string phoneNumber, CancellationToken cancellationToken)
    {
        _userData[chatId] = new UserData { PhoneNumber = phoneNumber };
        _userStates[chatId] = UserState.WaitingForName;

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Gracias. Ahora, por favor, ingresa tu nombre:",
            cancellationToken: cancellationToken
        );
    }

    private async Task HandleNameInput(long chatId, string name, CancellationToken cancellationToken)
    {
        _userData[chatId].Name = name;
        _userStates[chatId] = UserState.WaitingForIsTechnician;

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "¿Eres técnico? (Sí/No)",
            cancellationToken: cancellationToken
        );
    }

    private async Task HandleIsTechnicianInput(long chatId, string isTechnician, CancellationToken cancellationToken)
    {
        _userData[chatId].IsTechnician = isTechnician.ToLower() == "sí" || isTechnician.ToLower() == "si";
        _userStates[chatId] = UserState.WaitingForSubscription;

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "¿Deseas suscribirte? (Sí/No)",
            cancellationToken: cancellationToken
        );
    }

    private async Task HandleSubscriptionInput(long chatId, string subscription, CancellationToken cancellationToken)
    {
        _userData[chatId].WantsSubscription = subscription.ToLower() == "sí" || subscription.ToLower() == "si";
        _userStates.Remove(chatId);

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Gracias por proporcionar tu información. ¡Hemos terminado!",
            cancellationToken: cancellationToken
        );

        var userData = _userData[chatId];
        Console.WriteLine($"Número de teléfono: {userData.PhoneNumber}, Nombre: {userData.Name}, Es técnico: {userData.IsTechnician}, Desea suscribirse: {userData.WantsSubscription}");
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error: {exception.Message}");
        return Task.CompletedTask;
    }

    private enum UserState
    {
        WaitingForPhoneNumber,
        WaitingForName,
        WaitingForIsTechnician,
        WaitingForSubscription
    }

    private readonly Dictionary<long, UserState> _userStates = new Dictionary<long, UserState>();

    private class UserData
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsTechnician { get; set; }
        public bool WantsSubscription { get; set; }
    }

    private readonly Dictionary<long, UserData> _userData = new Dictionary<long, UserData>();

    public void StopBot()
    {
        Console.ReadLine();
        _cts.Cancel();
        Console.WriteLine("Bot has been stopped.");
    }
}