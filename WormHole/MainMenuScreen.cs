// MainMenuScreen.cs
// Contributors: Josh Bridges
//
// A screen type that is used for doing menu screens in the game

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;

namespace WormHole
{
    //Used to cycle through the different screens - CLos
    enum GameState
    {
        Main,
        Instructions,
        Pause,
        Game,
        Gameover
    }

    class MainMenuScreen : GameScreen
    {
        private Texture2D currentDisplay;
        private int selectedButton;
        private Rectangle displayLocation;

        //What is currently getting shown on screen
        private GameState currentState = GameState.Main;

        //Button code - CLos
        private List<MenuButton> buttons;
        private MenuButton button;

        public MainMenuScreen(Dictionary<string, Texture2D> textures, SpriteFont font) : base(textures, font)
        {
            displayLocation = new Rectangle(((int)ScreenManager.Instance.Dimensions.X / 2) - 384, ((int)ScreenManager.Instance.Dimensions.Y / 2) - 384, 768, 768);
            this.currentDisplay = Displays["Initial"];

            buttons = new List<MenuButton>();

            //Creating the buttons
            buttons.Add(button = new MenuButton(new Rectangle(315, 414, 394, 82), Displays["button0"]));
            buttons.Add(button = new MenuButton(new Rectangle(351, 544, 322, 64), Displays["button1"]));
            buttons.Add(button = new MenuButton(new Rectangle(315, 414, 394, 82), Displays["button2"]));
            buttons.Add(button = new MenuButton(new Rectangle(351, 544, 322, 64), Displays["button3"]));

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "Hello!", new Vector2(480.0f, 200.0f), Color.Aquamarine);
            spriteBatch.Draw(currentDisplay, displayLocation, Color.White);

            switch (currentState) //-CLos
            {
                case GameState.Main:
                    spriteBatch.Draw(buttons[0].Texture, buttons[0].Position, Color.White);
                    spriteBatch.Draw(buttons[1].Texture, buttons[1].Position, Color.White);
                    break;
                case GameState.Instructions:
                    spriteBatch.Draw(buttons[3].Texture, new Rectangle(542, 685, 322, 64), Color.White);
                        break;
                case GameState.Game:
                    ScreenManager.Instance.NextFloor(); // Enter the first (and only) room
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //We should add another state for they arrow keys
            //so it's WASD and arrow key controls
            KeyboardState keyStatus = Keyboard.GetState();
            MouseState mouseStatus = Mouse.GetState();

            switch (currentState) //-CLos
            {
                case GameState.Main:
                    currentDisplay = Displays["Initial"];
                    if (buttons[0].LeftButtonPress(mouseStatus, buttons[0].Position))
                    {
                        currentState = GameState.Game;
                    }

                    if (buttons[1].LeftButtonPress(mouseStatus, buttons[1].Position))
                    {
                        currentState = GameState.Instructions;
                    }
                    break;
                case GameState.Instructions:
                    currentDisplay = Displays["SubMenu"];
                    if (buttons[3].LeftButtonPress(mouseStatus, new Rectangle(542, 685, 322, 64)))
                    {
                        currentState = GameState.Main;
                    }
                    break;
                case GameState.Game:
                    ScreenManager.Instance.ChangeScreen("Room"); // Enter the first (and only) room                 
                    break;
            }
            /*
            if (keyStatus.IsKeyDown(Keys.Space))
            {
                this.currentDisplay = Displays["NewGame"];     //Highlight New game button
                selectedButton = 1;
            }
            else if (keyStatus.IsKeyDown(Keys.LeftShift))
            {
                this.currentDisplay = Displays["Initial"];      // Un-highlight NG button
                selectedButton = 0;
            }
            else if (keyStatus.IsKeyDown(Keys.Enter))
            {
                if (selectedButton == 1)
                {
                    ScreenManager.Instance.NextFloor(); // Enter the first (and only) room
                }
            }
            */
            //Close game
            

            base.Update(gameTime);
        }
    }
}
