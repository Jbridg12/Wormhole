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

    class PauseScreen : GameScreen
    {
        private Texture2D currentDisplay;
        private int selectedButton;
        private Rectangle displayLocation;

        private SpriteFont font;


        //Button code - CLos
        private List<MenuButton> buttons;
        private MenuButton button;

        public PauseScreen(Dictionary<string, Texture2D> textures, SpriteFont font) : base(textures, font)
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
            buttons.Add(button = new MenuButton(new Rectangle(((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - 161), ((Game1._graphics.GraphicsDevice.Viewport.Height / 2) + 171), 322, 64), Displays["button4"]));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(currentDisplay, displayLocation, Color.White);

            switch (Game1.CurrentState) //-CLos
            {
                case Game1.GameState.Pause:
                    spriteBatch.Draw(buttons[2].Texture, buttons[2].Position, Color.White);


                    spriteBatch.Draw(buttons[3].Texture, new Rectangle(700, 550, 322, 64), Color.White);
                    spriteBatch.DrawString(font, "Pause", new Vector2(Game1._graphics.GraphicsDevice.Viewport.Width / 2 - 65, 30), Color.Red);

                    spriteBatch.Draw(buttons[4].Texture, new Rectangle(256, 550, 322, 64), Color.White);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyStatus = Keyboard.GetState();
            MouseState mouseStatus = Mouse.GetState();

            switch (Game1.CurrentState) //-CLos
            {
                case Game1.GameState.Pause:
                    if (buttons[3].LeftButtonPress(mouseStatus, new Rectangle(700, 550, 322, 64)))
                    {
                        Game1.CurrentState = Game1.GameState.Main;
                        Player.Instance.Reset();
                    }

                    if (buttons[2].LeftButtonPress(mouseStatus, buttons[2].Position))
                    {
                        Player.Instance.Pause();
                    }
                    //Button that allows the player to leave if all enemies have been killed
                    if (buttons[4].LeftButtonPress(mouseStatus, new Rectangle(256, 550, 322, 64)))
                    {
                        Player.Instance.GameOver();
                    }
                    break;

            }

            //Close game


            base.Update(gameTime);
        }

    }
}
