using EloBuddy.SDK.Menu.Values;
using PerfectWard.Config.Interfaces;

namespace PerfectWard.Config
{
    public class DynamicSlider : ICustomControl<int>
    {
        /// <summary>
        /// The internal <see cref="DynamicSlider"/> used. Use <seealso cref="GetSlider"/> to get the slider.
        /// </summary>
        private Slider _slider;
        private readonly string _configKey;

        public DynamicSlider(string key, string displayName, int defaultValue, int minValue, int maxValue)
        {
            _configKey = key;
            DynamicSliderInit(displayName, defaultValue, minValue, maxValue);
            Properties.SetData(_configKey, defaultValue, false);
        }

        public void DynamicSliderInit(string displayName, int defaultValue, int minValue, int maxValue)
        {
            _slider = new Slider(displayName, defaultValue, minValue, maxValue);

            _slider.OnValueChange += Slider_OnValueChange;
            Properties.OnConfigValueChanged += Config_OnConfigValueChanged;
        }


        private void Config_OnConfigValueChanged(ConfigValueChangedArgs args)
        {
            if (args.Key == _configKey)
            {
                _slider.CurrentValue = (int) args.Value;
            }
        }
        private void Slider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            Properties.SetData(_configKey, sender.CurrentValue, false);
        }

        public ValueBase<int> GetValueBase()
        {
            return _slider;
        }
    }
}