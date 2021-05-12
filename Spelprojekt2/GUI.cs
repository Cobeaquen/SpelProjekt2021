using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2
{
    public static class GUI
    {
        public static Tower selectedTower;
        public static List<UIElement> elements;
        public static void Load()
        {
            elements = new List<UIElement>();
            elements.Add(new InteractiveElement(Vector2.Zero, Assets.Stats.Width, Assets.Stats.Height, Assets.Stats));
        }

        public static void SelectTower(Tower tower)
        {
            if (selectedTower != null)
            {
                selectedTower.SetRange(false);
            }
            selectedTower = tower;
            selectedTower.SetRange(true);
        }

        public static void Draw()
        {
            Vector2 statsPosition = new Vector2(Global.GameWidth, 0);
            Main.spriteBatch.Draw(Assets.Stats, statsPosition, null, Color.White, 0f, Assets.StatsOrigin, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Assets.Meny, new Vector2(Global.GameWidth, 0), null, Color.White, 0f, new Vector2(Assets.Meny.Width, 0), 1f, SpriteEffects.None, 0f);
            Vector2 temp = new Vector2(Global.GameWidth - 38, 1);
            Main.spriteBatch.DrawString(Assets.DefaultFont, Global.HP.ToString(), temp, Color.Black);
            temp = new Vector2(Global.GameWidth - 38, 17);
            Main.spriteBatch.DrawString(Assets.DefaultFont, Global.Coins.ToString(), temp, Color.Black);
            temp = new Vector2(100, 100);
            Main.spriteBatch.DrawString(Assets.DefaultFont, "Laser MK1", temp, Color.Black);
            Vector2 dims = Assets.DefaultFont.MeasureString("Laser MK1");
            Console.WriteLine(dims);
            //Main.spriteBatch.Draw(Assets.Meny, new Vector2(Global.GameWidth, 40), null, Color.White, 0f, new Vector2(Assets.Meny.Width, 0), 1f, SpriteEffects.None, 0f);
            foreach(var element in elements)
            {
                element.Draw();
            }
        }
        public static void HandleInput()
        {
            HandleLeftClick();
        }
        public static void HandleLeftClick()
        {
            if (Input.GetLeftClick())
            {
                foreach (var tower in Global.placedTowers)
                {
                    if (tower.Bounds.Contains(Input.MousePosition) && selectedTower != tower)
                    {
                        Console.WriteLine("Selected tower");
                        SelectTower(tower);
                        return;
                    }
                }
                foreach (var element in elements)
                {
                    //if (element is TowerElement && ((TowerElement)element).bounds.Contains(Input.MousePosition))
                    //{
                    //    selectedTower = element.tower;
                    //}
                }
                if (selectedTower != null)
                {
                    selectedTower = null;
                }
            }
        }
        
    }
}
