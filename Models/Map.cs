using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Models
{
    class Map
    {
        Dictionary<Vector2, int> TileMap;
        List<Rectangle> TextureStore;
        public Map(string filePath)
        {
            TileMap = LoadMap(filePath);
            TextureStore = new List<Rectangle> {
                new Rectangle(0, 0, 256, 256),
                new Rectangle(256, 0, 256, 256),
                new Rectangle(256 * 1, 0, 256, 256)
            };
        }

        private Dictionary<Vector2, int> LoadMap(string filePath)
        {
            var result = new Dictionary<Vector2, int>();
            StreamReader reader = new StreamReader(filePath);
            string line;
            int y = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > 0)
                            result[new Vector2(x, y)] = value;
                    }
                }
                y++;
            }
            reader.Close();
            return result;
        }

        public List<Vector2> GetPath()
        {
            var path = new List<Vector2>();
            foreach (var item in TileMap)
                if (item.Value == 2)
                    path.Add(item.Key);
            return path;

        }
        public List<Vector2> GetTowerList()
        {
            var result = new List<Vector2>();
            foreach (var item in TileMap)
                if (item.Value == 3)
                    result.Add(item.Key);
            return result;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var item in TileMap)
            {
                Rectangle dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                Rectangle src;
                if (item.Value == 3)
                {//Tower
                    src = TextureStore[item.Value - 2];
                }
                if (item.Value == 2)
                {//Path
                    src = TextureStore[item.Value - 2];
                }
                else src = TextureStore[item.Value - 1];
                Globals.SpriteBatch.Draw(Globals.TextureAtlas, dest, src, Color.White);
            }

        }
    }
}
