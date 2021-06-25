using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    class UIManger
    {
        private static UIManger instance;
        public static UIManger Instance
        {
            get
            {
                if (instance == null)
                    instance = new UIManger();

                return instance;
            }
        }
        public Dictionary<string, Texture2D> Textures { get; set; }
        public ContentManager Content { get; private set; }
        public List<UIElement> elements { get; set; }

        public UIManger()
        {
            Textures = new Dictionary<string, Texture2D>();
        }
        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");

        }
        public void Update(GameTime time)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
