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
        private Texture2D texture;
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        private Rectangle position;
        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int X
        {
            get
            {
                return position.X;
            }

            set
            {
                position.X = value;
            }
        }
        public int Y
        {
            get
            {
                return position.Y;
            }

            set
            {
                position.Y = value;
            }
        }
        public Entity(int x, int y, int width, int height, Texture2D texture)
        {
            Position = new Rectangle(x, y, width, height); // create new rectangle for coordiantes
            Texture = texture;
        }

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
