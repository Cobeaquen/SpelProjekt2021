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
        public int numberOfBullets;
        public GunTower(Vector2 position, TowerInfo ti, int path, int tier) : base(position, ti, 1.5f, 1f, 0.1f, 2.5f, 120f, 1, Assets.GunTower, Assets.GunTowerOrigin, Assets.GunTowerHead, Assets.GunTowerHeadOrigin, path, tier)
        {
            numberOfBullets = 1;
        }

        public override void Fire()
        {
            base.Fire();
            for (int i = 0; i < numberOfBullets; i++)
            {
                RegularBullet bullet = new RegularBullet(this, 200f, firePosition, GetBulletDirection(out float offset), LookRotation + offset, 1, Hit);
                Bullets.Add(bullet);
            }
        }

        public override void Upgrade(int path, int tier)
        {
            base.Upgrade(path, tier);
            switch (path)
            {
                case 0:
                    switch (tier)
                    {
                        case 1:
                            DamageModifier = 1.5f;
                            FireRateModifier = 2f;
                            break;
                        case 2:
                            RangeModifier = 1.5f;
                            DamageModifier = 2f;
                            PierceAdd = 2;
                            break;
                        case 3:
                            DamageModifier = 3f;
                            RangeModifier = 2.5f;
                            MoneyModifier = 1.5f;
                            break;
                    }
                    break;
                case 1:
                    switch (tier)
                    {
                        case 1:
                            DamageModifier = 1.25f;
                            FireRateModifier = 1.5f;
                            break;
                        case 2:
                            numberOfBullets = 5;
                            spreadModifier = 1f;
                            break;
                        case 3:
                            numberOfBullets = 10;
                            spreadModifier = 2f;
                            DamageModifier = 1.5f;
                            break;
                    }
                    break;
            }
        }
    }
}
