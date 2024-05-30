using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MarketBot.Core
{
    internal interface IBaseCommand
    {
        public string Name { get; }
        public void Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
