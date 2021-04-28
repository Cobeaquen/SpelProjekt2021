using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class Bar
    {
        public Vector2 Position;

        public float Value;
        public float MaxValue;
        public Texture2D sprite;
        private Texture2D sprite2;
        private Vector2 spriteOrigin;

        public Bar(float maxValue)
        {
            this.MaxValue = maxValue;
            Value = 1f;
            sprite = DebugTextures.GenerateRectangle(32, 4, Color.White);
            sprite2 = DebugTextures.GenerateRectangle(32, 4, Color.White);
            spriteOrigin = Assets.GetOrigin(sprite);
        }

        public void SetValue(float value)
        {
            this.Value = value / MaxValue;
        }

        public void Draw()
        {
            Assets.HPBarEffect.Parameters["value"].SetValue(Value);
            foreach (var pass in Assets.HPBarEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }
            Main.spriteBatch.Draw(sprite, Position, null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(sprite2, Position + new Vector2(100, 100), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 0f);

            
        }
    }
}
