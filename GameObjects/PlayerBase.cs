using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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
        private int scoreMax;
        private int number;
        public bool gameOver, lastHit, powerUpped;
        public float originalScale;
        public PlayerBase(Vector2 startPosition, int number, string assetName) : base(assetName)
        {
            this.position = startPosition;
            originalScale = 1f;
            speed = 15;
            velocity = Vector2.Zero;
            screenWidth = GameEnvironment.Screen.X / 2 - 50;
            screenHeight = GameEnvironment.Screen.Y - 40;
            scoreMax = 4;
            this.number = number;
            gameOver = false;
            lastHit = false;
            powerUpped = false;
            origin = Center;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position += velocity;
            CheckScore();
        }

        public void RevertScale()
        {
            scale = originalScale;
        }
        public void CheckScore()
        {
            if (score >= scoreMax)
            {
                System.Diagnostics.Debug.WriteLine("Player " + number + " wins!");
                gameOver = true;
            } else
            {
                gameOver = false;
            }
        }

        public override void Reset()
        {
            base.Reset();
            score = 0;
        }
        public int GetNumber()
        {
            return number;
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
