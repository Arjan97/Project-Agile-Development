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
            background = new SpriteGameObject("veld");
            Add(background);

            title = new SpriteGameObject("titel", 0);
            title.Origin = title.Center;
            title.Position = new Vector2(GameEnvironment.Screen.X / 2, 50);
            Add(title);

            playButton = new SpriteGameObject("start", 1);
            playButton.Origin = playButton.Center;
            playButton.Position = new Vector2(GameEnvironment.Screen.X / 2, 300);
            Add(playButton);

            quitButton = new SpriteGameObject("stoppen", 1);
            quitButton.Origin = quitButton.Center;
            quitButton.Position = new Vector2(GameEnvironment.Screen.X / 2, 500);
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
                //playButton.Shade = Color.Yellow;
                //quitButton.Shade = Color.White;
            }
            else
            {
                //playButton.Shade = Color.White;
                //quitButton.Shade = Color.Yellow;
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
