using System.Collections.Generic;

namespace AutoClickerConsole
{
    interface ICommand
    {
        void Run(List<string> args);
    }
}
