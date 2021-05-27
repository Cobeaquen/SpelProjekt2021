using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.UI
{
    public class TextureElement : UIElement
    {
        public Color color;
        public float rotation;
        public Texture2D texture { get; set; }
        protected Vector2 origin;
        public TextureElement(Vector2 position, Texture2D texture, Vector2 origin, Color color, float rotation = 0f) : base(position)
        {
            this.texture = texture;
            this.origin = origin;
            this.color = color;
            this.rotation = rotation;
        }
        public override void Update()
        {

        }
        public override void Draw()
        {
            if (texture == null)
                return;
            Main.spriteBatch.Draw(texture, (position + offsetPosition).ToPoint().ToVector2(), null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
