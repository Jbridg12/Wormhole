// EnemyRoom.cs
// Contributors: Josh Bridges

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class EnemyRoom : RoomScreen
    {
        
        public EnemyRoom(int depth) : base(ScreenManager.Instance.ScreenTextures["room"], ScreenManager.Instance.ScreenFonts["base"], depth)
        {
            this.Entities = GenerateEntities();
        }
        
        public EnemyRoom(int depth, int index) : this(depth)
        {
            this.Index = index;
        }

        public EnemyRoom(string layout, int index) : base(ScreenManager.Instance.ScreenTextures["room_tiles"], ScreenManager.Instance.ScreenFonts["base"], 0)
        {
            this.Layout = layout;
            this.Index = index;
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

        public override void Draw(SpriteBatch spriteBatch)  // new Draw method can draw the room from the provided Layout string
        {
            if(Layout == null)  // If not using the tool just draw as normal
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
                        spriteBatch.Draw(Display,
                                    new Rectangle(j * 64, i * 60, 64, 60),
                                    GetTile(chars[j], i, j),
                                    Color.White,
                                    0f,
                                    Vector2.Zero,
                                    SpriteEffects.None,
                                    0);
                    }
                }
            }
            Parsed = true;
        }

        public Rectangle GetTile(char c, int i, int j)  // method to identify the proper tile from spritesheet
        {
            switch (c)
            {
                case 'C':
                    return new Rectangle(64, 600, 64, 60);
                case '*':
                    if (i == 0)
                    {
                        return new Rectangle(0, 660, 64, 60);
                    }
                    else if (i == 11)
                    {
                        return new Rectangle(64, 660, 64, 60);
                    }
                    else if (j == 0)
                    {
                        return new Rectangle(0, 720, 64, 60);
                    }
                    else
                    {
                        return new Rectangle(64, 720, 64, 60);
                    }
                case 'd':
                    if (i == 0) // This means we are working with the Topmost row
                    {
                        this.Entities.Add(new Door(new Rectangle(j * 64, i * 60, 128, 120), Game1.Direction.Up, ScreenManager.Instance.Floor[Index-(int)Math.Sqrt(ScreenManager.Instance.FloorSize)]));
                        return new Rectangle(0, 60 * doorAnimationState, 64, 60);
                    }
                    else if (i == 11)   // Bottom-most row
                    {

                        this.Entities.Add(new Door(new Rectangle(j * 64, i * 60, 128, 120), Game1.Direction.Down, ScreenManager.Instance.Floor[Index + (int)Math.Sqrt(ScreenManager.Instance.FloorSize)]));
                        return new Rectangle(128, (300 - (60 * doorAnimationState)), 64, 60);
                    }
                    else if (j == 0)    // left-most column
                    {
                        this.Entities.Add(new Door(new Rectangle(j * 64, i * 60, 128, 120), Game1.Direction.Left, ScreenManager.Instance.Floor[Index - 1]));
                        return new Rectangle(64 * doorAnimationState, 420, 64, 60);
                    }
                    else    // right-most column
                    {
                        this.Entities.Add(new Door(new Rectangle(j * 64, i * 60, 128, 120), Game1.Direction.Right, ScreenManager.Instance.Floor[Index + 1]));
                        return new Rectangle((320 - (64 * doorAnimationState)), 480, 64, 60);
                    }
                    
                case 'D':
                    if (i == 0)
                    {
                        return new Rectangle(64, 60 * doorAnimationState, 64, 60);
                    }
                    else if (i == 11)
                    {
                        return new Rectangle(192, (300 - (60 * doorAnimationState)), 64, 60);
                    }
                    else if (j == 0)
                    {
                        return new Rectangle(64 * doorAnimationState, 360, 64, 60);
                    }
                    else
                    {
                        return new Rectangle((320 - (64 * doorAnimationState)), 540, 64, 60);
                    }
                case '-':
                    return new Rectangle(0, 600, 64, 60);
                case 'E':
                    if(!Parsed)
                        this.Entities.Add(new Enemy(new Rectangle(j*64, i*60, 100, 100), EntityManager.Instance.Textures["enemy"]));
                    return new Rectangle(0, 600, 64, 60);
                default:
                    return new Rectangle();
            }


        }
    }
}
