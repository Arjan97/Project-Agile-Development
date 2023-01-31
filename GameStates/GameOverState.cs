using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameStates
{
    class GameOverState : GameObjectList
    {
        private SpriteGameObject background, playButton;
        private TextGameObject highScore, winner;

        public GameOverState()
        {
            background = new SpriteGameObject("background");
            Add(background);

            winner = new TextGameObject("font");
            winner.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2 + 100);
            //Add(winner);

            highScore = new TextGameObject("font");
            //highScore.Text = "Player " + players[0].GetNumber() + " wins!";
            highScore.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            //Add(highScore);
            Add(highScore);

            playButton = new SpriteGameObject("start", 1);
            playButton.Origin = playButton.Center;
            playButton.Position = new Vector2(GameEnvironment.Screen.X / 2, 300);
            playButton.Scale = 1f;
            Add(playButton);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.Enter))
                {
                    GameEnvironment.GameStateManager.SwitchTo("playingState");
                }
            }
        }
    }
