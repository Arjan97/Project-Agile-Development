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
        public int Duration, Timer, randomX, randomY;
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
            origin = Center;
        }
        public virtual void ApplySize()
        {
            if (playingState.players[0].lastHit && IsActive)
                {
                playingState.players[0].Scale *= 1.5f;
                playingState.players[0].lastHit = false;

            }  
            else if (IsActive && Timer >= Duration - 1)
                {
                playingState.players[0].RevertScale();
                //player.lastHit = false;
                System.Diagnostics.Debug.WriteLine("uit ermee!");
            }
            if (playingState.players[1].lastHit && IsActive && !isSpawned)
            {
                playingState.players[1].Scale *= 1.5f;
                playingState.players[1].lastHit = false;

            }
            else if (IsActive && Timer >= Duration - 1)
            {
                playingState.players[1].RevertScale();
                //player.lastHit = false;
                System.Diagnostics.Debug.WriteLine("uit ermee!");
            }
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
                    Timer = 0;
                    Deactivate();
                }
            } 
        }
    }
}
