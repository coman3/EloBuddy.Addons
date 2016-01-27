using System.Drawing.Drawing2D;
using CameraBuddy.Spectate.Core;
using SharpDX;

namespace CameraBuddy.Spectate.Data
{
    public static class Lanes
    {
        private static readonly Vector3[] _botLane =
        {
            new Vector3(3068.528f, 2183.064f, 95.74805f),
            new Vector3(12778.01f, 11630.71f, 91.42981f),
            new Vector3(14608.59f, 11079.43f, 93.31592f),
            new Vector3(14642.59f, 1107.029f, 51.36719f),
            new Vector3(3035.383f, 121.2808f, 93.37561f),
            new Vector3(3068.528f, 2183.064f, 95.74805f),
        };

        private static readonly Vector3[] _midLane =
        {
            new Vector3(3065.168f, 2183.984f, 95.74805f),
            new Vector3(12787.34f, 11621.73f, 91.42993f),
            new Vector3(11491.68f, 12895.69f, 91.42969f),
            new Vector3(2013.595f, 3189.201f, 95.7478f),
            new Vector3(3065.168f, 2183.984f, 95.74805f),
        };

        private static readonly Vector3[] _topLane =
        {
            new Vector3(2147.596f, 3132.478f, 95.7478f),
            new Vector3(11501.03f, 12887.89f, 91.42993f),
            new Vector3(11378.33f, 14680.23f, 93.33679f),
            new Vector3(76.02869f, 14536.01f, 52.83813f),
            new Vector3(68.64069f, 2931.173f, 93.37573f),
            new Vector3(2147.596f, 3132.478f, 95.7478f),
        };

        public static readonly GraphicsPath TopLane;
        public static readonly GraphicsPath MidLane;
        public static readonly GraphicsPath BotLane;

        public static bool IsInBotLane(this Vector3 pos)
        {
            return BotLane.IsVisible(pos.ToPointF());
        }
        public static bool IsInMidLane(this Vector3 pos)
        {
            return MidLane.IsVisible(pos.ToPointF());
        }
        public static bool IsInTopLane(this Vector3 pos)
        {
            return TopLane.IsVisible(pos.ToPointF());
        }

        public static Lane InWhatLane(this Vector3 pos)
        {
            if (pos.IsInLane(Lane.Top))
                return Lane.Top;
            if (pos.IsInLane(Lane.Bottom))
                return Lane.Bottom;
            if (pos.IsInLane(Lane.Middle))
                return Lane.Middle;
            return Lane.Jungle;
        }
        public static bool IsInLane(this Vector3 pos,  Lane lane)
        {
            return pos.InWhatLane() == lane;
        }

        static Lanes()
        {
            TopLane = new GraphicsPath();
            TopLane.AddPolygon(_topLane.ToPointF());

            MidLane = new GraphicsPath();
            MidLane.AddPolygon(_midLane.ToPointF());

            BotLane = new GraphicsPath();
            BotLane.AddPolygon(_botLane.ToPointF());
        }
    }

    public enum Lane
    {
        Auto,
        Top,
        Middle,
        Jungle,
        Bottom
    }
}