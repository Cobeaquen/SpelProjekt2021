﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class Burst
    {
        public int amount;
        public Enemy enemy;
        public float timeInterval;

        public Burst(int amount, Enemy enemy, float timeInterval)
        {
            this.amount = amount;
            this.enemy = enemy;
            this.timeInterval = timeInterval;
        }
    }
}
