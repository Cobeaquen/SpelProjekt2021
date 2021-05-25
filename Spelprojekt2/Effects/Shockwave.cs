using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.Effects
{
    public class Shockwave : ParticleEffect
    {
        public float radius;
        public Shockwave(Vector2 position, float timeActive, float radius) : base(position, timeActive, DebugTextures.pixel, new Vector2(0.5f, 0.5f), new Vector2(radius * 2, radius * 2), Assets.ShockwaveEffect)
        {
            this.radius = radius;
        }
        public override void Draw()
        {
            Assets.ShockwaveEffect.Parameters["time"].SetValue(time);
            base.Draw();
        }
    }
}
