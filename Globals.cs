using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD
{
    public static class Globals
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static GraphicsDevice GraphicsDevice { get; set; }

        public static Texture2D TextureAtlas { get; set; }
        public static Vector2 TranslateCoordsToTile(int x, int y)
        {
            return new Vector2(x / 64, y / 64);
        }
        public static Vector2 TranslateTileToCoords(int x, int y)
        {
            return new Vector2(x * 64, y * 64);
        }
        public static List<Rectangle> textureStore = new List<Rectangle> {
                new Rectangle(0, 0, 256, 256),
                new Rectangle(256, 0, 256, 256),
                new Rectangle(256 * 1, 0, 256, 256)
            };
        public static float Time { get; set; }
        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
