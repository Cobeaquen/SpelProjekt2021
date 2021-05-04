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
    public class Tower
    {
        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position;
        protected Vector2 firePosition;
        public float LookRotation { get { return lookRotation; } set { lookRotation = value; } }
        public float TurnSpeed { get; private set; }
        public float FireRate { get; private set; }
        public float DamageModifier { get; private set; }
        public float Damage { get; private set; }
        public float Range { get; private set; }
        public TargetType Targetting { get; private set; }
        public Enemy Target { get; private set; }
        public Rectangle Bounds { get; private set; }

        private float lookRotation;
        private float rotOffset = MathHelper.PiOver2;
        private Texture2D bodySprite;
        private Vector2 bodyOrigin;
        private Texture2D headSprite;
        private static Texture2D rangeSprite;
        private Vector2 headOrigin;
        protected int cannonLength;

        protected Texture2D debugFirePoint;
        protected bool debug = false;

        private float prevRotation;
        private Enemy prevTarget;

        private bool viewRange = false;

        public Tower(Vector2 position, float damage, float fireRate, float turnSpeed, float range, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin)
        {
            this.Position = position;
            this.DamageModifier = 1f;
            this.Damage = damage;
            this.FireRate = fireRate;
            this.TurnSpeed = turnSpeed;
            this.Range = range;
            this.LookRotation = 0;
            this.bodySprite = bodySprite;
            this.bodyOrigin = bodyOrigin;
            this.headSprite = headSprite;
            this.headOrigin = headOrigin;

            Bounds = new Rectangle((position - bodyOrigin).ToPoint(), new Point(bodySprite.Width, bodySprite.Height));
            rangeSprite = DebugTextures.pixel;
            cannonLength = this.headSprite.Height;
            Targetting = TargetType.First;

            debugFirePoint = DebugTextures.GenerateRectangle(2, 2, Color.Red);
        }

        public virtual void Update(GameTime gameTime)
        {
            TrackTarget();
            //Vector2 mouseDir = Input.MousePosition - position;
            //LookRotation = (float)Math.Atan2(mouseDir.Y, mouseDir.X);
        }

        public void TrackTarget()
        {
            Target = FindTarget();
            if (Target == null)
                return;

            Vector2 dir = Target.position - Position;
            float targetAngle = (float)Math.Atan2(dir.Y, dir.X);
            if (LookRotation != targetAngle)
            {
                float angleDiff = -GetShortestAngle(LookRotation, targetAngle);

                if (Math.Abs(angleDiff) < 0.01f)
                {
                    LookRotation = targetAngle;
                }
                else
                {
                    LookRotation -= TurnSpeed * Math.Sign(angleDiff);
                }
            }
            prevRotation = LookRotation;
            prevTarget = Target;
        }

        public Enemy FindTarget()
        {
            if (Main.instance.level.Enemies.Count == 0)
                return null;

            int recordProg = 0;
            float maxT = 0;
            Enemy enemy = null;
            foreach (var e in Main.instance.level.Enemies)
            {
                if (Vector2.DistanceSquared(e.position, position) > Range * Range)
                    continue;
                if (e.progress > recordProg)
                {
                    recordProg = e.progress;
                    enemy = e;
                }
                else if (e.progress == recordProg && e.t > maxT)
                {
                    maxT = e.t;
                    enemy = e;
                }
            }
            return enemy;
        }

        private static float GetShortestAngle(float from, float to)
        {
            float max_angle = MathHelper.TwoPi;
            float difference = (to - from) % max_angle;
            return ((2 * difference) % max_angle) - difference;
        }

        public void SetRange(bool state)
        {
            viewRange = state;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            // Draw body
            sb.Draw(bodySprite, position, null, Color.White, 0f, bodyOrigin, 1f, SpriteEffects.None, 0f);

            // Draw head
            sb.Draw(headSprite, position, null, Color.White, LookRotation + rotOffset, headOrigin, 1f, SpriteEffects.None, 0f);
        }

        public static void DrawRange()
        {
            Tower selected = GUI.selectedTower;
            if (selected == null)
                return;
            Main.spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: Assets.RangeEffect);
            Assets.RangeEffect.CurrentTechnique.Passes[0].Apply();
            Main.spriteBatch.Draw(rangeSprite, selected.position, null, Color.White, 0f, new Vector2(0.5f), selected.Range * 2f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
        }

        public enum TargetType
        {
            First, Last
        }
    }
}
