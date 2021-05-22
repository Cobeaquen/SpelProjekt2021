using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using Spelprojekt2.Effects;

namespace Spelprojekt2
{
    public static class Global
    {
        public static GameState gameState { get; set; }
        public static bool Paused { get; private set; }
        public static float gameSpeed { get; set; }

        public static int ScreenWidth { get; private set; } = 1920;
        public static int GameWidth { get; private set; } = 480;
        public static int GameHeight { get; private set; } = 270;
        public static int ScreenHeight { get; private set; } = 1080;

        public static int StartHP = 200;
        public static int HP;
        public static int StartCoins = 100;
        public static int Coins;

        public static List<Tower> placedTowers;
        public static List<ParticleEffect> Effects;
        public static double time;

        public static Random ran = new Random();

        public static void Load()
        {
            Effects = new List<ParticleEffect>();
            gameState = GameState.Idle;
            gameSpeed = 1f;
            HP = StartHP;
            Coins = StartCoins;
            GUI.Load();
        }

        public static void Update(GameTime gameTime)
        {
            if (!Paused)
            {
                time += gameTime.ElapsedGameTime.TotalSeconds;
                foreach (Tower tower in placedTowers)
                {
                    tower.Update(gameTime);
                }
                for (int i = 0; i < Effects.Count; i++)
                {
                    Effects[i].Update(gameTime);
                }
                Main.instance.level.Update(gameTime);
            }
            GUI.Update();
            GUI.HandleInput();
        }
        public static void Pause()
        {
            Paused = true;
        }
        public static void Resume()
        {
            Paused = false;
        }

        public static bool Buy(int cost)
        {
            bool buy = cost <= Coins;
            Coins = buy ? Coins - cost : Coins;
            return buy;
        }

        public static void DrawEffects()
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                Effects[i].Draw();
            }
        }

        public static T LoadJSON<T>(string relativePath)
        {
            string text = File.ReadAllText("data/" + relativePath);
            object obj = JsonConvert.DeserializeObject<T>(text);


            return (T)JsonConvert.DeserializeObject<T>(text);
        }
        public static void SaveJSON<T>(T item, string path)
        {
            string obj = JsonConvert.SerializeObject(item, Formatting.Indented);
            StreamWriter sw = new StreamWriter("data/" + path, false);
            sw.Write(obj);
            sw.Close();
        }

        public static float GetDistanceFromLine(Vector2 a, Vector2 b, Vector2 p)
        {
            Vector2 AB = b - a;
            Vector2 BP = p - b;
            Vector2 AP = p - a;

            float AB_BP = Vector2.Dot(AB, BP);
            float AB_AP = Vector2.Dot(AB, AP);

            if (AB_BP > 0)
            {

                // Finding the magnitude
                Vector2 vec = p - b;
                return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            }

            // Case 2
            else if (AB_AP < 0)
            {
                Vector2 vec = p - a;
                return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            }

            else
            { // Vinkelrät
                float mod = (float)Math.Sqrt(AB.X * AB.X + AB.Y * AB.Y);
                return (float)Math.Abs(AB.X * AP.Y - AB.Y * AP.X) / mod;
            }
        }
        public static float GetShortestAngle(float from, float to)
        {
            float maxAngle = MathHelper.TwoPi;
            float diff = (to - from) % maxAngle;
            return ((2 * diff) % maxAngle) - diff;
        }

        public enum GameState
        {
            Running, Idle, DoneWave
        }
    }
}
