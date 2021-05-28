using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    /// <summary>
    /// Manage and load all game textures and origins of them
    /// </summary>
    public static class Assets
    {
        public static ContentManager Content { get; internal set; }

        #region Towers
        public static Texture2D GunTower { get; internal set; }
        public static Vector2 GunTowerOrigin { get; internal set; }
        public static Texture2D GunTowerHead { get; internal set; }
        public static Vector2 GunTowerHeadOrigin { get; internal set; }
        public static Texture2D LaserTower { get; internal set; }
        public static Vector2 LaserTowerOrigin { get; internal set; }
        public static Texture2D LaserTowerHead { get; internal set; }
        public static Vector2 LaserTowerHeadOrigin { get; internal set; }
        public static Texture2D BombTower { get; internal set; }
        public static Texture2D BombTowerHead { get; internal set; }
        public static Texture2D SniperTower { get; internal set; }
        public static Vector2 SniperTowerOrigin { get; internal set; }
        public static Texture2D SniperTowerHead { get; internal set; }
        public static Vector2 SniperTowerHeadOrigin { get; internal set; }
        public static Texture2D CircleShooter { get; internal set; }
        public static Vector2 CircleShooterOrigin { get; internal set; }
        public static Texture2D CircleShooterHead { get; internal set; }
        public static Vector2 CircleShooterHeadOrigin { get; internal set; }
        #endregion

        #region Bullets
        public static Texture2D Bullet { get; internal set; }
        public static Vector2 BulletOrigin { get; internal set; }
        public static Texture2D BombBullet { get; internal set; }
        public static Vector2 BombBulletOrigin { get; internal set; }
        #endregion

        #region Enemies
        public static Texture2D Enemy1 { get; internal set; }
        public static Texture2D Enemy2 { get; internal set; }
        public static Texture2D Enemy3 { get; internal set; }
        public static Texture2D Enemy5 { get; internal set; }
        public static Vector2 EnemyOrigin { get; internal set; }
        #endregion

        #region GUI
        public static Texture2D Stats { get; internal set; }
        public static Vector2 StatsOrigin { get; internal set; }
        public static Texture2D TowerMenu { get; internal set; }
        public static Vector2 TowerMenuOrigin { get; internal set; }
        public static Texture2D HPBarFrame { get; internal set; }
        public static Texture2D Meny { get; internal set; }
        public static Vector2 MenyOrigin { get; internal set; }
        public static Texture2D UpgradeButton { get; internal set; }
        public static Texture2D SellButton { get; internal set; }
        public static Texture2D StartWave { get; internal set; }
        public static Texture2D SpeedWave { get; internal set; }
        public static Texture2D SpeedWave2 { get; internal set; }
        public static Texture2D PauseWave { get; internal set; }
        public static Texture2D PlayWave { get; internal set; }
        public static Vector2 WaveButtonOrigin { get; internal set; }
        public static Texture2D Level1 { get; internal set; }
        public static Texture2D ShopButton { get; internal set; }
        public static Vector2 ShopButtonOrigin { get; internal set; }
        public static Texture2D Level1Waypoints { get; internal set; }
        public static Texture2D UpgradeMeter { get; internal set; }
        public static SpriteFont DefaultFont { get; internal set; }
        #endregion

        #region Effects
        public static Effect HPBarEffect { get; internal set; }
        public static Effect RangeEffect { get; internal set; }
        public static Effect ButtonEffect { get; internal set; }
        public static Effect ShockwaveEffect { get; internal set; }
        #endregion
        public static void Initialize(ContentManager content)
        {
            Content = content;

            //Level1 = Content.Load<Texture2D>("graphics/level1");

            #region Towers
            GunTower = Content.Load<Texture2D>("graphics/gun_tower_body");
            GunTowerOrigin = GetOrigin(GunTower);
            GunTowerHead = Content.Load<Texture2D>("graphics/gun_tower_head");
            GunTowerHeadOrigin = new Vector2(16, 24);
            LaserTower = Content.Load<Texture2D>("graphics/laser_tower_body");
            LaserTowerHead = Content.Load<Texture2D>("graphics/laser_tower_head");
            BombTower = Content.Load<Texture2D>("graphics/bomb_tower_body");
            BombTowerHead = Content.Load<Texture2D>("graphics/bomb_tower_head");
            CircleShooterHead = Content.Load<Texture2D>("graphics/circleshooter_head");
            CircleShooter = Content.Load<Texture2D>("graphics/circleshooter_body");
            CircleShooterOrigin = GetOrigin(CircleShooter);
            CircleShooterHeadOrigin = GetOrigin(CircleShooterHead);
            #endregion

            #region Bullets
            Bullet = Content.Load<Texture2D>("graphics/bullet_small");
            BulletOrigin = GetOrigin(Bullet);
            BombBullet = Content.Load<Texture2D>("graphics/bomb_bullet");
            BombBulletOrigin = GetOrigin(BombBullet);
            #endregion

            #region Enemies
            Enemy1 = content.Load<Texture2D>("graphics/enemies/enemy1");
            Enemy2 = content.Load<Texture2D>("graphics/enemies/enemy2");
            Enemy3 = content.Load<Texture2D>("graphics/enemies/enemy3");
            Enemy5 = content.Load<Texture2D>("graphics/enemies/enemy5");

            EnemyOrigin = new Vector2(12, 12);
            #endregion

            #region Effects
            HPBarEffect = Content.Load<Effect>("graphics/shaders/hpbar");
            RangeEffect = Content.Load<Effect>("graphics/shaders/range");
            ButtonEffect = Content.Load<Effect>("graphics/shaders/button");
            ShockwaveEffect = Content.Load<Effect>("graphics/shaders/shockwave");
            #endregion

            #region GUI
            HPBarFrame = content.Load<Texture2D>("graphics/hpbar_frame");
            Stats = Content.Load<Texture2D>("graphics/ui/stats");
            StatsOrigin = Vector2.Zero;
            TowerMenu = Content.Load<Texture2D>("graphics/ui/menues/tower_menu");
            TowerMenuOrigin = Vector2.Zero;
            StatsOrigin = Vector2.Zero;
            Meny = Content.Load<Texture2D>("graphics/ui/menues/meny");
            MenyOrigin = new Vector2(Meny.Width, 0);
            UpgradeButton = Content.Load<Texture2D>("graphics/ui/buttons/upgrade_button");
            SellButton = Content.Load<Texture2D>("graphics/ui/buttons/sell");
            Level1 = Content.Load<Texture2D>("graphics/levels/level1");
            Level1Waypoints = Content.Load<Texture2D>("graphics/levels/level1_waypoints");
            UpgradeMeter = Content.Load<Texture2D>("graphics/ui/upgrade_meter");
            DefaultFont = Content.Load<SpriteFont>("graphics/ui/fonts/default");
            DefaultFont.LineSpacing = 14;

            StartWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/start");
            SpeedWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/speed");
            SpeedWave2 = Content.Load<Texture2D>("graphics/ui/icons/waveflow/speed2");
            PauseWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/pause");
            PlayWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/play");
            WaveButtonOrigin = GetLocation(StartWave, Location.BottomLeft);
            ShopButton = Content.Load<Texture2D>("graphics/ui/buttons/shop");
            ShopButtonOrigin = new Vector2(Global.GameWidth, 0f);

            #endregion
            //SniperTower = Content.Load<Texture2D>("graphics/sniper_tower_body");
            //SniperTowerHead = Content.Load<Texture2D>("graphics/sniper_tower_head");
        }

        public static Vector2 GetOrigin(Texture2D texture)
        {
            return new Vector2(texture.Width / 2f, texture.Height / 2f);
        }
        public static Vector2 GetLocation(Texture2D texture, Location loc)
        {
            switch (loc)
            {
                case Location.Top:
                    return new Vector2(texture.Width / 2f, 0f);
                case Location.Bottom:
                    return new Vector2(texture.Width / 2f, texture.Height);
                case Location.Right:
                    return new Vector2(texture.Width, texture.Height / 2f);
                case Location.Left:
                    return new Vector2(0f, texture.Height / 2f);
                case Location.TopRight:
                    return new Vector2(texture.Width, 0f);
                case Location.TopLeft:
                    return new Vector2(0f, 0f);
                case Location.BottomRight:
                    return new Vector2(texture.Width, texture.Height);
                case Location.BottomLeft:
                    return new Vector2(0f, texture.Height);
                default:
                    return Vector2.Zero;
            }
        }

        public enum Location
        {
            Top, Bottom, Right, Left,
            TopRight, TopLeft, BottomRight, BottomLeft,
        }
    }
}
