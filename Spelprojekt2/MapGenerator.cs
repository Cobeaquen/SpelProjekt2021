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
    public static class MapGenerator
    {
        public static float[] GenerateDistances(SplineBase spline)
        {
            float[] dists = new float[spline.GetAllPoints.Length - 1];
            for (int i = 0; i < spline.GetAllPoints.Length - 1; i++)
            {
                dists[i] = GetSplineSegmentLength(spline.GetAllPoints[i], spline.GetAllPoints[i + 1]);
            }

            return dists;
        }
        public static float GetSplineLength(SplineBase spline)
        {
            float dx = 0.01f;
            Vector2 prevPoint;
            float dist = 0f;

            for (int i = 0; i < spline.GetAllPoints.Length - 1; i++)
            {
                Transform t = spline.GetAllPoints[i];
                prevPoint = t.Position;
                float x = 0f;
                while (x < 1f)
                {
                    Vector2 newPoint = spline.GetPoint(x);
                    dist += Vector2.Distance(prevPoint, newPoint);
                    prevPoint = newPoint;
                    x += dx;
                }
            }

            return dist;
        }
        public static float GetSplineSegmentLength(Transform a, Transform b)
        {
            float x = 0f;
            float dx = 0.001f;
            float dist = 0f;
            while (x < 1f)
            {
                dist = Vector2.Distance(a.Position, b.Position);
                x += dx;
            }
            return dist;
        }
    }
}
