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
        Game1.Direction Looking;
        public int Range { get; set; }
        public int DistTravelled { get; set;}

        public Bullet(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.Looking = Game1.P1.Looking;
            this.Range = 200;
            this.DistTravelled = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if (this.Active)
            {
                if(this.DistTravelled < this.Range)
                {
                    switch (Looking)
                    {
                        case Game1.Direction.Up:
                            this.Y--;
                            break;
                        case Game1.Direction.Down:
                            this.Y++;
                            break;
                        case Game1.Direction.Right:
                            this.X++;
                            break;
                        case Game1.Direction.Left:
                            this.X--;
                            break;
                    }

                    this.HandleBounds(false);

                    this.DistTravelled++;
                }
                else
                {
                    this.Active = false;
                }

                base.Update(gameTime);
            }
        }
    }
}
