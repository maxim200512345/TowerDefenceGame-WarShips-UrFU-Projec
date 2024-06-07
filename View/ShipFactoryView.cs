using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Factories;
using TD.Models;

namespace TD.View
{
    public class ShipFactoryView
    {
        ShipView shipView = new ShipView();
        ShipFactory shipFactory = new ShipFactory();
        public void Draw(List<UnitShip> ShipList)
        {
            foreach (UnitShip ship in ShipList)
            {
                shipView.Draw(ship);
            }
            foreach (var killed in shipFactory.GetKilled())
            {
                Globals.SpriteBatch.Draw(Globals.TextureAtlas, new Rectangle((int)killed.GetCoords().X, (int)killed.GetCoords().Y, ConstantsView.ShipPictureSize, ConstantsView.ShipPictureSize), ConstantsView.fireAsset, Color.White);
            }
        }
    }
}
