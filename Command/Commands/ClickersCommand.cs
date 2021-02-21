using System;
using System.Collections.Generic;

namespace AutoClickerConsole.Command.Commands
{
    class ClickersCommand : ICommand
    {
        private readonly AutoClicker instance;

        public ClickersCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            if (instance.clickers.Count == 0)
            {
                instance.PrintMessage("There're no created clickers.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------");

            if (!instance.IsActivated())
            {
                Console.Write("All clickers are ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("deactivated");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(". Activate them with ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("activate");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(".");
                Console.WriteLine("");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("List of your clickers:");
            Console.WriteLine("");

            foreach (KeyValuePair<UInt32, Clicker> item in instance.clickers)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.Key);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ActivationButton: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.Value.GetActivationKey());

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ButtonToClick: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.Value.GetKeyToClick() == -1 ? "LEFT MOUSE" : (item.Value.GetKeyToClick() == -2 ? "RIGHT MOUSE" : item.Value.GetKeyToClick() + ""));

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" CPS: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.Value.GetCps());

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" RandomValue: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.Value.GetRandomValue());
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
