// ScreenManager.cs
// Contributors: Josh Bridges
//
// A manager to wrap all of the code regarding the screens updating and 
// loading into the Game1.cs functions.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class ScreenManager
    {
        private static ScreenManager instance;

        //Load Variables - CLos
        StreamReader input = null;
        string loadBoard = "";
        List<RoomGen> rooms = new List<RoomGen>();
        private string line = null;
        private string[,] arrayRoom;

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
            Dimensions = new Vector2(1280, 720);
            screens = new Dictionary<string, GameScreen>();
            ScreenFonts = new Dictionary<string, SpriteFont>();
            ScreenTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            ScreenFonts.Add("base", Content.Load<SpriteFont>("Base"));
            ScreenTextures.Add("room", Content.Load<Texture2D>("room1"));
            ScreenTextures.Add("room_tiles", Content.Load<Texture2D>("doors_spritesheet"));

            Dictionary<string, Texture2D> mainMenu = new Dictionary<string, Texture2D>();
            mainMenu.Add("Initial", Content.Load<Texture2D>("menu"));
            mainMenu.Add("NewGame", Content.Load<Texture2D>("menu1"));
            mainMenu.Add("SubMenu", Content.Load<Texture2D>("instructions")); //-CLos

            //Button textures here because I was having trouble making a unique element for them
            mainMenu.Add("button0", Content.Load<Texture2D>("button0"));
            mainMenu.Add("button1", Content.Load<Texture2D>("button1"));
            mainMenu.Add("button2", Content.Load<Texture2D>("button2"));
            mainMenu.Add("button3", Content.Load<Texture2D>("button3"));

            //Adding room tiles - CLos
            mainMenu.Add("*", Content.Load<Texture2D>("wall"));
            mainMenu.Add("B", Content.Load<Texture2D>("Barrier"));
            mainMenu.Add("C", Content.Load<Texture2D>("corner"));
            mainMenu.Add("D", Content.Load<Texture2D>("nebula"));

            screens.Add("MainMenu", new MainMenuScreen(mainMenu, ScreenFonts["base"]));

            // Set Globals for room scaling
            Globals.SCREEN_SCALING = (float)Game1._graphics.GraphicsDevice.Viewport.Height / ScreenTextures["room"].Height;
            Globals.XMAX = (int)(((ScreenTextures["room"].Width*Globals.SCREEN_SCALING) + ((Game1._graphics.GraphicsDevice.Viewport.Width - (ScreenTextures["room"].Width*Globals.SCREEN_SCALING)) / 2)) + (50 * Globals.SCREEN_SCALING));
            Globals.XMIN = (int)((((Game1._graphics.GraphicsDevice.Viewport.Width - (ScreenTextures["room"].Width*Globals.SCREEN_SCALING)) / 2)) - (50 * Globals.SCREEN_SCALING));
            Globals.ROOM_TEXTURE_LEFT = (int)((Game1._graphics.GraphicsDevice.Viewport.Width - ((ScreenTextures["room"].Width)*Globals.SCREEN_SCALING)) / 2);
            Globals.ROOM_TEXTURE_RIGHT = (int)(((ScreenTextures["room"].Width*Globals.SCREEN_SCALING) + ((Game1._graphics.GraphicsDevice.Viewport.Width - (ScreenTextures["room"].Width*Globals.SCREEN_SCALING)) / 2)));
            CurrentScreen = screens["MainMenu"];
            //screens.Add("Room", new RoomScreen(ScreenTextures["room"], ScreenFonts["base"], new List<Entity> { new Enemy(new Rectangle(20, 20, 100, 100), EntityManager.Instance.Textures["enemy"]) }));

            loadBoard = "test Floor.txt";
            GenerateFloor("..\\..\\..\\" + loadBoard, mainMenu);

        }

        public void Update(GameTime time)
        {
            CurrentScreen.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
        public void ReadFloor(string textFile)
        {
            //We should change up the look by like changing between 3 or 4 different
            //arts for the background, it's kind of confusing 
            Random rand = new Random();
            var reader = File.OpenText(String.Format(@"{0}", textFile));
            var floorSize = File.ReadLines(String.Format(@"{0}", textFile)).Count();

            Floor = new RoomScreen[floorSize];

            string format = reader.ReadLine();
            for(int i = 0; i < floorSize; i++)
            {
                string[] parts = format.Split(' ');
                if (Int32.Parse(parts[0]) == 1)
                {
                    Floor[i] = new EnemyRoom(parts[1]);
                }
            }

            NextRoom = Floor[floorSize / 2];

            SetupDoors(floorSize);

            EntityManager.Instance.NextScreen = NextRoom;
            if (!screens.ContainsKey("Room"))
            {
                screens.Add("Room", NextRoom);
            }
            else
            {
                screens["Room"] = NextRoom;
            }
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
            EntityManager.Instance.NextScreen = NextRoom;
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
                    //room.Entities.Add(new Door(new Rectangle(0, 0, 400, 200), (Game1.Direction)i, Floor[nextIndex]));
                }
                else if (rand.Next(3) != 0)
                {
                    Floor[nextIndex] = new EnemyRoom(depth, nextIndex);
                    //room.Entities.Add(new Door(new Rectangle(0, 0, 400, 200), (Game1.Direction)i, Floor[nextIndex]));
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
        public void ChangeScreen(string str)       // Function to allow changing the CurrentScreen variable
        {
            EntityManager.Instance.NextScreen = screens[str];
        }

        public void ChangeScreen(GameScreen rs)
        {
            EntityManager.Instance.NextScreen = rs;
        }

        public void UpdateScreen(GameScreen gs)
        {
            CurrentScreen.Entities = EntityManager.Instance.CurrentScreenEntities;
            CurrentScreen = gs;

            EntityManager.Instance.SetCurrentEntities(CurrentScreen.Entities);
            EntityManager.Instance.NextScreen = null;
        }
       
        //Generate Floor from text file
        private void GenerateFloor(string loadBoard, Dictionary<string, Texture2D> roomTiles)
        {
            //room name that's used as the key in the rooms dictionary
            string roomName;
            //The actual layout
            string roomLayout;
            try
            {
                //Takes the loadboard file
                input = new StreamReader(loadBoard);

                //Loops through every line in the test Floor text file
                while ((line = input.ReadLine()) != null)
                {
                    //the data array stores the information in the text file by splitting it where the { is in the text file
                    string[] data = line.Split('{');

                    roomName = data[0].Trim(); ;
                    roomLayout = data[1];
                    //If you put a break point on line 253 and look at the locals tab in the output window below
                    //you can see how the line of the text file is split up and stored in the data array

                    data = roomLayout.Split(',');
                    //Now data is reused for the actual room data stored in the roomLayout variable
                    //if you put a breakpoint on line 260 you can see how data stores the actual room text that
                    //will be used to draw the room in the room class

                    //initializes the 2d array that stores the room data using the length of the data array
                    //In this case it creates a 15x15 array
                    arrayRoom = new string[data.Length, data[0].Length];

                    for (int i = 0; i < data.Length; i++)
                    {
                        for (int j = 0; j < data[0].Length; j++)
                        {
                            //Sets each index of the testRoom array to a letter in the data array
                            arrayRoom[i, j] = data[i][j].ToString();
                        }
                    }

                    //Creates a room object using the testRoom 2d array, the roomName, and the textures Dictionary
                    //and then adds it the rooms dictionary
                    rooms.Add(new RoomGen(arrayRoom, roomName, roomTiles));
                    //Then it loops and starts over with the next line in the test Floor text file
                }


            }
            //Error stuff in case something goes wrong with the text file
            catch (Exception e)
            {
                Console.WriteLine("\nThere was an error loading the level or that level file does not exist:\n\n " + e.Message);
            }
            finally
            {
                if (input != null)
                {
                    input.Close();

                }
            }
        }

        /*
        public void DrawRoom(SpriteBatch spriteBatch)
        {
            //Code to determine which room to load

            rooms[4].Draw(spriteBatch);

        }
        */
    }
}
