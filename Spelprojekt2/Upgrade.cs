using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public struct Upgrade
    {
        public string name;
        public string desc;
        public int cost;

        public Upgrade(string name, string desc, int cost)
        {
            this.name = name;
            this.desc = desc;
            this.cost = cost;
        }
    }
}
