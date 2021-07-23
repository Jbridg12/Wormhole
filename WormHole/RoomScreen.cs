// RoomScreen.cs
// Contributors: Josh Bridges, Chris LoSardo

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
        public RoomScreen parent;

        public int Depth { get; set; }
        public int Index { get; set; }  // Index in the floor array that this room is in

        protected int doorAnimationState;   // animation state for door animation later

        KeyboardState pvState;

        public RoomScreen(Texture2D texture, SpriteFont font, int depth) : base(texture, font)
        {
            this.Depth = depth;
            this.Entities.Add(Player.Instance);
            doorAnimationState = 0;
        }


        public RoomScreen(Texture2D texture, SpriteFont font, int depth, List<Entity> entities) : this(texture, font, depth)
        {
            this.Entities = entities;
        }


        /*
        //Level Creation Constructor
        public RoomScreen(Texture2D texture, SpriteFont font, int depth, string[,] arrayRoom, Dictionary<string, Texture2D> textures) : base(texture, font)
        {
            this.arrayRoom = arrayRoom;
            this.textures = textures;

            //gets the x and y dimensions of the room array
            xArray = arrayRoom.GetLength(0);
            yArray = arrayRoom.Length / xArray;
        }
        */

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            spriteBatch.Draw(Display,
                new Vector2(Game1._graphics.GraphicsDevice.Viewport.Width / 2, Game1._graphics.GraphicsDevice.Viewport.Height / 2),
                null,
                Color.White,
                0f,
                new Vector2(Display.Width / 2, Display.Height / 2),
                new Vector2(Globals.SCREEN_SCALING, Globals.SCREEN_SCALING),
                SpriteEffects.None,
                0f);

            //spriteBatch.DrawString(Font, String.Format("Max X: {0} Scale: {1} Index: {2}", (int)((Display.Width + ((Game1._graphics.GraphicsDevice.Viewport.Width - Display.Width) / 2)) - (50 * Globals.SCREEN_SCALING)), Globals.SCREEN_SCALING, Index), Vector2.Zero, Color.White);
            //spriteBatch.DrawString(Font, String.Format("Height: {0} Scale: {1} Index: {2}", Game1._graphics.GraphicsDevice.Viewport.Height, Globals.SCREEN_SCALING, Index), Vector2.Zero, Color.White);
            spriteBatch.DrawString(Font, String.Format("Salvage: {0}", Player.Instance.Consumables["Salvage"]), new Vector2(1500f, 60f), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();

            if (!EnemiesAlive())
            {
                foreach (Entity door in Entities)
                {
                    if (door.GetType() == typeof(Door))
                        door.Active = true;
                }

            }

            if (status.IsKeyDown(Keys.Escape) && !pvState.IsKeyDown(Keys.Escape))// If the player presses escape to reset the room in case the door 
                                                                                 //doesn't load.  This will be replaced by a proper pause function that takes
                                                                                 //you back to the main menu later
            {
                Player.Instance.Reset();
            }

            pvState = status;  // Set prvious state
            base.Update(gameTime);
        }

        public bool EnemiesAlive()
        {
            foreach (Entity en in Entities)
            {
                if (en is Enemy && en.Active) return true;
            }
            return false;
        }
    }
}
