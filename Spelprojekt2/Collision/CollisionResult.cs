using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.Collision
{
    public struct CollisionResult
    {
        public static CollisionResult Empty { get { return new CollisionResult(null, Vector2.Zero); } }

        public object owner;
        public Vector2 point;
        public CollisionResult(object owner, Vector2 point)
        {
            this.owner = owner;
            this.point = point;
        }
    }
}
