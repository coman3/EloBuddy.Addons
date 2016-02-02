using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using EloBuddy;

namespace Coman3.API.Champion
{
    public class IconGenerator
    {
        public IconType Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Color BorderColor { get; set; }
        public float BorderWidth { get; set; }
        public float Padding  { get; set; }
        public static readonly Color DefaultGoldColor = Color.FromArgb(143, 122, 72);
        public static readonly Color DefaultGreenColor = Color.FromArgb(44, 99, 94);
        public IconGenerator(IconType type, int width, int height, Color borderColor, float borderWidth)
        {
            Type = type;
            Padding = 5;
            Width = Math.Max(width, 1);
            Height = Math.Max(height, 1);
            BorderColor = borderColor;
            BorderWidth = borderWidth;
        }

        public Tuple<string, Bitmap>[] GenerateAllIcons()
        {
            var items = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true).GetEnumerator();
            List<string> allIcons = new List<string>();
            while (items.MoveNext())
            {
                allIcons.Add((string) items.Key);
            }
            return allIcons.Select(x=> new Tuple<string, Bitmap>(x, GetIcon(x))).ToArray();
        }
        public Bitmap GetIcon(string itemName)
        {
            var container = new IconContatiner(Width, Height);
            var resource =
                Properties.Resources.ResourceManager.GetObject(itemName) as Bitmap;
            if (resource == null) return CreateErrorIcon(container);

            container.DrawImage(Type, resource, new IconContatiner.Padding(Padding, Padding * 2));

            if (Type == IconType.Circle)
                container.Graphics.DrawEllipse(new Pen(BorderColor, BorderWidth), Padding, Padding, Width - Padding * 2, Height - Padding * 2);
            else
                container.Graphics.DrawRectangle(new Pen(BorderColor, BorderWidth), Padding, Padding, Width - Padding * 2, Height - Padding * 2);
            return container.Icon;
        }
        public Bitmap GetSpellIcon(string name)
        {
            return GetIcon(name);
        }
        public Bitmap GetSpellIcon(SpellData data)
        {
            return GetIcon(data.Name);
        }
        public Bitmap GetChampionIcon(AIHeroClient champion)
        {
            return GetIcon(string.Format("{0}_Square_0", champion.ChampionName));
        }
        public Bitmap GetChampionIcon(string championName)
        {
            return GetIcon(string.Format("{0}_Square_0", championName));
        }

        private Bitmap CreateErrorIcon(IconContatiner container)
        {
            if (Type == IconType.Circle)
            {
                var bitmap = new Bitmap(128, 128);
                var g = Graphics.FromImage(bitmap);
                g.DrawLine(Pens.Red, Padding, Padding, 128 - Padding, 128 - Padding);
                g.DrawLine(Pens.Red, Padding, 128, 128 - Padding, Padding);
                g.Save();
                container.DrawImage(IconType.Circle, bitmap, new IconContatiner.Padding(Padding, Padding * 2), false);
                container.Graphics.DrawEllipse(new Pen(BorderColor, BorderWidth), Padding, Padding, Width - Padding * 2,
                    Height - Padding * 2);
            }
            else
            {
                container.Graphics.DrawLine(Pens.Red, new Point(0, 0), new Point(Width, Height));
                container.Graphics.DrawLine(Pens.Red, new Point(0, Height), new Point(Width, 0));
                container.Graphics.DrawRectangle(new Pen(BorderColor, BorderWidth), Padding, Padding, Width - Padding * 2,
                    Height - Padding * 2);
            }
            container.Save();
            return container.Icon;
        }

        public enum IconType
        {
            Circle,
            Square
        }

        private class IconContatiner
        {
            public readonly Bitmap Icon;
            public readonly Graphics Graphics;

            public IconContatiner(int width, int height)
            {
                Icon = new Bitmap(width, height);
                Graphics = Graphics.FromImage(Icon);
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }

            public void Save()
            {
                if (Graphics == null) return;
                Graphics.Save();
            }

            public void DrawImage(IconType type, Image image, Padding padding, bool stripBorderFromSource = true)
            {
                if (type == IconType.Circle)
                {
                    var path = new GraphicsPath();
                    path.AddEllipse(padding.Left, padding.Top, Icon.Width - padding.Right, Icon.Width - padding.Bottom);
                    Graphics.SetClip(path);
                }
                Graphics.DrawImage(image,
                    new RectangleF(padding.Left, padding.Top, Icon.Width - padding.Right, Icon.Height - padding.Bottom),
                    stripBorderFromSource
                        ? new RectangleF(5, 5, image.Width - 10, image.Height - 10)
                        : new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                Graphics.SetClip(new Rectangle(0, 0, Icon.Width, Icon.Height)); // Resets clipping region
                Graphics.Save();
            } 

            public class Padding
            {
                public float Top { get; set; }
                public float Left { get; set; }
                public float Right { get; set; }
                public float Bottom { get; set; }

                public Padding(float all)
                {
                    Top = all;
                    Left = all;
                    Right = all;
                    Bottom = all;
                }

                public Padding(float topLeft, float bottomRight)
                {
                    Top = topLeft;
                    Left = topLeft;
                    Right = bottomRight;
                    Bottom = bottomRight;
                }

                public Padding(float top, float left, float right, float bottom)
                {
                    Top = top;
                    Left = left;
                    Right = right;
                    Bottom = bottom;
                }
            }
        }
    }
}