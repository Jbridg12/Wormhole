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
    class UIEntity : GameObject
    {
        public UIEntity(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }
    }
}
