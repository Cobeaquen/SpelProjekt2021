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

namespace Spelprojekt2
{
    public class Level
    {
        public List<Enemy> Enemies = new List<Enemy>();
        public List<Waypoint> waypoints;
        public int wave = 0;
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

            //Waves = new List<Wave>();
            //Waves.Add(new Wave(new List<Burst>() { new Burst(1, typeof(Minion).FullName, 1f) }));
            //Waves.Add(new Wave(new List<Burst>() { new Burst(1, typeof(Minion).FullName, 1f) }));
            Waves = Global.LoadJSON<List<Wave>>("waves.json");
            foreach (var w in Waves)
            {
                foreach (var b in w.bursts)
                {
                    b.enemy = b.GetEnemyDuplicate();
                }
            }
            waitTime = Waves[0].bursts[0].timeInterval;

            //SaveJSON(Waves, "waves.json");
        }
        public Level(List<Waypoint> waypoints, Texture2D sprite)
        {
            this.waypoints = waypoints;
            this.sprite = sprite;
        }

        public static Level GenerateExampleLevel()
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(0, 191));
            points.Add(new Vector2(87, 191));
            points.Add(new Vector2(136, 144));
            points.Add(new Vector2(168, 144));
            points.Add(new Vector2(207, 184));
            points.Add(new Vector2(207, 200));
            points.Add(new Vector2(248, 239));
            points.Add(new Vector2(312, 239));
            points.Add(new Vector2(351, 199));
            points.Add(new Vector2(351, 167));
            points.Add(new Vector2(296, 112));
            points.Add(new Vector2(231, 112));
            points.Add(new Vector2(207, 88));
            points.Add(new Vector2(207, 56));
            points.Add(new Vector2(232, 32));
            points.Add(new Vector2(328, 32));
            points.Add(new Vector2(415, 120));
            points.Add(new Vector2(415, 269));
            return new Level(Assets.Level1, points);
        }

        public void Update(GameTime gameTime)
        {
            if (spawnTime > waitTime && Global.gameState == Global.GameState.Playing)
            { // Spawn new enemy
                spawnTime = 0f;
                if (Waves.Count > wave)
                {
                    waitTime = Waves[wave].GetCurrentTimeInterval() / Global.gameSpeed;
                    Enemy enemy = Waves[wave].GetEnemy();
                    if (enemy == null)
                    { // Denna Wave är slut
                        wave++;
                        Global.gameState = Global.GameState.Idle;
                    }
                    else
                        Enemies.Add(enemy);
                }
                else
                { // Inga fler waves
                    return;
                }
            }

            spawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(gameTime);
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

        public void StartWave()
        {
            wave++;
        }

        public void Draw()
        {
            //splinePath.DrawSpline(Main.spriteBatch);
            // Rita bakgrunden
            Main.spriteBatch.Draw(sprite, Vector2.Zero, Color.White);

            foreach (var enemy in Enemies)
            {
                enemy.Draw();
            }
        }
    }
}
