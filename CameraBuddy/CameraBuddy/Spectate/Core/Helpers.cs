using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CameraBuddy.Spectate.Extensions;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Point = SharpDX.Point;
using RectangleF = System.Drawing.RectangleF;

namespace CameraBuddy.Spectate.Core
{
    public static class Helpers
    {
        #region Utility's
        public static TItem RandomItem<TItem>(this IEnumerable<TItem> enumerable)
        {
            var list = enumerable.ToList();
            if (enumerable == null || !list.Any())
                return default(TItem);
            if (list.Count == 1)
                return list[0];
            return list[Random.Next(0, list.Count)];
        }
        public static PointF ToPointF(this Vector3 value)
        {
            return new PointF(value.To2D().X, value.To2D().Y);
        }
        public static PointF[] ToPointF(this Vector3[] values)
        {
            return values.Select(vector3 => vector3.ToPointF()).ToArray();
        }
        public static Vector3 ToVector3(this PointF value)
        {
            return new Vector2(value.X, value.Y).To3D();
        }
        public static Vector3[] ToVector3(this PointF[] values)
        {
            return values.Select(point => point.ToVector3()).ToArray();
        }
        public static Vector3 ToVector3(this Point value)
        {
            return new Vector2(value.X, value.Y).To3DWorld();
        }
        public static Vector3[] ToVector3(this Point[] values)
        {
            return values.Select(point => point.ToVector3()).ToArray();
        }

        public static Vector3 AverageLocation(this IEnumerable<Vector3> points)
        {
            var list = points.ToList();
            if (points == null) return Vector3.Zero;

            var xTotal = list.Average(x => x.X);
            var yTotal = list.Average(x => x.Y);
            return  new Vector2(xTotal, yTotal).To3D();
        }
        public static Vector3 AverageLocation(this IEnumerable<Obj_AI_Base> objects)
        {
            return AverageLocation(objects.Select(x => x.Position));
        }

        public static System.Drawing.Point[] ToSystemPoint(this Point[] points)
        {
            return points.Select(x => new System.Drawing.Point(x.X, x.Y)).ToArray();
        }

        public static bool IsVisible(this AIHeroClient hero)
        {
            return hero.IsHPBarRendered;
        }

        public static bool IsDeadOrVisible(this AIHeroClient hero)
        {
            return hero.IsDead || hero.IsHPBarRendered;
        }

        public static SmartList<TItem> ToSmartList<TItem>(this List<TItem> list)
        {
            return (SmartList<TItem>) list;
        }
        #endregion

        #region Math
        public static double ToRadians(this float angle)
        {
            return (Math.PI / 180) * angle;
        }
        public static PointF Center(this RectangleF rect)
        {
            return new PointF(rect.Left + rect.Width / 2,
                             rect.Top + rect.Height / 2);
        }
        #endregion
    }
}