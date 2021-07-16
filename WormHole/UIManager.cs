// UIManager.cs
// Contributors: Josh

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    class UIManager
    {
        public ContentManager Content { private set; get; }
        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new UIManager();

                return instance;
            }
        }

        public Dictionary<string, Texture2D> Textures { get; set; }
        public List<UIElement>  Elements { get; set; }
        public SpriteFont Font { get; set; }

        public UIManager()
        {
            Textures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            this.Font = Content.Load<SpriteFont>("Base");
            Textures.Add("Full_Hull", Content.Load<Texture2D>("Full_Hull"));
            Textures.Add("Half_Hull", Content.Load<Texture2D>("Half_Hull"));
            Textures.Add("Empty_Hull", Content.Load<Texture2D>("Empty_Hull"));
            Textures.Add("Full_Shield", Content.Load<Texture2D>("Full_Shield"));
            Textures.Add("Half_Shield", Content.Load<Texture2D>("Half_Shield"));
            Textures.Add("Empty_Shield", Content.Load<Texture2D>("Empty_Shield"));


        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
