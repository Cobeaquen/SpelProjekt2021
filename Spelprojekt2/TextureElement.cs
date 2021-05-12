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
        protected Texture2D texture;
        protected Vector2 origin;
        public TextureElement(Vector2 position, Texture2D texture, Vector2 origin) : base(position)
        {
            this.texture = texture;
            this.origin = origin;
        }
        public override void Draw()
        {
            Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
