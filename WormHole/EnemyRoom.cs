// EnemyRoom.cs
// Contributors: Josh Bridges

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class EnemyRoom : RoomScreen
    {
        public string Layout { get; set; }
        public EnemyRoom(int depth) : base(ScreenManager.Instance.ScreenTextures["room"], ScreenManager.Instance.ScreenFonts["base"], depth)
        {
            this.Entities = GenerateEntities();
        }
        
        public EnemyRoom(int depth, int index) : this(depth)
        {
            this.Index = index;
        }
        public EnemyRoom(string layout) : base(ScreenManager.Instance.ScreenTextures["room_tiles"], ScreenManager.Instance.ScreenFonts["base"], 0)
        {
            this.Layout = layout;
        }

        private List<Entity> GenerateEntities()
        {
            List<Entity> temp = new List<Entity>();
            Random rand = new Random();

            temp.Add(Player.Instance);

            switch (rand.Next(5))
            {
                case 0:
                    temp.Add(new Enemy(new Rectangle(500, 450, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(700, 450, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(9000, 450, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(1100, 450, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    break;
                case 1:
                    temp.Add(new Enemy(new Rectangle(20, 20, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(1820, 20, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(20, 980, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(1820, 980, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    break;
                case 2:
                    temp.Add(new Enemy(new Rectangle(200, 450, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    temp.Add(new Enemy(new Rectangle(1600, 450, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    break;
                case 3:
                    temp.Add(new Enemy(new Rectangle(900, 700, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    break;
                case 4:
                    break;
            }

            return temp;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Layout == null)
            {
                base.Draw(spriteBatch);
            }
            else
            {
                string[] rows = Layout.Split(',');
                for(int i = 0; i < rows.Length; i++)
                {
                    var chars = rows[i].ToCharArray();
                    for(int j = 0; j < chars.Length; j++)
                    {
                        Rectangle texture = new Rectangle();
                        switch (chars[j])
                        {
                            case 'C':
                                texture = new Rectangle();
                                break;
                            case '*':
                                texture = new Rectangle();
                                break;
                            case 'd':
                                texture = new Rectangle();
                                break;
                            case 'D':
                                texture = new Rectangle();
                                break;
                            case '-':
                                texture = new Rectangle(0, 616, 64, 60);
                                break;
                            case 'E':
                                texture = new Rectangle();
                                break;
                        }

                        spriteBatch.Draw(Display,
                                    new Rectangle(j * 64, i * 60, 64, 60),
                                    texture,
                                    Color.White,
                                    0f,
                                    Vector2.Zero,
                                    SpriteEffects.None,
                                    0);
                    }
                }
            }
        }
    }
}
