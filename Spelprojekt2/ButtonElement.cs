using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2
{
    public class ButtonElement : InteractiveElement
    {
        public delegate void ClickCallback();
        public ClickCallback clickCallback;
        public ButtonElement(Vector2 position, int width, int height, ClickCallback clickCallback, Texture2D sprite, Vector2 origin) : base(position, width, height, sprite, origin)
        {
            this.clickCallback = clickCallback;
        }
        public virtual void Clicked()
        {

        }
    }
}
