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

        private List<Entity> entities;              // The list of all entity types that need to load content
        private List<Entity> currentScreenEntities; // list of all entities in the current room that need to call Update/Draw

        public ContentManager Content;

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManager(Game1.player1);

                return instance;
            }
        }

        public EntityManager(Player player)
        {
            entities = new List<Entity>();
            currentScreenEntities = new List<Entity>();

            entities.Add(player);       // Add the player to the list by default
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
            foreach (var item in currentScreenEntities)
            {
                item.Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in currentScreenEntities)
            {
                item.Draw(spriteBatch);
            }
        }

        public void SetCurrentEntities(List<Entity> newEntities)
        {
            currentScreenEntities = newEntities;    // When screen changes it switches the current entities with new ones
        }
    }
}