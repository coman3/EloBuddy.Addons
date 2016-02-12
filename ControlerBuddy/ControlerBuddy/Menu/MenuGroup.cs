using System.Collections.Generic;
using System.Linq;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;

namespace ControlerBuddy.Menu
{
    /// <summary>
    /// A Collection Of Menu Items, into a pad style menu with 4 options: A, B, X, Y
    /// </summary>
    public abstract class MenuGroup : Dictionary<ControllerButton, MenuItem>
    {
        public Text GroupNameText { get; set; }
        public RectangleF Rectangle { get; set; }
        public RectangleF CenterRectangle { get; set; }
        public bool Active { get; set; }
        public RectangleF[] Rectangles { get; set; }

        protected MenuGroup(string name, RectangleF rectangle)
        {
            var itemWidth = rectangle.Width / 3f;
            var itemHeight = rectangle.Height / 3f;
            Rectangle = rectangle;
            Rectangles = new[]
            {
                // A
                new RectangleF(rectangle.Left + itemWidth, rectangle.Bottom - itemHeight, itemWidth, itemHeight),
                // B
                new RectangleF(rectangle.Right - itemWidth, rectangle.Top + itemWidth, itemWidth, itemHeight),
                // X
                new RectangleF(rectangle.Left, rectangle.Bottom - itemHeight * 2, itemWidth, itemHeight),
                // Y
                new RectangleF(rectangle.Left + itemWidth, rectangle.Top, itemWidth, itemHeight),
            };
            CenterRectangle = new RectangleF(rectangle.X + itemWidth, rectangle.Y + itemHeight, itemWidth, itemHeight);
            CreateModeText(name);
        }
        private void CreateModeText(string text)
        {
            GroupNameText = new Text(text, new FontDescription
            {
                FaceName = "Century Gothic",
                Weight = FontWeight.Normal,
                OutputPrecision = FontPrecision.Default,
                Quality = FontQuality.Antialiased
            })
            {
                Position = Rectangle.Center,
                Color = Color.White,
                TextValue = text
            };
            GroupNameText.Position = Rectangle.Center.Offset(-new Vector2(10 * GroupNameText.TextValue.Length / 2f, 10));
        }

        public bool SelectItem(ControllerButton button)
        {
            if (Active)
                return ContainsKey(button) && this[button].Select();
            foreach (var child in this.Where(child => child.Value != null).Where(child => child.Value.Child != null && child.Value.Child.Active))
            {
                return child.Value.Child.SelectItem(button);
            }
            return true;
        }

        public void ResetItems()
        {
            foreach (var item in Values)
            {
                item.Reset();
            }
        }
        public virtual void Draw()
        {
            if (Active)
            {
                foreach (var child in this.Where(child => child.Value != null))
                {
                    child.Value.Draw(Rectangles[(int) child.Key]);
                }
                CenterRectangle.DrawFillRectangle(Color.Black);
                GroupNameText.Draw();
            }
            else
            {
                foreach (var child in this.Where(child => child.Value != null).Where(child => child.Value.Child != null && child.Value.Child.Active))
                {
                    child.Value.Draw(Rectangle);
                    return;
                }

            }
        }
    }
}