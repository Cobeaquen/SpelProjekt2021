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
    public class BombBullet : Bullet
    {
        public float bombRadius;
        public int miniBombs;
        public BombBullet(ProjectileTower owner, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, HitCallback destroyCallback, float bombRadius, int miniBombs) : base(owner, 100f, position, lookDirection, lookRotation, damage, 0, destroyCallback, Assets.BombBullet, Assets.BombBulletOrigin)
        {
            this.bombRadius = bombRadius;
            this.miniBombs = miniBombs;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            lookRotation += 20f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public override float GetDamage()
        {
            return base.GetDamage();
        }
        public override Enemy CollisionCheck()
        {
            return base.CollisionCheck();
        }
        public override void Hit(Enemy enemy, float damage)
        {
            for (int i = 0; i < Main.instance.level.Enemies.Count; i++)
            {
                var e = Main.instance.level.Enemies[i];
                float distance = Vector2.Distance(Position, e.position);
                if (enemy != e && distance <= bombRadius)
                {
                    distance = MathHelper.Clamp(distance, 1f, float.MaxValue);
                    damage = e.Hit(GetDamage() * (1 - distance / bombRadius));
                    Owner.TotalDamage += (float)damage;
                }
            }
            if (miniBombs != 0)
            {
                for (float i = 0; i < 2 * MathHelper.Pi; i += (2 * MathHelper.Pi) / miniBombs)
                {
                    Vector2 dir = new Vector2((float)Math.Cos(i), (float)Math.Sin(i));
                    MiniBomb bomb = new MiniBomb(Owner, Position, dir, 0f, 0f, hitCallback, 30, enemy);
                    Owner.Bullets.Add(bomb);
                }
            }
            base.Hit(enemy, damage);
        }
    }
}
