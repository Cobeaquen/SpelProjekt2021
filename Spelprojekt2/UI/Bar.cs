using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.UI
{
    public class Bar
    {
        public Vector2 Position { get; set; }

        public float Value;
        public float MaxValue;
        public float Smoothing;
        public Texture2D sprite;
        private Vector2 spriteOrigin;

        private float dispValue;
        private float prevValue;
        private float t;

        public Bar(float maxValue, float smoothing = 0f, Texture2D sprite = null, int width = 32, int height = 4)
        {
            this.MaxValue = maxValue;
            this.Smoothing = smoothing;
            prevValue = 1f;
            Value = 1f;
            t = 1f;
            if (sprite == null)
                sprite = DebugTextures.GenerateRectangle(width, height, Color.White);
            else
                this.sprite = sprite;
            spriteOrigin = Assets.GetOrigin(sprite);
        }

        public void SetValue(float value)
        {
            prevValue = this.dispValue;
            this.Value = value / MaxValue;
            t = 0f;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.Position = position;
            t = t < 1.0f ? t + (float)gameTime.ElapsedGameTime.TotalSeconds / Smoothing : 1f;
        }

        public void Draw()
        {
            dispValue = MathHelper.Lerp(prevValue, Value, t);
            Assets.HPBarEffect.Parameters["value"].SetValue(dispValue);
            foreach (var pass in Assets.HPBarEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }
            Main.spriteBatch.Draw(sprite, Position, null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}
