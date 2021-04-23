using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2
{
    public class ProjectileTower : Tower
    {
        public ProjectileTower(Vector2 position, Texture2D bodySprite, Vector2 bodyOrigin, Texture2D headSprite, Vector2 headOrigin) : base(position, bodySprite, bodyOrigin, headSprite, headOrigin)
        {

        }

        /// <summary>
        /// Fires a projectile in the turret's look direction
        /// </summary>
        /// <returns></returns>
        public virtual float Fire()
        {
            return 0f;
        }
    }
}
