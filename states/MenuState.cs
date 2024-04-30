using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TD.Controls;


namespace TD.states;

public class MenuState : State
{
    private List<Component> _components;

    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
      : base(game, graphicsDevice, content)
    {
        var buttonTexture = _content.Load<Texture2D>("Controls/Button");
        var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

        var newGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(300, 200),
            Text = "Start Game",
        };

        newGameButton.Click += NewGameButton_Click;
        _components = new List<Component>()
  {
    newGameButton,
  };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        foreach (var component in _components)
            component.Draw(gameTime, spriteBatch);

        spriteBatch.End();
    }

    private void NewGameButton_Click(object sender, EventArgs e)
    {
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
    }

    public override void PostUpdate(GameTime gameTime)
    {
        // remove sprites if they're not needed
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var component in _components)
            component.Update(gameTime);
    }
}