// Entity.cs
// Contributors: Josh Bridges

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
        
        public bool Active { get; set; }

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

        public virtual void HandleCollision(Entity other)
        {
            //I dont know if you have this planned but it should be simple in theory
            //If the players bullet intersects(I think there is a method for this) with
            //the entity bounds then the en
        }

        public virtual void HandleBounds()
        {
            if(this.X > Game1._graphics.GraphicsDevice.Viewport.Width-50)
            {
                this.X = Game1._graphics.GraphicsDevice.Viewport.Width-50;
            }
            if (this.X < 50)
            {
                this.X = 50;
            }
            if (this.Y > Game1._graphics.GraphicsDevice.Viewport.Height-50)
            {
                this.Y = Game1._graphics.GraphicsDevice.Viewport.Height-50;
            }
            if (this.Y < 50)
            {
                this.Y = 50;
            }
        }
    }
}
