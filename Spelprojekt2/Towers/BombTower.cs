using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt2.Effects;

namespace Spelprojekt2
{
    public class BombTower : ProjectileTower
    {
        public BombTower(Vector2 position, TowerInfo ti) : base(position, ti, 15, 1, 0.2f, 3, 200, 1, Assets.BombTower, Assets.GunTowerOrigin, Assets.BombTowerHead, Assets.GunTowerHeadOrigin)
        {

        }
        public override void Fire()
        {
            base.Fire();
            var bullet = new BombBullet(this, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, Hit, 100f);
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
