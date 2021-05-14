using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class TextureElement : UIElement
    {
        public Texture2D texture { get; set; }
        protected Vector2 origin;
        public TextureElement(Vector2 position, Texture2D texture, Vector2 origin) : base(position)
        {
            this.texture = texture;
            this.origin = origin;
        }
        public override void Update()
        {

        }
        public override void Draw()
        {
            Main.spriteBatch.Draw(texture, position + offsetPosition, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
