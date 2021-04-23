using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Spel
{
    public static class Assets
    {
        public static Texture2D sprite;
        
        public static void LoadAssets(ContentManager content)
        {
            content.Load<Texture2D>("sprite");
        }
    }
}
