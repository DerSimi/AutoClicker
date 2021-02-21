using System;
using System.Collections.Generic;

namespace AutoClickerConsole.Command.Commands
{
    class HelpCommand : ICommand
    {
        private readonly AutoClicker instance;

        public HelpCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
            => ShowHelp();

        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Overview of commands");
            Console.WriteLine();

            PrintHelpPoint("button", "Get the id from a button(PS: left mouse click is just LEFT and right mouse click is just RIGHT)");
            PrintHelpPoint("clickers", "Show list of the created clickers");
            PrintHelpPoint("clear", "Delete all clickers");
            PrintHelpPoint("rmcl <id>", "Remove one of your clickers");
            PrintHelpPoint("addcl <activation key> <key to click> <cps> <random value>", "Add a clicker");
            PrintHelpPoint("a, activate", "Activate all clickers");
            PrintHelpPoint("d, deactivate", "Deactivate all clickers");
            PrintHelpPoint("exit", "You know exactly what is going to happen!");

            Console.WriteLine("");
            Console.WriteLine("To get a list of valid key numbers, visit:");
            Console.WriteLine("https://docs.microsoft.com/de-de/dotnet/api/system.windows.forms.keys?view=net-5.0");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------");
        }

        private void PrintHelpPoint(string command, string description)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(command);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" - " + description);
            Console.WriteLine();
        }
    }
}
