using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace Spelprojekt2
{
    public static class Global
    {
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

        public static List<Wave> Waves;

        public static void Load()
        {
            HP = StartHP;
            Coins = StartCoins;
            //Waves.Add(new Wave(new List<Burst>() { new Burst(1, null, 1f) }));
            GUI.Load();
        }

        public static void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            GUI.HandleInput();
        }

        public static T LoadJSON<T>(string relativePath)
        {
            string text = File.ReadAllText(relativePath);
            return (T)JsonConvert.DeserializeObject(text);
        }
        public static void SaveJSON(object item, string path)
        {
            string obj = JsonConvert.SerializeObject(item);
            StreamWriter sw = new StreamWriter(path, false);
            sw.Write(obj);
        }
    }
}
