//Entity Base Class

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class Entity
    {
        // Attributes all entities share
        protected ContentManager content;
        public Rectangle position;
        public Texture2D texture;

        public virtual void LoadContent()
        {
            content = new ContentManager(EntityManager.Instance.Content.ServiceProvider, "Content");

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White); // If called by default just draw the texture in the specific position
        }
    }
}
