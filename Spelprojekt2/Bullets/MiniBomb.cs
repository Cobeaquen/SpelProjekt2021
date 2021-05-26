using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt2.Enemies;

namespace Spelprojekt2.Bullets
{
    public class MiniBomb : BombBullet
    {
        public MiniBomb(BombTower owner, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, HitCallback destroyCallback, Enemy avoid, int miniBombs) : base(owner, position, lookDirection, lookRotation, damage, destroyCallback, miniBombs)
        {
            collided.Add(avoid);
            TimeAlive = 0.5f;
            radiusModifier = 0.5f;
        }
        public override float GetDamage()
        {
            return base.GetDamage() * 0.5f;
        }
        public override void Draw()
        {
            Main.spriteBatch.Draw(sprite, Position, null, Color.White, lookRotation + rotOffset, spriteOrigin, 0.5f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
