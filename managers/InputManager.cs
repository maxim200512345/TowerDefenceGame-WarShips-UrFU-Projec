using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TD.managers
{
    internal class InputManager
    {
        private MouseState mouseState;

        public InputManager() {
            mouseState = Mouse.GetState();
        }
        public bool Clicked { get; }

        public bool IsClicked => mouseState.LeftButton == ButtonState.Pressed;
    }
}
