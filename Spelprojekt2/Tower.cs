using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt2
{
    public class Tower
    {
        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position;
        public float LookRotation { get { return lookRotation; } set { lookRotation = value; } }
        private float lookRotation;
        private float rotOffset = MathHelper.PiOver2;
        private Texture2D bodySprite;
        private Vector2 bodyOrigin;
        private Texture2D headSprite;
        private Vector2 headOrigin;

        public Tower(Vector2 position, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin)
        {
            this.Position = position;
            this.LookRotation = 0;
            this.bodySprite = bodySprite;
            this.bodyOrigin = bodyOrigin;
            this.headSprite = headSprite;
            this.headOrigin = headOrigin;
        }

        public virtual void Update(GameTime gameTime)
        {
            Vector2 mouseDir = Mouse.GetState().Position.ToVector2() / 4f - position;
            LookRotation = (float)Math.Atan2(mouseDir.Y, mouseDir.X);
        }

        private static float GetShortestAngle(float from, float to)
        {
            float max_angle = MathHelper.TwoPi;
            float difference = (to - from) % max_angle;
            return ((2 * difference) % max_angle) - difference;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            // Draw body
            sb.Draw(bodySprite, position, null, Color.White, 0f, bodyOrigin, 1f, SpriteEffects.None, 0f);

            // Draw head
            sb.Draw(headSprite, position, null, Color.White, LookRotation + rotOffset, headOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}
