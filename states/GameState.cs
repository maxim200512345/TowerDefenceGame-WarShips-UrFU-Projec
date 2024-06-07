using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using TD.Factories;
using TD.managers;
using TD.Models;
using TD.View;

namespace TD.states
{
    public class GameState : State
    {
        private GameDraw gameDraw;
        private Map Map;
        private List<Vector2> PotentialTowerList;
        private List<Vector2> clicked;
        private List<UnitShip> KilledBoats;
        private ShipFactory shipFactory;
        private SoundEffect soundEffectBuild;
        private SoundEffect soundEffectBoom;
        private int points = 15;
        private const int radius = 128;
        private SpriteFont ScoreBlock;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            initialize();
        }
        public void initialize()
        {
            Map = new Map("../../../Data/map.csv");
            gameDraw = new GameDraw(Map);
            shipFactory = new ShipFactory();
            clicked = new();
            PotentialTowerList = Map.GetTowerList();
            KilledBoats = new();
            soundEffectBuild = Globals.Content.Load<SoundEffect>("build");
            ScoreBlock = Globals.Content.Load<SpriteFont>("score");
            soundEffectBoom = Globals.Content.Load<SoundEffect>("boom");
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Globals.SpriteBatch.Begin();
            gameDraw.Draw(shipFactory.ShipList, clicked, points);
            Globals.SpriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }
        
        public override void Update(GameTime gameTime)
        {
            shipFactory.Clear();
            shipFactory.Update(gameTime);
            if (InputManager.IsClicked)
            {
                var coords = InputManager.GetTileCoords();
                if (PotentialTowerList.Contains(new Vector2(coords.X, coords.Y)))
                {
                    if (points > 10)
                    {
                        soundEffectBuild.Play();
                        clicked.Add(new Vector2(coords.X, coords.Y));
                        points -= 10;
                    }

                }
            }

            foreach (var tower in clicked)
            {
                var flag = false;
                var positionInPixels = Globals.TranslateTileToCoords((int)tower.X, (int)tower.Y);
                shipFactory.BlowCheck(radius, positionInPixels, soundEffectBoom, ref flag, ref points);
                shipFactory.RemoveUnexistedShips();
                if (flag) break;
            }
        }
        public bool GameIsWin() => shipFactory.IsWin();

        public bool GameIsLost() => shipFactory.IsLost();
    }
}
