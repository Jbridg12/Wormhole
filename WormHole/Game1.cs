// Game1.cs
// Contributors: Josh Bridges
//
// The main game class.
// This is where all of the other classes and methods have to be involved somehow.


using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    public class Game1 : Game
    {

        public static GraphicsDeviceManager _graphics { private set; get; }
        private SpriteBatch _spriteBatch;
        public static SpriteFont Font { get; set; }
        public enum GameState
        {
            Main,
            Instructions,
            Pause,
            Game,
            Gameover
        }

        public enum Direction { Up, Right, Down, Left }

        //What is currently getting shown on screen
        public static GameState CurrentState { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            CurrentState = GameState.Main;

            //Sets Full Screen, more experimentation needed
            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            _graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Base");
            EntityManager.Instance.LoadContent(Content);
            ScreenManager.Instance.LoadContent(Content);
            UIManager.Instance.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F1))
                Exit();

            if (CurrentState == GameState.Game)
                this.IsMouseVisible = false;
            if (CurrentState == GameState.Main)
                this.IsMouseVisible = true;
            if (CurrentState == GameState.Gameover)
                this.IsMouseVisible = true;

            ScreenManager.Instance.Update(gameTime);
            UIManager.Instance.Update(gameTime);
            EntityManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);


            _spriteBatch.Begin();
            ScreenManager.Instance.Draw(_spriteBatch);
            UIManager.Instance.Draw(_spriteBatch);
            EntityManager.Instance.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }

}
