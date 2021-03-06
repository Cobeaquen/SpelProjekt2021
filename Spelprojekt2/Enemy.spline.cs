//using MonoGame.SplineFlower;
//using MonoGame.SplineFlower.Spline;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Spelprojekt2
//{
//    public class Enemy2 : SplineWalker
//    {
//        public float speed;

//        public int value { get; private set; }
//        public float maxHP { get; private set; }
//        public float HP;

//        public Bar hpBar;

//        public float masterResistance = 1f;
//        public float laserResistance;
//        public float gunResistance;
//        public float bombResistance;
//        public float sniperResistance;

//        public Texture2D sprite;
//        public Texture2D textrect;
//        public Rectangle rectangle;
//        public Vector2 position;

//        public float LookRotation { get; private set; }
//        public float t = 0;
//        private float progress;
//        private float progressPerEdge;
//        private Vector2 hpOffset = new Vector2(0, -15);

//        public Enemy()
//        {
//            maxHP = 50;
//            HP = maxHP;
//            value = 10;
//            hpBar = new Bar(maxHP, 0.5f, Assets.HPBarFrame, 24, 4);
//            sprite = DebugTextures.GenerateRectangle(20, 20, Color.Brown);
//            CreateSplineWalker(Main.instance.level.splinePath, SplineWalkerMode.PingPong, 2);
//            position = GetPositionOnCurve(t);
//            progressPerEdge = 1f / (Main.instance.level.splinePath.GetAllPoints.Length - 1);
//            progress = progressPerEdge;
//            rectangle = new Rectangle(position.ToPoint() - new Point(sprite.Height / 2, sprite.Height / 2), new Point(sprite.Height, sprite.Height));

//            textrect = DebugTextures.GenerateHollowRectangele(rectangle.Width, rectangle.Height, 1, Color.Red);
//        }
//        public override void CreateSplineWalker(SplineBase spline, SplineWalkerMode mode, int duration, bool canTriggerEvents = true, SplineWalkerTriggerDirection triggerDirection = SplineWalkerTriggerDirection.Forward, bool autoStart = true)
//        {
//            base.CreateSplineWalker(spline, mode, duration, canTriggerEvents, triggerDirection, autoStart);
//        }
//        public override void Update(GameTime gameTime)
//        {
//            position = GetPositionOnCurve(t);
//            //LookRotation = (float)Math.Atan2(dir.Y, dir.X);
//            rectangle = new Rectangle(position.ToPoint() - new Point(sprite.Height / 2, sprite.Height / 2), new Point(sprite.Width, sprite.Height));
//            //Console.WriteLine("Progress per edge: " + progressPerEdge);
            
//            //position
//            t += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.1f;
//            t = t > 1f ? 0f : t;

//            if (t > progress)
//            {
//                //progress = Math.Round(progress) * progressPerEdge;
//            }

//            hpBar.Update(gameTime, position + hpOffset);

//            base.Update(gameTime);
//        }

//        public float Hit(float damage)
//        {
//            float dmg = damage / masterResistance;
//            HP -= dmg;
//            if (HP <= 0)
//                Die();

//            hpBar.SetValue(Math.Max(HP, 0f));
//            return Math.Min(dmg, HP + dmg);
//        }

//        public void Die()
//        {
//            Global.Coins += value;
//            Destroy();
//        }

//        public void Draw()
//        {
//            Main.spriteBatch.Draw(sprite, position, null, Color.White, LookRotation, new Vector2(sprite.Width / 2, sprite.Height / 2), 1f, SpriteEffects.None, 0f);
//            Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
//            //base.Draw(Main.spriteBatch);
//        }

//        public void Destroy()
//        {
//            Main.instance.level.Enemies.Remove(this);
//        }

//        public static void DrawHPBars()
//        {
//            Main.spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
//            foreach (var enemy in Main.instance.level.Enemies)
//            {
//                enemy.hpBar.Draw();
//            }
//            Main.spriteBatch.End();
//        }
//    }
//}
