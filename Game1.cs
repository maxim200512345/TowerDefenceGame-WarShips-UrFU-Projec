using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace TD
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<Vector2, int> tileMap;
        private List<Rectangle> textureStore;
        private Texture2D textureAtlas;

        private Dictionary<Vector2, int> loadMap(string filePath)
        {
            var result = new Dictionary<Vector2, int>();
            StreamReader reader = new StreamReader(filePath);
            string line;
            int y = 0;
            while ((line = reader.ReadLine()) != null){
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > 0)
                            result[new Vector2(x,y)] = value;
                    }
                }
                y++;
            }
            reader.Close();
            return result;


        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            tileMap = loadMap("../../../Data/map.csv");
            textureStore = new List<Rectangle> {
                new Rectangle(0, 0, 256, 256),
                new Rectangle(256, 0, 256, 256)

            
            };
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureAtlas = Content.Load<Texture2D>("spritesheet");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            var path = new List<Vector2>();
            foreach (var item in tileMap)
            {
                Rectangle dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                Rectangle src;
                if (item.Value - 1 == 1)
                {
                    path.Add(item.Key);
                    src = textureStore[item.Value - 2];
                }
                else src = textureStore[item.Value - 1];
                _spriteBatch.Draw(textureAtlas, dest, src, Color.White);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}