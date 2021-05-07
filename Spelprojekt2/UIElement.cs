using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spelprojekt2
{
    public class UIElement
    {
        public Vector2 position;
        public int width;
        public int height;
        public UIElement(Vector2 position, int width, int height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
        }

        public virtual void Draw()
        {
            
        }
    }
}
