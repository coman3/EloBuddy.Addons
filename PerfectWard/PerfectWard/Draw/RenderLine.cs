using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
namespace PerfectWard.Draw
{
    public class RenderLine : RenderObject
    {
        public Vector2 Start;
        public Vector2 End;

        public float Width;

        public Color Color;

        public RenderLine(Vector2 start, Vector2 end, Color color, float width = 3)
        {
            Start = start;
            End = end;

            Color = color;

            Width = width;
        }

        public override void Draw()
        {
            //if (Start.IsOnScreen() || End.IsOnScreen())
            {
                Line.DrawLine(Color, Width, Start, End);
            }
        }

        public override Vector3 GetScreenPos()
        {
            return Start.ScreenToWorld();
        }
    }
}