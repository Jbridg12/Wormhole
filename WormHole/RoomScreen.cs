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

        //bool pauseState = false;

        public int Depth { get; set; }
        public int Index { get; set; }  // Index in the floor array that this room is in

        protected int doorAnimationState;   // animation state for door animation later
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        protected bool Parsed;
        public string Layout { get; set; }
        public bool Open { get; set; }

        KeyboardState pvState;

        public RoomScreen(Texture2D texture, SpriteFont font, int depth) : base(texture, font)
        {
            this.Depth = depth;
            this.Entities.Add(Player.Instance);
            doorAnimationState = 0;
            this.Parsed = false;
            millisecondsPerFrame = 1500 / 6;
            timeSinceLastFrame = 0;
            Open = false;
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
        }

        public override void Update(GameTime gameTime)
        {


            KeyboardState status = Keyboard.GetState();

            if (!EnemiesAlive() && doorAnimationState < 5)
            {
                switch (Game1.CurrentState) //Only update the door when the game is running -CLoS
                {
                    case Game1.GameState.Game:
                        timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                        if (timeSinceLastFrame > millisecondsPerFrame)
                        {
                            doorAnimationState++;
                            timeSinceLastFrame = 0;
                        }
                        break;
                }

            }

            if (doorAnimationState == 5 && !Open)
            {
                Open = true;
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

            if (status.IsKeyDown(Keys.P) && !pvState.IsKeyDown(Keys.P))// If the player presses escape to reset the room in case the door 
                                                                       //doesn't load.  This will be replaced by a proper pause function that takes
                                                                       //you back to the main menu later
            {
                Mouse.SetPosition(515, 320);
                if (Game1.CurrentState == Game1.GameState.Game)
                {
                    //pauseState = true;
                    Player.Instance.Pause();
                }
                else
                {
                    //pauseState = false;
                    Player.Instance.Pause();
                }
            }

            pvState = status;  // Set previous state
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
