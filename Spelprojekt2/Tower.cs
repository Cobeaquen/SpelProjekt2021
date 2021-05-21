using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Spelprojekt2
{
    public class Tower
    {
        public static Tower GunTowerMK1 { get; private set; }
        public static Tower LaserTowerMK1 { get; private set; }

        public static Color RangeColor { get; set; }

        [JsonIgnore]
        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position;
        protected Vector2 firePosition;
        public float LookRotation { get { return lookRotation; } set { lookRotation = value; } }
        public float TurnSpeed { get; private set; }
        public float FireRate { get; private set; }
        public float DamageModifier { get; private set; }
        public float Damage { get; private set; }
        public float Range { get; private set; }
        public float RangeModifier { get; private set; }
        [JsonIgnore]
        public TargetType Targetting { get; private set; }
        [JsonIgnore]
        public Enemy Target { get; private set; }
        [JsonIgnore]
        public Rectangle Bounds { get; private set; }

        protected float lookRotation;
        protected float rotOffset = MathHelper.PiOver2;
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

        public static void GenerateTowers()
        {
            GunTowerMK1 = new GunTower(Vector2.Zero);
        }

        public Tower(Vector2 position, float damage, float fireRate, float turnSpeed, float range, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin)
        {
            this.Position = position;
            this.DamageModifier = 1f;
            this.Damage = damage;
            this.FireRate = fireRate;
            this.TurnSpeed = turnSpeed;
            this.Range = range;
            this.LookRotation = -MathHelper.PiOver2;
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

        public virtual void OnPlaced()
        {
            UpdateBounds();
        }

        public void UpdateBounds()
        {
            Bounds = new Rectangle((position - bodyOrigin).ToPoint(), new Point(bodySprite.Width, bodySprite.Height));
        }

        public virtual void Update(GameTime gameTime)
        {
            TrackTarget(gameTime);
            //Vector2 mouseDir = Input.MousePosition - position;
            //LookRotation = (float)Math.Atan2(mouseDir.Y, mouseDir.X);
        }

        public void TrackTarget(GameTime gameTime)
        {
            Target = FindTarget();
            if (Target == null)
                return;

            Vector2 dir = Target.position - Position;
            //Vector2 dir = Input.MousePosition - Position; // testa systemet
            float targetAngle = (float)Math.Atan2(dir.Y, dir.X);
            if (LookRotation != targetAngle)
            {
                float angleDiff = -Global.GetShortestAngle(LookRotation, targetAngle);

                if (Math.Abs(angleDiff) < 0.01f)
                {
                    LookRotation = targetAngle;
                }
                else
                {
                    LookRotation -= TurnSpeed * Math.Sign(angleDiff) * (float)gameTime.ElapsedGameTime.TotalSeconds * Global.gameSpeed;
                }
            }
            prevRotation = LookRotation;
            prevTarget = Target;
        }

        public Enemy FindTarget()
        {
            if (Main.instance.level.Enemies.Count == 0)
                return null;

            float recordProg = 0f;
            Enemy enemy = null;
            foreach (var e in Main.instance.level.Enemies)
            {
                if (Vector2.DistanceSquared(e.position, position) > Range * Range)
                    continue;
                if (e.progress + e.t > recordProg)
                {
                    recordProg = e.progress + e.t;
                    enemy = e;
                }
            }
            return enemy;
        }

        public void SetRange(bool state)
        {
            viewRange = state;
        }

        public virtual void Draw()
        {
            // Draw body
            Main.spriteBatch.Draw(bodySprite, position, null, Color.White, 0f, bodyOrigin, 1f, SpriteEffects.None, 0f);

            // Draw head
            Main.spriteBatch.Draw(headSprite, position, null, Color.White, LookRotation + rotOffset, headOrigin, 1f, SpriteEffects.None, 0f);
        }

        public static void DrawRange()
        {
            Tower selected = GUI.towerHeld != null ? GUI.towerHeld : GUI.selectedTower;
            if (selected == null)
                return;
            Main.spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: Assets.RangeEffect);
            Assets.RangeEffect.CurrentTechnique.Passes[0].Apply();
            Main.spriteBatch.Draw(rangeSprite, selected.position, null, RangeColor, 0f, new Vector2(0.5f), selected.Range * 2f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
        }

        public enum TargetType
        {
            First, Last
        }

        public struct TowerInfo
        {
            public string name;
            public string type;
            public string desc;
            public int cost;
            public string icon;
            [JsonIgnore]
            public Texture2D iconSprite;

            public TowerInfo(Type type, string name, string desc, int cost, string icon)
            {
                this.type = type.FullName;
                this.name = name;
                this.desc = desc;
                this.cost = cost;
                this.icon = icon;
                this.iconSprite = Main.instance.Content.Load<Texture2D>("graphics/ui/icons/towers/" + icon);
            }
            public void SetSprite()
            {
                iconSprite = Main.instance.Content.Load<Texture2D>("graphics/ui/icons/towers/" + icon);
            }
            public Tower GetTowerDuplicate()
            {
                return (Tower)Type.GetType(type).GetConstructors()[0].Invoke(new object[] { Vector2.Zero });
            }
        }
    }
}
