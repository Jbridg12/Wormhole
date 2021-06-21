using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace WormHole
{
    class RoomScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D display;
        private Texture2D currentdisplay;

        public override void LoadContent()
        {
            base.LoadContent();
            font = content.Load<SpriteFont>("Base");
            display = content.Load<Texture2D>("room1");
            currentdisplay = display;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            DrawHelper.ImageCenter(spriteBatch, graphics, currentdisplay);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState status = Keyboard.GetState();
            if (status.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen(0);
            }
            base.Update(gameTime);
        }
    }
}
