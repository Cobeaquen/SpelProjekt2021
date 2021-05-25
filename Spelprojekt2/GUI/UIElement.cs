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
        public Vector2 offsetPosition;
        public UIElement(Vector2 position)
        {
            this.position = position;
            offsetPosition = Vector2.Zero;
        }
        public virtual void Update()
        {

        }
        public virtual void Draw()
        {
            
        }
    }
}
