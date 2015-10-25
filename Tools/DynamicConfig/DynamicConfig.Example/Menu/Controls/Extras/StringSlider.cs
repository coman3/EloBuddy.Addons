using DynamicConfig.Example.Menu.Interfaces;
using EloBuddy.SDK.Menu.Values;

namespace DynamicConfig.Example.Menu.Controls.Extras
{
    public class StringSlider : ICustomControl<int>
    {
        /// <summary>
        /// The internal <see cref="DynamicSlider"/> used. Use <seealso cref="GetSlider"/> to get the slider.
        /// </summary>
        private readonly DynamicSlider _slider;
        /// <summary>
        /// Options accociated to this <see cref="StringSlider"/>
        /// </summary>
        public readonly string[] Options;
        /// <summary>
        /// Creates a new <see cref="StringSlider"/> with an internal <see cref="DynamicSlider"/>.
        /// </summary>
        /// <param name="key">The key you want to associate this <see cref="StringSlider"/> within the <see cref="Properties"/>.<see cref="Properties.Data"/>.</param>
        /// <param name="defaultValueIndex">The zero-based index of options to display (<see cref="Slider.DisplayName"/>)</param>
        /// <param name="options">The array of choices / options</param>
        public StringSlider(string key, int defaultValueIndex = 0, params string[] options)
        {
            Options = options;
            _slider = new DynamicSlider(key, options[defaultValueIndex], defaultValueIndex, 0, options.Length - 1);
            GetValueBase().OnValueChange += _slider_OnValueChange;
            GetValueBase().DisplayName = options[GetValueBase().CurrentValue];
        }
        /// <summary>
        /// Gets the <see cref="DynamicSlider"/> associated with this <see cref="StringSlider"/>.
        /// </summary>
        /// <returns>The Associated <see cref="DynamicSlider"/></returns>
        /// <summary>
        /// Update <see cref="Slider.DisplayName"/> when value of internal <see cref="DynamicSlider"/> changes.
        /// </summary>
        private void _slider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            sender.DisplayName = Options[args.NewValue];
        }

        public ValueBase<int> GetValueBase()
        {
            return _slider.GetValueBase();
        }
    }
}