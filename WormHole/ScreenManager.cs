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
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        public Dictionary<string, Texture2D> ScreenTextures { get; set; }
        public Dictionary<string, SpriteFont> ScreenFonts { get; set; }

        private Dictionary<string, GameScreen> screens;
        public Dictionary<string, GameScreen> Screens
        {
            get
            {
                return screens;
            }
        }
        public RoomScreen NextRoom { get; private set; }
        public GameScreen CurrentScreen { get; private set; }
        public RoomScreen[] Floor { get; private set; }

        public ScreenManager()
        {
            Dimensions = new Vector2(1920, 1080);
            //Dimensions = new Vector2(1024, 768);
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
            mainMenu.Add("SubMenu", Content.Load<Texture2D>("instructions")); //-CLos

            //Button textures here because I was having trouble making a unique element for them
            mainMenu.Add("button0", Content.Load<Texture2D>("button0"));
            mainMenu.Add("button1", Content.Load<Texture2D>("button1"));
            mainMenu.Add("button2", Content.Load<Texture2D>("button2"));
            mainMenu.Add("button3", Content.Load<Texture2D>("button3"));

            screens.Add("MainMenu", new MainMenuScreen(mainMenu, ScreenFonts["base"]));

            // Set Globals for room scaling
            Globals.SCREEN_SCALING = (float)Game1._graphics.GraphicsDevice.Viewport.Height / ScreenTextures["room"].Height;
            Globals.XMAX = (int)((ScreenTextures["room"].Width + ((Game1._graphics.GraphicsDevice.Viewport.Width - ScreenTextures["room"].Width) / 2)) + (50 * Globals.SCREEN_SCALING));
            Globals.XMIN = (int)((((Game1._graphics.GraphicsDevice.Viewport.Width - ScreenTextures["room"].Width) / 2)) - (50 * Globals.SCREEN_SCALING));

            CurrentScreen = screens["MainMenu"];
            //screens.Add("Room", new RoomScreen(ScreenTextures["room"], ScreenFonts["base"], new List<Entity> { new Enemy(new Rectangle(20, 20, 100, 100), EntityManager.Instance.Textures["enemy"]) }));

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
            EntityManager.Instance.NextRoom = null;

            CurrentScreen = rs;
            EntityManager.Instance.SetCurrentEntities(CurrentScreen.Entities);
        }

        public void NextFloor(int floorSize)
        {
            //We should change up the look by like changing between 3 or 4 different
            //arts for the background, it's kind of confusing 
            Random rand = new Random();
            Floor = new RoomScreen[floorSize];

            NextRoom = new EnemyRoom(0, floorSize / 2);
            Floor[floorSize / 2] = NextRoom;
            PopulateAdjacent(floorSize, NextRoom, rand, 1, 0, floorSize / 2);
            SetupDoors(floorSize);

            EntityManager.Instance.NextRoom = NextRoom;
            if (!screens.ContainsKey("Room"))
            {
                screens.Add("Room", NextRoom);
            }
            else
            {
                screens["Room"] = NextRoom;
            }
        }

        public void PopulateAdjacent(int floorSize, RoomScreen room, Random rand, int depth, int direction, int index)
        {
            int nextIndex;

            if (depth > 3)
                return;

            for (int i = 0; i < 4; i++)
            {
                nextIndex = 0;
                switch (i)
                {
                    case 0:
                        nextIndex = index - (int)Math.Sqrt(floorSize);
                        
                        break;
                    case 1:
                        nextIndex = index + 1;
                        break;
                    case 2:
                        nextIndex = index + (int)Math.Sqrt(floorSize);
                        break;
                    case 3:
                        nextIndex = index - 1;
                        break;
                }

                if (nextIndex >= floorSize || nextIndex < 0)
                    continue;

                if (Floor[nextIndex] != null)
                {
                    room.Entities.Add(new Door(new Rectangle(0, 0, 400, 200), (Game1.Direction)i, Floor[nextIndex]));
                }
                else if (rand.Next(3) != 0)
                {
                    Floor[nextIndex] = new EnemyRoom(depth, nextIndex);
                    room.Entities.Add(new Door(new Rectangle(0, 0, 400, 200), (Game1.Direction)i, Floor[nextIndex]));
                    PopulateAdjacent(floorSize, Floor[nextIndex], rand, depth + 1, i, nextIndex);
                }
            }

        }
        public void SetupDoors(int floorSize)
        {
            int[] edges = new int[4];

            for (int i = 0; i < floorSize; i++)
            {
                if (Floor[i] == null)
                    continue;

                edges[0] = i - (int)Math.Sqrt(floorSize);
                edges[1] = i + 1;
                edges[2] = i + (int)Math.Sqrt(floorSize);
                edges[3] = i - 1;

                for (int index = 0; index < edges.Length; index++)
                {
                    if (edges[index] >= floorSize || edges[index] < 0)
                        continue;

                    if (Floor[edges[index]] != null)
                    {
                        Floor[i].Entities.Add(new Door(new Rectangle(0, 0, 400, 200), (Game1.Direction)index, Floor[edges[index]]));
                    }
                }
            }
        }

    }
}
