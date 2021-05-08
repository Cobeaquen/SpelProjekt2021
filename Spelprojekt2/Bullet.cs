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
        public ProjectileTower Owner { get; private set; }
        public float Velocity { get; private set; }
        public float TimeAlive { get; private set; } = 2f;
        public float Damage { get; private set; }
        public Vector2 Position { get; private set; }
        
        public delegate void DestroyBulletCallback(Bullet bullet);
        private DestroyBulletCallback destroyCallback;
        private Rectangle rectangle;
        
        private Texture2D sprite;
        private Texture2D textrect;

        private float lookRotation;
        private Vector2 lookDirection;
        private float time;
        
        private float rotOffset = MathHelper.PiOver2;

        public Bullet(ProjectileTower owner, float velocity, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, DestroyBulletCallback destroyCallback)
        {
            this.Owner = owner;
            this.Velocity = velocity;
            this.Position = position;
            this.lookDirection = lookDirection;
            this.lookRotation = lookRotation;
            this.destroyCallback = destroyCallback;
            this.Damage = damage;
            textrect = DebugTextures.GenerateHollowRectangele(Assets.Bullet.Height, Assets.Bullet.Height, 1, Color.Red);
        }

        public void Update(GameTime gameTime)
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

        public Enemy CollisionCheck()
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
            Main.spriteBatch.Draw(Assets.Bullet, Position, null, Color.White, lookRotation + rotOffset, Assets.BulletOrigin, 1f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
