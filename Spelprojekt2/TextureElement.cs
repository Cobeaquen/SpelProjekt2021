using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    class TextureElement : UIElement
    {
        protected Texture2D texture;
        public TextureElement(Vector2 position, int width, int height, Texture2D texture) : base(position, width, height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.texture = texture;
        }
        public void Draw()
        {
            Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
