using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    class SizePowerup : PowerUp
    {
        private PlayingState playingState;
        public SizePowerup(PlayingState playingState, int duration, string assetName) : base(playingState, duration, assetName)
        {
            this.playingState = playingState;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
               ApplySize();
                
        }
        public void RemovePowerUp()
        {
            //puck.Scale /= increaseAmount;
        }
    }
}
