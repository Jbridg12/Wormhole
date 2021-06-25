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
        public Bullet(Rectangle position, Texture2D texture) : base(position, texture)
        {
            Looking = Game1.P1.Looking;
        }
        public override void Update(GameTime gameTime)
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

            base.Update(gameTime);
        }
    }
}
