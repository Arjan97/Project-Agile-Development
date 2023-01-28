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
        public SpeedPowerup(Vector2 position) : base(position, 100, "speed", "speedimg")
        {
            IsActive = false;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsActive)
            {
               Velocity *= 2f;
                System.Diagnostics.Debug.WriteLine("sneller!");
            } else if (!IsActive)
            {
               Velocity /= 2f;
            }
        }
    }
}
