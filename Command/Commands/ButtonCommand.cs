using System.Collections.Generic;
using System.Threading;

namespace AutoClickerConsole.Command.Commands
{
    class ButtonCommand : ICommand
    {
        private readonly AutoClicker instance;

        public ButtonCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {

            instance.PrintMessage("Please press and hold a button.");

            Thread.Sleep(1000);
            instance.PrintMessage("Reading...");
            Thread.Sleep(500);

            int buttonId = 0;

            while (buttonId == 0)
            {
                for (int i = 0; i < 255; i++)
                {
                    int keyState = NativeMethods.GetAsyncKeyState(i);

                    if (keyState != 0 && i != 85)
                        buttonId = i;
                }
            }

            instance.PrintMessage("Buttonid: " + buttonId);
            instance.PrintMessage("If you pressed to late or something else, this could be a wrong value.");
        }
    }
}
