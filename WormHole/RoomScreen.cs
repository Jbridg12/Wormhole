using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class RoomScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D display;
        private Texture2D currentdisplay;

        public override void LoadContent()
        {
            base.LoadContent();
            this.font = content.Load<SpriteFont>("Base");
            this.display = content.Load<Texture2D>("room1");
            this.currentdisplay = display;
            this.entities = new List<Entity>();
            
            entities.Add(Game1.player1);    // Add player to combat screens
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHelper.ImageCenter(spriteBatch, Game1._graphics, currentdisplay);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();

            if (status.IsKeyDown(Keys.Escape))          // If the player presses escape in a room it returns to the menu
            {
                ScreenManager.Instance.ChangeScreen(0);
            }
            base.Update(gameTime);
        }
    }
}
