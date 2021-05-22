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
        public GunTower(Vector2 position, TowerInfo ti) : base(position, ti, 0.05f, 100f, 1f, 3f, 120f, Assets.GunTower, Assets.GunTowerOrigin, Assets.GunTowerHead, Assets.GunTowerHeadOrigin)
        {
            
        }

        public override void Fire()
        {
            base.Fire();
            var bullet = new RegularBullet(this, 200f, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, DestroyBullet);
            Bullets.Add(bullet);
        }
    }
}
