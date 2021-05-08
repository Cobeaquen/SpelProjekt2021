using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public static class WaveControl
    {
        public static void ToggleStart(bool state)
        {
            if (state)
            { // Starta ny wave, eller långsamma ner spelet
                if (Global.gameState != Global.GameState.Playing)
                { // Starta ny wave
                    Global.gameState = Global.GameState.Playing;
                }
                else
                { // Långsamma ner skiten
                    Global.gameSpeed = 1f;
                }
            }
            else
            { // Snabba på :D
                Global.gameSpeed = 2f;
            }
        }
        public static void TogglePause(bool state)
        {
            Console.WriteLine("PAUSE");
            if (state)
            { // Pausa

            }
            else
            { // Fortsätt spela

            }
        }
    }
}
