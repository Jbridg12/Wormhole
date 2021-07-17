// Bullet.cs
// Contributors: Josh Bridges

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class Bullet : Entity
    {
        private Game1.Direction Direction;
        public int Range { get; set; }
        private int speed;
        public int DistTravelled { get; set;}

        public Bullet(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.speed = 900;
            this.Direction = Player.Instance.Direction;
            this.Range = 500;
            this.DistTravelled = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if (this.Active)
            {
                float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (this.DistTravelled < this.Range)
                {
                    switch (this.Direction)
                    {
                        case Game1.Direction.Up:
                            this.Y-= (int)(this.speed * deltaT);
                            break;
                        case Game1.Direction.Down:
                            this.Y+= (int)(this.speed * deltaT);
                            break;
                        case Game1.Direction.Right:
                            this.X+= (int)(this.speed * deltaT);
                            break;
                        case Game1.Direction.Left:
                            this.X-= (int)(this.speed * deltaT);
                            break;
                    }

                    this.HandleBounds();

                    this.DistTravelled+= (int)(this.speed * deltaT);
                }
                else
                {
                    this.Active = false;
                }

                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture,
                         this.Position,
                         null,              // get the area of the Texture
                         Color.White,
                         (float)(((float)this.Direction * (float)(Math.PI / 2)) - (Math.PI/2)),   // using north as origin rotate in radians 
                         new Vector2(Position.Width, Position.Height),   // keep image centered while rotating
                         SpriteEffects.None,
                         0);
        }
        public override void HandleBounds()
        {
            if (this.X > Game1._graphics.GraphicsDevice.Viewport.Width || this.Y < 0 || this.X < 0 || this.Y > Game1._graphics.GraphicsDevice.Viewport.Height)
                this.Active = false;

        }

        public override void HandleCollision(Entity other)
        {
            if (other.GetType() == typeof(Enemy))
            {
                this.Active = false;
            }
        }
        
    }
}
