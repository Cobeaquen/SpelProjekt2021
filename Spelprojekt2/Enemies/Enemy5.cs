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
    class Enemy5 : Enemy
    {
        private float acceleration;
        public Enemy5() : base(250f, 100, 20f, Assets.Enemy5, Assets.EnemyOrigin)
        {
            acceleration = 0.25f;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            speed += acceleration;
        }

    }
}
