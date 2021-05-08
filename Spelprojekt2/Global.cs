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

namespace Spelprojekt2
{
    public static class Global
    {
        public static GameState gameState { get; set; }
        public static float gameSpeed { get; set; }

        public static int ScreenWidth { get; private set; } = 1920;
        public static int GameWidth { get; private set; } = 480;
        public static int GameHeight { get; private set; } = 270;
        public static int ScreenHeight { get; private set; } = 1080;

        public static int StartHP = 200;
        public static int HP;
        public static int StartCoins = 400;
        public static int Coins;

        public static List<Tower> placedTowers;
        public static double time;

        public static void Load()
        {
            gameState = GameState.Idle;
            gameSpeed = 1f;
            HP = StartHP;
            Coins = StartCoins;
            GUI.Load();
        }

        public static void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            GUI.Update();
            GUI.HandleInput();
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

        public enum GameState
        {
            Playing, Paused, Idle
        }
    }
}
