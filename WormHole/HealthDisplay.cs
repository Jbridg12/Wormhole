// HealthDisplay.cs
// Contributors: Josh Bridges

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace WormHole
{
    class HealthDisplay : UIElement
    {
        private string healthTitle = "Hull Strength ";
        private List<int> healthBar;
        private List<int> shieldBar;
        private int healthStart;
        private int shieldStart;

        public HealthDisplay(Texture2D texture) : base(new Rectangle(), texture)
        {
            this.X = 0;
            this.Y = 0;
            this.shieldBar = new List<int>();
            this.healthBar = new List<int>();
            this.healthStart = Globals.ROOM_TEXTURE_LEFT + (int)Game1.Font.MeasureString(healthTitle).X;
        }

        public override void Update(GameTime gameTime)
        {
            int i = Player.Instance.MaxHealth;
            List<int> updatedHealth = new List<int>();
            while (i > 0)
            {
                if(i > Player.Instance.CurrentHealth)
                {
                    if((i - Player.Instance.CurrentHealth) == 1)
                    {
                        updatedHealth.Insert(0, 1);
                    }
                    else
                    {
                        updatedHealth.Insert(0, 0);
                    }
                }
                else if(i % 2 == 1)
                {
                    updatedHealth.Insert(0, 1);
                    i--;
                    continue;
                }
                else
                {
                    updatedHealth.Insert(0, 2);
                }

                i -= 2;
            }

            healthBar = updatedHealth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.Font, healthTitle, new Vector2(Globals.ROOM_TEXTURE_LEFT, (Game1.Font.MeasureString(healthTitle).Y / 2)), Color.Black);
            Rectangle texture = new Rectangle();
            for (int i = 0; i < healthBar.Count; i++)
            {
                switch (healthBar[i])
                {
                    case 0:
                        texture = new Rectangle(80, 0, 40, 40);
                        break;
                    case 1:
                        texture = new Rectangle(40, 0, 40, 40);
                        break;
                    case 2:
                        texture = new Rectangle(0, 0, 40, 40);
                        break;
                }

                spriteBatch.Draw(Texture,
                        new Rectangle(healthStart + (i*40), (int)(Game1.Font.MeasureString(healthTitle).Y / 2), 40, 40),
                        texture,              
                        Color.White,
                        0f,    
                        Vector2.Zero,   
                        SpriteEffects.None,
                        0);
            }

            /*for(int i = 0; i < shieldBar.Count; i++)
            {
                switch (shieldBar[i])
                {
                    case 0:
                        texture = new Rectangle(80, 40, 40, 20);
                        break;
                    case 1:
                        texture = new Rectangle(40, 40, 40, 20);
                        break;
                    case 2:
                        texture = new Rectangle(0, 40, 40, 20);
                        break;
                }

                spriteBatch.Draw(Texture,
                        new Rectangle(i * 40, 50, 40, 20),
                        texture,
                        Color.Red,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);
            }*/
        }
    }
}
