﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt2
{
    public class LaserTower : Tower
    {
        public LaserTower(Vector2 position, float fireRate) : base(position, fireRate, Assets.LaserTower, Assets.LaserTowerOrigin, Assets.LaserTowerHead, Assets.LaserTowerHeadOrigin)
        {

        }
    }
}
