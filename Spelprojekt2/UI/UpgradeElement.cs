using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.UI
{
    public class UpgradeElement : ButtonElement
    {
        public Upgrade upgrade;
        public UpgradeElement(Vector2 position, int width, int height, Tower.TowerInfo tower, Upgrade upgrade, ClickCallback callback = null) : base(position, width, height, callback, tower.iconSprite, Vector2.Zero)
        {
            this.upgrade = upgrade;
        }
        public override void Clicked()
        {
            base.Clicked();
        }
    }
}
