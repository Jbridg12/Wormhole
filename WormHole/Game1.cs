﻿// The main game class.
// THis is where all of the other classes and methods have to be involved somehow.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics { private set; get; }
        private SpriteBatch _spriteBatch;
        public SpriteFont Font { get; set; }
        public static Player P1 {get; set;}   // one player across the entire game so make it here for use everywhere
        public enum Direction { Up, Right, Down, Left }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F1))
                Exit();
            

            ScreenManager.Instance.Update(gameTime);
            EntityManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();
            ScreenManager.Instance.Draw(_spriteBatch);
            EntityManager.Instance.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }

}
