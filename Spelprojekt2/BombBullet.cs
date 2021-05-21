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
    public class BombBullet : Bullet
    {
        protected float bombRadius;
        public BombBullet(ProjectileTower owner, float velocity, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, DestroyBulletCallback destroyCallback, float bombRadius) : base(owner, velocity, position, lookDirection, lookRotation, damage, destroyCallback)
        {
            this.bombRadius = bombRadius;
        }
        public override void Update(GameTime gameTime)
        {
            Position += lookDirection * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Global.gameSpeed;
            rectangle = new Rectangle(Position.ToPoint() - new Point(Assets.Bullet.Height / 2, Assets.Bullet.Height / 2), new Point(Assets.Bullet.Height, Assets.Bullet.Height));
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time >= TimeAlive || Vector2.Distance(Owner.Position, Position) >= Owner.reach)
            {
                time = 0f;
                destroyCallback(this); // Destroy bullet
            }
            if (BombCollision())
            {
                destroyCallback(this);
            }
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
                dmg = collided.Hit(50 * Damage + Owner.Damage * Owner.DamageModifier);
                Owner.TotalDamage += dmg;

                for (int i = 0; i < Main.instance.level.Enemies.Count; i++)
                {
                    var enemy = Main.instance.level.Enemies[i];
                    float distance = Vector2.Distance(Position, enemy.position);
                    if (collided != enemy && distance <= bombRadius)
                    {
                        distance = MathHelper.Clamp(distance, 1f, float.MaxValue);
                        dmg = enemy.Hit(50 * (Damage + Owner.Damage * Owner.DamageModifier) / distance);
                        Owner.TotalDamage += (float)dmg;
                    }
                }
                return true;
            }
            return false;
        }
        
    }
}
