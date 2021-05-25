using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    public class RegularBullet : Bullet
    {
        public RegularBullet(ProjectileTower owner, float velocity, Vector2 position, Vector2 lookDirection, float lookRotation, float damage, HitCallback destroyCallback) : base(owner, velocity, position, lookDirection, lookRotation, damage, 1, destroyCallback, Assets.Bullet, Assets.BulletOrigin)
        {
            
        }
    }
}
