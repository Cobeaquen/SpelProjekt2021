using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.Collision
{
    public class Edge
    {
        public Vector2 a;
        public Vector2 b;

        public Edge(Vector2 a, Vector2 b)
        {
            this.a = a;
            this.b = b;
        }
    }
}
