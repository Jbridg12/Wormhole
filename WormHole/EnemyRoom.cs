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
        public EnemyRoom() : base(ScreenManager.Instance.ScreenTextures["room"], ScreenManager.Instance.ScreenFonts["base"])
        {
            this.Entities = GenerateEntities();
        }

        private List<Entity> GenerateEntities()
        {
            List<Entity> temp = new List<Entity>();
            Random rand = new Random();

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
    }
}
