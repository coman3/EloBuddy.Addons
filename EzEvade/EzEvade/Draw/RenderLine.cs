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
    class RenderLine : RenderObject
    {
        public Vector2 Start = new Vector2(0, 0);
        public Vector2 End = new Vector2(0, 0);

        public int Width = 3;
        public Color color = Color.White;

        public RenderLine(Vector2 start, Vector2 end, float renderTime,
            int radius = 65, int width = 3)
        {
            this.StartTime = EvadeUtils.TickCount;
            this.EndTime = this.StartTime + renderTime;
            this.Start = start;
            this.End = end;

            this.Width = width;
        }

        public RenderLine(Vector2 start, Vector2 end, float renderTime,
            Color color, int radius = 65, int width = 3)
        {
            this.StartTime = EvadeUtils.TickCount;
            this.EndTime = this.StartTime + renderTime;
            this.Start = start;
            this.End = end;

            this.color = color;

            this.Width = width;
        }

        override public void Draw()
        {
            if (Start.IsOnScreen() || End.IsOnScreen())
            {
                var realStart = Drawing.WorldToScreen(Start.To3D());
                var realEnd = Drawing.WorldToScreen(End.To3D());

                Drawing.DrawLine(realStart, realEnd, Width, color);
            }
        }
    }
}
