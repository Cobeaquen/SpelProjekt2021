using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.Collision
{
    public class RectCollider : Collider
    {
        public Rectangle bounds;

        public RectCollider(Rectangle bounds)
        {
            this.bounds = bounds;
            UpdateEdges(this.bounds);
        }
        public void UpdateEdges(Rectangle newBounds)
        {
            bounds = newBounds;
            edges = new Edge[] {new Edge(new Vector2(bounds.Left, bounds.Top), new Vector2(bounds.Right, bounds.Top)),
            new Edge(new Vector2(bounds.Right, bounds.Top), new Vector2(bounds.Right, bounds.Bottom)),
            new Edge(new Vector2(bounds.Right, bounds.Bottom), new Vector2(bounds.Left, bounds.Bottom)),
            new Edge(new Vector2(bounds.Left, bounds.Bottom), new Vector2(bounds.Left, bounds.Top)) };
        }
    }
}
