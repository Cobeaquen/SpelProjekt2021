using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    public class TextElement : UIElement
    {
        public string Text
        { 
            get 
            { 
                return text;
            }
            set 
            {
                text = value;
            } 
        }
        public int width { get; private set; }
        public int height { get; private set; }
        public Color color;
        private string text;

        protected SpriteFont font;
        public TextElement(Vector2 position, int width, int height, string text, Color color, SpriteFont font) : base(position)
        {
            this.width = width;
            this.height = height;
            this.text = text;
            this.color = color;
            this.font = font;
        }

        public override void Draw()
        {
            Main.spriteBatch.DrawString(font, text, position, color);
        }
    }
}
