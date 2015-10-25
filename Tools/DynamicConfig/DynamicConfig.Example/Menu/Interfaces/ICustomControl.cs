using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace DynamicConfig.Example.Menu.Interfaces
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface ICustomControl<TValueBase>
    {
        ValueBase<TValueBase> GetValueBase();
    }
}