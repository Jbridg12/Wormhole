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
        public bool Active;
        public Texture2D Texture { get; set; }

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
        public Entity(Rectangle position, Texture2D texture)
        {
            this.Position = position;
            this.Texture = texture;
            this.Active = true;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.position, Color.White); // If called by default just draw the texture in the specific position
        }

        public void HandleBounds(bool wrap)
        {
            if(this.X > Game1._graphics.GraphicsDevice.Viewport.Width)
            {
                if (wrap)
                {
                    this.X = 0;
                    return;
                }

                this.Active = false;
            }
            if (this.X < 0)
            {
                if (wrap)
                {
                    this.X = Game1._graphics.GraphicsDevice.Viewport.Width;
                    return;
                }

                this.Active = false;
            }
            if (this.Y > Game1._graphics.GraphicsDevice.Viewport.Height)
            {
                if (wrap)
                {
                    this.Y = 0;
                    return;
                }

                this.Active = false;
            }
            if (this.Y < 0)
            {
                if (wrap)
                {
                    this.Y = Game1._graphics.GraphicsDevice.Viewport.Height;
                    return;
                }

                this.Active = false;
            }
        }
    }
}
