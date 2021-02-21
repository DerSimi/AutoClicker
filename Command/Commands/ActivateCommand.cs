using System.Collections.Generic;

namespace AutoClickerConsole.Command.Commands
{
    class ActivateCommand : ICommand
    {
        private readonly AutoClicker instance;

        public ActivateCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            if (!instance.IsActivated())
                instance.Activate();
            else
                instance.PrintMessage("All clickers are already activated.");
        }
    }
}
