using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;


namespace BaseProject.GameObjects
{
     class PowerUp : SpriteGameObject
    {
        public int Duration;
        public bool IsActive;
        public int Timer;
        public string Name;
        private Random rand = new Random();
        private int randomX, randomY;
        public PowerUp(int duration, string name, string assetName) : base(assetName)
        {
            IsActive = false;
            Duration = duration;
            Name = name;
            Timer = 0;
            randomX = rand.Next(0, GameEnvironment.Screen.X - Width);
            randomY = rand.Next(0, GameEnvironment.Screen.Y - Height);
            position = new Vector2(randomX, randomY);
        }

        public void Activate()
        {
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
        } 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsActive)
            {
                Timer++;
                System.Diagnostics.Debug.WriteLine("timer:" + Timer);
                System.Diagnostics.Debug.WriteLine("dura:" + Duration);
                if (Timer >= Duration)
                {
                    Deactivate();
                    Timer = 0;
                }
            } 
        }
    }
}
