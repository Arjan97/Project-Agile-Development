using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects
{
    class PlayerBase : SpriteGameObject
    {
        protected int speed;
        protected int screenWidth;
        protected int screenHeight;
        protected Rectangle bounds;

        public PlayerBase(Vector2 startPosition, string assetName) : base(assetName)
        {
            this.position = startPosition;
            speed = 15;
            velocity = Vector2.Zero;
            screenWidth = GameEnvironment.Screen.X / 2 - 50;
            screenHeight = GameEnvironment.Screen.Y - 40;
           // bounds = new Rectangle((int)startPosition.X, (int)startPosition.Y, Width, Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position += velocity;
            //bounds.X = (int)position.X;
            //bounds.X = (int)position.Y;
        }
        public Rectangle Bounds
        {
            get { return bounds; }
        }
    }
}
