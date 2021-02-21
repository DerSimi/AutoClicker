using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoClickerConsole.Command.Commands
{
    class RemoveClickerCommand : ICommand
    {
        private readonly AutoClicker instance;

        public RemoveClickerCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            if (args.Count != 2)
            {
                instance.PrintMessage("rmcl <Id> - Remove one of your clickers");
                return;
            }
            try
            {
                uint id = UInt32.Parse((args.ElementAt(1)));

                if (instance.clickers.ContainsKey(id))
                {
                    instance.clickers[id].Stop();
                    instance.clickers.Remove(id);
                    instance.PrintMessage("Clicker #" + id + " were stopped and deleted.");
                }
                else
                    instance.PrintMessage("There's no clicker with the id " + id + ".");
            }
            catch (FormatException e)
            {
                instance.PrintMessage(args.ElementAt(1) + " is not a valid clicker id.");
            }
        }
    }
}
