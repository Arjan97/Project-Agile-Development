using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
     class PowerUp : SpriteGameObject
    {
        public int Duration, Timer, randomX, randomY, limit;
        public bool IsActive, isSpawned;
        public string Name;
        private Random rand = new Random();
        private PlayingState playingState;
        public PowerUp(PlayingState playingstate, int duration, string assetName) : base(assetName)
        {
            this.playingState = playingstate;
            IsActive = false;
            isSpawned = false;
            Duration = duration;
            Timer = 0;
            randomX = rand.Next(0, GameEnvironment.Screen.X - Width);
            randomY = rand.Next(0, GameEnvironment.Screen.Y - Height);
            origin = Center; //draws sprite from center of the sprite
            limit = 2;
        }
        public void Activate()
        {
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
            isSpawned = false;
            position = new Vector2(randomX, randomY);
            limit++;
        } 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsActive)
            {
                limit--;
                Timer++;
                System.Diagnostics.Debug.WriteLine("timer:" + Timer);
                System.Diagnostics.Debug.WriteLine("dura:" + Duration);
                if (Timer >= Duration)
                {
                    Timer = 0;
                    Deactivate();
                }
            } 
        }
    }
}
