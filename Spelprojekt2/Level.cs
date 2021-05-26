using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.SplineFlower;
using MonoGame.SplineFlower.Spline;
using MonoGame.SplineFlower.Spline.Types;
using Spelprojekt2.Enemies;

namespace Spelprojekt2
{
    public class Level
    {
        public List<Enemy> enemies = new List<Enemy>();
        public List<Waypoint> waypoints;
        public int wave = 0;
        public Vector2 StartDirection { get; private set; }
        public List<Wave> Waves { get; private set; }
        //public HermiteSpline splinePath;
        public float[] splineLengths;

        private Texture2D sprite;
        private float spawnTime;
        private float waitTime;

        public Level(Texture2D sprite, List<Vector2> waypointsData)
        {
            this.sprite = sprite;
            waypoints = new List<Waypoint>();

            Vector2 prevPoint = waypointsData[0];

            foreach (var point in waypointsData)
            {
                float len = Vector2.Distance(prevPoint, point);
                waypoints.Add(new Waypoint(point, len));
                prevPoint = point;
            }
            StartDirection = waypoints[0].point - waypoints[1].point;

            //Waves = new List<Wave>();
            //Waves.Add(new Wave(new List<Burst>() { new Burst(1, typeof(Minion).FullName, 1f) }));
            //Waves.Add(new Wave(new List<Burst>() { new Burst(1, typeof(Minion).FullName, 1f) }));
            Waves = Global.LoadJSON<List<Wave>>("waves.json");

            //SaveJSON(Waves, "waves.json");
        }
        public void LoadEnemies()
        {
            foreach (var w in Waves)
            {
                foreach (var b in w.bursts)
                {
                    b.enemy = b.GetEnemyDuplicate();
                }
            }
            waitTime = Waves[0].bursts[0].timeInterval;
        }
        public Level(List<Waypoint> waypoints, Texture2D sprite)
        {
            this.waypoints = waypoints;
            this.sprite = sprite;
        }

        public static Level GenerateExampleLevel()
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(0 + 1, 191 + 1));
            points.Add(new Vector2(87 + 1, 191 + 1));
            points.Add(new Vector2(136 + 1, 144 + 1));
            points.Add(new Vector2(168 + 1, 144 + 1));
            points.Add(new Vector2(207 + 1, 184 + 1));
            points.Add(new Vector2(207 + 1, 200 + 1));
            points.Add(new Vector2(248 + 1, 239 + 1));
            points.Add(new Vector2(312 + 1, 239 + 1));
            points.Add(new Vector2(351 + 1, 199 + 1));
            points.Add(new Vector2(351 + 1, 167 + 1));
            points.Add(new Vector2(296 + 1, 112 + 1));
            points.Add(new Vector2(231 + 1, 112 + 1));
            points.Add(new Vector2(207 + 1, 88 + 1));
            points.Add(new Vector2(207 + 1, 56 + 1));
            points.Add(new Vector2(232 + 1, 32 + 1));
            points.Add(new Vector2(328 + 1, 32 + 1));
            points.Add(new Vector2(415 + 1, 120 + 1));
            points.Add(new Vector2(415 + 1, 269 + 1));
            return new Level(Assets.Level1, points);
        }

        public void Update(GameTime gameTime)
        {
            if (spawnTime > waitTime && Global.gameState == Global.GameState.Running)
            { // Spawn new enemy
                spawnTime = 0f;
                if (Waves.Count > wave)
                {
                    bool end = Waves[wave].GetEnemy(out Enemy enemy);
                    enemies.Add(enemy);
                    if (end)
                    { // Denna Wave har skickat ut alla sina fiender
                        Global.gameState = Global.GameState.DoneWave;
                        //wave++;
                    }
                    else
                    {
                        waitTime = Waves[wave].GetCurrentTimeInterval() + (Waves[wave].state == Wave.State.Waiting ? Waves[wave].restingTime : 0f);
                    }
                }
                else
                { // Inga fler waves
                    return;
                }
            }

            spawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds * Global.gameSpeed;

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);
            }
        }

        public Vector2 GetPosition(int progress, float t, out bool outOfBounds)
        {
            outOfBounds = progress > waypoints.Count - 1;
            if (outOfBounds)
            {
                return Vector2.Zero;
            }
            return Vector2.Lerp(waypoints[progress - 1].point, waypoints[progress].point, t);
        }
        public Vector2 GetDirection(int progress)
        {
            if (waypoints.Count <= progress)
            {
                return Vector2.Zero;
            }
            return waypoints[progress - 1].point - waypoints[progress].point;
        }

        public void StartWave()
        {
            wave++;
        }

        public bool IsInsideLineRange(Vector2 pos)
        {
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                if (Global.GetDistanceFromLine(waypoints[i].point, waypoints[i + 1].point, pos) < 35f)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            //splinePath.DrawSpline(Main.spriteBatch);
            // Rita bakgrunden
            Main.spriteBatch.Draw(sprite, Vector2.Zero, Color.White);

            foreach (var enemy in enemies)
            {
                enemy.Draw();
            }
        }
    }
}
