using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
     class Puck : SpriteGameObject
    {
        public float maxSpeed;
        public Puck(Vector2 startPosition, string assetName) : base(assetName)
        {
            this.position = startPosition;
            maxSpeed = 600f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Check for collision with the left and right walls
            if (position.X <= 0 || position.X >= GameEnvironment.Screen.X)
            {
                velocity = new Vector2(-velocity.X, velocity.Y);
            }

            // Check for collision with the top and bottom walls
            if (position.Y <= 0 || position.Y >= GameEnvironment.Screen.Y)
            {
                velocity = new Vector2(Velocity.X, Velocity.Y);
            }
            //Puts a clamp on the speed, thus giving it a maxspeed
            velocity = Vector2.Clamp(velocity, new Vector2(-maxSpeed, -maxSpeed), new Vector2(maxSpeed, maxSpeed));

            // Update position based on velocity and added time based increase of speed for challenge
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
