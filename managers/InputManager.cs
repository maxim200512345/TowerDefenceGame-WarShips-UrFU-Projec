using Microsoft.Xna.Framework;
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

        public InputManager() {
        }
        public bool Clicked { get; }
        public static Vector2 GetTileCoords()
        {
            var xCoord = Mouse.GetState().X;
            var yCoord = Mouse.GetState().Y;
            var convertedCoords = Globals.TranslateCoordsToTile(xCoord, yCoord);
            return convertedCoords;
        }
        public static bool IsClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;
    }
}
