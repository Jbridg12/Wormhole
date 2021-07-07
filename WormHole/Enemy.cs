// Enemy.cs
// Contributors: Josh Bridges

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
        private bool hit;
        public Enemy(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.hit = false;
            this.Health = 100;
            this.Speed = 100;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.hit)
            {
                base.Draw(spriteBatch);
            }
            else
            {
                if (this.Active)
                    spriteBatch.Draw(this.Texture, this.Position, Color.Red);
            }

        }

        public override void Update(GameTime gameTime)
        {
            float time = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (this.Health <= 0)
                this.Destroy();

            this.hit = false;

            if (this.Active)
            {
                this.ChasePlayer(time);
                this.HandleBounds();
            }
        }

        private void Destroy()
        {
            this.Active = false;
            EntityManager.Instance.AddEntity(new Salvage(this.X, this.Y));
        }

        public override void HandleCollision(Entity other)
        {
            if (other.GetType() == typeof(Enemy))
                return;

            this.hit = true;
            if (other.GetType() == typeof(Bullet))
            {
                this.Health -= 25;
            }

            if (other.GetType() == typeof(Player))
            {
                this.Health = 0;
            }
        }

        private void ChasePlayer(float deltaT)
        { 
            Vector2 pos = new Vector2(this.X, this.Y);
            Vector2 playerPos = new Vector2(Game1.P1.X, Game1.P1.Y);

            Vector2 direction = playerPos - pos;
            direction.Normalize();

            pos += direction * Speed * deltaT;
            this.X = (int)pos.X;
            this.Y = (int)pos.Y;

        }

    }
}
