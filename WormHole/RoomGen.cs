using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WormHole
{
    class RoomGen
    {//Room Fields
        private string[,] arrayRoom;
        private string name;

        private Dictionary<string, Texture2D> textures;

        private Rectangle spritePos;

        private int xArray;
        private int yArray;
        public RoomGen(string[,] arrayRoom, string name, Dictionary<string, Texture2D> textures)
        {

            this.arrayRoom = arrayRoom;
            this.name = name;
            this.textures = textures;

            //gets the x and y dimensions of the room array
            xArray = arrayRoom.GetLength(0);
            yArray = arrayRoom.Length / xArray;


        }

        public void Draw(SpriteBatch _spritebatch)
        {
            //reads the array for the room the same way that the array is created in the GenerateFloor method in Game1

            for (int i = 0; i < xArray; i++)
            {
                for (int j = 0; j < yArray; j++)
                {
                    //sets the position of the current sprite by multiplying i and j by the size of each sprite
                    spritePos = new Rectangle(j * 64, i * 60, 64, 60);
                    switch (arrayRoom[i, j].ToString())
                    {
                        //Determines which sprite to draw based on the letter stored in the arrayRoom 2d array
                        case "C":
                            _spritebatch.Draw(textures["C"], spritePos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
                            break;
                        case "*":
                            _spritebatch.Draw(textures["*"], spritePos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
                            break;
                        case "B":
                            _spritebatch.Draw(textures["B"], spritePos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
                            break;
                        case "D":
                            _spritebatch.Draw(textures["D"], spritePos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
                            break;
                        case "E":
                            _spritebatch.Draw(textures["E"], spritePos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
                            break;
                    }
                }
            }
        }
    }
}
