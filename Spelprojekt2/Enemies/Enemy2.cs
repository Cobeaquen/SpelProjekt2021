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
    class Enemy2 : Enemy
    {
        public Enemy2() : base(50f, 15, 60f, Assets.Enemy2, Assets.EnemyOrigin)
        {
        }        
    }
}
