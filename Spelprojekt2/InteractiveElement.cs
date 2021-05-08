using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class InteractiveElement : TextureElement
    {
        public Rectangle bounds;
        public int width;
        public int height;

        public InteractiveElement(Vector2 position, int width, int height, Texture2D texture, Vector2 origin) : base(position, texture, origin)
        {
            this.width = width;
            this.height = height;
            this.texture = texture;
            bounds = new Rectangle(new Point((int)position.X, (int)position.Y) - origin.ToPoint(), new Point(width, height));
        }
        public virtual void Clicked()
        {

        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
