using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarketBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MarketBot.Core
{
    internal class Bot
    {
        private List<IBaseCommand> Commands { get; set; }

        public Bot()
        {
            string token = System.IO.File.ReadAllText("BotToken.txt");
            var botClient = new TelegramBotClient(token);
            using var cts = new CancellationTokenSource();

            Commands = new List<IBaseCommand>()
            {
                new StartCommand(),
                new SellCommand(),
                new BackCommand(),
                new ForwardMessageToOwnCommand(),
                new BuyCommand()
            };

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );
        }
            
        async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
            {
                if (message.Type == MessageType.Photo)
                {
                    Commands.Find(command => command.Name == "ForwardMessage").Execute(client, update, token);
                }
                return;
            }
                
            Console.WriteLine($"[REQUEST]-> ChatID = {message.Chat.Id}, FirstName = {message.Chat.FirstName}, Username = {message.Chat.Username}: '{messageText}'");

            var findCommand = Commands.Find(command => command.Name == messageText);

            if (findCommand != null)
            {
                findCommand.Execute(client, update, token);
            }
            else
            {
                Message sentMessage = await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"I do not understand. I don't have an answer for this command",
                    cancellationToken: token);
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
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
