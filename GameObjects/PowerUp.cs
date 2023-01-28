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
        //public PowerUpType Type { get; set; }
        public int Duration;
        public bool IsActive;
        public int Timer;
        public string Name;
        public PowerUp(Vector2 startPosition, int duration, string name, string assetName) : base(assetName)
        {
            //Type = type;
            position = startPosition;
            IsActive = false;
            Duration = duration;
            Name = name;
            Timer = 0;
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

            Timer += gameTime.ElapsedGameTime.Milliseconds;
            if (Timer >= Duration)
            {
                 Deactivate();
            }
        }
    }
}
