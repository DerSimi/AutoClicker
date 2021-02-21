using System;
using System.Threading;

namespace AutoClickerConsole
{
    class Clicker
    {
        private AutoClicker instance;

        private readonly int activationKey, cps, randomValue, keyToClick;

        private long lastChange;

        private bool run, enabled;

        public Clicker(AutoClicker instance, int activationKey, int keyToClick, int cps, int randomValue)
        {
            if (cps <= 0)
                throw new ArgumentException("cps must be greater than zero.");

            if (randomValue > cps)
                throw new ArgumentException("randomValue can't be greater than cps.");

            if (randomValue < 0)
                throw new ArgumentException("randomValue must be greater than zero or zero.");

            if(!instance.IsValidKey(activationKey))
                throw new ArgumentException(keyToClick + " is invalid.");

            if (keyToClick != -1 && keyToClick != -2 && !instance.IsValidKey(keyToClick))
                throw new ArgumentException(keyToClick + " is invalid.");

            this.instance = instance;
            this.activationKey = activationKey;
            this.keyToClick = keyToClick;
            this.cps = cps;
            this.randomValue = randomValue;

            Start();
        }

        private void Run()
        {
            while (run)
            {
                Thread.Sleep(25);

                if (activationKey != 0 && NativeMethods.IsButtonPressed(activationKey))
                    lastChange = CurrentMillis();
                else
                    continue;

                if (CurrentMillis() - lastChange > 10)
                    enabled = false;
                else
                    enabled = true;

                if (enabled)
                {
                    int waitTime = 1000 / (cps - new Random().Next(0, randomValue));

                    for (uint i = 0; i < cps; i++)
                    {
                        if (NativeMethods.IsButtonPressed(activationKey))
                        {
                            Thread.Sleep(waitTime);
                            PressButton();
                        }
                    }
                }
            }
        }

        public void Start()
        {
            run = true;
            new Thread(Run).Start();
        }

        public void Stop()
            => run = false;

        private void PressButton()
        {
            if (keyToClick == -1)
            {
                NativeMethods.MouseClick(false);
                return;
            }

            if (keyToClick == -2)
            {
                NativeMethods.MouseClick(true);
                return;
            }

            NativeMethods.PressButton((ushort)keyToClick);
        }

        public int GetActivationKey()
        {
            return activationKey;
        }

        public int GetCps()
        {
            return cps;
        }

        public int GetRandomValue()
        {
            return randomValue;
        }

        public int GetKeyToClick()
        {
            return keyToClick;
        }

        private long CurrentMillis()
        {
            TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)timeSpan.TotalMilliseconds;
        }
    }
}
