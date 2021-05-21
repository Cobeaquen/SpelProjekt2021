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
        public HoverElement hoverElement;
        public TowerElement(Vector2 position, int width, int height, Tower.TowerInfo tower, ClickCallback callback = null) : base(position, width, height, callback, tower.iconSprite, Vector2.Zero)
        {
            this.tower = tower;
            hoverElement = new HoverElement(position, MouseOver, bounds, new List<UIElement>() {
                new TextElement(new Vector2(-150, 0), 100, 100, tower.name, Color.White, Assets.DefaultFont),
                new TextElement(new Vector2(-150, 16), 200, 100, tower.desc, Color.White, Assets.DefaultFont),
                new TextElement(new Vector2(-150, 32), 100, 100, tower.cost.ToString(), Color.White, Assets.DefaultFont)
            });
        }
        public override void Update()
        {
            hoverElement.Update();
            base.Update();
        }
        public override void Clicked()
        { // Skapa tornet för placering
            Tower t = tower.GetTowerDuplicate();
            if (Global.Buy(tower.cost))
            {
                GUI.StartTowerPlacement(t);
            }
        }
        public void MouseOver(Vector2 position)
        {
            
        }
        public override void Draw()
        {
            base.Draw();
            hoverElement.Draw();
        }
    }
}
