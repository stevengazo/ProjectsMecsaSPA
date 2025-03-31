using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using ProjectsMecsaSPA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectsMecsaSPA.Model;

public class TelegramService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<TelegramService> _logger;
    private readonly string _botToken ="";
    private readonly TelegramBotClient _botClient;
    private readonly ProjectsDBContext _dbContext;

    #region Properties
    private enum UserState
    {
        WaitingForPhoneNumber,
        WaitingForName,
        WaitingForIsTechnician,
        WaitingForSubscription,
        WaitingForProjectNumber,
        WaitingForOfferNumber
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
    #endregion

    #region Constructor

    public TelegramService(
        ILogger<TelegramService> logger, 
        IServiceScopeFactory serviceScopeFactory,
        IOptions<TelegramSettings> telegramSettings
        )
    {
        _logger = logger;
        _botToken = telegramSettings.Value.BotToken;
        _serviceScopeFactory = serviceScopeFactory; // Usamos el scope factory para crear un alcance
        _botClient = new TelegramBotClient(_botToken);

        // Log some bot info
        InfoBot(_botClient.GetMe().Result);

        // Start receiving updates
        StartReceiving();
    }

    private async Task InfoBot(User bot)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("---------------------------");
        Console.WriteLine("Bot ID: " + bot.Id);
        Console.WriteLine("Bot Name: " + bot.FirstName);
        Console.WriteLine("Bot Username: " + bot.Username);
        Console.WriteLine("---------------------------");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    #endregion

    private void StartReceiving()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // Recibir todos los tipos de actualizaciones
        };

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: CancellationToken.None
        );
    }

    #region Telegram Methods

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update)
        {
            case { CallbackQuery: not null }:
                await HandleCallbackQuery(update.CallbackQuery, cancellationToken);
                break;
            case { Message: { Text: not null } message }:
                await HandleMessageAsync(message, cancellationToken);
                break;
            case { MyChatMember: not null }:
                await HandleChatMemberUpdate(update.MyChatMember, cancellationToken);
                break;
        }
    }
    private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        switch (message.Text)
        {
            case "/menu":
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Buscar proyecto", "search_project") },
                    new[] { InlineKeyboardButton.WithCallbackData("Buscar Oferta", "search_offer") },
                    new[] { InlineKeyboardButton.WithCallbackData("Ingresar información", "enter_info") },
                    new[] { InlineKeyboardButton.WithCallbackData("Crear grupo", "create_group") }
                });

                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "Selecciona una opción:",
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken
                );
                break;

            case var _ when message.Text.StartsWith("/"):

                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "Escribe /menu para ver las opciones disponibles.",
                    cancellationToken: cancellationToken
                );
                break;

            default:
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
                        case UserState.WaitingForProjectNumber: // Nuevo estado
                            await HandleProjectNumberInput(chatId, message.Text, cancellationToken);
                            break;
                        case UserState.WaitingForOfferNumber: // Nuevo estado
                            await HandleOfferNumberInput(chatId, message.Text, cancellationToken);
                            break;
                    }
                }
                break;
        }
    }



    private async Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var chatId = callbackQuery.Message.Chat.Id;
        var data = callbackQuery.Data;

        switch (data)
        {
            case "search_project":
                await _botClient.SendMessage(chatId, "Por favor, ingresa el número del proyecto:");
                _userStates[chatId] = UserState.WaitingForProjectNumber; // Nuevo estado
                break;
            case "search_offer":
                await _botClient.SendMessage(chatId, "Por favor, ingresa el número del oferta:");
                _userStates[chatId] = UserState.WaitingForOfferNumber; // Nuevo estado
                break;

            case "enter_info":
                await _botClient.SendMessage(chatId, "Por favor, ingresa tu número de teléfono:");
                _userStates[chatId] = UserState.WaitingForPhoneNumber;
                break;

            default:
                await _botClient.SendMessage(chatId, "Opción no válida. /menu");
                break;
        }
    }
    private async Task HandleChatMemberUpdate(ChatMemberUpdated chatMemberUpdate, CancellationToken cancellationToken)
    {
        var chatId = chatMemberUpdate.Chat.Id;

        if (chatMemberUpdate.NewChatMember.Status == ChatMemberStatus.Member)
        {
            await _botClient.SendMessage(
                chatId: chatId,
                text: "¡Hola! Gracias por agregarme a este grupo. ¿En qué puedo ayudar?",
                cancellationToken: cancellationToken
            );
        }
    }
    private async Task HandlePhoneNumberInput(long chatId, string phoneNumber, CancellationToken cancellationToken)
    {
        _userData[chatId] = new UserData { PhoneNumber = phoneNumber };
        _userStates[chatId] = UserState.WaitingForName;

        await _botClient.SendMessage(
            chatId: chatId,
            text: "Gracias. Ahora, por favor, ingresa tu nombre:",
            cancellationToken: cancellationToken
        );
    }
    private async Task HandleNameInput(long chatId, string name, CancellationToken cancellationToken)
    {
        _userData[chatId].Name = name;
        _userStates[chatId] = UserState.WaitingForIsTechnician;

        await _botClient.SendMessage(
            chatId: chatId,
            text: "¿Eres técnico? (Sí/No)",
            cancellationToken: cancellationToken
        );
    }
    private async Task HandleIsTechnicianInput(long chatId, string isTechnician, CancellationToken cancellationToken)
    {
        _userData[chatId].IsTechnician = isTechnician.ToLower() == "sí" || isTechnician.ToLower() == "si";
        _userStates[chatId] = UserState.WaitingForSubscription;

        await _botClient.SendMessage(
            chatId: chatId,
            text: "¿Deseas suscribirte? (Sí/No)",
            cancellationToken: cancellationToken
        );
    }
    private async Task HandleSubscriptionInput(long chatId, string subscription, CancellationToken cancellationToken)
    {
        _userData[chatId].WantsSubscription = subscription.ToLower() == "sí" || subscription.ToLower() == "si";
        _userStates.Remove(chatId);

        await _botClient.SendMessage(
            chatId: chatId,
            text: "Gracias por proporcionar tu información. ¡Hemos terminado!",
            cancellationToken: cancellationToken
        );

        var userData = _userData[chatId];
        Console.WriteLine($"Número de teléfono: {userData.PhoneNumber}, Nombre: {userData.Name}, Es técnico: {userData.IsTechnician}, Desea suscribirse: {userData.WantsSubscription}");
    }
    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"Error en el bot: {exception.Message}");
        return Task.CompletedTask;
    }

    private async Task HandleProjectNumberInput(long chatId, string projectNumber, CancellationToken cancellationToken)
    {
        // Crear un nuevo scope para resolver el DbContext
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ProjectsDBContext>();

            int.TryParse(projectNumber, out var _projectNumber);
            var project = await dbContext.Projects
                .Include(e => e.State)
                .Include(e => e.Seller)
                .Include(e => e.Type)
                .FirstOrDefaultAsync(p => p.ProjectId == _projectNumber, cancellationToken);

            if (project != null)
            {
                await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: @$"
Información del Proyecto {projectNumber}-{project.TaskNumber}
Estado: {project.State?.StateName ?? "No disponible"}
Tipo: {project.Type?.Name ?? "No disponible"}
Vendedor: {project.Seller?.SellerName ?? "No disponible"}
Facturación: {(project.Isfactured ? "Completado" : "Pendiente")}

Si tienes más dudas, no dudes en comunicarte con el vendedor: {project.Seller?.SellerName ?? "No disponible"}.
",
                                            cancellationToken: cancellationToken
                                        );


            }
            else
            {
                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "Lo siento, no se encontró un proyecto con ese número.",
                    cancellationToken: cancellationToken
                );
            }
        }

        _userStates.Remove(chatId); // Volver al estado inicial
    }
    private async Task HandleOfferNumberInput(long chatId, string offertNumber, CancellationToken cancellationToken)
    {
        // Crear un nuevo scope para resolver el DbContext
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ProjectsDBContext>();

            int.TryParse(offertNumber, out var _OfferId);
            var offer = await dbContext.Offers
                .FirstOrDefaultAsync(p => p.OfferId == _OfferId, cancellationToken);

            if (offer != null)
            {
                await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: @$"
Información del Oferta {offer.OfferId}
Estado: {offer.State}
Fecha de Registro: {offer.Creation.ToLongDateString()}
Descripcion: {offer.Description}
Autor: {offer.Responsible}
",
                                            cancellationToken: cancellationToken
                                        );


            }
            else
            {
                await _botClient.SendMessage(
                    chatId: chatId,
                    text: "Lo siento, no se encontró una oferta con ese número.",
                    cancellationToken: cancellationToken
                );
            }
        }

        _userStates.Remove(chatId); // Volver al estado inicial
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // El bot ya está recibiendo actualizaciones en segundo plano.
        // Aquí no se necesita agregar más lógica ya que StartReceiving lo maneja.
        await Task.Delay(Timeout.Infinite, stoppingToken); // Esperar hasta que el servicio se detenga
    }
}
#endregion