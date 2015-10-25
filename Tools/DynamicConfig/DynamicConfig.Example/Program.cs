using System;
using System.Drawing;
using DynamicConfig.Example.Menu.Controls;
using DynamicConfig.Example.Menu.Interfaces;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace DynamicConfig.Example
{
    class Program
    {
        private static EloBuddy.SDK.Menu.Menu menu;

        private static void Main(string[] args) { Loading.OnLoadingComplete += Loading_OnLoadingComplete; }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            menu = MainMenu.AddMenu("Color Picker Example", "ColorPickerExample", "Color Picker Example Menu");

            menu.AddGroupLabel("Color Picker Example Menu");
            menu.AddLabel("Auto Atack Range");

            menu.AddColorPicker(new ColorPicker("ColorPicker1", System.Drawing.Color.Purple)); // Can be added using menu.Add(), but will not have spacing underneath control.

            menu.Add("Private_SetDefaultColor", new CheckBox("Set Color To Default", false)).OnValueChange +=
                delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs changeArgs)
                {
                    if (!changeArgs.OldValue && changeArgs.NewValue) // Make sure we are not going to change the color when we set the checkbox back to false
                    {
                        menu.Get<ColorPicker>("ColorPicker1").SetColor(System.Drawing.Color.Purple); // Set Colorpickers value
                        Core.DelayAction(() => { sender.CurrentValue = false; }, 125); //DelayAction for Tatical Feedback
                    }
                };
        }
    }
}
