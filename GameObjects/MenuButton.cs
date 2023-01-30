using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace BaseProject.GameObjects
{
    internal class MenuButton : TextGameObject
    {
        public MenuButton(Vector2 position, string assetName) : base(assetName)
        {
            this.position = position;
            
        }
    }
}
