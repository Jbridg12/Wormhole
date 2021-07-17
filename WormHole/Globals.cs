// DrawHelper.cs
// Contributors: Josh Bridges
//
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
    static class Globals
    {
        public static float SCREEN_SCALING = 0;
        public static int ROOM_TEXTURE_LEFT = 0;
        public static int ROOM_TEXTURE_RIGHT = 0;
        public static int XMAX = 0;
        public static int XMIN = 0;
        public static void ImageCenter(SpriteBatch sb, GraphicsDeviceManager g, Texture2D texture)
        {
            sb.Draw(
                texture, 
                new Vector2(g.GraphicsDevice.Viewport.Width / 2, g.GraphicsDevice.Viewport.Height / 2), 
                null, 
                Color.White, 
                0f, 
                new Vector2(texture.Width / 2, texture.Height / 2), 
                Vector2.One, 
                SpriteEffects.None, 
                0f);
        }

        public static Vector2 CenterText(string str, SpriteFont font)  // helper method to give a vector for drawing the string centered in the middle of the screen
        {
            Vector2 result = new Vector2((Game1._graphics.GraphicsDevice.Viewport.Width / 2) - (font.MeasureString(str).X / 2),
                                        (Game1._graphics.GraphicsDevice.Viewport.Height / 2) - (font.MeasureString(str).Y / 2));  //subtract the center of the string coordinates from screen coordinates to center
            return result;
        }
    }
}
