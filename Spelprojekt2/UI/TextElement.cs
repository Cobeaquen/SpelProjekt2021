using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spelprojekt2.UI
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
            
            if (font.MeasureString(text).X > width)
            {
                StringBuilder sb = new StringBuilder();
                string[] words = text.Split(' ');
                foreach (var w in words)
                {
                    Vector2 newSize = font.MeasureString(sb + w);
                    if (newSize.X < width)
                    {
                        sb.Append(w + " ");
                    }
                    else
                    {
                        sb.AppendLine().Append(w + " ");
                    }
                }
                this.text = sb.ToString();
            }
        }

        public override void Draw()
        {
            Main.spriteBatch.DrawString(font, text, (position + offsetPosition).ToPoint().ToVector2(), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
