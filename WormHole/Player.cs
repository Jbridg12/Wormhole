﻿// Player Class
// Inherits the entity class

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace WormHole
{
    public class Player : Character
    {
        // specific player attributes
        enum Mode { Vertical, Horizontal}
        private Mode state;

        private float shotsPerSecond;
        private float currentTime;

        private KeyboardState previousState;    // For single press input control
        

        public Player(Texture2D texture) : base(new Rectangle(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight / 2, 100, 100), texture)
        {
            this.state = Mode.Vertical;
            this.shotsPerSecond = 3f;
            this.currentTime = 0f;
            this.Direction = Game1.Direction.Up;
            this.Speed = 200;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState input = Keyboard.GetState();

            float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentTime += deltaT;

            if(previousState != null)   // make sure there is a previous state
            {
                if (input.IsKeyDown(Keys.LeftShift) && !previousState.IsKeyDown(Keys.LeftShift))    // this only allows single presses to count as one input
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
                    if (Direction != Game1.Direction.Up)
                        Direction = Game1.Direction.Up;

                    this.Y -= (int)(this.Speed * deltaT);
                }
                if (input.IsKeyDown(Keys.S))
                {
                    if (Direction != Game1.Direction.Down)
                        Direction = Game1.Direction.Down;

                    this.Y += (int)(this.Speed * deltaT);
                }
                if (input.IsKeyDown(Keys.A))
                {
                    if (Direction != Game1.Direction.Left)
                        Direction = Game1.Direction.Left;

                    this.X -= (int)(this.Speed * deltaT);
                }
                if (input.IsKeyDown(Keys.D))
                {
                    if (Direction != Game1.Direction.Right)
                        Direction = Game1.Direction.Right;

                    this.X += (int)(this.Speed * deltaT);
                }
                if (input.IsKeyDown(Keys.Space))
                {
                    if(currentTime >= (1/this.shotsPerSecond))  // shooting depends on the inverse of the amount of shots per second
                    {
                        currentTime = 0f;
                        this.Shoot();
                    }
                }

            }
            this.HandleBounds(true);

            previousState = input;  // Set prvious state
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case Mode.Vertical:                                     // Draw the sprite in mode 0
                    spriteBatch.Draw(Texture, 
                        Position,
                        new Rectangle(230, 105, 322, 160),              // get the area of the Texture
                        Color.White,
                        (float)((float)Direction * (float)(Math.PI/2)),   // using north as origin rotate in radians 
                        new Vector2(Position.Width, Position.Height),   // keep image centered while rotating
                        SpriteEffects.None,
                        0);
                    break;
                case Mode.Horizontal:                                     // Draw the sprite in mode 1
                    spriteBatch.Draw(Texture,
                        Position,
                        new Rectangle(103, 300, 207, 260),                // get the arae of the Texture
                        Color.White,
                        (float)((float)Direction * (float)(Math.PI/2)),     // using north as origin rotate in radians 
                        new Vector2(Position.Width, Position.Height),     // keep image centered while rotating
                        SpriteEffects.None,
                        0);
                    break;
            }
            
        }

        public void Shoot()
        {
            // create new bullet and add it to current entities
            EntityManager.Instance.AddEntity(new Bullet(this.Position, EntityManager.Instance.Textures["elec_bullet"]));
        }
    }
}
