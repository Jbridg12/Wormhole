// ScreenManager.cs
// Contributors: Josh Bridges
//
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
        public RoomScreen CurrentRoom { get; private set; }
        public GameScreen CurrentScreen { get; private set; }

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
            ScreenTextures.Add("blueroom", Content.Load<Texture2D>("bluescreen"));

            Dictionary<string, Texture2D> mainMenu = new Dictionary<string, Texture2D>();
            mainMenu.Add("Initial", Content.Load<Texture2D>("menu"));
            mainMenu.Add("NewGame", Content.Load<Texture2D>("menu1"));

            screens.Add("MainMenu", new MainMenuScreen(mainMenu, ScreenFonts["base"]));
            CurrentScreen = screens["MainMenu"];
        }

        public void Update(GameTime time)
        {
            CurrentScreen.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }

        public void ChangeScreen(string str)       // Function to allow changing the CurrentScreen variable
        {
            CurrentScreen.Entities = EntityManager.Instance.CurrentScreenEntities;  // store entities status

            CurrentScreen = screens[str];
            EntityManager.Instance.SetCurrentEntities(CurrentScreen.Entities);  // also change the entities to the new list
        }

        public void ChangeScreen(RoomScreen rs) 
        {
            CurrentScreen.Entities = EntityManager.Instance.CurrentScreenEntities;  

            CurrentScreen = rs;
            EntityManager.Instance.SetCurrentEntities(CurrentScreen.Entities);  
        }

        public void NextFloor()
        {
            Random rand = new Random();
            CurrentRoom = new EnemyRoom();
            PopulateAdjacent(CurrentRoom, rand, 1);
            if (!screens.ContainsKey("Room"))
            {
                screens.Add("Room", CurrentRoom);
            }
            else
            {
                screens["Room"] = CurrentRoom;
            }
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
                    room.Entities.Add(new Door(new Rectangle(0, 0, 400, 200), (Game1.Direction)i, room.AdjacentRooms[i]));
                }
            }

        }
    }
}
