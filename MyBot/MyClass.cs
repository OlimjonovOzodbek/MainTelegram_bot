using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot

{
    public class MyBot
    {
        public bool isContactShared = false;
        private string MyToken;
        public MyBot(string token)
        {
            MyToken = token;
        }
        public async Task Begin()
        {
            var botClient = new TelegramBotClient(MyToken);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }
        public async Task CRUD_buttons(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
                    {
                        new KeyboardButton("CREATE"),
                        new KeyboardButton("DELETE"),
                        new KeyboardButton("UPDATE"),
                                    new KeyboardButton("READ")

                            })
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(
                 chatId: update.Message.Chat.Id,
                 text: "Choose a response",
                 replyMarkup: replyKeyboardMarkup1,
                 cancellationToken: cancellationToken);
            return;

        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = "";
            if (update.Message is not { } message)
                return;
            if (message.Type == MessageType.Text)
            {
                messageText = message.Text;
            }

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' \n{chatId}.");

            if (messageText == "/start")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(
                    new[]
            {
                  KeyboardButton.WithRequestContact("Contact")
            })

                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "𝐀𝐬𝐬𝐚𝐥𝐨𝐦𝐮 𝐚𝐥𝐞𝐲𝐤𝐮𝐦, 𝐛𝐨𝐭𝐝𝐚𝐧 𝐟𝐨𝐲𝐝𝐚𝐥𝐚𝐧𝐢𝐬𝐡 𝐮𝐜𝐡𝐮𝐧 𝐤𝐨𝐧𝐭𝐚𝐤𝐭𝐧𝐢 𝐣𝐨'𝐧𝐚𝐭𝐢𝐧𝐠",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                return;

            }
            if (message.Contact is not null)
            {
                isContactShared = true;
            }
            if (isContactShared)
            {
                if (messageText == "Phone Crud")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (messageText == "Office Crud")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (messageText == "Customer Region")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (messageText == "Order Status")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (messageText == "Any Status")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (messageText == "PaytType Crud")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (messageText == "Payment History")
                {
                    await CRUD_buttons(botClient, update, cancellationToken);
                    return;
                }
                if (message.Chat.Id == 967332332)
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(
                        new[]
                {
                                new[]
                                {

                                new KeyboardButton("Phone Crud"),
                                new KeyboardButton("Download Excel"),
                                new KeyboardButton("Office Crud"),
                                new KeyboardButton("Customer Region"),
                                    },
                                new []
                                {
                                 new KeyboardButton("Order Status"),
                                new KeyboardButton("Any Status"),
                                new KeyboardButton("PayType Crud"),
                                new KeyboardButton("Payment History")
                                    },


                })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Bosh Menu",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                    return;
                }
            }
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}

