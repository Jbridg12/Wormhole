// RoomScreen.cs
// Contributors: Josh Bridges

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
            //base.Draw(spriteBatch);
            spriteBatch.Draw(Display,
                new Vector2(Game1._graphics.GraphicsDevice.Viewport.Width / 2, Game1._graphics.GraphicsDevice.Viewport.Height / 2),
                null,
                Color.White,
                0f,
                new Vector2(Display.Width / 2, Display.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            spriteBatch.DrawString(Font, String.Format("Salvage: {0}", Game1.P1.Consumables["Salvage"]), new Vector2(1500f, 60f), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();

            if (!EnemiesAlive())
            {
                foreach (Entity door in Entities)
                {
                    if(door.GetType() == typeof(Door))
                        door.Active = true;
                }
                    
            }

            if (status.IsKeyDown(Keys.Escape))          // If the player presses escape in a room it returns to the menu
            {
                ScreenManager.Instance.ChangeScreen("MainMenu");
            }
            base.Update(gameTime);
        }

        public bool EnemiesAlive()
        {
            foreach(Entity en in Entities)
            {
                if (en is Enemy && en.Active) return true;
            }
            return false;
        }
    }
}
