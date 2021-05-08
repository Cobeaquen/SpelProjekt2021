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
        public static Tower towerHeld;
        public static List<UIElement> elements;
        public static TextElement HPElement;
        public static TextElement CoinsElement;
        public static TextureElement TowerPurcahseMenu;
        public static ToggleElement WaveStartToggle;
        public static ToggleElement WavePauseToggle;

        public static bool placingTower;

        public static void Load()
        {
            placingTower = false;

            elements = new List<UIElement>();
            elements.Add(new InteractiveElement(Vector2.Zero, Assets.Stats.Width, Assets.Stats.Height, Assets.Stats, Vector2.Zero));

            HPElement = new TextElement(new Vector2(18, 1), 100, 12, Global.HP.ToString(), Color.Black, Assets.DefaultFont);
            CoinsElement = new TextElement(new Vector2(18, 17), 100, 12, Global.Coins.ToString(), Color.Black, Assets.DefaultFont);
            TowerPurcahseMenu = new TextureElement(new Vector2(Global.GameWidth, 0), Assets.Meny, Assets.MenyOrigin);
            elements.Add(HPElement);
            elements.Add(CoinsElement);
            elements.Add(TowerPurcahseMenu);

            WaveStartToggle = new ToggleElement(new Vector2(Assets.PauseWave.Width, Global.GameHeight), Assets.StartWave.Width, Assets.StartWave.Height, false, Assets.StartWave, Assets.WaveButtonOrigin, Assets.SpeedWave, WaveControl.ToggleStart);
            WavePauseToggle = new ToggleElement(new Vector2(0, Global.GameHeight), Assets.PauseWave.Width, Assets.StartWave.Height, false, Assets.PauseWave, Assets.WaveButtonOrigin, Assets.PlayWave, WaveControl.TogglePause);
            elements.Add(WaveStartToggle);
            elements.Add(WavePauseToggle);
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

            towerHeld?.Draw();
            
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
                        if (placingTower)
                        { // Avbryt placering

                        }
                        Console.WriteLine("Selected tower");
                        SelectTower(tower);
                        return;
                    }
                }
                foreach(var element in elements)
                {
                    if (element is InteractiveElement ie)
                    { // Elementet är interaktivt - kör lämpliga funktioner
                        if (ie.bounds.Contains(Input.MousePosition))
                        {
                            ie.Clicked();
                            return;
                        }
                    }
                }
                if (placingTower)
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

        public static void StartTowerPlacement(Tower tower)
        {
            placingTower = true;
            towerHeld = tower;
        }
        public static void PlaceTower()
        {
            towerHeld.Position = Input.MousePosition;
        }
        public static void FinishTowerPlacement()
        {
            placingTower = false;
            Global.placedTowers.Add(towerHeld);
            towerHeld.OnPlaced();
            selectedTower = towerHeld;
            towerHeld = null;
        }
    }
}
