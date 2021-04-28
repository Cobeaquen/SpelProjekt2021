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
        public float Velocity { get; private set; } = 100f;
        public float TimeAlive { get; private set; } = 2f;

        public float Damage { get; private set; }
        public Vector2 Position { get; private set; }
        
        public delegate void DestroyBulletCallback(Bullet bullet);
        private DestroyBulletCallback destroyCallback;
        private Rectangle rectangle;
        
        private Texture2D sprite;

        private float lookRotation;
        private Vector2 lookDirection;
        private float time;

        private float rotOffset = MathHelper.PiOver2;

        public Bullet(Vector2 position, Vector2 lookDirection, float lookRotation, float damage, DestroyBulletCallback destroyCallback)
        {
            this.Position = position;
            this.lookDirection = lookDirection;
            this.lookRotation = lookRotation;
            this.destroyCallback = destroyCallback;
            this.Damage = damage;
        }

        public void Update(GameTime gameTime)
        {
            Position += lookDirection * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rectangle = new Rectangle(Position.ToPoint(), new Point(Assets.Bullet.Height, Assets.Bullet.Height));
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time >= TimeAlive)
            {
                time = 0f;
                destroyCallback(this); // Destroy bullet
            }
            Enemy enemy = CollisionCheck();
            enemy?.Destroy();
        }

        public Enemy CollisionCheck()
        {
            foreach (var enemy in Main.instance.level.Enemies)
            {
                bool collided = rectangle.Intersects(enemy.rectangle);

                if (collided)
                {
                    Console.WriteLine("Collided!!");
                    return enemy;
                }
            }
            return null;
        }

        public void Draw()
        {
            Main.spriteBatch.Draw(Assets.Bullet, Position, null, Color.White, lookRotation + rotOffset, Assets.BulletOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}
