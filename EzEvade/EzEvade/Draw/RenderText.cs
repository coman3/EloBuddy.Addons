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
    class RenderText : RenderObject
    {
        public Vector2 RenderPosition = new Vector2(0, 0);
        public string Text = "";

        public Color color = Color.White;

        public RenderText(string text, Vector2 renderPosition, float renderTime)
        {
            StartTime = EvadeUtils.TickCount;
            EndTime = StartTime + renderTime;
            RenderPosition = renderPosition;

            Text = text;
        }

        public RenderText(string text, Vector2 renderPosition, float renderTime,
            Color color)
        {
            StartTime = EvadeUtils.TickCount;
            EndTime = StartTime + renderTime;
            RenderPosition = renderPosition;

            this.color = color;

            Text = text;
        }

        override public void Draw()
        {
            if (RenderPosition.IsOnScreen())
            {
                var textDimension = Drawing.GetTextEntent(Text, 12);
                var wardScreenPos = Drawing.WorldToScreen(RenderPosition.To3D());

                Drawing.DrawText(wardScreenPos.X - textDimension.Width / 2, wardScreenPos.Y, color, Text);
            }
        }
    }
}
