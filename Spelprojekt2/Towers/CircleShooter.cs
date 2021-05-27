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
        public CircleShooter(Vector2 position, TowerInfo ti, int path, int tier) : base(position, ti, 0.4f, 1f, 0.1f, 2.5f, 50f, 1, Assets.CircleShooter, Assets.CircleShooterOrigin, Assets.CircleShooterHead, Assets.CircleShooterHeadOrigin, path, tier)
        {
            bulletAmount = 10;
            reach = Range;
        }
        public override void Fire()
        {
            base.Fire();
            for (float i = 0; i < 2*MathHelper.Pi; i += (2*MathHelper.Pi) / bulletAmount)
            {
                Vector2 dir = new Vector2((float)Math.Cos(i), (float)Math.Sin(i));
                RegularBullet bullet = new RegularBullet((ProjectileTower)this, 200f, Position, dir, (float)Math.Atan2(dir.Y,dir.X), 1, Hit);
                Bullets.Add(bullet);
            }
        }
    }
}
