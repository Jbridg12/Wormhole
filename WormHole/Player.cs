// Player Class
// Inherits the entity class

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace WormHole
{
    public class Player : Entity
    {
        // specific player attributes
        enum Mode { Vertical, Horizontal}
        private Mode state = Mode.Vertical;

        private KeyboardState previousState;    // For single press input control
        public Game1.Direction Looking { get; set; }

        public Player(Texture2D texture) : base(new Rectangle(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight / 2, 100, 100), texture)
        {
            Looking = Game1.Direction.Up;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState input = Keyboard.GetState();

            if(previousState != null)   // make sure there is a previous state
            {
                if (input.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))    // this only allows single presses to count as one input
                {
                    if (state == Mode.Vertical)      // Just changes the mode for now
                    {
                        state = Mode.Horizontal;
                    }
                    else
                    {
                        state = Mode.Vertical;
                    }

                }


                // Handle basic movement with WASD
                if (input.IsKeyDown(Keys.W))
                {
                    if (Looking != Game1.Direction.Up)
                        Looking = Game1.Direction.Up;

                    this.Position = new Rectangle(this.Position.X, this.Position.Y - 5, this.Position.Width, this.Position.Height);
                }
                if (input.IsKeyDown(Keys.S))
                {
                    if (Looking != Game1.Direction.Down)
                        Looking = Game1.Direction.Down;

                    this.Position = new Rectangle(this.Position.X, this.Position.Y + 5, this.Position.Width, this.Position.Height);
                }
                if (input.IsKeyDown(Keys.A))
                {
                    if (Looking != Game1.Direction.Left)
                        Looking = Game1.Direction.Left;   
                    
                    this.Position = new Rectangle(this.Position.X - 5, this.Position.Y, this.Position.Width, this.Position.Height);
                }
                if (input.IsKeyDown(Keys.D))
                {
                    if (Looking != Game1.Direction.Right)
                        Looking = Game1.Direction.Right;

                    this.Position = new Rectangle(this.Position.X + 5, this.Position.Y, this.Position.Width, this.Position.Height);
                }
            }
            
            previousState = input;  // Set prvious state
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case Mode.Vertical:                                     // Draw the sprite in mode 0
                    spriteBatch.Draw(Texture, 
                        Position,
                        new Rectangle(230, 105, 322, 160),  // get the area of the Texture
                        Color.White,
                        (float)((float)Looking * (float)(Math.PI/2)),
                        new Vector2(Position.Width, Position.Height),
                        SpriteEffects.None,
                        0);
                    break;
                case Mode.Horizontal:                                     // Draw the sprite in mode 1
                    spriteBatch.Draw(Texture,
                        Position,
                        new Rectangle(103, 300, 207, 260),  // get the arae of the Texture
                        Color.White,
                        (float)((float)Looking * (float)(Math.PI/2)),
                        new Vector2(Position.Width, Position.Height),
                        SpriteEffects.None,
                        0);
                    break;
            }
            
        }
    }
}
