using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public static class WaveControl
    {
        private static bool spedUp;
        private static bool paused;
        public static void ToggleStart()
        {
            if (Global.gameState != Global.GameState.Running && Global.gameState != Global.GameState.DoneWave)
            { // Starta ny wave
                paused = false;
                Global.gameState = Global.GameState.Running;
                GUI.WaveStartToggle.texture = spedUp ? Assets.SpeedWave2 : Assets.SpeedWave;
            }
            else
            { // Långsamma ner eller öka hastigheten på skiten
                Global.gameSpeed = 1f;
                spedUp = !spedUp;
                Global.gameSpeed = spedUp ? 2f : 1f;
                GUI.WaveStartToggle.texture = spedUp ? Assets.SpeedWave2 : Assets.SpeedWave;
            }
        }
        public static void TogglePause()
        {
            paused = !paused;
            GUI.WavePauseToggle.texture = paused ? Assets.PlayWave : Assets.PauseWave;
            if (paused)
            { // Pausa
                Global.Pause();
            }
            else
            { // Fortsätt spela
                Global.Resume();
            }
        }

        public enum GameFlow
        {
            Paused, Running, RunningFast, Idle
        }
    }
}
