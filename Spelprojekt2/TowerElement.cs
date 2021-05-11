using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spelprojekt2
{
    public class TowerElement : ButtonElement
    {
        public Tower.TowerInfo tower;
        public TowerElement(Vector2 position, int width, int height, Tower.TowerInfo tower, ClickCallback callback = null) : base(position, width, height, callback, tower.iconSprite, Vector2.Zero)
        {
            this.tower = tower;
        }
        public override void Clicked()
        { // Skapa tornet för placering
            Tower t = tower.GetTowerDuplicate();
            GUI.StartTowerPlacement(t);
        }
    }
}
