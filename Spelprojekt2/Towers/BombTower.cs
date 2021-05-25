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
        public BombTower(Vector2 position, TowerInfo ti) : base(position, ti, 6f, 0.5f, 0.2f, 1f, 100, Assets.BombTower, Assets.GunTowerOrigin, Assets.BombTowerHead, Assets.GunTowerHeadOrigin)
        {

        }
        public override void Fire()
        {
            base.Fire();
            var bullet = new BombBullet(this, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, DestroyBullet, 100f);
            Bullets.Add(bullet);
        }
        protected override void DestroyBullet(Bullet bullet)
        {
            Shockwave sw = new Shockwave(bullet.Position, 1f, ((BombBullet)bullet).bombRadius);
            Global.Effects.Add(sw);
            base.DestroyBullet(bullet);
        }
    }
}
