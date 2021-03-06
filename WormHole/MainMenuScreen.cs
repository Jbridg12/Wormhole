// MainMenuScreen.cs
// Contributors: Josh Bridges, Chris LoSardo
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

    class MainMenuScreen : GameScreen
    {
        private Texture2D currentDisplay;
        private int selectedButton;
        private Rectangle displayLocation;


        //Button code - CLos
        private List<MenuButton> buttons;
        private MenuButton button;

        public MainMenuScreen(Dictionary<string, Texture2D> textures, SpriteFont font) : base(textures, font)
        {
            displayLocation = new Rectangle(((int)ScreenManager.Instance.Dimensions.X / 2) - 384, ((int)ScreenManager.Instance.Dimensions.Y / 2) - 384, 768, 768);
            this.currentDisplay = Displays["Initial"];

            buttons = new List<MenuButton>();

            //Creating the buttons
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 197), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 41), 394, 82), Displays["button0"]));
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 161), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 171), 322, 64), Displays["button1"]));
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 197), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 41), 394, 82), Displays["button2"]));
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 161), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 171), 322, 64), Displays["button3"]));

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "Hello!", new Vector2(480.0f, 200.0f), Color.Aquamarine);
            spriteBatch.Draw(currentDisplay, displayLocation, Color.White);


            switch (Game1.CurrentState) //-CLos
            {
                case Game1.GameState.Main:
                    spriteBatch.Draw(buttons[0].Texture, buttons[0].Position, Color.White);
                    spriteBatch.Draw(buttons[1].Texture, buttons[1].Position, Color.White);
                    break;
                case Game1.GameState.Instructions:

                    //Text Draw
                    spriteBatch.Draw(Displays["Instructions"], new Rectangle(Game1._graphics.GraphicsDevice.Viewport.Width / 2 - 384, Game1._graphics.GraphicsDevice.Viewport.Height / 2 - 192, 768, 384), Color.White);
                    spriteBatch.Draw(buttons[3].Texture, new Rectangle(600, 630, 322, 64), Color.White);
                    break;
                case Game1.GameState.Game:
                    //ScreenManager.Instance.DrawRoom(spriteBatch);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //We should add another state for they arrow keys
            //so it's WASD and arrow key controls
            KeyboardState keyStatus = Keyboard.GetState();
            MouseState mouseStatus = Mouse.GetState();

            switch (Game1.CurrentState) //-CLos
            {
                case Game1.GameState.Main:
                    currentDisplay = Displays["Initial"];
                    if (buttons[0].LeftButtonPress(mouseStatus, buttons[0].Position))
                    {
                        Game1.CurrentState = Game1.GameState.Game;
                    }

                    if (buttons[1].LeftButtonPress(mouseStatus, buttons[1].Position))
                    {
                        Game1.CurrentState = Game1.GameState.Instructions;
                    }
                    break;
                case Game1.GameState.Instructions:
                    currentDisplay = Displays["SubMenu"];
                    if (buttons[3].LeftButtonPress(mouseStatus, new Rectangle(600, 630, 322, 64)))
                    {
                        Game1.CurrentState = Game1.GameState.Main;
                    }
                    break;
                case Game1.GameState.Game:
                    Mouse.SetPosition(515, 320);

                    // This creates the updated room design using the text files to build to the room from a spritesheet
                    ScreenManager.Instance.ReadFloor("WormholeTest.txt");

                    //ScreenManager.Instance.NextFloor(25); // Enter the first (and only) room               
                    break;
            }

            //Close game


            base.Update(gameTime);
        }

    }
}
