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
        int mode = 0;
        KeyboardState previousState;    // For single press input control

        public Player()
        {
            this.position = new Rectangle(Game1._graphics.PreferredBackBufferWidth/2, Game1._graphics.PreferredBackBufferHeight/2, 100, 100);   // Start center screen
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.texture = content.Load<Texture2D>("ship_game_moc");    // The placeholder player texture
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState input = Keyboard.GetState();

            if(previousState != null)   // make sure there is a previous state
            {
                if (input.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))    // this only allows single presses to count as one input
                {
                    if (mode == 0)      // Just changes the mode for now
                    {
                        mode = 1;
                    }
                    else
                    {
                        mode = 0;
                    }

                }


                // Handle basic movement with WASD
                if (input.IsKeyDown(Keys.W))
                {
                    this.position = new Rectangle(this.position.X, this.position.Y - 5, this.position.Width, this.position.Height);
                }
                if (input.IsKeyDown(Keys.S))
                {
                    this.position = new Rectangle(this.position.X, this.position.Y + 5, this.position.Width, this.position.Height);
                }
                if (input.IsKeyDown(Keys.A))
                {
                    this.position = new Rectangle(this.position.X - 5, this.position.Y, this.position.Width, this.position.Height);
                }
                if (input.IsKeyDown(Keys.D))
                {
                    this.position = new Rectangle(this.position.X + 5, this.position.Y, this.position.Width, this.position.Height);
                }
            }
            
            previousState = input;  // Set prvious state
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (mode)
            {
                case 0:                                     // Draw the sprite in mode 0
                    spriteBatch.Draw(texture, 
                        position,
                        new Rectangle(230, 105, 322, 160),  // get the area of the texture
                        Color.White,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);
                    break;
                case 1:                                     // Draw the sprite in mode 1
                    spriteBatch.Draw(texture,
                        position,
                        new Rectangle(103, 300, 207, 260),  // get the arae of the texture
                        Color.White,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);
                    break;
            }
            
        }
    }
}
