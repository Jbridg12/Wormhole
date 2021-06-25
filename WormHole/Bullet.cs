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
            base.Update(gameTime);
        }
    }
}
