// EntityManager.cs
// Contributors: Josh Bridges
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
        public Dictionary<string, Texture2D> Textures { get; set; }
        public ContentManager Content { get; private set; }
        public List<Entity> UpdatedEntities { get; set; }
        public GameScreen NextScreen { get; set; }
        public bool paused { get; set; }


        public EntityManager()
        {
            CurrentScreenEntities = new List<Entity>();
            UpdatedEntities = new List<Entity>();
            Textures = new Dictionary<string, Texture2D>();
            NextScreen = null;
            paused = false;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            Textures.Add("player", Content.Load<Texture2D>("ship_game_moc"));
            Textures.Add("elec_bullet", Content.Load<Texture2D>("elec_bullet"));
            Textures.Add("enemy", Content.Load<Texture2D>("enemy"));
            Textures.Add("salvage", Content.Load<Texture2D>("salvage"));
            Textures.Add("door", Content.Load<Texture2D>("nebula"));

            //Buttons -CLos
            Textures.Add("button0", Content.Load<Texture2D>("button0"));
            Textures.Add("button1", Content.Load<Texture2D>("button1"));
            Textures.Add("button2", Content.Load<Texture2D>("button2"));
            Textures.Add("button3", Content.Load<Texture2D>("button3"));


        }

        public void Update(GameTime time)
        {
            switch (Game1.CurrentState)
            {
                case Game1.GameState.Game:

                    if (NextScreen != null)
                        ScreenManager.Instance.UpdateScreen(NextScreen);


                    CurrentScreenEntities = new List<Entity>(this.UpdatedEntities);     // dynamically update the entites
                    UpdatedEntities.Clear();



                    for (int i = 0; i < CurrentScreenEntities.Count; i++)
                    {
                        if (CurrentScreenEntities[i].Active)
                        {

                            CurrentScreenEntities[i].Update(time);

                            for (int j = i; j < CurrentScreenEntities.Count; j++)
                            {
                                if (!CurrentScreenEntities[j].Active)
                                    continue;

                                if (CurrentScreenEntities[i].Position.Intersects(CurrentScreenEntities[j].Position))
                                {
                                    CurrentScreenEntities[i].HandleCollision(CurrentScreenEntities[j]);
                                    CurrentScreenEntities[j].HandleCollision(CurrentScreenEntities[i]);
                                }
                            }
                        }

                        if (CurrentScreenEntities[i].Active || (CurrentScreenEntities[i].GetType() == typeof(Door)))            // any active entities carry over to next Update call
                            this.AddEntity(CurrentScreenEntities[i]);

                    }
                    break;
                case Game1.GameState.Main:
                case Game1.GameState.Gameover:
                    if (NextScreen != null)
                    {
                        ScreenManager.Instance.UpdateScreen(NextScreen);
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Game1.CurrentState)
            {
                case Game1.GameState.Game:
                case Game1.GameState.Pause:
                    foreach (Entity item in CurrentScreenEntities)
                    {
                        item.Draw(spriteBatch);
                    }
                    break;
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

        public void SetScreenUpdate(GameScreen gs)
        {
            NextScreen = gs;
        }
    }
}