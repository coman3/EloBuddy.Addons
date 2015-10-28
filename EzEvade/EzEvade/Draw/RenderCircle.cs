using EloBuddy.SDK;
using EzEvade.Utils;
using SharpDX;
using Color = System.Drawing.Color;

namespace EzEvade.Draw
{
    class RenderCircle : RenderObject
    {
        public Vector2 RenderPosition = new Vector2(0, 0);

        public int Radius = 65;
        public int Width = 5;
        public Color color = Color.White;

        public RenderCircle(Vector2 renderPosition, float renderTime,
            int radius = 65, int width = 5)
        {
            StartTime = EvadeUtils.TickCount;
            EndTime = StartTime + renderTime;
            RenderPosition = renderPosition;

            Radius = radius;
            Width = width;
        }

        public RenderCircle(Vector2 renderPosition, float renderTime,
            Color color, int radius = 65, int width = 5)
        {
            StartTime = EvadeUtils.TickCount;
            EndTime = StartTime + renderTime;
            RenderPosition = renderPosition;

            this.color = color;

            Radius = radius;
            Width = width;
        }

        override public void Draw()
        {
            if (RenderPosition.IsOnScreen())
            {
                Render.Circle.DrawCircle(RenderPosition.To3D(), Radius, color, Width);
            }
        }
    }
}
