// UIEntity.cs
// Contributors: Josh

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace WormHole
{
    class HealthDisplay : UIElement
    {
        public HealthDisplay(Texture2D texture) : base(new Rectangle(), texture)
        {
            this.X = 0;
            this.Y = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
