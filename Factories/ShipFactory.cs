using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Models;

namespace TD.Factories
{
    public class ShipFactory
    {
        public List<UnitShip> ShipList { get; }


        public ShipFactory(List<Vector2> path)
        {
            ShipList = GenerateShips(10, path);
            /*ShipList.Add(new UnitShip(5f, path, Globals.TranslateTileToCoords((int)path[1].X, (int)path[1].Y)));
            for (int i = 10; i < 100;  i++)
                ShipList.Add(new UnitShip())*/
        }
        public List<UnitShip> GenerateShips(int amount, List<Vector2> path)
        {
            var result = new List<UnitShip>();
            var startedPoint = ((int)path[0].X, (int)path[0].Y);
            var coords = Globals.TranslateTileToCoords(startedPoint.Item1, startedPoint.Item2);
            for (int dy = 0, dx = 64/amount; dy <  32 && dx < 64; dy += 10, dx += 10)
            {
                result.Add(new UnitShip(1f, path, new Vector2(coords.X += dx, coords.Y += dy)));
            }
            return result;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (UnitShip ship in ShipList)
            {
                ship.Draw(gameTime, ship);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (UnitShip ship in ShipList)
            {
                ship.Update(gameTime);
            }
        }
    }
}
