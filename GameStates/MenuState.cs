using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameStates
{
    class MenuState : GameObjectList
    {
        private SpriteGameObject background, title, playButton, quitButton;
        private int selectedButton = 0;
        public MenuState()
        {
            background = new SpriteGameObject("background");
            Add(background);

            title = new SpriteGameObject("title", 0);
            title.Origin = title.Center;
            title.Position = new Vector2(GameEnvironment.Screen.X / 2, 50);
            title.Scale = 0.5f;
            Add(title);

            playButton = new SpriteGameObject("play", 1);
            playButton.Origin = playButton.Center;
            playButton.Position = new Vector2(GameEnvironment.Screen.X / 2, 300);
            playButton.Scale = 0.5f;
            Add(playButton);

            quitButton = new SpriteGameObject("exit", 1);
            quitButton.Origin = quitButton.Center;
            quitButton.Position = new Vector2(GameEnvironment.Screen.X / 2, 500);
            quitButton.Scale = 0.5f;
            Add(quitButton);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.Up))
            {
                selectedButton--;
                if (selectedButton < 0)
                {
                    selectedButton = 1;
                }
            }
            if (inputHelper.KeyPressed(Keys.Down))
            {
                selectedButton++;
                if (selectedButton > 1)
                {
                    selectedButton = 0;
                }
            }

            if (selectedButton == 0)
            {
                playButton.Shade = Color.Yellow;
                quitButton.Shade = Color.White;
            }
            else
            {
                playButton.Shade = Color.White;
                quitButton.Shade = Color.Yellow;
            }

            if (inputHelper.KeyPressed(Keys.Enter))
            {
                if (selectedButton == 0)
                {
                    GameEnvironment.GameStateManager.SwitchTo("playingState");
                }
                else
                {
                   Environment.Exit(0);
                }
            }
        }
    }
}
