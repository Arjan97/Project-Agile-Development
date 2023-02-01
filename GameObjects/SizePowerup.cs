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
            sizeIncrease();
        }
        public void sizeIncrease()
        {
            if (limit == 1)
            {
                if (playingState.players[0].lastHit && IsActive)
                {
                    playingState.players[0].Scale *= 1.5f;
                    playingState.players[0].lastHit = false;

                }
                if (playingState.players[1].lastHit && IsActive)
                {
                    playingState.players[1].Scale *= 1.5f;
                    playingState.players[1].lastHit = false;

                }
            } else if (!IsActive)
            {
                    playingState.players[1].RevertScale();
                    System.Diagnostics.Debug.WriteLine("uit ermee!");
               
                    playingState.players[0].RevertScale();
                    System.Diagnostics.Debug.WriteLine("uit ermee!");
            }
        }
    }
}
