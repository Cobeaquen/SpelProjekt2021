using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class InteractiveElement : UIElement
    {
        protected Texture2D texture;
        public Rectangle bounds;
        public InteractiveElement(Vector2 position, int width, int height, Texture2D texture) : base(position, width, height)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
            bounds = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(width, height));
        }
        public override void Draw()
        {
            Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
