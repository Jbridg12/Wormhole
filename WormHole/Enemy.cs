using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class Enemy : Character
    {

        public Enemy(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.Health = 100;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Health <= 0)
                this.Active = false;

            if (this.Active)
            {
                switch (this.GetDirection())
                {
                    case Game1.Direction.Up:
                        this.Y -= 2;
                        break;
                    case Game1.Direction.Down:
                        this.Y += 2;
                        break;
                    case Game1.Direction.Left:
                        this.X -= 2;
                        break;
                    case Game1.Direction.Right:
                        this.X += 2;
                        break;
                }
            }
        }
        public override void HandleCollision(Entity other)
        {
            if (this.Position.Intersects(other.Position))
            {
                if (other.GetType() == typeof(Bullet))
                {
                    this.Health -= 25;
                }
            }
        }

        private Game1.Direction GetDirection()
        {
            int verticalDistance, horizontalDistance;
            verticalDistance = this.Y - Game1.P1.Y;
            horizontalDistance = this.X - Game1.P1.Y;

            if(Math.Abs(verticalDistance) > Math.Abs(horizontalDistance))
            {
                if (verticalDistance >= 0)
                {
                    return Game1.Direction.Up;
                }
                return Game1.Direction.Down;
            }
            else
            {
                if (horizontalDistance >= 0)
                {
                    return Game1.Direction.Right;
                }
                return Game1.Direction.Left;
            }

        }
    }
}
