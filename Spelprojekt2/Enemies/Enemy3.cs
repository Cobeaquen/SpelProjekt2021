using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spelprojekt2.Collision;

namespace Spelprojekt2.Enemies
{
    class Enemy3 : Enemy
    {
        public Enemy3() : base(250f, 30, 20f, Assets.Enemy3, Assets.EnemyOrigin)
        {

        }
        
    }
}
