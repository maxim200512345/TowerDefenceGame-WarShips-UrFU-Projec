using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TD.managers;
using TD.states;

namespace TD
{
    public class Game1 : Game
    {

        private State _currentState;
        private State _nextState;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D textureAtlas;   
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.GraphicsDevice = GraphicsDevice;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureAtlas = Content.Load<Texture2D>("spritesheet2");
            Globals.TextureAtlas = textureAtlas;

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

        }
        protected override void Update(GameTime gameTime)
        {
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);
            /*gameManager.Update(gameTime);*/
            _currentState.PostUpdate(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            /*gameManager.Draw(gameTime);*/
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }
    }
}