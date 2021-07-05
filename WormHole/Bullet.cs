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
        public int DistTravelled { get; set;}

        public Bullet(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.Direction = Game1.P1.Direction;
            this.Range = 500;
            this.DistTravelled = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if (this.Active)
            {
                if (this.DistTravelled < this.Range)
                {
                    switch (this.Direction)
                    {
                        case Game1.Direction.Up:
                            this.Y-=7;
                            break;
                        case Game1.Direction.Down:
                            this.Y+=7;
                            break;
                        case Game1.Direction.Right:
                            this.X+=7;
                            break;
                        case Game1.Direction.Left:
                            this.X-=7;
                            break;
                    }

                    this.HandleBounds(false);

                    this.DistTravelled+=5;
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
    }
}
