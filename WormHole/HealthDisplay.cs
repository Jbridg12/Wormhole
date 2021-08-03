// HealthDisplay.cs
// Contributors: Josh Bridges, Chris LoSardo

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
        private string shieldTitle = "Shield Energy ";
        private SpriteFont font;
        private List<int> healthBar;
        private List<int> shieldBar;

        private int healthStart;
        private int shieldStart;

        public HealthDisplay(Texture2D texture, SpriteFont font) : base(new Rectangle(), texture)
        {
            this.X = 0;
            this.Y = 0;
            this.font = font;
            this.shieldBar = new List<int>();
            this.healthBar = new List<int>();
            this.healthStart = 100 + (int)Game1.Font.MeasureString(healthTitle).X;
            this.shieldStart = 100 + (int)Game1.Font.MeasureString(shieldTitle).X;
        }

        public override void Update(GameTime gameTime)
        {
            int i = Player.Instance.MaxHealth;
            List<int> updatedHealth = new List<int>();
            while (i > 0)
            {
                if (i > Player.Instance.CurrentHealth)
                {
                    if ((i - Player.Instance.CurrentHealth) == 1)
                    {
                        updatedHealth.Insert(0, 1);
                    }
                    else
                    {
                        updatedHealth.Insert(0, 0);
                    }
                }
                else if (i % 2 == 1)
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

            int j = Player.Instance.MaxShields;
            List<int> updatedShields = new List<int>();
            while (j > 0)
            {
                if (j > Player.Instance.CurrentShields)
                {
                    if ((j - Player.Instance.CurrentShields) == 1)
                    {
                        updatedShields.Insert(0, 1);
                    }
                    else
                    {
                        updatedShields.Insert(0, 0);
                    }
                }
                else if (j % 2 == 1)
                {
                    updatedShields.Insert(0, 1);
                    j--;
                    continue;
                }
                else
                {
                    updatedShields.Insert(0, 2);
                }

                j -= 2;
            }

            shieldBar = updatedShields;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            //Background Rect for HUD -CLoS
            spriteBatch.Draw(EntityManager.Instance.Textures["HUDRect"], new Rectangle(10, 10, 530, 140), Color.White);


            spriteBatch.DrawString(font, healthTitle, new Vector2(100, 30f), Color.Black);
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
                        new Rectangle(healthStart + (i * 40), 30, 40, 40),
                        texture,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);
            }

            float shieldY = Game1.Font.MeasureString(healthTitle).Y + (Game1.Font.MeasureString(shieldTitle).Y / 2);
            spriteBatch.DrawString(font, shieldTitle, new Vector2(100, Game1.Font.MeasureString(healthTitle).Y + 30), Color.Black);

            for (int i = 0; i < shieldBar.Count; i++)
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
                        new Rectangle(shieldStart + (i * 40), (int)shieldY + 23, 40, 20),
                        texture,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);
            }

            //Salvage and Background Rect -CLoS
            spriteBatch.Draw(EntityManager.Instance.Textures["HUDRect"], new Rectangle(940, 10, 335, 100), Color.White);
            spriteBatch.DrawString(font, String.Format("Salvage: {0}", Player.Instance.Consumables["Salvage"]), new Vector2(988, 37), Color.Black);
        }
    }
}
