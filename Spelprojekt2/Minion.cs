using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    public class Minion : Enemy
    {
        public Minion() : base(50f, 10, 50f, DebugTextures.GenerateRectangle(24, 24, Color.Beige), new Vector2(12, 12))
        {

        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
