using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt2
{
    public static class Global
    {
        public static int ScreenWidth { get; private set; } = 1920;
        public static int GameWidth { get; private set; } = 480;
        public static int GameHeight { get; private set; } = 270;
        public static int ScreenHeight { get; private set; } = 1080;

        public static Vector2 mousePosition { get; private set; }

        public static List<Tower> placedTowers;
        public static double time;

        public static void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            mousePosition = Mouse.GetState().Position.ToVector2() / 4f;
        }
    }
}
