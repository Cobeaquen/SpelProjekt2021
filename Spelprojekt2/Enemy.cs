using MonoGame.SplineFlower;
using MonoGame.SplineFlower.Spline;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class Enemy : SplineWalker
    {
        public float speed;
        
        public float maxHP;
        public float health;

        public float laser_resistance;
        public float gun_resistance;
        public float bomb_resistance;
        public float sniper_resistance;

        public Texture2D sprite;

        public Vector2 position;
        public float t = 0;
        private float progress;
        private float progressPerEdge;

        public Rectangle rectangle;
        public Enemy()
        {
            sprite = DebugTextures.GenerateRectangle(20, 20, Color.Brown);
            CreateSplineWalker(Main.instance.level.splinePath, SplineWalkerMode.PingPong, 2);
            position = GetPositionOnCurve(t);
            progressPerEdge = 1f / (Main.instance.level.splinePath.GetAllPoints.Length - 1);
            progress = progressPerEdge;
        }
        public override void CreateSplineWalker(SplineBase spline, SplineWalkerMode mode, int duration, bool canTriggerEvents = true, SplineWalkerTriggerDirection triggerDirection = SplineWalkerTriggerDirection.Forward, bool autoStart = true)
        {
            base.CreateSplineWalker(spline, mode, duration, canTriggerEvents, triggerDirection, autoStart);
        }
        public override void Update(GameTime gameTime)
        {
            position = GetPositionOnCurve(t);
            rectangle = new Rectangle(position.ToPoint(), new Point(sprite.Width, sprite.Height));
            //Console.WriteLine("Progress per edge: " + progressPerEdge);
            
            //position
            t += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.1f;
            t = t > 1f ? 0f : t;

            if (t > progress)
            {
                //progress = Math.Round(progress) * progressPerEdge;
            }
            SetPosition(t);
            base.Update(gameTime);
        }

        public void Draw()
        {
            Main.spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //base.Draw(Main.spriteBatch);
        }

        public void Destroy()
        {
            Main.instance.level.Enemies.Remove(this);
        }
    }
}
