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

        public List<Entity> CurrentScreenEntities { get; set; } // list of all Entities in the current room that need to call Update/Draw
        public Dictionary<string, Texture2D> Textures {get; set; }
        public ContentManager Content { get; private set; }



        public EntityManager()
        {
            CurrentScreenEntities = new List<Entity>();
            Textures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            Textures.Add("player", Content.Load<Texture2D>("ship_game_moc"));
            Game1.P1 = new Player(Textures["player"]);
        }
        public void Update(GameTime time)
        {
            foreach (var item in CurrentScreenEntities)
            {
                item.Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in CurrentScreenEntities)
            {
                item.Draw(spriteBatch);
            }
        }

        public void SetCurrentEntities(List<Entity> newEntities)
        {
            CurrentScreenEntities = newEntities;    // When screen changes it switches the current Entities with new ones
        }
    }
}