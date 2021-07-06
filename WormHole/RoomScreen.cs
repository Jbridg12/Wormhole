using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    public class RoomScreen : GameScreen
    {
        public RoomScreen[] AdjacentRooms { get; set; }
        public int Depth { get; set; }
        public RoomScreen(Texture2D texture, SpriteFont font, int depth) : base(texture, font)
        {
            this.Depth = depth;
            this.Entities.Add(Game1.P1);
            AdjacentRooms = new RoomScreen[4];
        }

        public RoomScreen(Texture2D texture, SpriteFont font, int depth, List<Entity> entities) : base(texture, font)
        {
            this.Depth = depth;
            this.Entities = entities;
            this.Entities.Add(Game1.P1);
            AdjacentRooms = new RoomScreen[4];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Font, String.Format("Salvage: {0}", Game1.P1.Consumables["Salvage"]), new Vector2(1500f, 60f), Color.Black);
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
