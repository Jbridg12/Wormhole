using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WormHole
{

    class MenuButton : Entity // - CLos
    {
        //Input
        private MouseState prevMState;

        //Button Variables
        private Texture2D texture;
        private Rectangle position;


        public MenuButton(Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.texture = texture;
            this.position = position;
        }

        //Controls the left mouse button
        public bool LeftButtonPress(MouseState mState, Rectangle collisionBox)
        {

            if (mState.LeftButton == ButtonState.Released &&
                prevMState.LeftButton == ButtonState.Pressed &&
                collisionBox.Contains(mState.Position))
            {
                return true;
            }
            else
            {
                prevMState = mState;
                return false;
            }
        }
    }
}
