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

        public static Texture2D Level1 { get; internal set; }
        public static Texture2D Enemy1 { get; internal set; }

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
        public static Texture2D SniperTowerHead { get; internal set; }
        public static Texture2D Bullet { get; internal set; }
        public static Vector2 BulletOrigin { get; internal set; }
        public static void Initialize(ContentManager content)
        {
            Content = content;

            //Level1 = Content.Load<Texture2D>("graphics/level1");
            //Enemy1 = Content.Load<Texture2D>("graphics/enemy1");
            GunTower = Content.Load<Texture2D>("graphics\\gun_tower_body");
            GunTowerOrigin = GetOrigin(GunTower);
            GunTowerHead = Content.Load<Texture2D>("graphics\\gun_tower_head");
            GunTowerHeadOrigin = new Vector2(16, 24);
            Bullet = Content.Load<Texture2D>("graphics/bullet");
            BulletOrigin = GetOrigin(Bullet);
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
    }
}
