// The enitity manager
//
// This loads and handles all Entities 

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
        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManager();

                return instance;
            }
        }

        public List<Entity> CurrentScreenEntities { get; private set; } // list of all Entities in the current room that need to call Update/Draw
        public Dictionary<string, Texture2D> Textures {get; set; }
        public ContentManager Content { get; private set; }
        public List<Entity> UpdatedEntities { get; set; }

        public EntityManager()
        {
            CurrentScreenEntities = new List<Entity>();
            UpdatedEntities = new List<Entity>();
            Textures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            Textures.Add("player", Content.Load<Texture2D>("ship_game_moc"));
            Textures.Add("elec_bullet", Content.Load<Texture2D>("elec_bullet"));
            Textures.Add("enemy", Content.Load<Texture2D>("enemy"));
            Game1.P1 = new Player(Textures["player"]);
        }

        public void Update(GameTime time)
        {
            CurrentScreenEntities = new List<Entity>(this.UpdatedEntities);     // dynamically update the entites
            UpdatedEntities.Clear();

            for (int i = 0; i < CurrentScreenEntities.Count; i++)
            {
                CurrentScreenEntities[i].Update(time);

                for (int j = i; j < CurrentScreenEntities.Count; j++)
                    CurrentScreenEntities[i].HandleCollision(CurrentScreenEntities[j]); // short term solution, will implment quadtree collision soon

                if(CurrentScreenEntities[i].Active)             // any active entities carry over to next Update call
                    this.AddEntity(CurrentScreenEntities[i]);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity item in CurrentScreenEntities)
            {
                item.Draw(spriteBatch);
            }

        }

        public void AddEntity(Entity newEn)
        {
            UpdatedEntities.Add(newEn);     // add entity to current list
        }

        public void SetCurrentEntities(List<Entity> newEntities)
        {
            UpdatedEntities = newEntities;    // When screen changes it switches the current Entities with new ones
        }
    }
}