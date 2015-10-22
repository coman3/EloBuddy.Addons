using EloBuddy.SDK.Menu.Values;

namespace EzEvade.Config
{
    public class DynamicKeyBind
    {
        public KeyBind KeyBind;
        private readonly string _configKey;

        public DynamicKeyBind(string key, string displayName, bool defaultValue, KeyBind.BindTypes type, uint defaultKey1 = 27, uint defaultKey2 = 27)
        {
            _configKey = key;
            KeyBind = new KeyBind(displayName, defaultValue, type, defaultKey1, defaultKey2);
            Config.SetKey(_configKey, KeyBind);
            KeyBind.OnValueChange += KeyBind_OnValueChange;
        }

        private void KeyBind_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Config.SetKey(_configKey, KeyBind);
        }
    }
}