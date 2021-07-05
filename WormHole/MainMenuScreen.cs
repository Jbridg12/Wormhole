// A screen type that is used for doing menu screens in the game

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;

namespace WormHole
{
    class MainMenuScreen : GameScreen
    {
        private Texture2D currentdisplay;
        private int selectedButton;
        private Rectangle displayLocation;

        public MainMenuScreen(Dictionary<string, Texture2D> textures, SpriteFont font) : base(textures, font)
        {
            displayLocation = new Rectangle(((int)ScreenManager.Instance.Dimensions.X / 2) - 384, ((int)ScreenManager.Instance.Dimensions.Y / 2) - 384, 768, 768);
            this.currentdisplay = Displays["Initial"];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "Hello!", new Vector2(480.0f, 200.0f), Color.Aquamarine);
            spriteBatch.Draw(currentdisplay, displayLocation, Color.White);

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();
            if (status.IsKeyDown(Keys.Space))
            {
                this.currentdisplay = Displays["NewGame"];     //Highlight New game button
                selectedButton = 1;
            }
            else if (status.IsKeyDown(Keys.LeftShift))
            {
                this.currentdisplay = Displays["Initial"];      // Un-highlight NG button
                selectedButton = 0;
            }
            else if (status.IsKeyDown(Keys.Enter))
            {
                if(selectedButton == 1)
                    ScreenManager.Instance.ChangeScreen("Room"); // Enter the first (and only) room
            }

            //Close game
            if (status.IsKeyDown(Keys.Escape) && currentdisplay == Displays["Initial"])
            {
            }

            base.Update(gameTime);
        }
    }
}
