using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2.UI
{
    public static class GUI
    {
        public static Tower selectedTower;
        public static Tower towerHeld;
        public static List<UIElement> elements;
        public static TextElement HPElement;
        public static TextElement CoinsElement;
        public static TextElement WaveElement;
        public static TextureElement TowerPurcahseMenu;
        public static TowerWindow TowerMenu;
        public static ButtonElement WaveStartToggle;
        public static ButtonElement WavePauseToggle;

        private static bool placingTower;
        private static bool TowerOverLevel;
        private static bool TowerOverTower;
        private static bool CanPlace;

        public static void Load()
        {
            placingTower = false;
            TowerOverLevel = false;
            TowerOverTower = false;

            elements = new List<UIElement>();
            elements.Add(new InteractiveElement(Vector2.Zero, Assets.Stats.Width, Assets.Stats.Height, Assets.Stats, Vector2.Zero));

            HPElement = new TextElement(new Vector2(18, 1), 100, 12, Global.HP.ToString(), Color.Black, Assets.DefaultFont);
            CoinsElement = new TextElement(new Vector2(18, 17), 100, 12, Global.Coins.ToString(), Color.Black, Assets.DefaultFont);
            TowerPurcahseMenu = new TextureElement(new Vector2(Global.GameWidth, 0), Assets.Meny, Assets.MenyOrigin, Color.White);
            TowerMenu = new TowerWindow(new Vector2(40, 40));
            elements.Add(HPElement);
            elements.Add(CoinsElement);
            elements.Add(TowerPurcahseMenu);

            WaveStartToggle = new ButtonElement(new Vector2(Assets.PauseWave.Width, Global.GameHeight), Assets.StartWave.Width, Assets.StartWave.Height, WaveControl.ToggleStart, Assets.StartWave, Assets.WaveButtonOrigin);
            WavePauseToggle = new ButtonElement(new Vector2(0, Global.GameHeight), Assets.PauseWave.Width, Assets.StartWave.Height, WaveControl.TogglePause, Assets.PauseWave, Assets.WaveButtonOrigin);
            WaveElement = new TextElement(new Vector2(40, Global.GameHeight - 16), 100, 20, "Wave: " + Main.instance.level.wave.ToString(), Color.Black, Assets.DefaultFont);
            elements.Add(WaveStartToggle);
            elements.Add(WavePauseToggle);
            elements.Add(WaveElement);
            //List<Tower.TowerInfo> tis = new List<Tower.TowerInfo>() { new Tower.TowerInfo(typeof(GunTower), "Gun Tower", "Most basic tower, great for medium-sized groups of enemies.", 50, "guntower") };
            //Global.SaveJSON(tis, "towers.json");
            //return;
            Tower.TowerInfo[] towerInfos = Global.LoadJSON<Tower.TowerInfo[]>("towers.json");

            for (int i = 0; i < towerInfos.Length; i++)
            {
                towerInfos[i].SetSprite();
                elements.Add(new TowerElement(new Vector2(TowerPurcahseMenu.position.X - 58f, 10 + i * 40), 32, 32, towerInfos[i]));
            }
        }

        public static void Update()
        {
            HPElement.Text = Global.HP.ToString();
            CoinsElement.Text = Global.Coins.ToString();
            WaveElement.Text = "Wave: " + (Main.instance.level.wave + 1).ToString();

            foreach (var e in elements)
            {
                e.Update();
            }
            TowerMenu.Update();

            if (placingTower)
            {
                PlaceTower();
            }
        }

        public static void SelectTower(Tower tower)
        {
            if (selectedTower != null)
            {
                selectedTower.SetRange(false);
            }
            selectedTower = tower;
            selectedTower.SetRange(true);
            TowerMenu.visible = true;
            TowerMenu.tower = tower;
        }

        public static void Draw()
        {
            Vector2 statsPosition = new Vector2(Global.GameWidth, 0);
            Main.spriteBatch.Draw(Assets.Stats, statsPosition, null, Color.White, 0f, Assets.StatsOrigin, 1f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(Assets.Meny, new Vector2(Global.GameWidth, 0), null, Color.White, 0f, new Vector2(Assets.Meny.Width, 0), 1f, SpriteEffects.None, 0f);
            //Vector2 temp = new Vector2(38, 1);
            //Main.spriteBatch.DrawString(Assets.DefaultFont, Global.HP.ToString(), temp, Color.Black);
            //temp = new Vector2(18, 17);
            //Main.spriteBatch.DrawString(Assets.DefaultFont, Global.Coins.ToString(), temp, Color.Black);
            //temp = new Vector2(100, 100);
            //Main.spriteBatch.DrawString(Assets.DefaultFont, "Laser MK1", temp, Color.Black);
            //Vector2 dims = Assets.DefaultFont.MeasureString("Laser MK1");
            //Console.WriteLine(dims);
            //Main.spriteBatch.Draw(Assets.Meny, new Vector2(Global.GameWidth, 40), null, Color.White, 0f, new Vector2(Assets.Meny.Width, 0), 1f, SpriteEffects.None, 0f);
            
            foreach(var element in elements)
            {
                element.Draw();
            }
            TowerMenu.Draw();
        }
        public static void HandleInput()
        {
            HandleLeftClick();
            HandleRightClick();
        }
        private static void HandleLeftClick()
        {
            if (Input.GetLeftClick())
            {
                foreach (var tower in Global.PlacedTowers)
                {
                    if (tower.Bounds.Contains(Input.MousePosition) && selectedTower != tower)
                    {
                        Console.WriteLine("Selected tower");
                        SelectTower(tower);
                        return;
                    }
                }
                foreach(var element in elements)
                {
                    element.Update();
                    if (element is ButtonElement ie && ie.mouseOver)
                    { // Elementet är interaktivt - kör lämpliga funktioner
                        ie.Clicked();
                        return;
                    }
                }
                if (placingTower && !TowerOverLevel && !TowerOverTower)
                { // Placera om möjligt
                    FinishTowerPlacement();
                    return;
                }
                if (selectedTower != null)
                {
                    selectedTower = null;
                }
            }
        }
        private static void HandleRightClick()
        {
            if (!Input.GetRightClick())
                return;
            if (placingTower)
            { // Avbryt placering
                towerHeld = null;
                placingTower = false;
                Tower.RangeColor = Color.Black;
                selectedTower = null;
            }
        }

        public static void StartTowerPlacement(Tower tower)
        {
            placingTower = true;
            towerHeld = tower;
            towerHeld.Position = Input.MousePosition;
        }
        public static void PlaceTower()
        {
            towerHeld.Position = Input.MousePosition.ToPoint().ToVector2();
            towerHeld.UpdateBounds();

            if (!Global.CanAfford(towerHeld.towerInfo.cost))
            {
                Tower.RangeColor = Color.Red;
                CanPlace = false;
                return;
            }

            // Se om den inkräktar banans yta
            //over = over || Main.instance.level.IsInsideLineRange(new Vector2(towerHeld.Bounds.Left, towerHeld.Bounds.Top));
            //over = over || Main.instance.level.IsInsideLineRange(new Vector2(towerHeld.Bounds.Right, towerHeld.Bounds.Top));
            //over = over || Main.instance.level.IsInsideLineRange(new Vector2(towerHeld.Bounds.Left, towerHeld.Bounds.Bottom));
            //over = over || Main.instance.level.IsInsideLineRange(new Vector2(towerHeld.Bounds.Right, towerHeld.Bounds.Bottom));
            TowerOverLevel = Main.instance.level.IsInsideLineRange(towerHeld.Position);

            TowerOverTower = false;

            if (!TowerOverLevel)
            {
                foreach (var t in Global.PlacedTowers)
                {
                    if (towerHeld.Bounds.Intersects(t.Bounds))
                    {
                        TowerOverTower = true;
                        break;
                    }
                }
            }

            if (TowerOverLevel || TowerOverTower || CanPlace)
            { // Rita både tornet och range med röd färg.
                Tower.RangeColor = Color.Red;
            }
            else
            {
                Tower.RangeColor = Color.Black;
            }
        }
        public static void FinishTowerPlacement()
        {
            if (Global.Buy(towerHeld.towerInfo.cost))
            {
                placingTower = false;
                Global.PlacedTowers.Add(towerHeld);
                towerHeld.OnPlaced();
                selectedTower = towerHeld;
                towerHeld = null;
                Console.WriteLine("Bought");
            }
        }
    }
}
