using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using MarketBot.Core;

namespace MarketBot.Commands
{
    internal class ForwardMessageToOwnCommand : IBaseCommand
    {
        public string Name => "ForwardMessage";

        public async void Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Продать" },
            })
            {
                ResizeKeyboard = true
            };

            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Пост отправлен на рассмотрение, ожидайте публикации.",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);

            await Update(botClient, update, cancellationToken);
        }

        async static Task Update(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            if (message != null)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Продать" },
            })
                {
                    ResizeKeyboard = true
                };

                await client.ForwardMessageAsync(
                chatId: -1001986123112,
                fromChatId: message.Chat.Id,
                messageId: message.MessageId);

                   Message message1 = await client.SendTextMessageAsync(
                    chatId: -1001986123112,
                    text:"Продавец: " + '@' + message.Chat.Username ?? "without username",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
            }
        }
    }
}
