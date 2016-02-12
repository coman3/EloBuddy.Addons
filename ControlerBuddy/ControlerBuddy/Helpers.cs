using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace ControlerBuddy
{
    public static class Helpers
    {
        public static Vector2 Offset(this Vector2 vector, float x, float y)
        {
            return new Vector2(vector.X + x, vector.Y + y);
        }
        public static Vector2 Offset(this Vector2 vector, Vector2 offset)
        {
            return new Vector2(vector.X + offset.X, vector.Y + offset.Y);
        }
        public static Vector3 Offset(this Vector3 vector, Vector2 offset)
        {
            return vector.Offset(offset.To3D());
        }
        public static Vector3 Offset(this Vector3 vector, Vector3 offset)
        {
            return new Vector3(vector.X + offset.X, vector.Y + offset.Y, vector.Z + offset.Z);
        }

        public static Vector2 TopCenter(this RectangleF rect)
        {
            return new Vector2(rect.Center.X, rect.Top);
        }
        public static Vector2 BottomCenter(this RectangleF rect)
        {
            return new Vector2(rect.Center.X, rect.Bottom);
        }
        public static Vector2 RightCenter(this RectangleF rect)
        {
            return new Vector2(rect.Right, rect.Center.Y);
        }
        public static Vector2 LeftCenter(this RectangleF rect)
        {
            return new Vector2(rect.Left, rect.Center.Y);
        }

        public static void DrawFillRectangle(this RectangleF rectangle, System.Drawing.Color color)
        {
            Line.DrawLine(color, rectangle.Width, rectangle.RightCenter(), rectangle.LeftCenter());
        }
        public static RectangleF RectangleFromCenter(this Vector2 center, float size)
        {
            return new RectangleF(center.X - size / 2f, center.Y - size / 2f, size, size);
        }
    }
}