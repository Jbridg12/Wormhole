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
            this.Speed = 100;
        }

        public override void Update(GameTime gameTime)
        {
            float time = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (this.Health <= 0)
                this.Active = false;

            if (this.Active)
            {
                this.ChasePlayer(gameTime);
                this.HandleBounds(true);
            }
        }
        public override void HandleCollision(Entity other)
        {
            if (this.Position.Intersects(other.Position))
            {
                if (other.GetType() == typeof(Bullet))
                {
                    this.Health -= 25;
                    other.Active = false;
                }

                if (other.GetType() == typeof(Player))
                {
                    this.Active = false;
                    ((Player)other).Health -= 10;

                }
            }
        }

        private void ChasePlayer(GameTime gameTime)
        { 
            Vector2 pos = new Vector2(this.X, this.Y);
            Vector2 playerPos = new Vector2(Game1.P1.X, Game1.P1.Y);

            Vector2 direction = playerPos - pos;
            direction.Normalize();

            pos += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.X = (int)pos.X;
            this.Y = (int)pos.Y;

        }
    }
}
