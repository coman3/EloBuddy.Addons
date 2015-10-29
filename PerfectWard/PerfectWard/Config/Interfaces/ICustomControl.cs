using EloBuddy.SDK.Menu.Values;

namespace PerfectWard.Config.Interfaces
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface ICustomControl<TValueBase>
    {
        ValueBase<TValueBase> GetValueBase();
    }
}