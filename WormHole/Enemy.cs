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
            this.MaxHealth = 1;
            this.CurrentHealth = 1;
            this.Speed = 100;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Switch Statement so that the enemy is only drawn on the pause screen and the game
            switch (Game1.CurrentState) // -CLoS
            {
                case Game1.GameState.Pause:
                case Game1.GameState.Game:
                    if (!this.hit)
                    {
                        base.Draw(spriteBatch);
                    }
                    else
                    {
                        if (this.Active)
                            spriteBatch.Draw(this.Texture, this.Position, Color.Red);
                    }
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Switch Statement so that the enemy is only updated in the game, preventing movement on the pause screen
            switch (Game1.CurrentState) // -CLoS
            {

                case Game1.GameState.Game:
                    float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (this.CurrentHealth <= 0)
                        this.Destroy();

                    this.hit = false;

                    if (this.Active)
                    {
                        this.ChasePlayer(time);
                        this.HandleBounds();
                    }

                    break;
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
                this.CurrentHealth--;
            }

            if (other.GetType() == typeof(Player))
            {
                this.CurrentHealth = 0;
            }
        }

        private void ChasePlayer(float deltaT)
        {
            Vector2 pos = new Vector2(this.X, this.Y);
            Vector2 playerPos = new Vector2(Player.Instance.X, Player.Instance.Y);

            Vector2 direction = playerPos - pos;
            direction.Normalize();

            pos += direction * Speed * deltaT;
            this.X = (int)pos.X;
            this.Y = (int)pos.Y;

        }

    }
}
