// Player.cs
// Contributors: Josh Bridges
//
// Player Class
// Inherits the character class


using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public Dictionary<string, int> Consumables { get; set; }

        public Player(Texture2D texture) : base(new Rectangle(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight / 2, 100, 100), texture)
        {
            this.Consumables = InitiateConsumables();
            this.state = Mode.Vertical;
            this.shotsPerSecond = 10f;
            this.currentTime = 0f;
            this.Direction = Game1.Direction.Up;
            this.Speed = 600;
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
            this.HandleBounds();

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
        private Dictionary<string, int> InitiateConsumables()
        {
            return new Dictionary<string, int>
            {
                {"Salvage", 0 }
            };
        }

        public override void HandleCollision(Entity other)
        {
            if (other.GetType() == typeof(Door))
            {
                EntityManager.Instance.NextRoom = ((Door)other).Destination;
                switch (((Door)other).Direction)
                {
                    case Game1.Direction.Up:
                        this.X = 800; this.Y = 700;
                        break;
                    case Game1.Direction.Down:
                        this.X = 800; this.Y = 400;
                        break;
                    case Game1.Direction.Right:
                        this.X = 300; this.Y = 500;
                        break;
                    case Game1.Direction.Left:
                        this.X = 1530; this.Y = 500;
                        break;
                }
            }

            if (other.GetType() == typeof(Enemy))
            {
                this.Health -= 10;

            }
        }


        public override void HandleBounds()
        {
            if (this.X > Game1._graphics.GraphicsDevice.Viewport.Width-50)
            {
                this.X = Game1._graphics.GraphicsDevice.Viewport.Width-50;
            }
            if (this.X < 50)
            {
                this.X = 50;
            }
            if (this.Y > Game1._graphics.GraphicsDevice.Viewport.Height-50)
            {
                this.Y = Game1._graphics.GraphicsDevice.Viewport.Height-50;
            }
            if (this.Y < 50)
            {
                this.Y = 50;
            }
        }

        public void Shoot()
        {
            // create new bullet and add it to current entities
            EntityManager.Instance.AddEntity(new Bullet(this.Position, EntityManager.Instance.Textures["elec_bullet"]));
        }

        public void Reset()
        {
            this.Position = new Rectangle(Game1._graphics.PreferredBackBufferWidth / 2, Game1._graphics.PreferredBackBufferHeight / 2, 100, 100);
            this.Consumables = InitiateConsumables();
            this.state = Mode.Vertical;
            this.shotsPerSecond = 10f;
            this.currentTime = 0f;
            this.Direction = Game1.Direction.Up;
            this.Speed = 600;
        }
    }
}
