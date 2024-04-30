using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using TD.Factories;
using TD.managers;
using TD.Models;

namespace TD
{
    public class Game1 : Game
    {
        private Map map;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D textureAtlas;
        private List<Vector2> PotentialTowerList;
        private List<Vector2> path;
        private List<BuildedTower> clicked;
        private List<UnitShip> KilledBoats;
        private ShipFactory shipFactory;
        private InputManager inputManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Content = Content;
            map = new Map("../../../Data/map.csv");
            path = map.GetPath();
            shipFactory = new ShipFactory(path);
            inputManager = new InputManager();
            clicked = new();
            PotentialTowerList = map.GetTowerList();
            KilledBoats = new();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.GraphicsDevice = GraphicsDevice;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureAtlas = Content.Load<Texture2D>("spritesheet2");
            Globals.TextureAtlas = textureAtlas;

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            KilledBoats.Clear();
            shipFactory.Update(gameTime);
            ////////////////////обработка изменения башен на карте
            // координаты мыши

            var xCoord = Mouse.GetState().X;
            var yCoord = Mouse.GetState().Y;
            var convertedCoords = Globals.TranslateCoordsToTile(xCoord, yCoord);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (PotentialTowerList.Contains(new Vector2(convertedCoords.X, convertedCoords.Y)))
                {
                    /*clicked.Add(new Vector2(convertedCoords.X, convertedCoords.Y));*/
                    clicked.Add(new BuildedTower(new Vector2(convertedCoords.X, convertedCoords.Y)));
                }
            }

            ////////////////////////////////// обработка попадания в корабль
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

            //////////////////////////////////
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin();

            map.Draw(gameTime);
            shipFactory.Draw(gameTime);

            ////////////////////прорисовка купленных башен
            foreach (var tower in clicked)
            {
                Globals.SpriteBatch.Draw(textureAtlas, new Rectangle((int)tower.Placement.X * 64, (int)tower.Placement.Y * 64, 64, 64), new Rectangle(256 * 2, 0, 256, 256), Color.White);
            }
            /*foreach (var killed in shipList)
                Globals.SpriteBatch.Draw(textureAtlas, new Rectangle((int)killed.currentCoords.X * 64, (int)killed.currentCoords.Y * 64, 64, 64), new Rectangle(256 * 6, 0, 256, 256), Color.White);
*/
            foreach (var killed in KilledBoats)
            {
                Globals.SpriteBatch.Draw(textureAtlas, new Rectangle((int)killed.currentCoords.X, (int)killed.currentCoords.Y, 64, 64), new Rectangle(256 * 6, 0, 256, 256), Color.White);
            }
            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}