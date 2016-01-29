using System.Collections.Generic;
using EloBuddy;

namespace Coman3.API.Objects
{
    /// <summary>
    /// A simple class that provides access to Ally or Enemy objects.
    /// </summary>
    public static class Entities
    {
        public static class Ally 
        {
            public static List<AIHeroClient> Heroes { get { return Objects.Heroes.Ally.Heroes; } }
            public static List<Obj_AI_Minion> Minions { get { return Objects.Minions.Ally.AllMinions; } }
        }

        public static class Enemies
        {
            public static List<AIHeroClient> Heroes { get { return Objects.Heroes.Ally.Heroes; } }
            public static List<Obj_AI_Minion> Minions { get { return Objects.Minions.Ally.AllMinions; } }
        }

    }
}