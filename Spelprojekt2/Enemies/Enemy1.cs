using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2.Enemies
{
    public class Enemy1 : Enemy
    {
        public Enemy1() : base(20f, 10, 50f, Assets.Enemy1, Assets.MinionOrigin)
        {

        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
