using System.Collections.Generic;

namespace AutoClickerConsole.Command.Commands
{
    class ExitCommand : ICommand
    {
        private readonly AutoClicker instance;

        public ExitCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            instance.PrintMessage("Stopping clickers...");

            foreach (Clicker clicker in instance.clickers.Values)
                clicker.Stop();

            instance.clickers.Clear();
            System.Environment.Exit(1);
        }
    }
}
