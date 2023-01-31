using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects
{
     class Player1 : PlayerBase
    {
        public Player1(Vector2 startPosition, string assetName) : base(startPosition, 2, assetName)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.Up))
            {
                velocity.Y = -speed;
            }
            else if (inputHelper.IsKeyDown(Keys.Down))
            {
                velocity.Y = speed;
            } else
            {
                velocity.Y = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.Y > screenHeight)
            {
                position.Y = screenHeight;
            }
        }
    }
}
