using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class Salvage : Entity
    {
        public Salvage(int X, int Y) : base(new Rectangle(X, Y, 40, 40), EntityManager.Instance.Textures["salvage"])
        {
        }

        public override void HandleCollision(Entity other)
        {
            if (this.Position.Intersects(other.Position))
            {
                if(other.GetType() == typeof(Player))
                {
                    Game1.P1.Consumables["Salvage"]+=10;
                    this.Active = false;
                }
            }
        }
    }
}
