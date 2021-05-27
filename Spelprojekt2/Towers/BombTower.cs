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
        public bool miniBombsActive;
        public float bombRadius;
        public float radiusModifier;
        private int miniBombsAdd;

        public BombTower(Vector2 position, TowerInfo ti, int path, int tier) : base(position, ti, 5f, 0.3f, 0.2f, 1, 100, 1, Assets.BombTower, Assets.GunTowerOrigin, Assets.BombTowerHead, Assets.GunTowerHeadOrigin, path, tier)
        {
            bombRadius = 100f;
            radiusModifier = 1f;          
        }
        public override void Fire()
        {
            base.Fire();
            var bullet = new BombBullet(this, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 2f, Hit, 4 + miniBombsAdd);
            Bullets.Add(bullet);
        }
        protected override void Hit(Bullet bullet)
        {
            Shockwave sw = new Shockwave(bullet.Position, 1f, bombRadius * radiusModifier * ((BombBullet)bullet).radiusModifier);
            Global.Effects.Add(sw);
            base.Hit(bullet);
        }
        public override void Upgrade(int path, int tier)
        {
            base.Upgrade(path, tier);
            switch (path)
            {
                case 1:
                    switch(tier)
                    {
                        case 1:
                            DamageModifier = 1.5f;
                            radiusModifier = 1.5f;
                            break;
                        case 2:
                            DamageModifier = 2f;
                            RangeModifier = 1.5f;
                            break;
                        case 3:
                            DamageModifier = 3f;
                            RangeModifier = 2.5f;
                            break;
                    }
                    break;
                case 2:
                    switch (tier)
                    {
                        case 1:
                            RangeModifier = 1.5f;
                            FireRateModifier = 1.5f;
                            break;
                        case 2:
                            miniBombsActive = true;
                            break;
                        case 3:
                            miniBombsAdd = 6;
                            break;
                    }
                    break;

            }
        }

    }
}
