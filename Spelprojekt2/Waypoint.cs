using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public struct Waypoint
    {
        public Vector2 point;
        public float length;

        public Waypoint(Vector2 point, float length)
        {
            this.point = point;
            this.length = length;
        }
    }
}
