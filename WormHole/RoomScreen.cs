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
        public RoomScreen(Texture2D texture, SpriteFont font) : base(texture, font)
        {
            this.Entities.Add(Game1.P1);
        }

        public RoomScreen(Texture2D texture, SpriteFont font, List<Entity> entities) : base(texture, font)
        {
            this.Entities = entities;
            this.Entities.Add(Game1.P1);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();

            if (status.IsKeyDown(Keys.Escape))          // If the player presses escape in a room it returns to the menu
            {
                ScreenManager.Instance.ChangeScreen("MainMenu");
            }
            base.Update(gameTime);
        }
    }
}
