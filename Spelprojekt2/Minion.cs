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
        public Minion() : base(50f, 10, 100f, DebugTextures.GenerateRectangle(20, 20, Color.Brown))
        {

        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
