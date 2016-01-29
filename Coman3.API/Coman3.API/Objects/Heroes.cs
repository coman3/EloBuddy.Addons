using System.Collections.Generic;
using System.Linq;
using Coman3.API.Data;
using Coman3.API.Extentions;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace Coman3.API.Objects
{
    public static class Heroes
    {
        public static List<AIHeroClient> AllHeros { get { return EntityManager.Heroes.AllHeroes; } }
        public static class Ally
        {
            public static int Total { get { return Heroes.Count; } }
            public static float TotalGold { get { return Heroes.Sum(x => x.GoldTotal); } }
            public static float TotalXp { get { return Heroes.Sum(x => x.Experience.XP); } }
            public static int TotalDead { get { return Heroes.Count(x => x.IsDead); } }
            public static List<AIHeroClient> Heroes { get { return EntityManager.Heroes.Allies; } }
            public static Dictionary<AIHeroClient, SpellAvaliblity> SpellAvaliblities = new Dictionary<AIHeroClient, SpellAvaliblity>();

            public static SmartList<AIHeroClient> InRegionOrDistance(Region.Location region, Vector3 point, float distance)
            {
                return InRegion(region) + InDistance(point, distance);
            }

            public static SmartList<AIHeroClient> InDistance(Vector3 point, float distance)
            {
                return Heroes.Where(x => x.Position.Distance(point) < distance).ToList().ToSmartList();
            }
            public static SmartList<AIHeroClient> InRegion(Region.Location region)
            {
                return Heroes.Where(x => x.Position.InWhatRegion() == region).ToList().ToSmartList();
            }
            static Ally()
            {
                foreach (var ally in Heroes)
                {
                    SpellAvaliblities.Add(ally, new SpellAvaliblity(ally));
                }
            }
        }
        public static class Enemy
        {
            public static int Total { get { return Heroes.Count; } }
            public static int TotalDead { get { return Heroes.Count(x => x.IsDead); } }
            public static List<AIHeroClient> Heroes { get { return EntityManager.Heroes.Enemies; } }
            public static Dictionary<AIHeroClient, SpellAvaliblity> SpellAvaliblities = new Dictionary<AIHeroClient, SpellAvaliblity>();

            public static SmartList<AIHeroClient> InPlayerRegionOrDistance(float distance = -1)
            {
                if (distance < 0) distance = Player.ExtraSafeDistance;

                return InRegionOrDistance(Player.RegionIn, Player.Posistion, distance);
            }

            public static SmartList<AIHeroClient> InRegionOrDistance(Region.Location region, Vector3 point, float distance)
            {
                return InRegion(region) + InDistance(point, distance);
            }

            public static SmartList<AIHeroClient> InDistance(Vector3 point, float distance)
            {
                return Heroes.Where(x => x.Position.Distance(point) < distance).ToList().ToSmartList();
            }
            public static SmartList<AIHeroClient> InRegion(Region.Location region)
            {
                return Heroes.Where(x => x.Position.InWhatRegion() == region).ToList().ToSmartList();
            }

            static Enemy()
            {
                foreach (var enemy in Heroes)
                {
                    SpellAvaliblities.Add(enemy, new SpellAvaliblity(enemy));
                }
            }
        }
        public static HeroGroupInfo AreGrouped(HeroType type, short count, float maxRadius = 2000)
        {
            Vector3 pos;
            List<AIHeroClient> heros;
            var result = AreGrouped(type, count, out pos, out heros, maxRadius);
            return new HeroGroupInfo(result, type, pos, heros);
        }
        public static bool AreGrouped(HeroType type, short count, out Vector3 posistion, out List<AIHeroClient> outHeros, float maxRadius = 2000)
        {
            var heros = type == HeroType.Ally ? Ally.Heroes : Enemy.Heroes.Where(x => x.IsHPBarRendered).ToList();
            foreach (var hero in heros)
            {
                var herosNear = new List<AIHeroClient>();
                foreach (var hero2 in heros)
                {
                    var heroRegion = hero2.Position.InWhatRegion();
                    if(heroRegion == hero.Position.InWhatRegion()) herosNear.Add(hero2);
                    else if (hero.IsInRange(hero2.Position, maxRadius)) herosNear.Add(hero2);

                    if (herosNear.Count < count) continue;
                    //Hero has at least the selected amount of people around him, return values
                    posistion = herosNear.AverageLocation();
                    outHeros = heros;
                    return true;
                }
            }
            posistion = Vector3.Zero;
            outHeros = null;
            return false;
        }
    }

    public class HeroGroupInfo
    {
        public bool Result { get; set; }
        public HeroType Type { get; set; }
        public Vector3 AveragePosistion { get; set; }
        public List<AIHeroClient> Heros { get; set; } 

        public Region.Location Region { get { return AveragePosistion.InWhatRegion(); } }

        public HeroGroupInfo(bool result, HeroType type, Vector3 averagePosistion, List<AIHeroClient> heros)
        {
            Result = result;
            Type = type;
            AveragePosistion = averagePosistion;
            Heros = heros;
        }

    }
    public enum HeroType
    {
        Ally, Enemy
    }
}