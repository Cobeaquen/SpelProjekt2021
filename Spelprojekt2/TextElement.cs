using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    class TextElement : UIElement
    {
        protected SpriteFont font;
        string text;
        public TextElement(Vector2 position, int width, int height, SpriteFont font, string text) : base(position, width, height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.font = font;
            this.text = text;
        }

        public override void Draw()
        {
            Main.spriteBatch.DrawString(font, text, position, Color.White);
        }
    }
}
