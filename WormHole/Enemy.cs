using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class Enemy : Entity
    {

        public Enemy(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.Health = 100;
        }

        public override void Update(GameTime gameTime)
        {
            switch (this.GetDirection())
            {

            }
        }

        private Game1.Direction GetDirection()
        {
            throw new NotImplementedException();
        }
    }
}
