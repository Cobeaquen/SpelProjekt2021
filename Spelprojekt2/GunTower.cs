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
        public GunTower(Vector2 position) : base(position, Assets.GunTower, Assets.GunTowerOrigin, Assets.GunTowerHead, Assets.GunTowerHeadOrigin)
        {
            
        }
    }
}
