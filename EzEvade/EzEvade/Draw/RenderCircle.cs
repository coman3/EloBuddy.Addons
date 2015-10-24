using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using EloBuddy;
using EloBuddy.SDK;
using EzEvade;
using EzEvade.Utils;
using SharpDX;

namespace ezEvade.Draw
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
