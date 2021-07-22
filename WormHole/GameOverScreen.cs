// GameOverScreen.cs
// Contributors: Josh Bridges, Deen Grey
//
// A screen type that is used for doing menu screens in the game, converted into the game over screen

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

    class GameOverScreen : GameScreen
    {
        private Texture2D currentDisplay;
        private int selectedButton;
        private Rectangle displayLocation;
        private SpriteFont font;


        //Button code - CLos
        private List<MenuButton> buttons;
        private MenuButton button;

        public GameOverScreen(Dictionary<string, Texture2D> textures, SpriteFont font) : base(textures, font)
        {
            displayLocation = new Rectangle(((int)ScreenManager.Instance.Dimensions.X / 2) - 384, ((int)ScreenManager.Instance.Dimensions.Y / 2) - 384, 768, 768);
            this.currentDisplay = Displays["Initial"];

            buttons = new List<MenuButton>();
            this.font = font;
            //Creating the buttons
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 197), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 41), 394, 82), Displays["button0"]));
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 161), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 171), 322, 64), Displays["button1"]));
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 197), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 41), 394, 82), Displays["button2"]));
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 161), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 171), 322, 64), Displays["button3"]));
            //buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 197), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 171), 322, 64), Displays["button4"]));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(currentDisplay, displayLocation, Color.White);
            spriteBatch.DrawString(font, "Game Over", new Vector2(500, 400), Color.White);

            switch (Game1.CurrentState) //-CLos
            {
                case Game1.GameState.Gameover:
                    spriteBatch.Draw(buttons[3].Texture, buttons[3].Position, Color.White);
                    //spriteBatch.Draw(buttons[4].Texture, buttons[4].Position, Color.White);
                    break;
                case Game1.GameState.Main:
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
                case Game1.GameState.Gameover:
                    currentDisplay = Displays["Initial"];
                    if (buttons[3].LeftButtonPress(mouseStatus, buttons[3].Position))
                    {
                        Game1.CurrentState = Game1.GameState.Main;
                    }
                    //if (buttons[4].LeftButtonPress(mouseStatus, buttons[4].Position))
                    //{
                    //    Environment.Exit(0);
                    //}
                    break;
                case Game1.GameState.Main:
                    Game1.CurrentState = Game1.GameState.Main;
                    ScreenManager.Instance.ChangeScreen("MainMenu");
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