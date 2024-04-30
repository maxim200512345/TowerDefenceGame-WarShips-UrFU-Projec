using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Factories;
using TD.Models;

namespace TD.managers
{
    public class GameManager
    {
        private Map Map;
        private List<Vector2> PotentialTowerList;
        private List<Vector2> path;
        private List<BuildedTower> clicked;
        private List<UnitShip> KilledBoats;
        private ShipFactory shipFactory;

        public GameManager() {
            
        
        }
        public void Initialize()
        {
            Map = new Map("../../../Data/map.csv");
            path = Map.GetPath();
            shipFactory = new ShipFactory(path);
            clicked = new();
            PotentialTowerList = Map.GetTowerList();
            KilledBoats = new();
        }

        public void Update(GameTime gameTime)
        {
            KilledBoats.Clear();
            shipFactory.Update(gameTime);

            var xCoord = Mouse.GetState().X;
            var yCoord = Mouse.GetState().Y;
            var convertedCoords = Globals.TranslateCoordsToTile(xCoord, yCoord);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (PotentialTowerList.Contains(new Vector2(convertedCoords.X, convertedCoords.Y)))
                {
                    clicked.Add(new BuildedTower(new Vector2(convertedCoords.X, convertedCoords.Y)));
                }
            }
            foreach (var tower in clicked)
            {
                var flag = false;
                var positionInPixels = Globals.TranslateTileToCoords((int)tower.Placement.X, (int)tower.Placement.Y);
                foreach (var ship in shipFactory.ShipList)
                {
                    if ((ship.currentCoords.X - positionInPixels.X) * (ship.currentCoords.X - positionInPixels.X) + (ship.currentCoords.Y - positionInPixels.Y) * (ship.currentCoords.Y - positionInPixels.Y) <= BuildedTower.ShootRadius * BuildedTower.ShootRadius)
                        if (ship.IsDrawned())
                        {
                            KilledBoats.Add(ship);
                            flag = true;
                            break;
                        }
                }

                foreach (var killed in KilledBoats)
                    shipFactory.ShipList.Remove(killed);
                if (flag) break;
            }

        }
        public void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Begin();
            Map.Draw(gameTime);
            shipFactory.Draw(gameTime);

            ////////////////////прорисовка купленных башен
            foreach (var tower in clicked)
            {
                Globals.SpriteBatch.Draw(Globals.TextureAtlas, new Rectangle((int)tower.Placement.X * 64, (int)tower.Placement.Y * 64, 64, 64), new Rectangle(256 * 2, 0, 256, 256), Color.White);
            }
            /*foreach (var killed in shipList)
                Globals.SpriteBatch.Draw(textureAtlas, new Rectangle((int)killed.currentCoords.X * 64, (int)killed.currentCoords.Y * 64, 64, 64), new Rectangle(256 * 6, 0, 256, 256), Color.White);
    */
            foreach (var killed in KilledBoats)
            {
                Globals.SpriteBatch.Draw(Globals.TextureAtlas, new Rectangle((int)killed.currentCoords.X, (int)killed.currentCoords.Y, 64, 64), new Rectangle(256 * 6, 0, 256, 256), Color.White);
            }
            Globals.SpriteBatch.End();
        }
    }
}
