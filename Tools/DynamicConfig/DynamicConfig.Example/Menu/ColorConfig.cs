using System;
using DynamicConfig.Example.Menu.Controls;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
namespace DynamicConfig.Example.Menu
{
    public class ColorConfig
    {
        public Slider RedSlider { get; set; }
        public Slider BlueSlider { get; set; }
        public Slider GreenSlider { get; set; }

        public string Id { get; private set; }
        private static EloBuddy.SDK.Menu.Menu _menu;

        public ColorConfig(EloBuddy.SDK.Menu.Menu menu, string id, Color color)
        {
            Id = id;
            _menu = menu;
            Init(color);
        }

        public void Init(Color color)
        {

            RedSlider = new Slider("Red", color.R, 0, 255);
            GreenSlider = new Slider("Green", color.B, 0, 255);
            BlueSlider = new Slider("Blue", color.G, 0, 255);


            _menu.Add(Id + "Red", RedSlider);
            _menu.Add(Id + "Green", GreenSlider);
            _menu.Add(Id + "Blue", BlueSlider);
            //TODO: Add Color Display (In Menu Perferably)
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

    }

    public enum ColorBytes
    {
        Red, Green, Blue
    }
}