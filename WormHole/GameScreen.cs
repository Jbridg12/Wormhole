// A parent class for the different screen types
// All of the screens will inherit from this class

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WormHole
{
    public class GameScreen
    {
        protected ContentManager content;
        public SpriteFont Font { get; set; }
        public Texture2D Display { get; set; }
        public Dictionary<string, Texture2D> Displays { get; set; }

        public List<Entity> Entities { get; set; }

        public GameScreen(Texture2D texture, SpriteFont font)
        {
            this.Entities = new List<Entity>();
            this.Font = font;
            this.Display = texture;
        }
        public GameScreen(Dictionary<string, Texture2D> textures, SpriteFont font)
        {
            this.Entities = new List<Entity>();
            this.Font = font;
            this.Displays = textures;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawHelper.ImageCenter(spriteBatch, Game1._graphics, this.Display);
        }
    }
}
