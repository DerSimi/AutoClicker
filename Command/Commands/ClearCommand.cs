using System;
using System.Collections.Generic;
namespace AutoClickerConsole.Command.Commands
{
    class ClearCommand : ICommand
    {
        private readonly AutoClicker instance;

        public ClearCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            if (instance.clickers.Count == 0)
            {
                instance.PrintMessage("No clickers have been created yet.");
                return;
            }

            foreach (KeyValuePair<UInt32, Clicker> item in instance.clickers)
                item.Value.Stop();

            instance.clickers.Clear();

            instance.PrintMessage("All clickers were stopped and deleted.");
        }
    }
}
