using EloBuddy.SDK.Menu.Values;

namespace PerfectWard.Config.Interfaces
{
    public static class Helpers
    {
        public static ValueBase<TValueBase> AddDynamicControl<TValueBase>(this EloBuddy.SDK.Menu.Menu menu, string uniqueIdentifier, ICustomControl<TValueBase> item)
        {
            menu.Add(uniqueIdentifier, item.GetValueBase());

            return item.GetValueBase();
        }
    }
}