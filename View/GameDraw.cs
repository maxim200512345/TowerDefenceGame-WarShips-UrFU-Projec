using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Factories;
using TD.Models;

namespace TD.View
{
    public class GameDraw
    {
        private Map Map;
        private ShipFactoryView ShipFactoryView = new ShipFactoryView();
        private SpriteFont ScoreBlock = Globals.Content.Load<SpriteFont>("score");
        public GameDraw(Map map)
        {
            Map = map;
        }
        public void Draw(List<UnitShip> ShipList, List<Vector2> clicked, int points)
        {
            Map.Draw();
            ShipFactoryView.Draw(ShipList);

            foreach (var tower in clicked)
            {
                Globals.SpriteBatch.Draw(Globals.TextureAtlas, new Rectangle((int)tower.X * 64, (int)tower.Y * 64, 64, 64), ConstantsView.towerAsset, Color.White);
            }
            var text = $"score : {points}";
            Globals.SpriteBatch.DrawString(ScoreBlock, text, new Vector2(0, 0), Color.Black);
        }

    }
}
