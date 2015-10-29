using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
namespace PerfectWard.Draw
{
    public class RenderCircle : RenderObject
    {
        public Vector2 RenderPosition;

        public int Radius;
        public int Width;
        public Color Color;
        public RenderCircle(Vector2 renderPosition, Color color, int radius = 65, int width = 5)
        {
            RenderPosition = renderPosition;

            Color = color;

            Radius = radius;
            Width = width;
        }

        override public void Draw()
        {
           // if (RenderPosition.IsOnScreen())
            {
                DrawCircle(RenderPosition.To3D(), Radius, Color, Width);
            }
        }

        public override Vector3 GetScreenPos()
        {
            return RenderPosition.To3D();
        }

        public static void DrawCircle(Vector3 pos, float radius, Color color, float width)
        {
            Circle.Draw(new ColorBGRA(color.R, color.G, color.B, 255), radius, width, pos);
        }
    }
}