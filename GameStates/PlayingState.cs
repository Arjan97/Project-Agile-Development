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
        private List<PlayerBase> players;
        private Puck puck;
        private float speedIncr = 1.1f;
        private List<PowerUp> powerUps;
        private List<TextGameObject> scores;
        private Vector2 respawnVelocity;
        private Vector2 respawnPosition;
        public PlayingState()
        {
            respawnVelocity = Vector2.Zero + new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));
            respawnPosition = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            players = new List<PlayerBase>()
            {
                new Player1(new Vector2(0, GameEnvironment.Screen.Y / 2),"player1"),
                new Player2(new Vector2(GameEnvironment.Screen.X -30, GameEnvironment.Screen.Y / 2), "player2")
            };
            Add(players[0]);
            Add(players[1]);

            puck = new Puck(respawnPosition, "ball");
            Add(puck);
            puck.Velocity = new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));

            powerUps = new List<PowerUp>
            {
                new SpeedPowerup(puck)
            };
            // Add(powerUps[0]);
            scores = new List<TextGameObject>
            {
                new TextGameObject("font", 1, "player1"),
                new TextGameObject("font", 1, "player2")
            };
            scores[0].Text = "Player 1: " + players[0].GetScore();
            scores[0].Position = new Vector2(20, 20);
            scores[1].Text = "Player 2: " + players[0].GetScore();
            scores[1].Position = new Vector2(GameEnvironment.Screen.X - 100, 20);
            Add(scores[0]);
            Add(scores[1]);
            /*
            score = new TextGameObject("font", 1, "player1");
            score.Text = "Player 1: " + players[0].GetScore();
            score.Position = new Vector2(20, 20);
            Add(score);
            score = new TextGameObject("font", 1, "player2");
            score.Text = "Player 1: " + players[1].GetScore();
            score.Position = new Vector2(GameEnvironment.Screen.X - 100, 20);
            Add(score);
            */

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //checks collision with top and bottom screen for puck using the boundingbox of spritegameobject
            if (puck.BoundingBox.Top <= 0 || puck.BoundingBox.Bottom > GameEnvironment.Screen.Y)
            {
                puck.Velocity = new Vector2(puck.Velocity.X, -puck.Velocity.Y);
                System.Diagnostics.Debug.WriteLine("werkt hm");

            } else if (puck.BoundingBox.Top >= 0 || puck.BoundingBox.Bottom < GameEnvironment.Screen.Y)
            {
                
            }
            if (puck.BoundingBox.Left <= 0)
            {
                // Reset the puck's position and velocity and deactivates powerups
                Vector2 random = new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));
                powerUps[0].Deactivate();
                Remove(powerUps[0]);
                puck.Position = respawnPosition;
                puck.Velocity = respawnVelocity;
                
                players[0].AddPoint();
                scores[1].Text = "Player 2: " + players[0].GetScore();
            }

            if (puck.BoundingBox.Right >= GameEnvironment.Screen.X)
            {
                // Reset the puck's position and velocity and deactivates powerups
                Vector2 random = new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));
                powerUps[0].Deactivate();
                Remove(powerUps[0]);
                puck.Position = respawnPosition;
                puck.Velocity = respawnVelocity;
                players[1].AddPoint();
                scores[0].Text = "Player 1: " + players[1].GetScore();

            }
            // Check for collision with player
            else if (puck.CollidesWith(players[0]) || puck.CollidesWith(players[1]))
            {
                // Calculate the new velocity of the puck based on the collision, using normalized positions of both p1 and p2
                for (int i = 0; i < players.Count; i++)
                {
                    /*
                    Vector2 newVelocity = Vector2.Reflect(puck.Velocity,
                    Vector2.Normalize(players[i].Position - puck.Position));
                    puck.Velocity = newVelocity * speedIncr;
                    System.Diagnostics.Debug.WriteLine("werkt kut1");
                    
                    Vector2 surfaceNormal = Vector2.Normalize(puck.Position - players[i].Position);
                    Vector2 projVelocity = Vector2.Dot(puck.Velocity, surfaceNormal) * surfaceNormal;
                    puck.Velocity = puck.Velocity - 2 * projVelocity;
                    puck.Velocity = puck.Velocity * speedIncr;
                    */
                    Vector2 newVelocity = Vector2.Reflect
                    (
                    puck.Velocity,
                    Vector2.Normalize(players[i].Position - puck.Position)
                    );
                    puck.Velocity = newVelocity * speedIncr;
                }
            }
           
            foreach (PowerUp powerup in powerUps)
            {
                powerup.Update(gameTime);
            }
            //Checks collision with powerup
           if (puck.CollidesWith(powerUps[0]))
            {
                powerUps[0].Activate();
               // System.Diagnostics.Debug.WriteLine("powerupped!");
                Remove(powerUps[0]);
            }

            if (!powerUps[0].IsActive && rand.Next(0, 500) == 250)
            {
                //powerUps[0].Activate();
                Add(powerUps[0]);
                System.Diagnostics.Debug.WriteLine("spawned!");
            }
        }
    }

}
