using System;
using AdEvade.Draw;
using EloBuddy.SDK.Menu.Values;

namespace AdEvade.Config
{
    public class DynamicCheckBox
    {
        public CheckBox CheckBox;
        private readonly ConfigDataType _type;
        private readonly string _configKey;
        private bool _isBasedOnSpell;
        private SpellConfigProperty _spellProperty;


        public DynamicCheckBox(ConfigDataType configDataType, string key, string displayName, bool defaultValue)
        {
            _type = configDataType;
            _configKey = key;
            DynamicCheckBoxInit(displayName, defaultValue, false, SpellConfigProperty.None);
        }

        public DynamicCheckBox(ConfigDataType configDataType, string key, string displayName, bool defaultValue,
            bool isBasedOnSpell, SpellConfigProperty property)
        {
            _type = configDataType;
            _configKey = key;
            DynamicCheckBoxInit(displayName, defaultValue, isBasedOnSpell, property);
        }

        public void DynamicCheckBoxInit(string displayName, bool defaultValue, bool isBasedOnSpell,
            SpellConfigProperty property)
        {
            CheckBox = new CheckBox(displayName, defaultValue);
            _spellProperty = property;
            _isBasedOnSpell = isBasedOnSpell;
            switch (_type)
            {
                case ConfigDataType.Data:
                    Properties.SetData(_configKey, defaultValue);
                    break;
                
            }
            CheckBox.OnValueChange += CheckBox_OnValueChange;
            //Properties.OnConfigValueChanged += Config_OnConfigValueChanged;
        }

        //private void Config_OnConfigValueChanged(ConfigValueChangedArgs args)
        //{
        //    if (args.Key == _configKey && args.Type == _type && (!_isBasedOnSpell || _isBasedOnSpell && _spellProperty == args.Property))
        //    {
        //        CheckBox.CurrentValue = (bool) args.Value;
        //    }
        //}

        private void CheckBox_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Debug.DrawTopLeft(_type + ": " +  _spellProperty + ": " + _configKey + ": " + "Value Changed To " + sender.CurrentValue);
            switch (_type)
            {
                case ConfigDataType.Data:
                    Properties.SetData(_configKey, sender.CurrentValue, false);
                    break;
                case ConfigDataType.Spells:
                    if (_isBasedOnSpell)
                    {
                        var spell = Properties.GetSpell(_configKey);
                        switch (_spellProperty)
                        {
                            case SpellConfigProperty.Dodge:
                                spell.Dodge = sender.CurrentValue;
                                break;
                            case SpellConfigProperty.Draw:
                                spell.Draw = sender.CurrentValue;
                                break;
                        }
                        Properties.SetSpell(_configKey, spell);
                    }
                    break;
                case ConfigDataType.EvadeSpell:
                    if (_isBasedOnSpell)
                    {
                        var spell = Properties.GetEvadeSpell(_configKey);
                        switch (_spellProperty)
                        {
                            case SpellConfigProperty.UseEvadeSpell:
                                spell.Use = sender.CurrentValue;
                                break;
                            default:
                                return;
                        }
                        Properties.SetEvadeSpell(_configKey, spell);
                    }
                    break;
            }
        }
    }
}