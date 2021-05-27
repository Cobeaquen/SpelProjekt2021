using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public UpgradeElement(Vector2 position, int width, int height, Tower tower, Upgrade[] upgradeTiers, int tier, int path, UpgradeTower upgradeCallback) : base(position, width, height, null, Assets.UpgradeButton, Vector2.Zero)
        {
            this.upgrades = upgradeTiers;
            this.tier = tier;
            this.path = path;
            this.tower = tower;
            Visible = tier == 0 || path == tower.Path;
            this.upgradeCallback = upgradeCallback;
            NextUpgrade();
        }
        public void NextUpgrade()
        {
            display = upgrades[tier];
            if (upgrades.Length <= tier + 1)
            {
                name = new TextElement(position + new Vector2(4, 4), width, height, display.name, Color.White, Assets.DefaultFont);
                name.Text = "All upgrades bought!";
                desc = null;
                cost = null;
                return;
            }
            name = new TextElement(position + new Vector2(4, 4), width, height, display.name, Color.White, Assets.DefaultFont);
            desc = new TextElement(position + new Vector2(4, 20), width, height, display.desc, Color.White, Assets.DefaultFont);
            cost = new TextElement(position + new Vector2(4, 52), width, height, "Cost: " + display.cost.ToString(), Color.White, Assets.DefaultFont);
        }
        public override void Update()
        {
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
        {
            if (upgrades.Length <= tier + 1)
            {
                doneUpgrading = true;
                name.Text = "All upgrades bought!";
                desc = null;
                cost = null;
                return;
            }
            tier++;
            upgradeCallback(upgrades[tier], path, tier);
            NextUpgrade();
            base.Clicked();
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
