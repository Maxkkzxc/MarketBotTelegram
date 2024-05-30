using MarketBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MarketBot.Core;

namespace MarketBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Bot();
            

            Console.ReadLine();
        }
    }
}