using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public static class Input
    {
        public static KeyboardState PrevKstate { get; private set; }
        public static KeyboardState Kstate { get; private set; }
        public static MouseState PrevMstate { get; private set; }
        public static MouseState Mstate { get; private set; }
        public static Vector2 MousePosition { get; private set; }
        public static void BeginInput()
        {
            Kstate = Keyboard.GetState();
            Mstate = Mouse.GetState();
            MousePosition = Mstate.Position.ToVector2() / 4f;
        }
        public static void EndInput()
        {
            PrevKstate = Kstate;
            PrevMstate = Mstate;
        }

        public static bool Pressed(Keys key)
        {
            return Kstate.IsKeyDown(key) && PrevKstate.IsKeyUp(key);
        }

        public static bool GetLeftClick()
        {
            return Mstate.LeftButton == ButtonState.Pressed && PrevMstate.LeftButton == ButtonState.Released;
        }
        public static bool GetRightClick()
        {
            return Mstate.RightButton == ButtonState.Pressed && PrevMstate.RightButton == ButtonState.Released;
        }
    }
}
