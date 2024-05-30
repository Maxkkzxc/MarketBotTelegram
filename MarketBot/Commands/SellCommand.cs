using MarketBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MarketBot.Commands
{
    internal class SellCommand : IBaseCommand
    {
        public string Name => "Продать";

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
                text: "Для продажи пришлите описание и фото товара",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
        }
    }
}