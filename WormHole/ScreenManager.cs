// A manager to wrap all of the code regarding the screens updating and 
// loading into the Game1.cs functions.

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        private List<GameScreen> screens;
        private GameScreen currentScreen;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                
                return instance;
            }
        }

        public ScreenManager()
        {
            dimensions = new Vector2(1920, 1080);
            screens = new List<GameScreen>();
            screens.Add(new MainMenuScreen());
            screens.Add(new RoomScreen());
            currentScreen = screens[0];
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            foreach(var item in screens)
            {
                item.LoadContent();
            }
        }

        public void Update(GameTime time)
        {
            currentScreen.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        public void ChangeScreen(int screenIndex)       // Function to allow changing the currentscreen variable
        {
            currentScreen = screens[screenIndex];
            EntityManager.Instance.SetCurrentEntities(currentScreen.entities);  // also change the entities to the new list
        }
    }
}
