using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace BaseProject.GameObjects
{
     class SpeedPowerup : PowerUp
    {
        private Puck puck;
        public SpeedPowerup(Puck puck) : base(100, "speed", "speedimg")
        {
            this.puck = puck;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsActive)
            {
               puck.Velocity *= 1.1f;
                System.Diagnostics.Debug.WriteLine("sneller!");
            } else if (!IsActive)
            {
               Velocity /= 1.1f;
            }
        }
    }
}
