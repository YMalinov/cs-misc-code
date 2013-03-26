using System;

namespace Frogger
{
    interface IUserInterface
    {
        event EventHandler OnLeftPressed;

        event EventHandler OnRightPressed;

        event EventHandler OnUpPressed;

        event EventHandler OnDownPressed;

        event EventHandler OnPausePressed;

        event EventHandler OnExitPressed;

        void ProcessInput();
    }
}
