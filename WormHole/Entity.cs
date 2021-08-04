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
    public class Entity : GameObject
    {
        // Attributes all entities share



        public Entity(Rectangle position, Texture2D texture) : base(position, texture)
        {
        }

        public virtual void HandleCollision(Entity other)
        {
            //I dont know if you have this planned but it should be simple in theory
            //If the players bullet intersects(I think there is a method for this) with
            //the entity bounds then the en
        }

        public virtual void HandleBounds()
        {

            if (ScreenManager.Instance.CurrentScreen.GetType().IsSubclassOf(typeof(RoomScreen)))
            {

                if (this.X > Globals.XMAX)
                {
                    this.X = Globals.XMAX;
                }
                if (this.X < Globals.XMIN)
                {
                    this.X = Globals.XMIN;
                }
                if (this.Y > Game1._graphics.GraphicsDevice.Viewport.Height - 50)
                {
                    this.Y = Game1._graphics.GraphicsDevice.Viewport.Height - 50;
                }
                if (this.Y < 50)
                {
                    this.Y = 50;
                }
            }
        }
    }
}
