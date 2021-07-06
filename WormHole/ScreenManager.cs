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
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        public Dictionary<string, Texture2D> ScreenTextures { get; set; }
        public Dictionary<string, SpriteFont> ScreenFonts { get; set; }

        private Dictionary<string, GameScreen> screens;
        private RoomScreen theRoom;
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
            Dimensions = new Vector2(1920, 1080);
            screens = new Dictionary<string, GameScreen>();
            ScreenFonts = new Dictionary<string, SpriteFont>();
            ScreenTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            ScreenFonts.Add("base", Content.Load<SpriteFont>("Base"));
            ScreenTextures.Add("room", Content.Load<Texture2D>("room1"));

            Dictionary<string, Texture2D> mainMenu = new Dictionary<string, Texture2D>();
            mainMenu.Add("Initial", Content.Load<Texture2D>("menu"));
            mainMenu.Add("NewGame", Content.Load<Texture2D>("menu1"));

            screens.Add("MainMenu", new MainMenuScreen(mainMenu, ScreenFonts["base"]));
            currentScreen = screens["MainMenu"];

            NextFloor();
        }

        public void Update(GameTime time)
        {
            currentScreen.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        public void ChangeScreen(string str)       // Function to allow changing the currentscreen variable
        {
            currentScreen.Entities = EntityManager.Instance.CurrentScreenEntities;
            currentScreen = screens[str];
            EntityManager.Instance.SetCurrentEntities(currentScreen.Entities);  // also change the entities to the new list
        }

        public void NextFloor()
        {
            Random rand = new Random();
            this.theRoom = new EnemyRoom();
            PopulateAdjacent(theRoom, rand, 1);
            screens.Add("Room", theRoom);
        }

        public void PopulateAdjacent(RoomScreen room, Random rand, int depth)
        {
            if (room.Depth >= 3)
                return;

            for(int i = 0; i < 4; i++)
            {
                if (rand.Next(2) == 0)
                {
                    room.AdjacentRooms[i] = new EnemyRoom(depth);
                    PopulateAdjacent(room.AdjacentRooms[i], rand, depth+1);
                }
            }
        }
    }
}
