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
        public Texture2D texture;
        public Rectangle bounds;
        public UIElement(Vector2 position, int width, int height, Texture2D texture)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.texture = texture;
            bounds = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(width, height));
        }

        public void Draw()
        {
            Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
