using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2.Towers
{
    class CircleShooter : GunTower
    {
        public CircleShooter(Vector2 position, TowerInfo ti, int path, int tier) : base(position, ti, path, tier)
        {
            numberOfBullets = 20;
        }
        public override void Fire()
        {
            base.Fire();
            for (float i = 0; i < 2*MathHelper.Pi; i += (2*MathHelper.Pi) / numberOfBullets)
            {
                RegularBullet bullet = new RegularBullet(this, 200f, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, Hit);
                Bullets.Add(bullet);
            }
        }
    }
}
