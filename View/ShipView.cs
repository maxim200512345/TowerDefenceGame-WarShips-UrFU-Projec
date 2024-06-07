using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Models;

namespace TD.View
{
    public class ShipView
    {

        public void Draw(UnitShip ship)
        {
            var destination = new Rectangle((int)ship.GetCoords().X, (int)ship.GetCoords().Y, ConstantsView.ShipPictureSize, ConstantsView.ShipPictureSize);
            Globals.SpriteBatch.Draw(Globals.TextureAtlas, destination, ConstantsView.shipAsset, Color.White);
        }
    }
}
