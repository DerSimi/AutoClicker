using System.Collections.Generic;

namespace AutoClickerConsole.Command.Commands
{
    class DeactivateCommand : ICommand
    {
        private readonly AutoClicker instance;

        public DeactivateCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            if (instance.IsActivated())
                instance.Deactivate();
            else
                instance.PrintMessage("All clickers are already deactivated.");
        }
    }
}
