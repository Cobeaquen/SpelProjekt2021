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
        public GunTower(Vector2 position) : base(position, 1f, 10f, 0.25f, 3f, 120f, Assets.GunTower, Assets.GunTowerOrigin, Assets.GunTowerHead, Assets.GunTowerHeadOrigin)
        {
            
        }
    }
}
