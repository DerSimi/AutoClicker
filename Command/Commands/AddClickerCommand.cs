using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoClickerConsole.Command.Commands
{
    class AddClickerCommand : ICommand
    {
        private readonly AutoClicker instance;

        public AddClickerCommand(AutoClicker instance)
            => this.instance = instance;

        public void Run(List<string> args)
        {
            int activationKey = 0, cps = 0, randomValue = 0, keyToClick = 0;

            if (args.Count != 4 && args.Count != 5)
            {
                instance.PrintMessage("addcl <Activation button> <Button to click> <CPS> <Random value> - Add a clicker");
                return;
            }

            try
            {
                activationKey = Int32.Parse((args.ElementAt(1)));

                if (activationKey <= 0)
                {
                    instance.PrintMessage("Activation key must be greater than zero.");
                    return;
                }

                if (!instance.IsValidKey(activationKey))
                {
                    instance.PrintMessage("Activation key is invalid.");
                    return;
                }

            }
            catch (FormatException e)
            {
                instance.PrintMessage(args.ElementAt(1) + " is not a valid number for the activation key.");
                return;
            }

            try
            {
                if (args.ElementAt(2).ToUpper().Equals("LEFT"))
                    keyToClick = -1;
                else if (args.ElementAt(2).ToUpper().Equals("RIGHT"))
                    keyToClick = -2;
                else
                {
                    keyToClick = Int32.Parse((args.ElementAt(2)));

                    if (keyToClick <= 0)
                    {
                        instance.PrintMessage("Key to click must be greater than zero.");
                        return;
                    }

                    if (!instance.IsValidKey(keyToClick))
                    {
                        instance.PrintMessage("Key to click is invalid.");
                        return;
                    }
                }
            }
            catch (FormatException e)
            {
                instance.PrintMessage(args.ElementAt(2) + " is not a valid argument for the key to click value.");
                return;
            }

            try
            {
                cps = Int32.Parse((args.ElementAt(3)));

                if (cps <= 0)
                {
                    instance.PrintMessage("CPS must be greater than zero.");
                    return;
                }
            }
            catch (FormatException e)
            {
                instance.PrintMessage(args.ElementAt(3) + " is not a valid number for the cps value.");
                return;
            }

            if (args.Count == 5)
            {
                try
                {
                    randomValue = Int32.Parse((args.ElementAt(4)));
                }
                catch (FormatException e)
                {
                    instance.PrintMessage(args.ElementAt(4) + " is not a valid number for the random value.");
                    return;
                }

                if (randomValue >= cps)
                {
                    instance.PrintMessage("The random value must be lower than the click value.");
                    return;
                }
            }

            if (keyToClick == activationKey)
            {
                instance.PrintMessage("buttonToClick and activationButton can't be same!");
                return;
            }

            uint modifyId = 0;
            bool stopped = false;

            foreach (KeyValuePair<UInt32, Clicker> item in instance.clickers)
            {
                modifyId++;
                if (item.Value.GetActivationKey() == activationKey && item.Value.GetKeyToClick() == keyToClick)
                {
                    stopped = true;
                    item.Value.Stop();
                    break;
                }
            }

            if (!stopped)
            {
                if (Error(activationKey, keyToClick, null))
                    return;

                Clicker clicker = new Clicker(instance, activationKey, keyToClick, cps, randomValue);
                instance.clickers.Add((uint)(instance.clickers.Count + 1), clicker);

                instance.PrintMessage("Clicker #" + instance.clickers.Count + " successfully created.");
            }
            else
            {
                if (Error(activationKey, keyToClick, instance.clickers[modifyId]))
                {
                    Console.WriteLine("Abbruch hier für " + modifyId);
                    return;
                }

                Clicker clicker = new Clicker(instance, activationKey, keyToClick, cps, randomValue);
                instance.clickers.Remove(modifyId);
                instance.clickers.Add(modifyId, clicker);

                instance.PrintMessage("Clicker #" + modifyId + " modified.");
            }
        }

        private bool Error(int activationKey, int keyToClick, Clicker clicker)
        {
            uint i = 0;

            foreach (KeyValuePair<UInt32, Clicker> item in instance.clickers)
            {
                if (item.Value.GetKeyToClick() == keyToClick)
                {
                    if (clicker != null && clicker == item.Value)
                        return false;

                    instance.PrintMessage("The button to click " + (item.Value.GetKeyToClick() == -1 ? "LEFT MOUSE" : (item.Value.GetKeyToClick() == -2 ? "RIGHT MOUSE" : item.Value.GetKeyToClick() + "")) + " is already used by clicker #" + (i + 1) + ".");
                    return true;
                }

                if (item.Value.GetActivationKey() == activationKey)
                {
                    if (clicker != null && clicker == item.Value)
                        return false;

                    instance.PrintMessage("The activation key " + item.Value.GetActivationKey() + " is already used by clicker #" + (i + 1) + ".");
                    return true;
                }

                i++;
            }

            return false;
        }
    }
}
