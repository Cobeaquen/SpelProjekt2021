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

        #region Enemies
        public static Texture2D Enemy1 { get; internal set; }
        #endregion

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
        #endregion

        #region Bullets
        public static Texture2D Bullet { get; internal set; }
        public static Vector2 BulletOrigin { get; internal set; }
        #endregion

        #region Enemies
        public static Texture2D Minion { get; internal set; }
        public static Vector2 MinionOrigin { get; internal set; }
        #endregion

        #region GUI
        public static Texture2D Stats { get; internal set; }
        public static Vector2 StatsOrigin { get; internal set; }
        public static Texture2D HPBarFrame { get; internal set; }
        public static Texture2D Meny { get; internal set; }
        public static Vector2 MenyOrigin { get; internal set; }
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
        public static SpriteFont DefaultFont { get; internal set; }
        #endregion

        #region Effects
        public static Effect HPBarEffect { get; internal set; }
        public static Effect RangeEffect { get; internal set; }
        public static Effect ButtonEffect { get; internal set; }
        #endregion
        public static void Initialize(ContentManager content)
        {
            Content = content;

            //Level1 = Content.Load<Texture2D>("graphics/level1");
            //Enemy1 = Content.Load<Texture2D>("graphics/enemy1");
            #region Towers
            GunTower = Content.Load<Texture2D>("graphics/gun_tower_body");
            GunTowerOrigin = GetOrigin(GunTower);
            GunTowerHead = Content.Load<Texture2D>("graphics/gun_tower_head");
            GunTowerHeadOrigin = new Vector2(16, 24);
            #endregion

            #region Bullets
            Bullet = Content.Load<Texture2D>("graphics/bullet_small");
            BulletOrigin = GetOrigin(Bullet);
            #endregion

            #region Enemies
            Minion = content.Load<Texture2D>("graphics/enemies/minion");
            MinionOrigin = GetOrigin(Minion);
            #endregion

            #region Effects
            HPBarEffect = Content.Load<Effect>("graphics/shaders/hpbar");
            RangeEffect = Content.Load<Effect>("graphics/shaders/range");
            ButtonEffect = Content.Load<Effect>("graphics/shaders/button");
            #endregion

            #region GUI
            HPBarFrame = content.Load<Texture2D>("graphics/hpbar_frame");
            Stats = Content.Load<Texture2D>("graphics/ui/stats");
            StatsOrigin = Vector2.Zero;
            Meny = Content.Load<Texture2D>("graphics/ui/meny");
            MenyOrigin = new Vector2(Meny.Width, 0);
            Level1 = Content.Load<Texture2D>("graphics/levels/level1");
            Level1Waypoints = Content.Load<Texture2D>("graphics/levels/level1_waypoints");
            DefaultFont = Content.Load<SpriteFont>("graphics/ui/fonts/default");

            StartWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/start");
            SpeedWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/speed");
            SpeedWave2 = Content.Load<Texture2D>("graphics/ui/icons/waveflow/speed2");
            PauseWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/pause");
            PlayWave = Content.Load<Texture2D>("graphics/ui/icons/waveflow/play");
            WaveButtonOrigin = GetLocation(StartWave, Location.BottomLeft);
            ShopButton = Content.Load<Texture2D>("graphics/ui/buttons/shop");
            ShopButtonOrigin = new Vector2(Global.GameWidth, 0f);
            #endregion
            //LaserTower = Content.Load<Texture2D>("graphics/laser_tower_body");
            //LaserTowerHead = Content.Load<Texture2D>("graphics/laser_tower_head");
            //BombTower = Content.Load<Texture2D>("graphics/bomb_tower_body");
            //BombTowerHead = Content.Load<Texture2D>("graphics/bomb_tower_head");
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
