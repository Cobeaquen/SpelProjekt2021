using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class TowerElement : InteractiveElement
    {      
        public Tower tower;
        public TowerElement(Vector2 position, int width, int height, Texture2D texture, int towerType, Tower tower) : base(position, width, height, texture)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
            this.tower = tower;

            bounds = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(width, height));
        }
    }
}
