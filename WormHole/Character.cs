// Character.cs
// Contributors: Josh Bridges

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class Character : Entity
    {
        public int Health { get; set; }
        public int Speed { get; set; }
        public Game1.Direction Direction { get; set; }

        public Character(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }
    }
}