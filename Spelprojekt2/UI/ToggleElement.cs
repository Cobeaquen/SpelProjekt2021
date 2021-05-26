using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spelprojekt2.UI
{
    public class ToggleElement : InteractiveElement
    {
        public delegate void ToggleInteract(bool state);
        public bool state;
        public Texture2D toggleTexture1;
        public Texture2D toggleTexture2;

        private ToggleInteract toggleCallback;

        public ToggleElement(Vector2 position, int width, int height, bool startState, Texture2D texture, Vector2 origin, Texture2D toggleTexture = null, ToggleInteract toggleCallback = null) : base(position, width, height, texture, origin)
        {
            this.toggleTexture1 = texture;
            this.toggleTexture2 = toggleTexture;
            this.toggleCallback = toggleCallback;
            state = startState;
        }
        public void Toggle()
        {
            state = !state;
            Console.WriteLine(state);
            // Förändra texturen
            SwitchTexture(state);
            toggleCallback(state);
        }
        public void SwitchTexture(bool second)
        {
            texture = second ? toggleTexture2 : toggleTexture1;
        }
    }
}
