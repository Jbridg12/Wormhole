// A screen type that is used for doing menu screens in the game

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class MainMenuScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D display, display0;
        private Texture2D currentdisplay;

        public override void LoadContent()
        {
            base.LoadContent();
            font = content.Load<SpriteFont>("Base");
            display = content.Load<Texture2D>("menu0");
            display0 = content.Load<Texture2D>("menu1");
            currentdisplay = display;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "Hello!", new Vector2(480.0f, 200.0f), Color.Aquamarine);
            spriteBatch.Draw(currentdisplay, Vector2.Zero, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();
            if (status.IsKeyDown(Keys.Space))
            {
                this.currentdisplay = display0;     //Highlight New game button
            }
            else if (status.IsKeyDown(Keys.LeftShift))
            {
                this.currentdisplay = display;      // Un-highlight NG button
            }

            base.Update(gameTime);
        }
    }
}
