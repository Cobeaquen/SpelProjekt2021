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
    public class Bullet
    {
        public ProjectileTower Owner { get; protected set; }
        public float Velocity { get; protected set; }
        public float TimeAlive { get; protected set; } = 2f;
        public float Damage { get; protected set; }
        public Vector2 Position { get; set; }
        
        public delegate void DestroyBulletCallback(Bullet bullet);
        protected DestroyBulletCallback destroyCallback;
        protected Rectangle rectangle;
        
        protected Texture2D sprite;
        protected Vector2 spriteOrigin;
        protected Texture2D textrect;

        protected float lookRotation;
        protected Vector2 lookDirection;
        protected float time;
        
        private float rotOffset = MathHelper.PiOver2;

        public Bullet(ProjectileTower owner, float velocity, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, DestroyBulletCallback destroyCallback, Texture2D sprite, Vector2 spriteOrigin)
        {
            this.Owner = owner;
            this.Velocity = velocity;
            this.Position = position;
            this.lookDirection = lookDirection;
            this.lookRotation = lookRotation;
            this.destroyCallback = destroyCallback;
            this.Damage = damage;
            this.sprite = sprite;
            this.spriteOrigin = spriteOrigin;
            textrect = DebugTextures.GenerateHollowRectangele(Assets.Bullet.Height, Assets.Bullet.Height, 1, Color.Red);
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += lookDirection * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Global.gameSpeed;
            rectangle = new Rectangle(Position.ToPoint() - new Point(Assets.Bullet.Height / 2, Assets.Bullet.Height / 2), new Point(Assets.Bullet.Height, Assets.Bullet.Height));
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time >= TimeAlive || Vector2.Distance(Owner.Position, Position) >= Owner.reach)
            {
                time = 0f;
                destroyCallback(this); // Destroy bullet
            }

            Enemy enemy = CollisionCheck();
            if (enemy != null)
            {
                float dmg = enemy.Hit(Damage + Owner.Damage * Owner.DamageModifier);
                Owner.TotalDamage += dmg;
                Console.WriteLine("Hit enemy for {0} HP", dmg);
                destroyCallback(this);
            }

        }

        public virtual Enemy CollisionCheck()
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

        public void Draw()
        {
            Main.spriteBatch.Draw(sprite, Position, null, Color.White, lookRotation + rotOffset, spriteOrigin, 1f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
