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
        public BombBullet(ProjectileTower owner, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, HitCallback destroyCallback, float bombRadius, int miniBombs) : base(owner, 100f, position, lookDirection, lookRotation, damage, 1, destroyCallback, Assets.BombBullet, Assets.BombBulletOrigin)
        {
            this.bombRadius = bombRadius;
            this.miniBombs = miniBombs;
        }
        public override void Update(GameTime gameTime)
        {
            Position += lookDirection * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Global.gameSpeed;
            rectangle = new Rectangle(Position.ToPoint() - new Point(Assets.Bullet.Height / 2, Assets.Bullet.Height / 2), new Point(Assets.Bullet.Height, Assets.Bullet.Height));
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time >= TimeAlive || Vector2.Distance(Owner.Position, Position) >= Owner.reach)
            {
                time = 0f;
                hitCallback(this); // Destroy bullet
            }
            if (BombCollision())
            {
                hitCallback(this);
            }

            lookRotation += 20f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (enemy != null)
            //{
            //    float dmg = enemy.Hit(Damage + Owner.Damage * Owner.DamageModifier);
            //    Owner.TotalDamage += dmg;
            //    Console.WriteLine("Hit enemy for {0} HP", dmg);
            //    destroyCallback(this);
            //}
        }
        public override Enemy CollisionCheck()
        {
            foreach (var enemy in Main.instance.level.Enemies)
            {
                bool collided = rectangle.Intersects(enemy.rectangle);

                if (collided)
                {
                    return enemy;
                }
            }
            return null;
        }
        public bool BombCollision()
        {
            Enemy collided = CollisionCheck();
            float dmg = 0;
            if (collided != null)
            {
                dmg = collided.Hit(Damage + Owner.Damage * Owner.DamageModifier);
                Owner.TotalDamage += dmg;

                for (int i = 0; i < Main.instance.level.Enemies.Count; i++)
                {
                    var enemy = Main.instance.level.Enemies[i];
                    float distance = Vector2.Distance(Position, enemy.position);
                    if (collided != enemy && distance <= bombRadius)
                    {
                        distance = MathHelper.Clamp(distance, 1f, float.MaxValue);
                        dmg = enemy.Hit((Damage + Owner.Damage * Owner.DamageModifier) * (1 - distance / bombRadius));
                        Owner.TotalDamage += (float)dmg;
                    }
                }
                for (float i = 0; i < 2*MathHelper.Pi; i += (2*MathHelper.Pi)/miniBombs)
                {
                    Vector2 dir = new Vector2((float)Math.Cos(i), (float)Math.Sin(i));
                    MiniBomb bomb = new MiniBomb(Owner, Position, dir, 0f, 2f, hitCallback, 30);
                    Owner.Bullets.Add(bomb);
                }
                return true;
            }
            return false;
        }
        
    }
}
