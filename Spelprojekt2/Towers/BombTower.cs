using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt2.Effects;
using Spelprojekt2.Bullets;

namespace Spelprojekt2
{
    public class BombTower : ProjectileTower
    {
        public BombTower(Vector2 position, TowerInfo ti) : base(position, ti, 2f, 0.3f, 0.2f, 1, 100, 1, Assets.BombTower, Assets.GunTowerOrigin, Assets.BombTowerHead, Assets.GunTowerHeadOrigin)
        {

        }
        public override void Fire()
        {
            base.Fire();
            var bullet = new BombBullet(this, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 2f, Hit, 100f, 6);
            Bullets.Add(bullet);
        }
        protected override void Hit(Bullet bullet)
        {
            Shockwave sw = new Shockwave(bullet.Position, 1f, ((BombBullet)bullet).bombRadius);
            Global.Effects.Add(sw);
            base.Hit(bullet);
        }

    }
}
