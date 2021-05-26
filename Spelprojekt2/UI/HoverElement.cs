using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.UI
{
    public class HoverElement : UIElement
    {
        public delegate void MouseOver(Vector2 position);
        public MouseOver mouseOver;
        
        public List<UIElement> elements;
        public Rectangle bounds;

        public TextureElement background;

        private bool display;
        public HoverElement(Vector2 position, MouseOver mouseOver, Rectangle bounds, List<UIElement> elements) : base(position)
        {
            this.mouseOver = mouseOver;
            this.bounds = bounds;
            this.elements = elements;

            background = new TextureElement(position, DebugTextures.GenerateRectangle(150, 75, Color.Gray), new Vector2(150, 0), Color.White);
            display = false;
        }
        public override void Update()
        {
            display = false;
            if (bounds.Contains(Input.MousePosition))
            {
                background.position = Input.MousePosition.ToPoint().ToVector2();
                foreach (var e in elements)
                {
                    e.offsetPosition = Input.MousePosition.ToPoint().ToVector2();
                }
                Console.WriteLine("Hover");
                display = true;
            }
            base.Update();
        }
        public override void Draw()
        {
            if (!display)
                return;
            background.Draw();
            foreach (var e in elements)
            {
                e.Draw();
            }
            base.Draw();
        }
    }
}
