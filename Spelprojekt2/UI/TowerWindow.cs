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
        public TextElement TowerName { get; private set; }
        public TextElement TotalDamage { get; private set; }
        public TextElement CashEarned { get; private set; }
        public TextElement Upgrade { get; private set; }
        public UpgradeElement[] Upgrades { get; private set; }

        private Vector2 prevPos;
        private bool move;
        public TowerWindow(Vector2 position)
        {
            this.Position = position;
            prevPos = Position;
            visible = true;
            move = false;
            UpdateBounds();
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
                Position = Position;
                UpdateBounds();
            }
            prevPos = Input.MousePosition;
        }
        public void SelectTower(Tower tower)
        {
            this.tower = tower;
        }
        public void Draw()
        {
            if (!visible)
                return;
            Main.spriteBatch.Draw(Assets.TowerMenu, Position.ToPoint().ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(DebugTextures.GenerateRectangle(CloseArea.Width, CloseArea.Height, Color.Yellow), CloseArea.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
