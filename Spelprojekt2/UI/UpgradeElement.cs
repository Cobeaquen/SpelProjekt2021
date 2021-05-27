using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spelprojekt2.Towers;

namespace Spelprojekt2.UI
{
    public class UpgradeElement : ButtonElement
    {
        public delegate void UpgradeTower(Upgrade upgrade, int path, int tier);
        public UpgradeTower upgradeCallback;
        public Upgrade[] upgrades;
        private int tier;
        private int path;
        private Tower tower;
        private Upgrade display;

        private TextElement name;
        private TextElement desc;
        private TextElement cost;

        private bool doneUpgrading;
        public UpgradeElement(Vector2 position, int width, int height, int path, UpgradeTower upgradeCallback) : base(position, width, height, null, Assets.UpgradeButton, Vector2.Zero)
        {
            this.path = path;
            Visible = tier == 0 || path == tower.Path;
            this.upgradeCallback = upgradeCallback;
            name = new TextElement(position + new Vector2(4, 4), width, height, "", Color.White, Assets.DefaultFont);
        }
        public void SelectTower(Tower tower, Upgrade[] upgradeTiers, int tier, int path)
        {
            this.tower = tower;
            this.upgrades = upgradeTiers;
            this.tier = tier;
            this.path = path;

            Visible = tier == 0 || path == tower.Path;
            NextUpgrade();
        }
        public void NextUpgrade()
        {
            if (upgrades.Length <= tier)
            {
                doneUpgrading = true;
                name.Text = "All upgrades bought!";
                color = Color.White;
                desc = null;
                cost = null;
                return;
            }
            display = upgrades[tier];
            name = new TextElement(position + new Vector2(4, 4), width, height, display.name, Color.White, Assets.DefaultFont);
            desc = new TextElement(position + new Vector2(4, 20), width, height, display.desc, Color.White, Assets.DefaultFont);
            cost = new TextElement(position + new Vector2(4, 52), width, height, "Cost: " + display.cost.ToString(), Color.White, Assets.DefaultFont);
        }
        public override void Update()
        {
            if (!doneUpgrading)
                color = Global.CanAfford(display.cost) ? Color.White : Color.Red;
            name.position = position + new Vector2(4, 4);
            if (desc != null)
                desc.position = position + new Vector2(4, 20);
            if (cost != null)
                cost.position = position + new Vector2(4, 52);
            base.Update();
        }
        public void HandleInput()
        {

        }
        public override void Clicked()
        { // Uppgradera om möjligt
            if (upgrades.Length <= tier)
            {
                doneUpgrading = true;
                name.Text = "All upgrades bought!";
                desc = null;
                cost = null;
                return;
            }
            if (Global.Buy(upgrades[tier].cost))
            {
                tier++;
                upgradeCallback(upgrades[tier - 1], path, tier);
                NextUpgrade();
                base.Clicked();
            }
        }
        public override void Draw()
        {
            if (!Visible)
            {
                return;
            }
            base.Draw();
            name?.Draw();
            desc?.Draw();
            cost?.Draw();
        }
    }
}
