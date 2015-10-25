using System;
using System.Collections.Generic;
using System.Drawing;
using DynamicConfig.Example.Properties;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;
using Line = EloBuddy.SDK.Rendering.Line;
using Rectangle = SharpDX.Rectangle;
using Sprite = EloBuddy.SDK.Rendering.Sprite;

namespace DynamicConfig.Example.Menu.Controls
{
    public static class ColorPickerHelpers
    {
        public static ColorPicker AddColorPicker(this EloBuddy.SDK.Menu.Menu menu, ColorPicker control)
        {
            menu.Add(control.SerializationId, control);
            menu.AddSeparator(250);
            return control;
        }
    }

    public class ColorPicker : ValueBase<Color>
    {
        private readonly string _name;
        private Vector2 _offset;
        
        private bool _isMouseDown;
        private static Vector2 MousePosition { get; set; }
        private static readonly int SelectorWidth = Resources.ColorSelectorSprite.Width;
        private static readonly int SelectorHeight = Resources.ColorSelectorSprite.Height;
        private Rectangle ColorSelectorRect
        {
            get
            {
                return new Rectangle(Convert.ToInt32(Position.X) + SelectorBorderWidth,
                    Convert.ToInt32(Position.Y) + SelectorBorderWidth,
                    SelectorWidth - SelectorBorderWidth*2,
                    SelectorHeight - SelectorBorderWidth*2);
            }
        }
        private Vector2 RelativeCursorPosistion { get; set; }
        private bool _isSetManualy = true;
        private static Color SelectedColor { get; set; }

        public Sprite ColorCursorSprite;
        public Sprite ColorSelectorSprite;
        public Sprite ColorPickerSprite;
        public Sprite ColorPickerOverlaySprite;
        public static int SelectorBorderWidth = 3;
        public static int CursorBorderWidth = 3;       

        public override string VisibleName { get { return _name; } }
        public override Vector2 Offset { get { return _offset; } }

        public ColorPicker(string uId,  Color defaultValue) : base(uId, "", 265)
        {
            Init(defaultValue);
        }

        private void Init(Color color)
        {
            InitSprites();
            _offset = new Vector2(0, 10);
            SelectedColor = color;
            Messages.OnMessage += Messages_OnMessage;
        }

        private void Messages_OnMessage(Messages.WindowMessage args)
        {
            if (MainMenu.IsVisible && IsVisible)
            {
                var move = args as Messages.MouseMove;
                if (move != null)
                {
                    MousePosition = move.MousePosition;
                }
                if (IsMouseInPickerRegion())
                {
                    var leftButtonDown = args as Messages.LeftButtonDown;
                    if (leftButtonDown != null)
                    {
                        _isMouseDown = true;
                        _isSetManualy = false;
                    }
                }
            }
            if (_isMouseDown)
            {
                CheckAndSetCursorPos();
                var leftButtonUp = args as Messages.LeftButtonUp;
                if (leftButtonUp != null)
                {
                    _isMouseDown = false;
                    _isSetManualy = false;
                    GetColorAtCursorPosition();
                }
            }
        }
        public bool SetColor(Color color)
        {
            SelectedColor = color;
            _isSetManualy = true;
            return true;
        }

        private void InitSprites()
        {
            ColorPickerSprite = new Sprite(Texture.FromMemory(
                Drawing.Direct3DDevice,
                (byte[])new ImageConverter().ConvertTo(Resources.ColorPickerSprite, typeof(byte[])),
                Resources.ColorPickerSprite.Width, Resources.ColorPickerSprite.Height, 0,
                Usage.None, Format.A1, Pool.Managed, Filter.Default, Filter.Default, 0));

            ColorSelectorSprite = new Sprite(Texture.FromMemory(
                Drawing.Direct3DDevice,
                (byte[])new ImageConverter().ConvertTo(Resources.ColorSelectorSprite, typeof(byte[])),
                Resources.ColorSelectorSprite.Width, Resources.ColorSelectorSprite.Height, 0,
                Usage.None, Format.A1, Pool.Managed, Filter.Default, Filter.Default, 0));

            ColorCursorSprite = new Sprite(Texture.FromMemory(
                Drawing.Direct3DDevice,
                (byte[])new ImageConverter().ConvertTo(Resources.ColorCusorSpite, typeof(byte[])),
                Resources.ColorCusorSpite.Width, Resources.ColorCusorSpite.Height, 0,
                Usage.None, Format.A1, Pool.Managed, Filter.Default, Filter.Default, 0));

            ColorPickerOverlaySprite = new Sprite(Texture.FromMemory(
                Drawing.Direct3DDevice,
                (byte[])new ImageConverter().ConvertTo(ContructColorOverlaySprite(), typeof(byte[])),
                58, 58, 0,
                Usage.None, Format.A1, Pool.Managed, Filter.Default, Filter.Default, 0));
        }

        private static Bitmap ContructColorOverlaySprite()
        {
            var bitmap = new Bitmap(58, 58);
            for (int x = 0; x < 58; x++)
            {
                for (int y = 0; y < 58; y++)
                {
                    bitmap.SetPixel(x, y, Color.White);
                }
            }
            return bitmap;
        }

        private bool IsMouseInPickerRegion()
        {
            return ColorSelectorRect.IsInside(MousePosition);
        }

        private void CheckAndSetCursorPos()
        {
            var x = MousePosition.X;
            var y = MousePosition.Y;
            x = Math.Max(ColorSelectorRect.Left, x);
            x = Math.Min(ColorSelectorRect.Right - CursorBorderWidth*2 - 1, x);

            y = Math.Max(ColorSelectorRect.Top, y);
            y = Math.Min(ColorSelectorRect.Bottom - CursorBorderWidth*2 - 1, y);
            RelativeCursorPosistion = new Vector2(x - ColorSelectorRect.Left, y - ColorSelectorRect.Top);
        }

        public Color GetColorAtCursorPosition()
        {
            if (_isSetManualy) return SelectedColor;
            try
            {
                SelectedColor = Resources.ColorSelectorSprite.GetPixel((int)(RelativeCursorPosistion.X + CursorBorderWidth),
                    (int)(RelativeCursorPosistion.Y + CursorBorderWidth));
                return SelectedColor;
            }
            catch (Exception)
            {
                return Color.White;
            }
        }

        public override Color CurrentValue { get { return SelectedColor; } }

        public override bool Draw()
        {
            if (MainMenu.IsVisible && IsVisible)
            {
                ColorSelectorSprite.Draw(Position);
                ColorPickerSprite.Draw(new Vector2(Position.X + 10 + SelectorWidth, Position.Y));
                if(_isMouseDown)
                    GetColorAtCursorPosition();
                ColorPickerOverlaySprite.Color = SelectedColor;
                ColorPickerOverlaySprite.Draw(new Vector2(Position.X + 10 + SelectorWidth + 3, Position.Y + 3));
                if(!_isSetManualy)
                    ColorCursorSprite.Draw(new Vector2(RelativeCursorPosistion.X + ColorSelectorRect.Left, RelativeCursorPosistion.Y + ColorSelectorRect.Top));

                return true;
            }
            return false;
        }

    }
}
