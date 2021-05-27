using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2.UI
{
    public class TowerWindow
    {
        public bool visible { get; set; }
        public Tower tower { get; set; }
        public Vector2 Position { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Rectangle MoveArea { get; private set; }
        public Rectangle CloseArea { get; private set; }
        public TextureElement TowerPreviewBody { get; private set; }
        public TextureElement TowerPreviewHead { get; private set; }
        public TextElement TowerName { get; private set; }
        public TextElement TotalDamage { get; private set; }
        public TextElement CashEarned { get; private set; }
        public ButtonElement SellButton { get; private set; }
        public UpgradeElement[] Upgrades { get; private set; }

        private List<UIElement> elements;

        private Vector2 prevPos;
        private bool move;
        public TowerWindow(Vector2 position)
        {
            this.Position = position;
            prevPos = Position;
            visible = false;
            move = false;
            UpdateBounds();
            elements = new List<UIElement>();
            TowerPreviewBody = new TextureElement(Position + new Vector2(3, 11), null, Vector2.Zero, Color.White);
            TowerPreviewHead = new TextureElement(Position + new Vector2(19, 27), null, Assets.GunTowerHeadOrigin, Color.White, MathHelper.PiOver2);
            TowerName = new TextElement(Position + new Vector2(48, 22), 200, 100, "", Color.White, Assets.DefaultFont);
            SellButton = new ButtonElement(position + new Vector2(48, 112), Assets.SellButton.Width, Assets.SellButton.Height, SellTower, Assets.SellButton, Vector2.Zero);

            elements.Add(TowerPreviewBody);
            elements.Add(TowerPreviewHead);
            elements.Add(TowerName);
            elements.Add(SellButton);
        }
        public void UpdateBounds()
        {
            Bounds = new Rectangle(Position.ToPoint(), new Point(Assets.TowerMenu.Width, Assets.TowerMenu.Height));
            MoveArea = new Rectangle(Position.ToPoint(), new Point(Assets.TowerMenu.Width - 12, 9));
            CloseArea = new Rectangle(new Vector2(Position.X + Assets.TowerMenu.Width - 12, Position.Y).ToPoint(), new Point(12, 9));
        }
        public void Update()
        {
            if (!visible)
                return;
            if (!move && Input.GetLeftClick() && CloseArea.Contains(Input.MousePosition))
            {
                visible = false;
                return;
            }
            else if (!move && Input.GetLeftClick() && MoveArea.Contains(Input.MousePosition))
            { // Flytta fönstret
                move = true;
                prevPos = Input.MousePosition;
            }
            else if (move && Input.Mstate.LeftButton == ButtonState.Released)
            {
                move = false;
            }
            if (move)
            {
                Vector2 newPos = Input.MousePosition - prevPos;
                Position += newPos;
                foreach (var e in elements)
                {
                    e.position += newPos;
                    SellButton.UpdateBounds();
                }
                if (Upgrades != null)
                {
                    foreach (var u in Upgrades)
                    {
                        u.position += newPos;
                        u.UpdateBounds();
                        u.Update();
                    }
                }
                UpdateBounds();
            }
            else if (Upgrades != null)
            {
                foreach (var u in Upgrades)
                {
                    u.Update();
                    if (u.mouseOver && Input.GetLeftClick())
                    {
                        u.Clicked();
                    }
                }
            }
            SellButton.Update();
            if (Input.GetLeftClick() && SellButton.mouseOver)
            {
                SellButton.Clicked();
            }
            prevPos = Input.MousePosition;
        }
        public void HandleInput()
        { // TODO: THIS!!!

        }
        public void SelectTower(Tower tower)
        {
            this.tower = tower;
            int paths = tower.Upgrades.GetLength(0);
            Upgrades = new UpgradeElement[paths];
            TowerName.Text = tower.towerInfo.name;
            for (int path = 0; path < paths; path++)
            {
                Upgrades[path] = new UpgradeElement(Position + new Vector2(Bounds.Width / 2, 9 + 71 * path), 149, 69, tower, tower.Upgrades[path], tower.Tier, path, UpgradeTower);
            }
            visible = true;
            TowerPreviewBody.texture = tower.bodySprite;
            TowerPreviewHead.texture = tower.headSprite;
        }
        public void UpgradeTower(Upgrade upgrade, int path, int tier)
        {
            Console.WriteLine("Upgraded tower");
            tower.Upgrade(path, tier);
            int not = path == 0 ? 1 : 0;
            Upgrades[not].Visible = false;
        }
        public void SellTower()
        {
            Console.WriteLine("Sell tower");
            Global.Sell(tower);
            visible = false;
        }
        public void Draw()
        {
            if (!visible)
                return;
            Main.spriteBatch.Draw(Assets.TowerMenu, Position.ToPoint().ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //TowerPreviewBody.Draw();
            //TowerPreviewHead.Draw();
            foreach (var e in elements)
            {
                e?.Draw();
            }
            if (Upgrades != null)
            {
                foreach (var u in Upgrades)
                {
                    u.Draw();
                }
            }
            
            //Main.spriteBatch.Draw(DebugTextures.GenerateRectangle(CloseArea.Width, CloseArea.Height, Color.Yellow), CloseArea.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
