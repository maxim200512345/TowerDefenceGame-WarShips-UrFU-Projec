using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Models;

namespace TD.Factories
{
    public class TowerFactory
    {
        public List<BuildedTower> BuildedTowers;
        private ShipFactory shipFactory;


        public void Update(GameTime gameTime)
        {
            foreach (var tower in BuildedTowers)
            {
                var positionInPixels = Globals.TranslateTileToCoords((int)tower.Placement.X, (int)tower.Placement.Y);

            }
        }
    }
}
