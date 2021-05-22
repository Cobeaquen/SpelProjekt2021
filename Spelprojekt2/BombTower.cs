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
        public BombTower(Vector2 position, TowerInfo ti) : base(position, ti, 15, 1, 0.2f, 3, 200, Assets.BombTower, Assets.GunTowerOrigin, Assets.BombTowerHead, Assets.GunTowerHeadOrigin)
        {

        }
        public override void Fire()
        {
            float Offset = spread * (Global.ran.Next(0, 2) == 1 ? 1 : -1) * spreadModifier * (float)Global.ran.NextDouble();

            Vector2 dir = new Vector2((float)Math.Cos(LookRotation), (float)Math.Sin(LookRotation));
            firePosition = Position + dir * cannonLength;

            Vector2 bulletDir = new Vector2((float)Math.Cos(LookRotation + Offset), (float)Math.Sin(LookRotation + Offset));

            var bullet = new BombBullet(this, firePosition, bulletDir, LookRotation + Offset, 1, DestroyBullet, 100f);
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
