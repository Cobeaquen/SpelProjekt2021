using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spelprojekt2.Collision;

namespace Spelprojekt2.Enemies
{
    public class Enemy
    {
        public float speed;

        public int value { get; private set; }
        public float maxHP { get; private set; }
        [JsonIgnore]
        public float HP { get; private set; }

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
        public Vector2 origin;
        [JsonIgnore]
        public Texture2D textrect;
        [JsonIgnore]
        public Rectangle rectangle;
        [JsonIgnore]
        public Vector2 position;
        [JsonIgnore]
        public RectCollider collider;

        [JsonIgnore]
        public float LookRotation { get; private set; }
        private float prevRotation;
        private float targetRotation;
        [JsonIgnore]
        public float t = 0;
        [JsonIgnore]
        public int progress { get; private set; }
        [JsonIgnore]
        private Vector2 hpOffset = new Vector2(0, -15);

        private const float turnSpeedScalar = 0.04f;

        public Enemy(float maxHP, int value, float speed, Texture2D sprite, Vector2 origin)
        {
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.value = value;
            this.speed = speed;
            hpBar = new Bar(maxHP, 0.2f, Assets.HPBarFrame, 24, 4);
            this.sprite = sprite;// DebugTextures.GenerateRectangle(20, 20, Color.Brown);
            this.origin = origin;
            progress = 1;
            rectangle = new Rectangle(position.ToPoint() - new Point(sprite.Height / 2, sprite.Height / 2), new Point(sprite.Height, sprite.Height));
            LookRotation = (float)Math.Atan2(Main.instance.level.StartDirection.Y, Main.instance.level.StartDirection.X) - MathHelper.Pi;

            collider = new RectCollider(rectangle);

            textrect = DebugTextures.GenerateHollowRectangele(rectangle.Width, rectangle.Height, 1, Color.Red);
        }
        public void Update(GameTime gameTime)
        {
            //LookRotation = (float)Math.Atan2(dir.Y, dir.X);
            rectangle = new Rectangle(position.ToPoint() - new Point(sprite.Height / 2, sprite.Height / 2), new Point(sprite.Width, sprite.Height));
            collider.UpdateEdges(rectangle);

            t += (float)gameTime.ElapsedGameTime.TotalSeconds / Main.instance.level.waypoints[progress].length * speed * Global.gameSpeed;
            if (t > 1f)
            {
                t = 0f;
                progress++;
                Vector2 dir = Main.instance.level.GetDirection(progress);
                targetRotation = (float)Math.Atan2(dir.Y, dir.X) - MathHelper.Pi;
            }
            position = Main.instance.level.GetPosition(progress, t, out bool outOfBounds);
            if (outOfBounds)
            {
                ReachEnd();
            }
            if (LookRotation != targetRotation)
            {
                float angleDiff = -Global.GetShortestAngle(LookRotation, targetRotation);

                if (Math.Abs(angleDiff) < 0.01f)
                {
                    LookRotation = targetRotation;
                }
                else
                {
                    LookRotation -= speed * turnSpeedScalar * Math.Sign(angleDiff) * (float)gameTime.ElapsedGameTime.TotalSeconds * Global.gameSpeed;
                }
            }
            prevRotation = LookRotation;

            hpBar.Update(gameTime, position + hpOffset);
        }

        public void ReachEnd()
        { // Förstör fienden och förlora liv
            Global.HP -= (int)HP;
            Destroy();
            return;
        }

        public float Hit(float damage, Bullet bullet)
        {
            float dmg = damage / masterResistance;
            HP -= dmg;
            if (HP <= 0)
            {
                float modifier = bullet == null ? 1 : bullet.Owner.MoneyModifier;
                Die(modifier);
            }

            hpBar.SetValue(Math.Max(HP, 0f));
            return Math.Min(dmg, HP + dmg);
        }

        public void Die(float modifier)
        {
            Global.Coins += (int)(value * modifier);
            Destroy();
        }

        public virtual void Draw()
        {
            Main.spriteBatch.Draw(sprite, position, null, Color.White, LookRotation, origin, 1f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(textrect, rectangle.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //base.Draw(Main.spriteBatch);
        }

        public void Destroy()
        {
            Main.instance.level.enemies.Remove(this);
            if (Main.instance.level.enemies.Count == 0 && Global.gameState == Global.GameState.DoneWave)
            { // Den sista fienden har dött (eller tagit sig igenom) - waven är över
                Global.gameState = Global.GameState.Idle;
                GUI.WaveStartToggle.texture = Assets.StartWave;
                Main.instance.level.wave++;
            }
        }

        public static void DrawHPBars()
        {
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
            foreach (var enemy in Main.instance.level.enemies)
            {
                enemy.hpBar.Draw();
            }
            Main.spriteBatch.End();
        }
    }
}
