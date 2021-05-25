using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt2.Bullets
{
    class MiniBomb : BombBullet
    {
        public MiniBomb(ProjectileTower owner, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, HitCallback destroyCallback, float bombRadius) : base(owner, position, lookDirection, lookRotation, damage, destroyCallback, bombRadius, 0)
        {

        }
        public override void Draw()
        {
            Main.spriteBatch.Draw(sprite, Position, null, Color.White, lookRotation + rotOffset, spriteOrigin, 0.5f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
