using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spelprojekt2.Collision;

namespace Spelprojekt2
{
    public class LaserTower : Tower
    {
        public Texture2D laserRay;
        public Raycast ray;
        public bool rayVisible;

        private Vector2 hitPoint;
        public LaserTower(Vector2 position, TowerInfo ti) : base(position, ti, 0.1f, 100f, 5f, 100f, Assets.LaserTower, Assets.GunTowerOrigin, Assets.LaserTowerHead, Assets.GunTowerHeadOrigin)
        {
            //laserRay = Assets.LaserRay;
            rayVisible = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (rayVisible)
            {
                fireDirection = new Vector2((float)Math.Cos(LookRotation), (float)Math.Sin(LookRotation));
                firePosition = Position + fireDirection * (cannonLength - 10);
                ray = new Raycast(firePosition, fireDirection, Range * RangeModifier);
            }
            //Fire();
            base.Update(gameTime);
        }
        public override void Fire()
        { // Skjut en ray
            if (ray.Intersecting(1, out List<CollisionResult> info, out hitPoint)) // DUPLICATE RESULTS !!!???????
            {
                for (int i = 0; i < info.Count; i++)
                {
                    if (info[i].owner is Enemy)
                    {
                        Enemy e = info[i].owner as Enemy;
                        e.Hit(Damage * DamageModifier);
                    }
                }
            }
        }
        public override void Draw()
        {
            if (ray != null)
            {
                DebugTextures.DrawDebugLine(ray.a, hitPoint == Vector2.Zero ? ray.b : hitPoint, Color.Red, 1);
            }
            
            base.Draw();
        }
    }
}
