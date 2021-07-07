// Door.cs
// Contributors: Josh Bridges
//
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace WormHole
{
    class Door : Entity
    {
        public Game1.Direction Direction { get; private set; }
        public RoomScreen Destination { get; private set; }
        public Door(Rectangle rectangle, Game1.Direction direction, RoomScreen room) : base(rectangle, EntityManager.Instance.Textures["door"])
        {
            this.Direction = direction;
            this.Active = false;
            switch (direction)
            {
                case Game1.Direction.Up:
                    this.Position = new Rectangle(760, 0, 200, 100);
                    break;
                case Game1.Direction.Right:
                    this.Position = new Rectangle(1820, 340, 200, 100);
                    break;
                case Game1.Direction.Down:
                    this.Position = new Rectangle(760, 980, 200, 100);
                    break;
                case Game1.Direction.Left:
                    this.Position = new Rectangle(0, 340, 200, 100);
                    break;
            }
            this.Destination = room;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                        Position,
                        null,              // get the area of the Texture
                        Color.White,
                        (float)((float)Direction * (float)(Math.PI / 2)),   // using north as origin rotate in radians 
                        new Vector2(Position.Width, Position.Height),   // keep image centered while rotating
                        SpriteEffects.None,
                        0);
        }
    }
}