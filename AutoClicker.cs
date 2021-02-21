using AutoClickerConsole.Command;
using AutoClickerConsole.Command.Commands;
using System;
using System.Collections.Generic;

namespace AutoClickerConsole
{
    class AutoClicker
    {
        public readonly Dictionary<UInt32, Clicker> clickers;

        private bool activated;

        private AutoClicker()
        {
            clickers = new Dictionary<UInt32, Clicker>();
            activated = true;

            PrintMessage("ClickMaster, developed by Simon R. Try help to see the commands.");
            Console.WriteLine();

            //Setup command handler
            CommandHandler commandHandler = new CommandHandler(this);
            commandHandler.RegisterCommand("help", new HelpCommand(this));
            commandHandler.RegisterCommand("addcl", new AddClickerCommand(this));
            commandHandler.RegisterCommand("button", new ButtonCommand(this));
            commandHandler.RegisterCommand("clear", new ClearCommand(this));
            commandHandler.RegisterCommand("clickers", new ClickersCommand(this));
            commandHandler.RegisterCommand("rmcl", new RemoveClickerCommand(this));
            commandHandler.RegisterCommand("activate", new ActivateCommand(this));
            commandHandler.RegisterCommand("deactivate", new DeactivateCommand(this));
            commandHandler.RegisterCommand("a", new ActivateCommand(this));
            commandHandler.RegisterCommand("d", new DeactivateCommand(this));
            commandHandler.RegisterCommand("exit", new ExitCommand(this));

            commandHandler.Run();
        }

        static void Main()
           => new AutoClicker();

        public void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ClickMaster");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("] ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public void Deactivate()
        {
            activated = false;

            foreach (Clicker clicker in clickers.Values)
                clicker.Stop();

            PrintMessage("All clickers were deactivated.");
        }

        public void Activate()
        {
            activated = true;

            foreach (Clicker clicker in clickers.Values)
                clicker.Start();

            PrintMessage("All clickers were activated.");
        }

        public bool IsActivated()
        {
            return activated;
        }

        public bool IsValidKey(int key)
        {
            if (1 <= key && key <= 145)
                return true;

            if (160 <= key && key <= 254)
                return true;

            if (key == 65536 ^ key == 131072 ^ key == 262144)
                return true;

            return false;
        }
    }
}
