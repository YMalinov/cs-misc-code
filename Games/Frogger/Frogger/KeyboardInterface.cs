using System;

namespace Frogger
{
    class KeyboardInterface : IUserInterface
    {
        public void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key.Equals(ConsoleKey.LeftArrow))
                {
                    if (this.OnLeftPressed != null)
                    {
                        this.OnLeftPressed(this, new EventArgs());
                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.RightArrow))
                {
                    if (this.OnRightPressed != null)
                    {
                        this.OnRightPressed(this, new EventArgs());
                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.UpArrow))
                {
                    if (this.OnUpPressed != null)
                    {
                        this.OnUpPressed(this, new EventArgs());
                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.DownArrow))
                {
                    if (this.OnDownPressed != null)
                    {
                        this.OnDownPressed(this, new EventArgs());
                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.P))
                {
                    if (this.OnPausePressed != null)
                    {
                        this.OnPausePressed(this, new EventArgs());
                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.E))
                {
                    if (this.OnExitPressed != null)
                    {
                        this.OnExitPressed(this, new EventArgs());
                    }
                }
            }
        }

        public event EventHandler OnLeftPressed;

        public event EventHandler OnRightPressed;

        public event EventHandler OnUpPressed;

        public event EventHandler OnDownPressed;

        public event EventHandler OnPausePressed;

        public event EventHandler OnExitPressed;
    }
}
