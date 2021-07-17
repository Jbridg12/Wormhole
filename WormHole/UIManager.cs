// UIManager.cs
// Contributors: Josh Bridges

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
            this.Textures = new Dictionary<string, Texture2D>();
            this.Elements = new List<UIElement>();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            this.Font = Content.Load<SpriteFont>("Base");
            Elements.Add(new HealthDisplay(Content.Load<Texture2D>("Health_Spritesheet"), Font));
        }

        public void Update(GameTime gameTime)
        {
            foreach(UIElement element in Elements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Game1.CurrentState == Game1.GameState.Game)
            {
                foreach (UIElement element in Elements)
                {
                    element.Draw(spriteBatch);
                }
            }
        }
    }
}
