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
        private Texture2D currentdisplay;
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
            this.currentdisplay = Displays["Initial"];

            buttons = new List<MenuButton>();

            //Creating the buttons
            buttons.Add(button = new MenuButton(new Rectangle(315, 414, 394, 82), Displays["button0"]));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "Hello!", new Vector2(480.0f, 200.0f), Color.Aquamarine);
            spriteBatch.Draw(currentdisplay, displayLocation, Color.White);

            switch (currentState) //-CLos
            {
                case GameState.Main:
                    spriteBatch.Draw(buttons[0].Texture, buttons[0].Position, Color.White);
                    break;
                case GameState.Instructions:
                    currentdisplay = Displays["SubMenu"];
                    break;
                case GameState.Game:
                    ScreenManager.Instance.ChangeScreen("Room"); // Enter the first (and only) room
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyStatus = Keyboard.GetState();
            MouseState mouseStatus = Mouse.GetState();

            switch (currentState) //-CLos
            {
                case GameState.Main:
                    currentdisplay = Displays["Initial"];
                    if (buttons[0].LeftButtonPress(mouseStatus, buttons[0].Position))
                    {
                        currentState = GameState.Game;
                    }
                    break;
                case GameState.Instructions:
                    currentdisplay = Displays["SubMenu"];
                    break;
                case GameState.Game:
                    ScreenManager.Instance.ChangeScreen("Room"); // Enter the first (and only) room
                    break;
            }

            if (keyStatus.IsKeyDown(Keys.Space))
            {
                this.currentdisplay = Displays["NewGame"];     //Highlight New game button
                selectedButton = 1;
            }
            else if (keyStatus.IsKeyDown(Keys.LeftShift))
            {
                this.currentdisplay = Displays["Initial"];      // Un-highlight NG button
                selectedButton = 0;
            }
            else if (keyStatus.IsKeyDown(Keys.Enter))
            {
                if (selectedButton == 1)
                    ScreenManager.Instance.ChangeScreen("Room"); // Enter the first (and only) room
            }

            //Close game
            if (keyStatus.IsKeyDown(Keys.Escape) && currentdisplay == Displays["Initial"])
            {
            }

            base.Update(gameTime);
        }
    }
}
