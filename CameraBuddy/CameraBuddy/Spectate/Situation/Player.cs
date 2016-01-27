using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Spectate.Core;
using CameraBuddy.Spectate.Data;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

// ReSharper disable ConvertPropertyToExpressionBody

namespace CameraBuddy.Spectate.Situation
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
        public static float HealthPercent { get { return Hero.Health / Hero.MaxHealth; } }
        public static float Mana { get { return Hero.Mana; } }
        public static float ManaPercent { get { return Hero.Mana / Hero.MaxMana; } }
        public static float TotalShield { get { return Hero.AllShield; } }
        //Info
        public static float Level { get { return Hero.Level; } }
        public static float BoundingRadius { get { return Hero.BoundingRadius; } }
        public static float Gold { get { return Hero.Gold; } }
        public static float AttackRange { get { return Hero.AttackRange; } }
        public static bool IsPlayerAp { get { return AbilityPower > AttackDamage; } }
        public static bool IsPlayerAd { get { return AttackDamage >= AbilityPower; } }
        public static PlayerState PlayerState { get; set; }
        public static Region.Location RegionIn { get { return Posistion.InWhatRegion(); } }
        public static GameObjectTeam MyTeam { get { return Hero.Team; } }
        public static GameObjectTeam EnemyTeam { get { return MyTeam == GameObjectTeam.Chaos ? GameObjectTeam.Order :  GameObjectTeam.Chaos; } } //Opposite Team
        //Damage
        public static float AttackDamage { get { return Hero.TotalAttackDamage; } }
        public static float AbilityPower { get { return Hero.TotalMagicalDamage; } }
        public static float LargestCrit { get { return Hero.LargestCriticalStrike; } }


        public static Lane GetPlayerLane()
        {
            return ChampionController.ChampionPlugin.PlayerLane;
        }

        public static bool IsLaneAssigned(params Lane[] lanes)
        {
            return lanes.Any(x => x == GetPlayerLane());
        }
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

        public static Region.Location[] GetAssignedLaneRegions()
        {
            switch (GetPlayerLane())
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

        public static PlayerState GetPlayerState()
        {
            if (Game.GameState == GameState.FirstMinionContact) return PlayerState.Farming;
            return PlayerStateRegionCheck(Region.PlayersInRegion(RegionIn));
        }

        private static bool IsPlayerLowHealth()
        {
            return HealthPercent < LowHealthPercentage;
        }
        private static bool IsPlayerLowMana()
        {
            return ManaPercent < LowManaPercentage;
        }
        private static PlayerState PlayerStateRegionCheck(List<AIHeroClient> players)
        {
            var data = new FightData(players);
            if (CommonFleeChecks(data)) return PlayerState.Fleeing;
            if (data.TotalAllyLevel > data.TotalEnemyLevel)
            {
                //We are higher level

                //TODO: Check if allies are already attacking
                if (players.GetAllies<AIHeroClient>().Any(x => x.IsAttackingPlayer))
                {
                    return PlayerState.FightingWithTeam;
                }

                if (data.TotalAllyHealth >= data.TotalEnemyHealth)
                {
                    //We have more health then their team
                    //We also have more damage, so lets attack ;)
                    if (data.TotalAllyDamage > data.TotalEnemyDamage)
                        return PlayerState.FightingWithTeam;
                    //TODO: Maybe add "SafeFighting" Player Mode
                    return PlayerState.Harass;
                }
                else
                {
                    //We have less health then their team...
                    if (data.TotalAllyDamage > data.TotalEnemyDamage)
                        return PlayerState.FightingWithTeam;
                    //TODO: Maybe add "SafeFighting" Player Mode
                    return PlayerState.Harass;
                }
            }
            //else if (data.TotalAllyLevel == data.TotalEnemyLevel)
            //{
            //    //We are equal level
            //    if (data.TotalAllyHealth > data.TotalEnemyHealth)
            //    {
            //        //We have more health then their team

            //    }
            //    else
            //    {
            //        //We have less health then their team...

            //    }
            //}
            else
            {
                //We are lower level
                if (data.TotalAllyHealth > data.TotalEnemyHealth)
                {
                    //We have more health then their team
                    if(CommonFleeChecks(data)) return PlayerState.Fleeing;
                    return PlayerState.Fighting;
                }
                else
                {
                    //We have less health then their team...
                    if (!IsPlayerLowHealth() && !IsPlayerLowMana()) return PlayerState.Harass;
                    if (!IsPlayerLowHealth() && IsPlayerLowMana()) return PlayerState.Farming;
                    return PlayerState.Fleeing;
                }
            }
        }

        private static bool CommonFleeChecks(FightData data)
        {
            if (data.TotalEnemyHealth < Health + TotalShield && data.TotalEnemyDamage < data.TotalAllyDamage) return false;
            if (data.TotalAllyDamage < data.TotalEnemyDamage) return false;

            return IsPlayerLowHealth() || (IsPlayerLowMana() && IsPlayerAp);
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
    }

    public enum PlayerState
    {
        /// <summary>
        /// When the player is at the shop
        /// </summary>
        BuyingItems,
        ///Laning
        /// <summary>
        /// When no enemy is in your lane, and minions are in safe position
        /// </summary>
        Farming,

        /// <summary>
        /// When enemy's are in your lane, but are also attacking minions
        /// </summary>
        Harass,

        /// <summary>
        /// When enemy's are in your lane, there are no minions and they are in attack range.
        /// </summary>
        Fighting,

        /// <summary>
        /// When more enemy's in the current region than allies
        /// </summary>
        Fleeing,

        /// <summary>
        /// When most enemy's are in your lane and no allies are nearby
        /// </summary>
        FightingWithoutTeam,

        /// <summary>
        /// When most enemy's are in your lane, and you are with around the same amount of Allies
        /// </summary>
        FightingWithTeam,

        //Turrents
        /// <summary>
        /// When you or your team are pushing a turrent
        /// </summary>
        PushingTurrent,

        /// <summary>
        /// When the enemy is pushing a turrent
        /// </summary>
        DefendingTurrent,

        //Nexus
        /// <summary>
        /// When you or your team are pushing the nexus
        /// </summary>
        PushingNexus,

        /// <summary>
        /// When the enemy is pushing your nexus
        /// </summary>
        DefendingNexus,
    }
}