using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    public class GunTower : ProjectileTower
    {
        public GunTower(Vector2 position, TowerInfo ti, int path, int tier) : base(position, ti, 1f, 1.5f, 0.1f, 2, 120f, 1, Assets.GunTower, Assets.GunTowerOrigin, Assets.GunTowerHead, Assets.GunTowerHeadOrigin, path, tier)
        {
            
        }

        public override void Fire()
        {
            base.Fire();
            var bullet = new RegularBullet(this, 200f, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, Hit);
            Bullets.Add(bullet);
        }
    }
}
