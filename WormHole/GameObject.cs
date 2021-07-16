// GameObject.cs
// Contributors: Josh

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class GameObject
    {
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
        public bool Active { get; set; }


        public GameObject(Rectangle position, Texture2D texture)
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
            if (this.Active)
                spriteBatch.Draw(this.Texture, this.Position, Color.White); // If called by default just draw the texture in the specific position
        }

    }
}
