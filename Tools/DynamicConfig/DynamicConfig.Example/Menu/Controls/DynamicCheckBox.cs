using System;
using DynamicConfig.Example.Menu.Interfaces;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace DynamicConfig.Example.Menu.Controls
{
    public class DynamicCheckBox : ICustomControl<bool>
    {
        public CheckBox CheckBox;
        private readonly string _configKey;


        public DynamicCheckBox(string key, string displayName, bool defaultValue)
        {
            _configKey = key;
            DynamicCheckBoxInit(displayName, defaultValue);
        }

        public void DynamicCheckBoxInit(string displayName, bool defaultValue)
        {
            CheckBox = new CheckBox(displayName, defaultValue);
            CheckBox.OnValueChange += CheckBox_OnValueChange;
            Properties.OnConfigValueChanged += Config_OnConfigValueChanged;
        }

        private void Config_OnConfigValueChanged(ConfigValueChangedArgs args)
        {
            if (args.Key == _configKey)
                CheckBox.CurrentValue = (bool) args.Value;
        }

        private void CheckBox_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Properties.SetData(_configKey, sender.CurrentValue, false);        
        }

        public ValueBase<bool> GetValueBase()
        {
            return CheckBox;
        }
    }
}