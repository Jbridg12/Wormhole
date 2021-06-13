// A screen type that is used for doing menu screens in the game

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Test
{
    class MainMenuScreen : GameScreen
    {
        private SpriteFont font;

        public override void LoadContent()
        {
            base.LoadContent();
            font = content.Load<SpriteFont>("Base");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Aquamarine);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
