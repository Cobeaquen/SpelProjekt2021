using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.SplineFlower;
using MonoGame.SplineFlower.Spline;
using MonoGame.SplineFlower.Spline.Types;

namespace Spelprojekt2
{
    public class Level
    {
        public List<Vector2> waypoints;
        public HermiteSpline splinePath;
        public float[] splineLengths;

        public Level()
        {
            waypoints = new List<Vector2>();
        }
        public Level(List<Vector2> waypoints)
        {
            this.waypoints = waypoints;
        }

        public static Level GenerateExampleLevel()
        {
            List<Transform> points = new List<Transform>();
            points.Add(new Transform(new Vector2(0, Global.GameHeight)));
            points.Add(new Transform(new Vector2(0, 100)));
            points.Add(new Transform(new Vector2(100, 100)));
            points.Add(new Transform(new Vector2(50, 50)));
            points.Add(new Transform(new Vector2(100, 20)));

            //return null;
            HermiteSpline spline = new HermiteSpline(points.ToArray());
            //spline.
            return new Level()
            {
                splinePath = spline,
                splineLengths = MapGenerator.GenerateDistances(spline)
            };

            //curve.Keys.Add(new CurveKey(new Vector2(0, Main.instance.GameHeight), ,));

        }

        public void Draw()
        {
            //splinePath.DrawSpline(Main.spriteBatch);
        }
    }
}
