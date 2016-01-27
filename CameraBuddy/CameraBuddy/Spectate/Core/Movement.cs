using System;
using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Spectate.Data;
using CameraBuddy.Spectate.Situation;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Player = CameraBuddy.Spectate.Situation.Player;

namespace CameraBuddy.Spectate.Core
{
    public static class Movement
    {
        public static Vector3 RandomPoint(this Vector3 point, float minDistance = 0, float maxDistance = 100)
        {
            return GetPossiableMovements(point, true, 60, minDistance, maxDistance).RandomItem();
        }
        public static Vector3[] GetPointsOnCirlce(Vector3 origin, float radius, float steps = 50)
        {
            var points = new List<Vector3>();
            var origin2D = origin.To2D();
            for (float a = 0; a < 360; a = a + steps) // For each angle via steps
            {
                var x = origin2D.X + (radius * Math.Cos(a.ToRadians()));
                var y = origin2D.Y + (radius * Math.Sin(a.ToRadians()));
                points.Add(new Vector3((float)x, (float)y, origin.Z));
            }
            return points.ToArray();
        }
        public static Vector3[] GetPossiableMovements(Vector3 origin, bool reverse = false, float step = 50, float minDistance = 200, float maxDistance = 500)
        {
            var points = new List<Vector3>();
            if (reverse)
                for (float d = maxDistance - 1; d >= minDistance; d = d - step)
                    //for each step within distance, starting from maxDistance
                {
                    points.AddRange(GetPointsOnCirlce(origin, d / 2, step));
                }
            else
                for (float d = minDistance; d < maxDistance; d = d + step)
                    //for each step within maxDistance, starting from origin
                {
                    points.AddRange(GetPointsOnCirlce(origin, d / 2, step));
                }
            return points.ToArray();
        }

        public static Vector3[] SortPointsByDistance(Vector3 origin, Vector3[] points)
        {
            var pointList = points.ToList();
            pointList.Sort((a, b) => a.Distance(origin).CompareTo(b.Distance(origin)));
            return pointList.ToArray();
        }

        public static bool WalkToTurrent(Lane lane, Buildings.TurrentTier tier, float distance, bool allyTurrent = true)
        {
            if (allyTurrent)
                return Player.IssueOrder(GameObjectOrder.MoveTo,
                    Player.Posistion.Extend(
                        Buildings.Ally.Turrents.First(x => x.Position.IsInLane(lane) && x.GetTurrentTier() == tier)
                            .Position, distance).To3DWorld());
            return Player.IssueOrder(GameObjectOrder.MoveTo,
                Buildings.Enemy.Turrents.First(x => x.Position.IsInLane(lane) && x.GetTurrentTier() == tier)
                    .Position.RandomPoint(10));
        }

    }
}