using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Spelprojekt2.Enemies;
using Spelprojekt2.UI;

namespace Spelprojekt2.Towers
{
    public class Tower
    {
        public static Color RangeColor { get; set; }
        public float TotalDamage { get; set; }
        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position;

        protected Vector2 firePosition;
        protected Vector2 fireDirection;
        protected bool canFire;
        public float MoneyModifier { get; protected set; }
        public float LookRotation { get { return lookRotation; } set { lookRotation = value; } }
        public float TurnSpeed { get; private set; }
        public float FireRate { get; private set; }
        public float FireRateModifier { get; protected set; }
        public float DamageModifier { get; protected set; }
        public float Damage { get; private set; }
        public float Range { get; private set; }
        public float RangeModifier { get; protected set; }
        public int Pierce { get; private set; }
        public int PierceAdd { get; protected set; }
        public int Path { get; private set; }
        public int Tier { get; private set; }
        public TargetType Targetting { get; private set; }
        public Enemy Target { get; private set; }
        public Rectangle Bounds { get; private set; }
        public TowerInfo towerInfo;
        public Upgrade[][] Upgrades;

        protected float lookRotation;
        protected float rotOffset = MathHelper.PiOver2;
        public Texture2D bodySprite;
        private Vector2 bodyOrigin;
        public Texture2D headSprite;
        private static Texture2D rangeSprite;
        private Vector2 headOrigin;
        protected int cannonLength;

        protected Texture2D debugFirePoint;
        protected bool debug = false;

        private float prevRotation;
        private Enemy prevTarget;

        private bool viewRange = false;
        private double timeSinceFired = 0f;

        public static void GenerateTowers()
        {
            //GunTowerMK1 = new GunTower(Vector2.Zero);
        }

        public Tower(Vector2 position, TowerInfo towerInfo, float damage, float fireRate, int pierce, float turnSpeed, float range, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin, int path, int tier)
        {
            this.Position = position;
            this.towerInfo = towerInfo;
            this.DamageModifier = 1f;
            this.Damage = damage;
            this.FireRate = fireRate;
            this.FireRateModifier = 1f;
            this.TurnSpeed = turnSpeed;
            this.Range = range;
            this.RangeModifier = 1f;
            this.LookRotation = -MathHelper.PiOver2;
            this.bodySprite = bodySprite;
            this.bodyOrigin = bodyOrigin;
            this.headSprite = headSprite;
            this.headOrigin = headOrigin;
            this.Pierce = pierce;
            this.PierceAdd = 0;
            this.Path = path;
            this.Tier = tier;
            this.MoneyModifier = 1f;

            string key = GetType().FullName;
            bool success = Global.Upgrades.Where(u => u.ContainsKey(key)).First().TryGetValue(key, out Upgrades);

            Bounds = new Rectangle((position - bodyOrigin).ToPoint(), new Point(bodySprite.Width, bodySprite.Height));
            rangeSprite = DebugTextures.pixel;
            cannonLength = this.headSprite.Height;
            Targetting = TargetType.First;
            canFire = false;

            debugFirePoint = DebugTextures.GenerateRectangle(2, 2, Color.Red);
        }

        public virtual void Fire()
        {
            fireDirection = new Vector2((float)Math.Cos(LookRotation), (float)Math.Sin(LookRotation));
            firePosition = Position + fireDirection * (cannonLength - 8);
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
            canFire = Target != null;
            float fireTime = (1f / (FireRate * FireRateModifier)) / Global.gameSpeed;
            timeSinceFired = timeSinceFired >= fireTime && !canFire ? fireTime : timeSinceFired + gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceFired >= fireTime && canFire)
            {
                timeSinceFired = 0f;
                Fire();
            }
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
            if (Main.instance.level.enemies.Count == 0)
                return null;

            float recordProg = 0f;
            Enemy enemy = null;
            foreach (var e in Main.instance.level.enemies)
            {
                if (Vector2.DistanceSquared(e.position, position) > Range * Range * RangeModifier * RangeModifier)
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
        public virtual void Upgrade(int path, int tier)
        {
            this.Path = path;
            this.Tier = tier;
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
                return (Tower)Type.GetType(type).GetConstructors()[0].Invoke(new object[] { Vector2.Zero, this, 0, 0 });
            }           
        }
    }
}
