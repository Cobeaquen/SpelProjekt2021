using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Spelprojekt2.Towers;

namespace Spelprojekt2.UI
{
    public class TowerElement : ButtonElement
    {
        public Tower.TowerInfo tower;
        public HoverElement hoverElement;
        public TowerElement(Vector2 position, int width, int height, Tower.TowerInfo tower, ClickCallback callback = null) : base(position, width, height, callback, tower.iconSprite, Vector2.Zero)
        {
            this.tower = tower;
            hoverElement = new HoverElement(position, MouseOver, bounds, new List<UIElement>() {
                new TextElement(new Vector2(-150, 0), 150, 100, tower.name, Color.White, Assets.DefaultFont),
                new TextElement(new Vector2(-150, 16), 150, 100, tower.desc, Color.White, Assets.DefaultFont),
                new TextElement(new Vector2(-150, 60), 150, 100, "Cost: " + tower.cost.ToString(), Color.White, Assets.DefaultFont)
            });
            GUI.hoverElements.Add(hoverElement);
        }
        public override void Update()
        {
            if (!Global.CanAfford(tower.cost))
            {
                color = Color.Red;
            }
            else
            {
                color = Color.White;
            }
            base.Update();
        }
        public override void Clicked()
        { // Skapa tornet för placering
            if (Global.CanAfford(tower.cost))
            {
                Tower t = tower.GetTowerDuplicate();
                GUI.StartTowerPlacement(t);
            }
        }
        public void MouseOver(Vector2 position)
        {
            
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
