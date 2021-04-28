using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2
{
    public class ProjectileTower : Tower
    {
        public float TotalDamage { get; set; }
        public List<Bullet> Bullets { get; private set; }
        public List<Bullet> BulletDestroyQueue { get; private set; }

        private double timeSinceFired = 0f;

        public ProjectileTower(Vector2 position, float fireRate, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin) : base(position, 5f, fireRate, bodySprite, bodyOrigin, headSprite, headOrigin)
        {
            Bullets = new List<Bullet>();
            BulletDestroyQueue = new List<Bullet>();
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceFired += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceFired >= 1f/FireRate)
            {
                timeSinceFired = 0f;
                Fire();
            }

            Vector2 dir = new Vector2((float)Math.Cos(LookRotation), (float)Math.Sin(LookRotation));
            firePosition = Position + dir * cannonLength;

            foreach (var bullet in Bullets)
            {
                bullet.Update(gameTime);
            }
            foreach (var bullet in BulletDestroyQueue)
            {
                Bullets.Remove(bullet);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Fires a projectile in the turret's look direction
        /// </summary>
        /// <returns></returns>
        public virtual void Fire()
        {
            //Console.WriteLine("Fire!");
            Vector2 dir = new Vector2((float)Math.Cos(LookRotation), (float)Math.Sin(LookRotation));
            firePosition = Position + dir * cannonLength;

            Bullet bullet = new Bullet(this, firePosition, dir, LookRotation, 1, DestroyBullet);
            Bullets.Add(bullet);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.Draw(debugFirePoint, firePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            foreach (var bullet in Bullets)
            {
                bullet.Draw();
            }
        }
        
        protected virtual void DestroyBullet(Bullet bullet)
        {
            BulletDestroyQueue.Add(bullet);
        }
    }
}
