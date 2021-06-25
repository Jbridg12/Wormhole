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
        private Texture2D currentdisplay;
        private int selectedButton;

        public MainMenuScreen(Dictionary<string, Texture2D> textures, SpriteFont font) : base(textures, font)
        {
            this.currentdisplay = Displays["Initial"];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentdisplay, Vector2.Zero, Color.White);
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

            base.Update(gameTime);
        }
    }
}
