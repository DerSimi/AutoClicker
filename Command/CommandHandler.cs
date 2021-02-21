using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutoClickerConsole.Command
{
    class CommandHandler
    {
        private readonly AutoClicker instance;

        private readonly Dictionary<string, ICommand> commands;

        private bool run;

        public CommandHandler(AutoClicker instance)
        {
            this.run = true;
            this.instance = instance;
            this.commands = new Dictionary<string, ICommand>();
        }

        public void Run()
        {
            while (run)
            {
                Thread.Sleep(25);

                string line = Console.ReadLine();

                if (line != null && line.Length != 0)
                {
                    var list = Regex.Matches(line.ToUpper(), @"[\""].+?[\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToList();

                    if (list.Count >= 1)
                    {
                        if (commands.ContainsKey(list.ElementAt(0)))
                            commands[list.ElementAt(0)].Run(list);
                        else
                            instance.PrintMessage("Unknown command, try help");
                    }
                }
            }
        }

        public void RegisterCommand(string command, ICommand icommand)
           => commands.Add(command.ToUpper(), icommand);

        public void UnregisterCommand(string command)
            => commands.Remove(command);

        public void StopHandler()
            => run = false;
    }
}
