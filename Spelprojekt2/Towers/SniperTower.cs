﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    public class SniperTower : ProjectileTower
    {
        public SniperTower(Vector2 position, TowerInfo ti) : base(position, ti, 10f, 0.4f, 0.1f, 0.2f, 100f, Assets.SniperTower, Assets.SniperTowerOrigin, Assets.SniperTowerHead, Assets.SniperTowerHeadOrigin)
        {
           
        }
    }
}