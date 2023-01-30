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
        private int score;
        public PlayerBase(Vector2 startPosition, string assetName) : base(assetName)
        {
            this.position = startPosition;
            speed = 15;
            velocity = Vector2.Zero;
            screenWidth = GameEnvironment.Screen.X / 2 - 50;
            screenHeight = GameEnvironment.Screen.Y - 40;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position += velocity;
        }
        public void AddPoint()
        {
            score++;
        }
        public int GetScore()
        {
            return score;
        }
    }
}
