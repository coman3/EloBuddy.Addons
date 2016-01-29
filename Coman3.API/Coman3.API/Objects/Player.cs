using System.Collections.Generic;
using System.Linq;
using Coman3.API.Data;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

// ReSharper disable ConvertPropertyToExpressionBody

namespace Coman3.API.Objects
{
    public static class Player
    {
        public static float LowHealthPercentage = 0.35f;
        public static float LowManaPercentage = 0.15f;
        public static float ExtraSafeDistance = 500;

        public static AIHeroClient Hero { get { return EloBuddy.Player.Instance; } }
        public static Vector3 Posistion { get { return Hero.Position; } }
        //Regen's
        public static float Health { get { return Hero.Health; } }
        public static float HealthPercent { get { return Hero.HealthPercent; } }
        public static float Mana { get { return Hero.Mana; } }
        public static float ManaPercent { get { return Hero.ManaPercent; } }
        public static float TotalShield { get { return Hero.AllShield; } }
        //Info
        public static float Level { get { return Hero.Level; } }
        public static float BoundingRadius { get { return Hero.BoundingRadius; } }
        public static float Gold { get { return Hero.Gold; } }
        public static float AttackRange { get { return Hero.AttackRange; } }

        public static bool IsPlayerAp { get { return AbilityPower > AttackDamage; } }
        public static bool IsPlayerAd { get { return AttackDamage >= AbilityPower; } }
        public static bool LowHealth { get { return IsLowHealth(Hero); } }
        public static bool LowMana { get { return IsLowMana(Hero); } }



        public static Region.Location RegionIn { get { return Posistion.InWhatRegion(); } }
        public static GameObjectTeam MyTeam { get { return Hero.Team; } }
        public static GameObjectTeam EnemyTeam { get { return MyTeam == GameObjectTeam.Chaos ? GameObjectTeam.Order :  GameObjectTeam.Chaos; } } //Opposite Team
        //Damage
        public static float AttackDamage { get { return Hero.TotalAttackDamage; } }
        public static float AbilityPower { get { return Hero.TotalMagicalDamage; } }
        public static float LargestCrit { get { return Hero.LargestCriticalStrike; } }

        public static float DistanceTo(this AIHeroClient hero, Vector3 pos)
        {
            return pos.Distance(hero);
        }
        public static float DistanceToMe(this AIHeroClient hero)
        {
            return Posistion.Distance(hero);
        }
        public static bool InRegion(params Region.Location[] location)
        {
            return Region.IsInRegion(Posistion, location);
        }

        public static bool InAllyTurrent(params Buildings.TurrentTier[] turrentTiers)
        {
            return Posistion.IsInTurrent(EntityManager.UnitTeam.Ally ,turrentTiers);
        }
        public static bool InEnemyTurrent(params Buildings.TurrentTier[] turrentTiers)
        {
            return Posistion.IsInTurrent(EntityManager.UnitTeam.Enemy, turrentTiers);
        }
        public static class Ally
        {
            public static float NearestDistance
            {
                get
                {
                    return
                        EntityManager.Heroes.Allies.Where(x => !x.IsDead)
                            .Min(ally => Hero.Position.Distance(ally.Position));
                }
            }
            public static float FurthestDistance
            {
                get
                {
                    return
                        EntityManager.Heroes.Allies.Where(x => !x.IsDead)
                            .Max(ally => Hero.Position.Distance(ally.Position));
                }
            }
            public static float NearestDistanceToTower
            {
                get
                {
                    return
                        EntityManager.Turrets.Allies.Where(x => !x.IsDead)
                            .Max(ally => Hero.Position.Distance(ally.Position));
                }
            }
        }

        public static class Enemy
        {
            public static float NearestDistance
            {
                get
                {
                    return
                        EntityManager.Heroes.Enemies.Where(x => !x.IsDead)
                            .Min(ally => Hero.Position.Distance(ally.Position));
                }
            }
            public static float FurthestDistance
            {
                get
                {
                    return
                        EntityManager.Heroes.Enemies.Where(x => !x.IsDead)
                            .Max(ally => Hero.Position.Distance(ally.Position));
                }
            }
            public static float NearestDistanceToTower
            {
                get
                {
                    return
                        EntityManager.Turrets.Enemies.Where(x => !x.IsDead)
                            .Max(ally => Hero.Position.Distance(ally.Position));
                }
            }
        }

        public static bool IssueOrder(GameObjectOrder command, Obj_AI_Base unit, bool trigerEvent = false)
        {
            return EloBuddy.Player.IssueOrder(command, unit, trigerEvent);
        }
        public static bool IssueOrder(GameObjectOrder command, Vector3 pos, bool trigerEvent = false)
        {
            return EloBuddy.Player.IssueOrder(command, pos, trigerEvent);
        }

        public static bool IsAtShop()
        {
            return Posistion.IsInTurrent(EntityManager.UnitTeam.Ally, Buildings.TurrentTier.Spawn);
        }

        public static Region.Location[] GetLaneRegions(Lane lane)
        {
            switch (lane)
            {
                case Lane.Top:
                    return new [] {Region.Location.LeftTopLane, Region.Location.CenterTopLane, Region.Location.RightTopLane};
                case Lane.Middle:
                    return new [] { Region.Location.LeftMidLane, Region.Location.CenterMidLane, Region.Location.RightMidLane };
                case Lane.Jungle:
                    return new[] { Region.Location.BottomLeftOuterJungle, Region.Location.BottomLeftOuterJungle, Region.Location.TopLeftOuterJungle, Region.Location.TopRightOuterJungle };
                case Lane.Bottom:
                    return new[] { Region.Location.LeftBotLane, Region.Location.CenterBotLane, Region.Location.RightBotLane };
            }
            return new[] {Region.Location.Unknown};
        }
        public static int CountEnemies(this IEnumerable<Obj_AI_Base> objects)
        {
            return objects.Count(x => x.Team == EnemyTeam);
        }
        public static int CountAllies(this IEnumerable<Obj_AI_Base> objects)
        {
            return objects.Count(x => x.Team == MyTeam);
        }
        public static List<T> GetEnemys<T>(this IEnumerable<Obj_AI_Base> objects)
        {
            return objects.Where(x => x.Team == EnemyTeam).Cast<T>().ToList();
        }
        public static List<T> GetAllies<T>(this IEnumerable<Obj_AI_Base> objects)
        {
            return objects.Where(x => x.Team == MyTeam).Cast<T>().ToList();
        }

        public static bool IsLowHealth(this AIHeroClient hero)
        {
            return hero.HealthPercent >= LowHealthPercentage;
        }
        public static bool IsLowMana(this AIHeroClient hero)
        {
            return hero.ManaPercent >= LowManaPercentage;
        }

        public static bool HasQ(this AIHeroClient hero)
        {
            if (hero.IsAlly)
                return Heroes.Ally.SpellAvaliblities[hero].Q;
            return Heroes.Enemy.SpellAvaliblities[hero].Q;
        }
        public static bool HasW(this AIHeroClient hero)
        {
            if (hero.IsAlly)
                return Heroes.Ally.SpellAvaliblities[hero].W;
            return Heroes.Enemy.SpellAvaliblities[hero].W;
        }
        public static bool HasE(this AIHeroClient hero)
        {
            if (hero.IsAlly)
                return Heroes.Ally.SpellAvaliblities[hero].E;
            return Heroes.Enemy.SpellAvaliblities[hero].E;
        }
        public static bool HasR(this AIHeroClient hero)
        {
            if(hero.IsAlly)
                return Heroes.Ally.SpellAvaliblities[hero].R;
            return Heroes.Enemy.SpellAvaliblities[hero].R;
        }


    }
}