using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using CameraBuddy.Camera;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace CameraBuddy
{
    public abstract class IntelligenceMenuGroup : IMenuGroup
    {
        private Dictionary<string, CheckBox> CheckBoxs { get; set; }
        private Dictionary<string, Slider> Sliders { get; set; }
        private Dictionary<string, KeyBind> KeyBinds { get; set; }

        public bool Enabled { get; set; }

        protected IntelligenceMenuGroup()
        {
            Enabled = true;
            CheckBoxs = new Dictionary<string, CheckBox>();
            Sliders = new Dictionary<string, Slider>();
            KeyBinds = new Dictionary<string, KeyBind>();
        }

        private void OnEnableChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Enabled = args.NewValue;
        }

        public KeyBind AddKeyBind(KeyBind item)
        {
            KeyBinds.Add(GetUniqueId() + "_" + item.DisplayName, item);
            return item;
        }
        public CheckBox AddCheckbox(CheckBox item)
        {
            CheckBoxs.Add(GetUniqueId() + "_" + item.DisplayName, item);
            return item;
        }
        public Slider AddSlider(Slider item)
        {
            Sliders.Add(GetUniqueId() + "_" + item.DisplayName, item);
            return item;
        }

        public Slider GetSlider(string displayName)
        {
            return Sliders[GetUniqueId() + "_" + displayName];
        }
        public CheckBox GetCheckBox(string displayName)
        {
            return CheckBoxs[GetUniqueId() + "_" + displayName];
        }
        public KeyBind GetKeyBind(string displayName)
        {
            return KeyBinds[GetUniqueId() + "_" + displayName];
        }

        public abstract string GetUniqueId();

        public virtual void AddToMenu(Menu menu)
        {
            menu.Add(GetUniqueId() + "_Enabled", new CheckBox("Enable")).OnValueChange += OnEnableChange;
            menu.AddSeparator();
            foreach (var keyBind in KeyBinds)
            {
                menu.Add(keyBind.Key, keyBind.Value);
            }
            foreach (var checkBox in CheckBoxs)
            {
                menu.Add(checkBox.Key, checkBox.Value);
            }
            foreach (var slider in Sliders)
            {
                menu.Add(slider.Key, slider.Value);
            }
        }
    }
    public interface IMenuGroup
    { 
        void AddToMenu(Menu menu);
    }

    public static class MenuHeleprs
    {
        public static void AddGroup(this Menu menu, IMenuGroup group)
        {
            group.AddToMenu(menu);
        }
    }
}
