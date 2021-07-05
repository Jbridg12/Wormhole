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
        private SpriteFont font;
        private Texture2D display, display0;
        private Texture2D currentdisplay;
        private int selectedButton;

        private Rectangle displayLocation;
       

        public override void LoadContent()
        {
            base.LoadContent();
            this.font = content.Load<SpriteFont>("Base");
            this.display = content.Load<Texture2D>("menu");
            this.display0 = content.Load<Texture2D>("menu1");
            this.entities = new List<Entity>();
            this.currentdisplay = display;

            displayLocation = new Rectangle(((int)ScreenManager.Instance.dimensions.X/2) - 384, ((int)ScreenManager.Instance.dimensions.Y / 2) - 384, 768, 768);
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
                this.currentdisplay = display0;     //Highlight New game button
                selectedButton = 1;
            }
            else if (status.IsKeyDown(Keys.LeftShift))
            {
                this.currentdisplay = display;      // Un-highlight NG button
                selectedButton = 0;
            }
            else if (status.IsKeyDown(Keys.Enter))
            {
                if(selectedButton == 1)
                    ScreenManager.Instance.ChangeScreen(1); // Enter the first (and only) room
            }

            //Close game
            if (status.IsKeyDown(Keys.Escape) && currentdisplay == display)
            {
            }

            base.Update(gameTime);
        }
    }
}
