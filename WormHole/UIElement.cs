// UIElement.cs
// Contributors: Josh

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    class UIElement : GameObject
    {
        public Dictionary<string, Texture2D> Textures { get; set; }

        public UIElement(Rectangle postion, Texture2D texture) : base(postion, texture)
        {

        }
        public UIElement(Rectangle postion, Dictionary<string, Texture2D> textures) : base(postion, null)
        {
            this.Textures = textures;
        }
    }
}
