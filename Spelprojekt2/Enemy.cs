using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spelprojekt2
{
    public class Enemy
    {
        public float speed;

        public int value { get; private set; }
        public float maxHP { get; private set; }
        [JsonIgnore]
        public float HP;

        [JsonIgnore]
        public Bar hpBar;

        public float masterResistance = 1f;
        public float laserResistance;
        public float gunResistance;
        public float bombResistance;
        public float sniperResistance;

        [JsonIgnore]
        public Texture2D sprite;
        [JsonIgnore]
        public Texture2D textrect;
        [JsonIgnore]
        public Rectangle rectangle;
        [JsonIgnore]
        public Vector2 position;

        [JsonIgnore]
        public float LookRotation { get; private set; }
        [JsonIgnore]
        public float t = 0;
        [JsonIgnore]
        public int progress { get; private set; }
        [JsonIgnore]
        private Vector2 hpOffset = new Vector2(0, -15);

        public Enemy(float maxHP, int value, float speed, Texture2D sprite)
        {
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.value = value;
            this.speed = speed;
            hpBar = new Bar(maxHP, 0.5f, Assets.HPBarFrame, 24, 4);
            this.sprite = sprite;// DebugTextures.GenerateRectangle(20, 20, Color.Brown);
            progress = 1;
            rectangle = new Rectangle(position.ToPoint() - new Point(sprite.Height / 2, sprite.Height / 2), new Point(sprite.Height, sprite.Height));

            textrect = DebugTextures.GenerateHollowRectangele(rectangle.Width, rectangle.Height, 1, Color.Red);
        }
        public void Update(GameTime gameTime)
        {
            //LookRotation = (float)Math.Atan2(dir.Y, dir.X);
            rectangle = new Rectangle(position.ToPoint() - new Point(sprite.Height / 2, sprite.Height / 2), new Point(sprite.Width, sprite.Height));
            //Console.WriteLine("Progress per edge: " + progressPerEdge);

            t += (float)gameTime.ElapsedGameTime.TotalSeconds / Main.instance.level.waypoints[progress].length * speed * Global.gameSpeed;
            if (t > 1f)
            {
                t = 0f;
                progress++;
            }
            position = Main.instance.level.GetPosition(progress, t, out bool outOfBounds);
            if (outOfBounds)
            {
                ReachEnd();
            }

            hpBar.Update(gameTime, position + hpOffset);
        }

        public void ReachEnd()
        { // Förstör fienden och förlora liv
            Global.HP -= value;
            Destroy();
            return;
        }

        public float Hit(float damage)
        {
            float dmg = damage / masterResistance;
            HP -= dmg;
            if (HP <= 0)
                Die();

            hpBar.SetValue(Math.Max(HP, 0f));
            return Math.Min(dmg, HP + dmg);
        }

        public void Die()
        {
            Global.Coins += value;
            Destroy();
        }

        public virtual void Draw()
        {
            Main.spriteBatch.Draw(sprite, position, null, Color.White, LookRotation, new Vector2(sprite.Width / 2, sprite.Height / 2), 1f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //base.Draw(Main.spriteBatch);
        }

        public void Destroy()
        {
            Main.instance.level.Enemies.Remove(this);
        }

        public static void DrawHPBars()
        {
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
            foreach (var enemy in Main.instance.level.Enemies)
            {
                enemy.hpBar.Draw();
            }
            Main.spriteBatch.End();
        }
    }
}
