using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    class SpeedPowerup : PowerUp
    {
        private PlayingState playingState;
        public SpeedPowerup(PlayingState playingState, int duration, string assetName) : base(playingState, duration, assetName)
        {
            this.playingState = playingState;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsActive)
            {
               playingState.puck.Velocity *= 1.3f;
                System.Diagnostics.Debug.WriteLine("sneller!");
            } else if (!IsActive && Timer >= Duration)
            {
                playingState.puck.Velocity /= 1.3f;
                Timer = 0;
            }
        }
    }
}
