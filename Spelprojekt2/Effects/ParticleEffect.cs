using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.Effects
{
    public class ParticleEffect
    {
        public float timeActive;
        public Vector2 position;

        protected Color color;
        protected Effect effect;
        protected Vector2 scale;
        protected float time;

        private Texture2D sprite;
        private Vector2 origin;

        public ParticleEffect(Vector2 position, float timeActive, Texture2D sprite, Vector2 origin, Vector2 scale, Effect effect)
        {
            this.position = position;
            this.timeActive = timeActive;
            this.sprite = sprite;
            this.origin = origin;
            this.effect = effect;
            this.color = Color.White;
            this.scale = scale;
        }
        public virtual void Update(GameTime gameTime)
        {
            if (time >= timeActive)
            {
                Destroy();
                return;
            }
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        void Destroy()
        {
            Global.Effects.Remove(this);
        }

        public virtual void Draw()
        {
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, effect: effect);
            Main.spriteBatch.Draw(sprite, position, null, color, 0f, origin, scale, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
        }
    }
}
