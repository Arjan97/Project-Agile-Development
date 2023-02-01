using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using System;
using System.Collections.Generic;

namespace BaseProject.GameStates
{
    class PlayingState : GameObjectList
    {
        Random rand = new Random();
        public List<PlayerBase> players; //public so other classes can use it
        public Puck puck;
        private List<PowerUp> powerUps;
        private List<TextGameObject> scores;
        private Vector2 respawnVelocity;
        private Vector2 respawnPosition;
        private SpriteGameObject background, win1, win2;
        public PlayingState()
        {
            //background instantiation using engine spritegameobject
            background = new SpriteGameObject("veld");
            Add(background);
            //win screens instantiation
            win1 = new SpriteGameObject("win1");
            win2 = new SpriteGameObject("win2");
            win1.Position = respawnPosition; 
            win2.Position = respawnVelocity;
            //respawn center screen position
            respawnPosition = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            //player list for p1 and p2 which inherit playerbase, instantiation
            players = new List<PlayerBase>()
            {
                new Player1(new Vector2(16, GameEnvironment.Screen.Y / 2),"links"),
                new Player2(new Vector2(GameEnvironment.Screen.X -16, GameEnvironment.Screen.Y / 2), "rechts")
            };
            Add(players[0]);
            Add(players[1]);
            //puck instantiation with random velocity to any direction on start
            puck = new Puck(respawnPosition, "bal");
            Add(puck);
            puck.Velocity = new Vector2(rand.Next(-150, 150), rand.Next(-150, 150));
            //powerups list inheriting from powerup instantiation
            powerUps = new List<PowerUp>
            {
                new SpeedPowerup(this, 200, "snelheid"),
                new SizePowerup(this, 300, "groei")
            };
            //score list instantiation using the engines textgameobject, also template text otherwise obj null error
            scores = new List<TextGameObject>
            {
                new TextGameObject("font", 1, "player1"),
                new TextGameObject("font", 1, "player2"),
                new TextGameObject("font", 1, "finalscore")
            };
            scores[0].Text = "Player 1: ";
            scores[0].Position = new Vector2(20, 20);
            scores[1].Text = "Player 2: ";
            scores[1].Position = new Vector2(GameEnvironment.Screen.X - 100, 20);
            Add(scores[0]);
            Add(scores[1]);

            //plays the music on start of the game through function
            playMusic();
        }
        //play music through engine assetmanager
        public void playMusic()
        {
            GameEnvironment.AssetManager.PlayMusic("song", true);
        }
        //play sound through engine assetmanager
        public void playSound(string assetName)
        {
            GameEnvironment.AssetManager.PlayMusic(assetName, true);
        }
        //update getting called in a time state of the game, always being updated
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //update score text for current scores
            scores[1].Text = "Player 2: " + players[0].GetScore();
            scores[0].Text = "Player 1: " + players[1].GetScore();
            //update the resp velo so it respawns in different directions
            respawnVelocity = Vector2.Zero + new Vector2(rand.Next(-150, 150), rand.Next(-150, 150));
            //checks collision with top and bottom screen for puck using the boundingbox of spritegameobject
            if (puck.BoundingBox.Top <= 0 || puck.BoundingBox.Bottom >= GameEnvironment.Screen.Y)
            {
                puck.Velocity = new Vector2(puck.Velocity.X, -puck.Velocity.Y);
                System.Diagnostics.Debug.WriteLine("werkt hm");
                //playSound("bounce");
            }
            //checks coli for left side of puck aka left screen, also used for scores
            if (puck.BoundingBox.Left <= 0)
            {
                // Reset the puck's position and velocity and deactivates powerups
                powerUps[0].Deactivate();
                Remove(powerUps[0]);
                Remove(puck);
                puck.Position = respawnPosition;
                puck.Velocity = respawnVelocity;
                Add(puck);
                //playerscore adding and updating
                players[0].AddPoint();
                players[0].CheckScore();
            }
            //checks coli for right side of puck aka right screen
            if (puck.BoundingBox.Right >= GameEnvironment.Screen.X)
            {
                // Reset the puck's position and velocity and deactivates powerups
                Vector2 random = new Vector2(rand.Next(-200, 200), rand.Next(-200, 200));
                powerUps[0].Deactivate();
                Remove(powerUps[0]);
                Remove(puck);
                puck.Position = respawnPosition;
                puck.Velocity = respawnVelocity;
                Add(puck);
                //playerscore adding and updating
                players[1].AddPoint();
                players[1].CheckScore();

            }
            //individual last hit setter bool for size increase powerup
            if (puck.CollidesWith(players[0]))
            {
                players[0].lastHit = true;
                players[1].lastHit = false;
            }
            if (puck.CollidesWith(players[1]))
            {
                players[1].lastHit = true;
                players[0].lastHit = false;
            }
            //checks game over state, then removes objects and display win screen for either p1 or p2
            if (players[0].gameOver)
            {
                Remove(powerUps[0]);
                Remove(powerUps[1]);
                Remove(players[0]);
                Remove(players[1]);
                Remove(puck);
                Remove(background);
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
                //playSound("win");
                Add(win1);
                //GameEnvironment.GameStateManager.SwitchTo("gameoverState");
            }
            //player code loop for all players from the list so no need for double coding
            foreach (PlayerBase player in players)
            {
                //check collision with puck with player
                if (puck.CollidesWith(player))
                {
                    // Calculate the new velocity of the puck based on the collision, using normalized positions of both p1 and p2
                    for (int i = 0; i < players.Count; i++)
                    {
                        Vector2 v = Vector2.Normalize(players[i].Position - puck.Position); //vector divided by length of vector
                        Vector2 newv = Vector2.Reflect(puck.Velocity, v); //reflection using normalized vector and velocity
                        puck.Velocity = newv * puck.speedIncr; //multiply new velocity by a speedincr each time puck is hit
                    }
                    //playSound("bounce");
                    //System.Diagnostics.Debug.WriteLine("player " + player.GetNumber() + "got last" + player.lastHit);
                }
            }
            //adjusts all powerups from the list accordingly without having to code for every diff powerup
            foreach (PowerUp powerup in powerUps)
            {
                powerup.Update(gameTime);
                //Checks collision with powerup
                if (puck.CollidesWith(powerup) && powerup.isSpawned) // isspawned added, double check so no potential invisible powerups get activated
                {
                    Remove(powerup);
                    powerup.Activate();
                    //playSound("powerup");
                }
                if (!powerup.IsActive && !powerup.isSpawned && rand.Next(0, 500) == 250) //random timer for when a powerup spawns if its not spawned in
                {
                    Add(powerup);
                    powerup.Position = new Vector2(GameEnvironment.Screen.X / 2, rand.Next(10, 550));
                    powerup.isSpawned = true;
                    //System.Diagnostics.Debug.WriteLine("spawned!");
                }
            }
        }
    }
}
