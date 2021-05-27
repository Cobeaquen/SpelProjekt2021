using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2.Towers
{
    class CircleShooter : ProjectileTower
    {
        public int bulletAmount;
        public CircleShooter(Vector2 position, TowerInfo ti, int path, int tier) : base(position, ti, 1.5f, 1f, 0.1f, 2.5f, 120f, 1, Assets.GunTower, Assets.GunTowerOrigin, Assets.GunTowerHead, Assets.GunTowerHeadOrigin, path, tier)
        {
            bulletAmount = 20;
        }
        public override void Fire()
        {
            base.Fire();
            for (float i = 0; i < 2*MathHelper.Pi; i += (2*MathHelper.Pi) / bulletAmount)
            {
                RegularBullet bullet = new RegularBullet((ProjectileTower)this, 200f, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, Hit);
                Bullets.Add(bullet);
            }
        }
    }
}
