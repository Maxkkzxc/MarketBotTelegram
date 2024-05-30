using MarketBot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MarketBot.Commands
{
    internal class BuyCommand : IBaseCommand
    {
        public string Name => "Купить";

        public async void Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Назад" },
            })
            {
                ResizeKeyboard = true
            };

            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Для покупки пришлите ID",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);

        }
    }
}
