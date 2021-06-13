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
    class GameScreen
    {
        protected ContentManager content;
        
        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
                
        }
    }
}
