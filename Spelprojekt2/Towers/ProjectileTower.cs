using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2.Towers
{
    public class ProjectileTower : Tower
    {
        public List<Bullet> Bullets { get; private set; }
        public List<Bullet> BulletDestroyQueue { get; protected set; }

        protected float spreadModifier;
        protected float spread;

        public float reach;

        public ProjectileTower(Vector2 position, TowerInfo towerInfo, float damage, float fireRate, float spread, float turnSpeed, float range, int pierce, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin, int path, int tier) : base(position, towerInfo, damage, fireRate, pierce, turnSpeed, range, bodySprite, bodyOrigin, headSprite, headOrigin, path, tier)
        {
            Bullets = new List<Bullet>();
            BulletDestroyQueue = new List<Bullet>();
            spreadModifier = 1;
            this.spread = spread;
            reach = 300;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update(gameTime);

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
        public override void Fire()
        {
            base.Fire();
        }
        protected Vector2 GetBulletDirection(out float offset)
        {
            offset = spread * (Global.ran.Next(0, 2) == 1 ? 1 : -1) * spreadModifier * (float)Global.ran.NextDouble();
            return new Vector2((float)Math.Cos(LookRotation + offset), (float)Math.Sin(LookRotation + offset));
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
        
        protected virtual void Hit(Bullet bullet)
        {
            BulletDestroyQueue.Add(bullet);
        }
    }
}
