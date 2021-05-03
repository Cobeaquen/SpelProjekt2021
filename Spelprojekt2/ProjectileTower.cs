﻿using System;
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

        Random ran = new Random();

        float spreadModifier;
        float spread;

        public float reach;

        public ProjectileTower(Vector2 position, float fireRate, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin) : base(position, 5f, fireRate, bodySprite, bodyOrigin, headSprite, headOrigin)
        {
            Bullets = new List<Bullet>();
            BulletDestroyQueue = new List<Bullet>();
            spreadModifier = 1f;
            spread = 1f;
            reach = 200;
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceFired += gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timeSinceFired >= 1f/FireRate)
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
            float Offset = spread * (ran.Next(0, 2) == 1 ? 1 : -1) * spreadModifier * (float)ran.NextDouble();

            Vector2 dir = new Vector2((float)Math.Cos(LookRotation + Offset), (float)Math.Sin(LookRotation + Offset));
            firePosition = Position + dir * cannonLength;

            Bullet bullet = new Bullet(this, 200f, firePosition, dir, LookRotation, 1, DestroyBullet);
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
