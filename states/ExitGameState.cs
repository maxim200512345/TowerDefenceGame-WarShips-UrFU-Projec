using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.states
{
    public class ExitGameState : State
    {
        private SpriteFont TextField;
        private string statusText;
        public ExitGameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var TextField = _content.Load<SpriteFont>("Fonts/Font");
            statusText = "поздравляю, вы выиграли";
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Globals.SpriteBatch.DrawString(TextField, statusText, new Vector2(500, 500), Color.Black);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
