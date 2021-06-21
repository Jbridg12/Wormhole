// The enitity manager
//
// This loads and handles all entities 

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class EntityManager
    {
        private static EntityManager instance;
        private List<Entity> entities;
        private ContentManager Content;

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManager();

                return instance;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            foreach (var item in entities)
            {
                item.LoadContent();
            }
        }
        public void Update(GameTime time)
        {
            foreach (var item in entities)
            {
                item.Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            foreach (var item in entities)
            {
                item.Draw(spriteBatch, graphics);
            }
        }
    }
}