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
        public static void Load()
        {

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
            Vector2 temp = new Vector2(Global.GameWidth - 38, 1);
            Main.spriteBatch.DrawString(Assets.DefaultFont, Global.HP.ToString(), temp, Color.Black);
            temp = new Vector2(Global.GameWidth - 38, 17);
            Main.spriteBatch.DrawString(Assets.DefaultFont, Global.Coins.ToString(), temp, Color.Black);
            //Main.spriteBatch.Draw(Assets.Meny, new Vector2(Global.GameWidth, 40), null, Color.White, 0f, new Vector2(Assets.Meny.Width, 0), 1f, SpriteEffects.None, 0f);
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
                if (selectedTower != null)
                {
                    selectedTower = null;
                }
            }
        }
    }
}
