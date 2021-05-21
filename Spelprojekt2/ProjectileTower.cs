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
        public List<Bullet> BulletDestroyQueue { get; protected set; }

        private double timeSinceFired = 0f;

        protected float spreadModifier;
        protected float spread;

        public float reach;

        protected bool canFire;

        public ProjectileTower(Vector2 position, float damage, float fireRate, float spread, float turnSpeed, float range, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin) : base(position, damage, fireRate, turnSpeed, range, bodySprite, bodyOrigin, headSprite, headOrigin)
        {
            Bullets = new List<Bullet>();
            BulletDestroyQueue = new List<Bullet>();
            spreadModifier = 1;
            this.spread = spread;
            reach = 300;
            canFire = false;
        }

        public override void Update(GameTime gameTime)
        {
            canFire = Target != null;
            float fireTime = (1f / FireRate) / Global.gameSpeed;
            timeSinceFired = timeSinceFired >= fireTime && !canFire ? fireTime : timeSinceFired + gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timeSinceFired >= fireTime && canFire)
            {
                timeSinceFired = 0f;
                Fire();
            }

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
            float Offset = spread * (Global.ran.Next(0, 2) == 1 ? 1 : -1) * spreadModifier * (float)Global.ran.NextDouble();

            Vector2 dir = new Vector2((float)Math.Cos(LookRotation), (float)Math.Sin(LookRotation));
            firePosition = Position + dir * cannonLength;

            Vector2 bulletDir = new Vector2((float)Math.Cos(LookRotation + Offset), (float)Math.Sin(LookRotation + Offset));

            Bullet bullet = new Bullet(this, 200f, firePosition, bulletDir, LookRotation + Offset, 1, DestroyBullet);
            Bullets.Add(bullet);
        }

        public override void Draw()
        {
            base.Draw();
            if (debug)
            {
                Main.spriteBatch.Draw(debugFirePoint, firePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
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
