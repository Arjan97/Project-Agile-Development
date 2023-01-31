using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;

namespace BaseProject.GameStates
{
    class PlayingState : GameObjectList
    {
        Random rand = new Random();
        public List<PlayerBase> players;
        public Puck puck;
        private float speedIncr = 1.02f;
        private List<PowerUp> powerUps;
        private List<TextGameObject> scores;
        private Vector2 respawnVelocity;
        private Vector2 respawnPosition;
        private SpriteGameObject background, win1, win2;
        private bool gamePaused = false;
        public PlayingState()
        {
            background = new SpriteGameObject("veld");
            Add(background);

            win1 = new SpriteGameObject("win1");
            win2 = new SpriteGameObject("win2");
            win1.Position = respawnPosition; 
            win2.Position = respawnVelocity;

            respawnPosition = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            players = new List<PlayerBase>()
            {
                new Player1(new Vector2(16, GameEnvironment.Screen.Y / 2),"links"),
                new Player2(new Vector2(GameEnvironment.Screen.X -16, GameEnvironment.Screen.Y / 2), "rechts")
            };
            Add(players[0]);
            Add(players[1]);

            puck = new Puck(respawnPosition, "bal");
            Add(puck);
            puck.Velocity = new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));

            powerUps = new List<PowerUp>
            {
                new SpeedPowerup(this, 100, "snelheid"),
                new SizePowerup(this, 200, "groei")
            };

            scores = new List<TextGameObject>
            {
                new TextGameObject("font", 1, "player1"),
                new TextGameObject("font", 1, "player2"),
                new TextGameObject("font", 1, "finalscore")
            };
            scores[0].Text = "Player 1: " + players[0].GetScore();
            scores[0].Position = new Vector2(20, 20);
            scores[1].Text = "Player 2: " + players[0].GetScore();
            scores[1].Position = new Vector2(GameEnvironment.Screen.X - 100, 20);
            Add(scores[0]);
            Add(scores[1]);
            playMusic();
        }
        public void playMusic()
        {
            GameEnvironment.AssetManager.PlayMusic("song", true);
        }
        public void playSound(string assetName)
        {
            GameEnvironment.AssetManager.PlayMusic(assetName, true);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            respawnVelocity = Vector2.Zero + new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));
            //checks collision with top and bottom screen for puck using the boundingbox of spritegameobject
            if (puck.BoundingBox.Top <= 0 || puck.BoundingBox.Bottom > GameEnvironment.Screen.Y)
            {
                puck.Velocity = new Vector2(puck.Velocity.X, -puck.Velocity.Y);
                System.Diagnostics.Debug.WriteLine("werkt hm");
                //playSound("bounce");
            }
            //checks coli for left side of puck aka left screen
            if (puck.BoundingBox.Left <= 0)
            {
                // Reset the puck's position and velocity and deactivates powerups
                Vector2 random = new Vector2(rand.Next(-100, 100), rand.Next(-100, 100));
                powerUps[0].Deactivate();
                Remove(powerUps[0]);
                puck.Position = respawnPosition;
                puck.Velocity = respawnVelocity;
                //playerscore adding and updating
                players[0].AddPoint();
                players[0].CheckScore();
                scores[1].Text = "Player 2: " + players[0].GetScore();
            }
            //checks coli for right side of puck aka right screen
            if (puck.BoundingBox.Right >= GameEnvironment.Screen.X)
            {
                // Reset the puck's position and velocity and deactivates powerups
                Vector2 random = new Vector2(rand.Next(-200, 200), rand.Next(-200, 200));
                powerUps[0].Deactivate();
                Remove(powerUps[0]);
                puck.Position = respawnPosition;
                puck.Velocity = respawnVelocity;
                //playerscore adding and updating
                players[1].AddPoint();
                players[1].CheckScore();
                scores[0].Text = "Player 1: " + players[1].GetScore();

            }
            //last hit setter for size increase
            if (puck.CollidesWith(players[0]))
            {

                players[0].lastHit = true;
                if (players[1].lastHit) {
                    players[1].lastHit = false;
                }
               
            }
            if (puck.CollidesWith(players[1]))
            {
                players[1].lastHit = true;
                if (players[0].lastHit)
                {
                    players[0].lastHit = false;
                }
            }

            if (players[0].gameOver)
            {
                Remove(powerUps[0]);
                Remove(powerUps[1]);
                Remove(players[0]);
                Remove(players[1]);
                Remove(puck);
                Remove(background);
                //scores[2].Text = "Player " + player.GetNumber() + " wins!";
                //scores[2].Position = respawnPosition;
                //playSound("win");
                Add(win2);
                //GameEnvironment.GameStateManager.SwitchTo("gameoverState");
            }

            if (players[1].gameOver)
            {
                Remove(powerUps[0]);
                Remove(powerUps[1]);
                Remove(players[0]);
                Remove(players[1]);
                Remove(puck);
                Remove(background);
                //scores[2].Text = "Player " + player.GetNumber() + " wins!";
                //scores[2].Position = respawnPosition;
                //Add(scores[2]);
                playSound("win");
                Add(win1);
                //GameEnvironment.GameStateManager.SwitchTo("gameoverState");
            }
            //player code for all players so no need for double coding
            foreach (PlayerBase player in players)
            {
                if (puck.CollidesWith(player))
                {
                    // Calculate the new velocity of the puck based on the collision, using normalized positions of both p1 and p2
                    for (int i = 0; i < players.Count; i++)
                    {
                        Vector2 newVelocity = Vector2.Reflect
                        (
                        puck.Velocity,
                        Vector2.Normalize(players[i].Position - puck.Position)
                        );
                        puck.Velocity = newVelocity * speedIncr;
                    }
                    //playSound("bounce");
                    System.Diagnostics.Debug.WriteLine("player " + player.GetNumber() + "got last" + player.lastHit);
                }
                
            }
            
            //adjusts all powerups from the list accordingly without having to code for every diff powerup
            foreach (PowerUp powerup in powerUps)
            {
                powerup.Update(gameTime);
                //Checks collision with powerup
                if (puck.CollidesWith(powerup) && powerup.isSpawned)
                {
                    Remove(powerup);
                    powerup.Activate();
                    //playSound("powerup");
                }
                if (!powerup.IsActive && !powerup.isSpawned && rand.Next(0, 500) == 250)
                {
                    Add(powerup);
                    powerup.Position = new Vector2(GameEnvironment.Screen.X / 2, rand.Next(10, 550) - powerup.Position.Y);
                    powerup.isSpawned = true;
                    System.Diagnostics.Debug.WriteLine("spawned!");
                }
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (gamePaused && inputHelper.IsKeyDown(Keys.Enter))
            {
                gamePaused = false;
            }
        }
    }

}
