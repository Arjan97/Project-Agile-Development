using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace BaseProject.GameStates
{
    class PlayingState : GameObjectList
    {
        Random rand = new Random();
        private Player1 player1;
        private Player2 player2;
        private Puck puck;
        private float speedIncr = 1.1f;
        private List<PowerUp> powerUps;
        public PlayingState()
        {
            int randX = rand.Next(-150, 150);
            int randY = rand.Next(-150, 150);

            player1 = new Player1(new Vector2(20, GameEnvironment.Screen.Y / 2),"player1");
            Add(player1);

            player2 = new Player2(new Vector2(GameEnvironment.Screen.X -40, GameEnvironment.Screen.Y / 2), "player2");
            Add(player2);

            puck = new Puck(new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), "ball");
            Add(puck);
            puck.Velocity = new Vector2(randY, randX);

            powerUps = new List<PowerUp>
            {
                new SpeedPowerup(puck)
            };
           // Add(powerUps[0]);


        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //checks collision with screen for puck using the boundingbox of spritegameobject
            if (puck.BoundingBox.Top <= 0 || puck.BoundingBox.Bottom > GameEnvironment.Screen.Y)
            {
                puck.Velocity = new Vector2(puck.Velocity.X, -puck.Velocity.Y);
                System.Diagnostics.Debug.WriteLine("werkt hm");
            }
            // Check for collision with player1 paddle
            else if (puck.CollidesWith(player1))
            {
                // Calculate the new velocity of the puck based on the collision
                Vector2 newVelocity = Vector2.Reflect(puck.Velocity,
                Vector2.Normalize(player1.Position - puck.Position));
                puck.Velocity = newVelocity * speedIncr;
                System.Diagnostics.Debug.WriteLine("werkt kut1");

            }
            // Check for collision with player2 paddle
            else if (puck.CollidesWith(player2))
            {
                // Calculate the new velocity of the puck based on the collision
                Vector2 newVelocity = Vector2.Reflect(puck.Velocity,
                Vector2.Normalize(player2.Position - puck.Position));
                puck.Velocity = newVelocity * speedIncr;
                System.Diagnostics.Debug.WriteLine("werkt kut");

            }
            foreach (PowerUp powerup in powerUps)
            {
                powerup.Update(gameTime);
            }
            //Checks collision with powerup
           if (puck.CollidesWith(powerUps[0]))
            {
                powerUps[0].Activate();
                System.Diagnostics.Debug.WriteLine("powerupped!");
                Remove(powerUps[0]);
            }
           

            if (!powerUps[0].IsActive && rand.Next(0, 100) == 50)
            {
                //powerUps[0].Activate();
                Add(powerUps[0]);
                System.Diagnostics.Debug.WriteLine("powerupped!");
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
        }
    }

}
