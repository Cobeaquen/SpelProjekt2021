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
        public UIElement(Vector2 position)
        {
            this.position = position;
        }

        public virtual void Draw()
        {
            
        }
    }
}
