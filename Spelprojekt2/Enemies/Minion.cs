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
        public Minion() : base(10f, 10, 50f, Assets.Minion, Assets.MinionOrigin)
        {

        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
