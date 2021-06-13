// A library I am expanding to create shorthands for longer calls that will
// hopefully save some time.

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    static class DrawHelper
    {
        public static void ImageCenter(SpriteBatch sb, GraphicsDeviceManager g, Texture2D texture, Color color)
        {
            sb.Draw(
                texture, 
                new Vector2(g.PreferredBackBufferWidth / 2, g.PreferredBackBufferHeight / 2), 
                null, 
                Color.White, 
                0f, 
                new Vector2(texture.Width / 2, texture.Height / 2), 
                Vector2.One, 
                SpriteEffects.None, 
                0f);
        }

        public static void DrawText(SpriteBatch sb, string text, Vector2 position)
        {
        }
    }
}
