using System.Collections.Generic;
using System.Linq;
using Coman3.API.Objects;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace Coman3.API.Data
{
    /// <summary>
    /// All Region Data is located from: 
    /// </summary>
    public static class Region
    {
        public enum Location
        {
            //TopTurrents Left Jungle
            TopLeftOuterJungle,
            ///TopLeftInnerJungle,
            //TopTurrents River
            TopOuterRiver,
            ///TopInnerRiver,
            //TopTurrents Right Jungle
            TopRightOuterJungle,
            ///TopRightInnerJungle,
            //Bottom Left Jungle
            BottomLeftOuterJungle,
            ///BottomLeftInnerJungle,
            //Bottom River
            BottomOuterRiver,
            ///BottomInnerRiver,
            //Bottom Right Jungle
            BottomRightOuterJungle,
            ///BottomRightInnerJungle,
            //MidTurrents Lane
            LeftMidLane,
            CenterMidLane,
            RightMidLane,
            //BotTurrents Lane
            LeftBotLane,
            CenterBotLane,
            RightBotLane,
            //TopTurrents Lane
            LeftTopLane,
            CenterTopLane,
            RightTopLane,

            //Base
            LeftBase,
            RightBase,
            //Misc

            None, Unknown

        }

        internal static readonly Dictionary<Location, Point[]> Regions = new Dictionary<Location, Point[]>();

        public static void Initialize()
        {
            Point[] pts = new[] { new Point(1770, 5001), new Point(2084, 11596), new Point(3421, 9782), new Point(3841, 9305), new Point(4703, 8844), new Point(6345, 7451), new Point(3518, 4587) };
            Regions[Location.TopLeftOuterJungle] = pts;

            //pts = new[] { new Point(3274, 5106), new Point(2071, 5398), new Point(2088, 10702), new Point(2878, 10382), new Point(3289, 9293), new Point(5589, 7887) };
            //Regions[Location.TopLeftInnerJungle] = pts;

            pts = new[] { new Point(6427, 7629), new Point(4693, 8805), new Point(3427, 9600), new Point(2410, 11629), new Point(3006, 12325), new Point(7340, 8331) };
            Regions[Location.TopOuterRiver] = pts;

            //pts = new[] { new Point(6217, 8077), new Point(5287, 8507), new Point(4440, 8988), new Point(3408, 9699), new Point(2667, 11359), new Point(3227, 11953), new Point(6886, 8668) };
            //Regions[Location.TopInnerRiver] = pts;

            pts = new[] { new Point(7417, 8209), new Point(5629, 9663), new Point(5425, 11054), new Point(4078, 11153), new Point(3111, 12709), new Point(6631, 12986), new Point(9777, 12970), new Point(10290, 11155) };
            Regions[Location.TopRightOuterJungle] = pts;

            //pts = new[] { new Point(7129, 9365), new Point(6319, 10046), new Point(5794, 10160), new Point(5435, 11144), new Point(4507, 11371), new Point(3916, 12150), new Point(7202, 12168), new Point(9002, 12524), new Point(9122, 10553), new Point(8205, 9990), new Point(8021, 9111) };
            //Regions[Location.TopRightInnerJungle] = pts;

            pts = new[] { new Point(4485, 3800), new Point(7368, 6600), new Point(9245, 5131), new Point(9247, 3949), new Point(10707, 3730), new Point(11388, 1980), new Point(10492, 1801), new Point(4938, 1780) };
            Regions[Location.BottomLeftOuterJungle] = pts;

            //pts = new[] { new Point(5132, 2358), new Point(4963, 3448), new Point(6850, 5663), new Point(7499, 5798), new Point(9151, 4810), new Point(9254, 4056), new Point(10663, 3012), new Point(10421, 2489) };
            //Regions[Location.BottomLeftInnerJungle] = pts;

            pts = new[] { new Point(11752, 2728), new Point(9485, 3968), new Point(9072, 5126), new Point(8449, 5828), new Point(7462, 6567), new Point(8327, 7223), new Point(9692, 6463), new Point(10907, 5673), new Point(12552, 3442) };
            Regions[Location.BottomOuterRiver] = pts;

            //pts = new[] { new Point(11236, 3200), new Point(10513, 4361), new Point(9961, 3480), new Point(9110, 4326), new Point(9455, 5250), new Point(7947, 6202), new Point(8742, 6731), new Point(10137, 6099), new Point(11429, 5293), new Point(12349, 3902) };
            //Regions[Location.BottomInnerRiver] = pts;

            pts = new[] { new Point(13014, 4103), new Point(12029, 4416), new Point(11447, 5317), new Point(8192, 7207), new Point(11118, 10396), new Point(13061, 9911) };
            Regions[Location.BottomRightOuterJungle] = pts;

            //pts = new[] { new Point(12491, 4049), new Point(11457, 5246), new Point(11553, 5671), new Point(10388, 6316), new Point(8881, 7164), new Point(11362, 9869), new Point(12550, 9567), new Point(12585, 6884), new Point(12956, 6405) };
            //Regions[Location.BottomRightInnerJungle] = pts;

            pts = new[] { new Point(3297, 4261), new Point(5930, 6897), new Point(6895, 6141), new Point(4112, 3575) };
            Regions[Location.LeftMidLane] = pts;

            pts = new[] { new Point(5930, 6897), new Point(7987, 8832), new Point(9112, 7958), new Point(6895, 6141) };
            Regions[Location.CenterMidLane] = pts;

            pts = new[] { new Point(9112, 7958), new Point(7987, 8832), new Point(10631, 11341), new Point(11361, 10869) };
            Regions[Location.RightMidLane] = pts;

            pts = new[] { new Point(4502, 492), new Point(4486, 1784), new Point(11218, 1953), new Point(12183, 485) };
            Regions[Location.LeftBotLane] = pts;

            pts = new[] { new Point(12183, 485), new Point(11218, 1953), new Point(12552, 3442), new Point(14283, 2620) };
            Regions[Location.CenterBotLane] = pts;

            pts = new[] { new Point(14283, 2620), new Point(12552, 3442), new Point(12997, 3971), new Point(13048, 10432), new Point(14580, 10329) };
            Regions[Location.RightBotLane] = pts;

            pts = new[] { new Point(23, 4744), new Point(104, 12521), new Point(1967, 11326), new Point(1719, 4564) };
            Regions[Location.LeftTopLane] = pts;

            pts = new[] { new Point(104, 12521), new Point(3332, 14683), new Point(3620, 12813), new Point(1967, 11326) };
            Regions[Location.CenterTopLane] = pts;

            pts = new[] { new Point(3620, 12813), new Point(3332, 14683), new Point(10295, 14390), new Point(10261, 13162), new Point(4284, 13087) };
            Regions[Location.RightTopLane] = pts;

            pts = new []{ new Point(0, 0), new Point(400, 5000),  new Point(2700, 4800), new Point(4100, 4100), new Point(4800, 2800), new Point(5000, 400) };
            Regions[Location.LeftBase] = pts;

            pts = new[] { new Point(14500, 14500), new Point(10000, 14400), new Point(10000, 12000), new Point(10900, 10900), new Point(12000, 10000), new Point(14400, 10000) };
            Regions[Location.RightBase] = pts;
        }

        #region Utilities
        public static void DrawRegion(Location region, Color color, int width = 2)
        {
            if (!Regions.ContainsKey(region)) {  Drawing.DrawText(0, 0, Color.Red, "Region Not In Database!"); return;}

            Point[] polygon = Regions[region];
            for (var i = 0; i <= polygon.Length - 1; i++)
            {
                var nextIndex = (polygon.Length - 1 == i) ? 0 : (i + 1);
                Vector2 start = Drawing.WorldToScreen(new Vector3(polygon[i].X, polygon[i].Y, 50));
                Vector2 end = Drawing.WorldToScreen(new Vector3(polygon[nextIndex].X, polygon[nextIndex].Y, 50));
                Drawing.DrawLine(start, end, width, color);
            }
            Drawing.DrawText(10, Drawing.Height - 20, Color.Red, "Region: " + region, 15);
        }

        public static Location[] SurroundingRegions(this Location location)
        {
            switch (location)
            {
                case Location.TopLeftOuterJungle:
                    return new[] {Location.TopOuterRiver, Location.LeftTopLane, Location.LeftMidLane, Location.LeftBase, Location.CenterMidLane, };
                //case Location.TopLeftInnerJungle:
                //    return new[] { Location.TopLeftOuterJungle};

                case Location.TopOuterRiver:
                    return new[] { Location.CenterTopLane,Location.TopLeftOuterJungle, Location.TopRightOuterJungle,  Location.CenterMidLane};
                //case Location.TopInnerRiver:
                //    return new[] { Location.TopOuterRiver};

                case Location.TopRightOuterJungle:
                    return new[] { Location.RightTopLane, Location.RightMidLane, Location.TopOuterRiver, Location.RightBase, Location.CenterMidLane, };
                //case Location.TopRightInnerJungle:
                //    return new[] { Location.TopRightOuterJungle, };

                case Location.BottomLeftOuterJungle:
                    return new[] { Location.LeftMidLane, Location.LeftBotLane, Location.BottomOuterRiver, Location.CenterMidLane, Location.LeftBase,  };
                //case Location.BottomLeftInnerJungle:
                //    return new[] {Location.BottomLeftOuterJungle, };

                case Location.BottomOuterRiver:
                    return new[] { Location.CenterMidLane, Location.CenterBotLane, Location.BottomLeftOuterJungle, Location.BottomRightOuterJungle,   };
                //case Location.BottomInnerRiver:
                //    return new[] { Location.BottomOuterRiver,  };

                case Location.BottomRightOuterJungle:
                    return new[] { Location.CenterMidLane, Location.RightBotLane,  Location.RightMidLane, Location.RightBase,  };
                //case Location.BottomRightInnerJungle:
                //    return new[] { Location.BottomRightOuterJungle,  };

                case Location.LeftMidLane:
                    return new[] { Location.LeftBase, Location.CenterMidLane, Location.TopLeftOuterJungle, Location.BottomLeftOuterJungle, };
                case Location.CenterMidLane:
                    return new[] { Location.LeftMidLane, Location.RightMidLane, Location.TopOuterRiver, Location.BottomOuterRiver, Location.TopLeftOuterJungle, Location.TopRightOuterJungle, Location.BottomRightOuterJungle, Location.BottomLeftOuterJungle, };
                case Location.RightMidLane:
                    return new[] { Location.RightBase, Location.CenterMidLane, Location.TopRightOuterJungle, Location.BottomRightOuterJungle, };

                case Location.LeftBotLane:
                    return new[] {Location.LeftBase, Location.CenterBotLane, Location.BottomLeftOuterJungle, };
                case Location.CenterBotLane:
                    return new[] {Location.BottomOuterRiver, Location.LeftBotLane, Location.RightBotLane, };
                case Location.RightBotLane:
                    return new[] { Location.RightBase, Location.CenterBotLane, Location.BottomRightOuterJungle,  };

                case Location.LeftTopLane:
                    return new[] { Location.LeftBase, Location.CenterTopLane, Location.TopLeftOuterJungle, };
                case Location.CenterTopLane:
                    return new[] { Location.RightTopLane, Location.LeftTopLane, Location.TopRightOuterJungle, Location.TopLeftOuterJungle, Location.TopOuterRiver, };
                case Location.RightTopLane:
                    return new[] { Location.RightBase, Location.CenterTopLane, Location.TopRightOuterJungle, };

                case Location.LeftBase:
                    return new[] { Location.LeftTopLane, Location.LeftMidLane, Location.LeftBotLane, Location.TopLeftOuterJungle, Location.BottomLeftOuterJungle, };
                case Location.RightBase:
                    return new[] { Location.RightTopLane, Location.RightBotLane, Location.RightMidLane, Location.TopRightOuterJungle, Location.BottomRightOuterJungle, };

                case Location.None:
                    return new[] { Location.Unknown,};
                case Location.Unknown:
                    return new[] { Location.Unknown, };
            }
            return new Location[0];
        }

        public static void DrawRegionOnMiniMap(Location region, Color color, int width = 2)
        {
            if (!Regions.ContainsKey(region))
            {
                Drawing.DrawText(0, 0, Color.Red, "Region Not In Database!");
                return;
            }

            Point[] polygon = Regions[region];
            var polygonVectors = polygon.ToVector3();
            var minimapPolygon = polygonVectors.Select(x => x.WorldToMinimap());
            Line.DrawLine(color, width, minimapPolygon.ToArray());
        }

        //credits to http://www.angusj.com/delphi/clipper.php
        //code taken from ClipperLib
        private static int PointInPolygon(Point[] path, Vector3 pt)
        {
            //returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            //See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            //http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.88.5498&rep=rep1&type=pdf
            int result = 0, cnt = path.Length;
            if (cnt < 3) return 0;
            Point ip = path[0];
            for (int i = 1; i <= cnt; ++i)
            {
                Point ipNext = (i == cnt ? path[0] : path[i]);
                if (ipNext.Y == pt.Y)
                {
                    if ((ipNext.X == pt.X) || (ip.Y == pt.Y && ((ipNext.X > pt.X) == (ip.X < pt.X))))
                        return -1;
                }
                if ((ip.Y < pt.Y) != (ipNext.Y < pt.Y))
                {
                    if (ip.X >= pt.X)
                    {
                        if (ipNext.X > pt.X) result = 1 - result;
                        else
                        {
                            double d = (double) (ip.X - pt.X)*(ipNext.Y - pt.Y) - (double) (ipNext.X - pt.X)*(ip.Y - pt.Y);
                            if (d == 0) return -1;
                            else if ((d > 0) == (ipNext.Y > ip.Y)) result = 1 - result;
                        }
                    }
                    else
                    {
                        if (ipNext.X > pt.X)
                        {
                            double d = (double) (ip.X - pt.X)*(ipNext.Y - pt.Y) - (double) (ipNext.X - pt.X)*(ip.Y - pt.Y);
                            if (d == 0) return -1;
                            else if ((d > 0) == (ipNext.Y > ip.Y)) result = 1 - result;
                        }
                    }
                }
                ip = ipNext;
            }
            return result;
        }

        private static bool IsPointInPolygon(Point[] path, Vector3 pt)
        {
            return PointInPolygon(path, pt) != 0;
        }

        #endregion

        public static bool IsInRegion(Vector3 point, params Location[] locations)
        {
            return locations.Any(location => IsPointInPolygon(Regions[location], point));
        }

        public static Location InWhatRegion(this Vector3 point)
        {
            return Regions.Any(location => IsPointInPolygon(location.Value, point)) ? Regions.First(location => IsPointInPolygon(location.Value, point)).Key : Location.None;
        }

        public static List<AIHeroClient> PlayersInRegion(Location location)
        {
            return Heroes.AllHeros.Where(hero => hero.Position.InWhatRegion() == location).ToList();
        }

        #region River

        public static bool InRiver(Obj_AI_Base unit)
        {
            return (InTopRiver(unit) || InBottomRiver(unit));
        }

        public static bool InTopRiver(Obj_AI_Base unit)
        {
            return IsPointInPolygon(Regions[Location.TopOuterRiver], unit.Position);
        }

        //public static bool InTopInnerRiver(Obj_AI_Base unit)
        //{
        //    return IsPointInPolygon(Regions[Location.TopInnerRiver], unit.Position);
        //}

        public static bool InTopOuterRiver(Obj_AI_Base unit)
        {
            return InTopRiver(unit) ;//&& !InTopInnerRiver(unit);
            ;
        }

        public static bool InBottomRiver(Obj_AI_Base unit)
        {
            return IsPointInPolygon(Regions[Location.BottomOuterRiver], unit.Position);
        }

        //public static bool InBottomInnerRiver(Obj_AI_Base unit)
        //{
        //    return IsPointInPolygon(Regions[Location.BottomInnerRiver], unit.Position);
        //}

        public static bool InBottomOuterRiver(Obj_AI_Base unit)
        {
            return InBottomRiver(unit) ;//&& !InBottomInnerRiver(unit);
        }

        public static bool InOuterRiver(Obj_AI_Base unit)
        {
            return (InTopOuterRiver(unit) || InBottomOuterRiver(unit));
        }

        //public static bool InInnerRiver(Obj_AI_Base unit)
        //{
        //    return (InTopInnerRiver(unit) || InBottomInnerRiver(unit));
        //}

        #endregion

        #region Base

        public static bool InBase(Obj_AI_Base unit)
        {
            return (!OnLane(unit) && !InJungle(unit) && !InRiver(unit));
        }

        public static bool InLeftBase(Obj_AI_Base unit)
        {
            return (InBase(unit) && unit.Distance(new Vector3(50, 0, 285)) < 6000);
        }

        public static bool InRightBase(Obj_AI_Base unit)
        {
            return (InBase(unit) && unit.Distance(new Vector3(50, 0, 285)) > 6000);
        }

        #endregion

        #region Lane

        public static bool OnLane(Obj_AI_Base unit)
        {
            return (OnTopLane(unit) || OnMidLane(unit) || OnBotLane(unit));
        }

        public static bool OnTopLane(Obj_AI_Base unit)
        {
            return (IsPointInPolygon(Regions[Location.LeftTopLane], unit.Position) || IsPointInPolygon(Regions[Location.CenterTopLane], unit.Position) || IsPointInPolygon(Regions[Location.RightTopLane], unit.Position));
        }

        public static bool OnMidLane(Obj_AI_Base unit)
        {
            return (IsPointInPolygon(Regions[Location.LeftMidLane], unit.Position) || IsPointInPolygon(Regions[Location.CenterMidLane], unit.Position) || IsPointInPolygon(Regions[Location.RightMidLane], unit.Position));
        }

        public static bool OnBotLane(Obj_AI_Base unit)
        {
            return (IsPointInPolygon(Regions[Location.LeftBotLane], unit.Position) || IsPointInPolygon(Regions[Location.CenterBotLane], unit.Position) || IsPointInPolygon(Regions[Location.RightBotLane], unit.Position));
        }

        #endregion

        #region Jungle

        public static bool InJungle(Obj_AI_Base unit)
        {
            return (InLeftJungle(unit) || InRightJungle(unit));
        }

        public static bool InOuterJungle(Obj_AI_Base unit)
        {
            return (InLeftOuterJungle(unit) || InRightOuterJungle(unit));
        }

        //public static bool InInnerJungle(Obj_AI_Base unit)
        //{
        //    return (InLeftInnerJungle(unit) || InRightInnerJungle(unit));
        //}

        public static bool InLeftJungle(Obj_AI_Base unit)
        {
            return (InTopLeftJungle(unit) || InBottomLeftJungle(unit));
        }

        public static bool InLeftOuterJungle(Obj_AI_Base unit)
        {
            return (InTopLeftOuterJungle(unit) || InBottomLeftOuterJungle(unit));
        }

        //public static bool InLeftInnerJungle(Obj_AI_Base unit)
        //{
        //    return (InTopLeftInnerJungle(unit) || InBottomLeftInnerJungle(unit));
        //}

        public static bool InTopLeftJungle(Obj_AI_Base unit)
        {
            return IsPointInPolygon(Regions[Location.TopLeftOuterJungle], unit.Position);
        }

        public static bool InTopLeftOuterJungle(Obj_AI_Base unit)
        {
            return InTopLeftJungle(unit) ;//&& !InTopLeftInnerJungle(unit));
        }

        //public static bool InTopLeftInnerJungle(Obj_AI_Base unit)
        //{
        //    return IsPointInPolygon(Regions[Location.TopLeftInnerJungle], unit.Position);
        //}

        public static bool InBottomLeftJungle(Obj_AI_Base unit)
        {
            return IsPointInPolygon(Regions[Location.BottomLeftOuterJungle], unit.Position);
        }

        public static bool InBottomLeftOuterJungle(Obj_AI_Base unit)
        {
            return InBottomLeftJungle(unit) ;//&& !InBottomLeftInnerJungle(unit));
        }

        //public static bool InBottomLeftInnerJungle(Obj_AI_Base unit)
        //{
        //    return IsPointInPolygon(Regions[Location.BottomLeftInnerJungle], unit.Position);
        //}

        public static bool InRightJungle(Obj_AI_Base unit)
        {
            return (InTopRightJungle(unit) || InBottomRightJungle(unit));
        }

        public static bool InRightOuterJungle(Obj_AI_Base unit)
        {
            return (InTopRightOuterJungle(unit) || InBottomRightOuterJungle(unit));
        }

        //public static bool InRightInnerJungle(Obj_AI_Base unit)
        //{
        //    return (InTopRightInnerJungle(unit) || InBottomRightInnerJungle(unit));
        //}

        public static bool InTopRightJungle(Obj_AI_Base unit)
        {
            return IsPointInPolygon(Regions[Location.TopRightOuterJungle], unit.Position);
        }

        public static bool InTopRightOuterJungle(Obj_AI_Base unit)
        {
            return InTopRightJungle(unit) ;//&& !InTopRightInnerJungle(unit));
        }

        //public static bool InTopRightInnerJungle(Obj_AI_Base unit)
        //{
        //    return IsPointInPolygon(Regions[Location.TopRightInnerJungle], unit.Position);
        //}

        public static bool InBottomRightJungle(Obj_AI_Base unit)
        {
            return IsPointInPolygon(Regions[Location.BottomRightOuterJungle], unit.Position);
        }

        public static bool InBottomRightOuterJungle(Obj_AI_Base unit)
        {
            return InBottomRightJungle(unit);// && !InBottomRightInnerJungle(unit));
        }

        //public static bool InBottomRightInnerJungle(Obj_AI_Base unit)
        //{
        //    return IsPointInPolygon(Regions[Location.BottomRightInnerJungle], unit.Position);
        //}

        public static bool InTopJungle(Obj_AI_Base unit)
        {
            return (InTopLeftJungle(unit) || InTopRightJungle(unit));
        }

        public static bool InTopOuterJungle(Obj_AI_Base unit)
        {
            return (InTopLeftOuterJungle(unit) || InTopRightOuterJungle(unit));
        }

        //public static bool InTopInnerJungle(Obj_AI_Base unit)
        //{
        //    return (InTopLeftInnerJungle(unit) || InTopRightInnerJungle(unit));
        //}

        public static bool InBottomJungle(Obj_AI_Base unit)
        {
            return (InBottomLeftJungle(unit) || InBottomRightJungle(unit));
        }

        public static bool InBottomOuterJungle(Obj_AI_Base unit)
        {
            return (InBottomLeftOuterJungle(unit) || InBottomRightOuterJungle(unit));
        }

        //public static bool InBottomInnerJungle(Obj_AI_Base unit)
        //{
        //    return (InBottomLeftInnerJungle(unit) || InBottomRightInnerJungle(unit));
        //}

        #endregion
    }
}