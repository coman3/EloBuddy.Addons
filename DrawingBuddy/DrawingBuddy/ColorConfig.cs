using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Color = System.Drawing.Color;
namespace DrawingBuddy
{
    public class ColorConfig
    {
        public Slider RedSlider { get; set; }
        public Slider BlueSlider { get; set; }
        public Slider GreenSlider { get; set; }

        private static int[] _savedValues = new int[3];
        private static bool _hasSavedValues = false;
        public string Id { get; private set; }
        private static Menu Menu;
        public ColorConfig(Menu menu, string id)
        {
            Id = id;
            Menu = menu;
            Init();
        }

        public void Init()
        {

            RedSlider = new Slider("Red", _hasSavedValues ? _savedValues[0] : 255, 0, 255);
            GreenSlider = new Slider("Green", _hasSavedValues ? _savedValues[1] : 255, 0, 255);
            BlueSlider = new Slider("Blue", _hasSavedValues ? _savedValues[2] : 255, 0, 255);

            Menu.Add(Id + "Red", RedSlider);
            Menu.Add(Id + "Green", GreenSlider);
            Menu.Add(Id + "Blue", BlueSlider);
        }

        public byte GetValue(ColorBytes color)
        {
            switch (color)
            {
                case ColorBytes.Red:
                    return Convert.ToByte(RedSlider.CurrentValue);
                case ColorBytes.Blue:
                    return Convert.ToByte(BlueSlider.CurrentValue);
                case ColorBytes.Green:
                    return Convert.ToByte(GreenSlider.CurrentValue);
            }
            return 255;
        }

        public ColorBGRA GetColor()
        {
            return new ColorBGRA(GetValue(ColorBytes.Red), GetValue(ColorBytes.Green), GetValue(ColorBytes.Blue), 255);
        }

        public Color GetSystemColor()
        {
            return Color.FromArgb(GetValue(ColorBytes.Red), GetValue(ColorBytes.Green), GetValue(ColorBytes.Blue));
        }

        public void Hide()
        {
            _savedValues = new int[3] {RedSlider.CurrentValue, GreenSlider.CurrentValue, BlueSlider.CurrentValue};
            Menu.Remove(RedSlider.SerializationId);
            Menu.Remove(BlueSlider.SerializationId);
            Menu.Remove(GreenSlider.SerializationId);
        }
    }

    public enum ColorBytes
    {
        Red, Green, Blue
    }
}