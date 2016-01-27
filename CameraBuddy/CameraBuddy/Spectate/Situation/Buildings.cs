using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Spectate.Data;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace CameraBuddy.Spectate.Situation
{
    public static class Buildings
    {
        private const string SpawnTurrent = "Order5";
        private const string NexusTurrent = "Order4";
        private const string InhibitorTurrent = "Order3";
        private const string InnerTurrent = "Order2";
        private const string OutterTurrent = "Order1";
        
        public static readonly float TurrentsRange = 780 + Player.BoundingRadius;
        public static List<Obj_AI_Turret> AllTurrents { get { return EntityManager.Turrets.AllTurrets; } }

        public static class Ally
        {
            public static List<Obj_AI_Turret> Turrents { get { return EntityManager.Turrets.Allies; } }
            public static Obj_HQ Nexus { get { return ObjectManager.Get<Obj_HQ>().First(x => x.Team == Player.MyTeam); } } 
            public static List<Obj_AI_Turret> BotTurrents { get { return Turrents.Where(x => Lanes.IsInBotLane(x.Position)).ToList(); } }
            public static List<Obj_AI_Turret> TopTurrents { get { return Turrents.Where(x => Lanes.IsInTopLane(x.Position)).ToList(); } }
            public static List<Obj_AI_Turret> MidTurrents { get { return Turrents.Where(x => Lanes.IsInMidLane(x.Position)).ToList(); } }
        }
        public static class Enemy
        {
            public static List<Obj_AI_Turret> Turrents { get { return EntityManager.Turrets.Enemies; } }
            public static Obj_HQ Nexus { get { return ObjectManager.Get<Obj_HQ>().First(x => x.Team != Player.MyTeam); } }
            public static List<Obj_AI_Turret> BotTurrents { get { return Turrents.Where(x => Lanes.IsInBotLane(x.Position)).ToList(); } }
            public static List<Obj_AI_Turret> TopTurrents { get { return Turrents.Where(x => Lanes.IsInTopLane(x.Position)).ToList(); } }
            public static List<Obj_AI_Turret> MidTurrents { get { return Turrents.Where(x => Lanes.IsInMidLane(x.Position)).ToList(); } }
        }

        public static TurrentTier GetTurrentTier(this Obj_AI_Turret turrent)
        {
            if(turrent.BaseSkinName.EndsWith(SpawnTurrent)) return TurrentTier.Spawn;
            if (turrent.BaseSkinName.EndsWith(NexusTurrent)) return TurrentTier.Nexus;
            if (turrent.BaseSkinName.EndsWith(InhibitorTurrent)) return TurrentTier.Inhibitor;
            if (turrent.BaseSkinName.EndsWith(InnerTurrent)) return TurrentTier.Inner;
            return TurrentTier.Outter;
        }

        public static Obj_AI_Turret[] GetTeir(this List<Obj_AI_Turret> turrents, TurrentTier turrentTier)
        {
            return turrents.Where(x => x.GetTurrentTier() == turrentTier).ToArray();
        }
        public static bool IsNearTurrent(this Vector3 pos, EntityManager.UnitTeam team, float range, params TurrentTier[] turrentTiers)
        {
            return pos.IsInTurrent(team, range, turrentTiers);
        }

        public static bool IsInTurrent(this Vector3 pos, EntityManager.UnitTeam team, params TurrentTier[] turrentTiers)
        {
            return pos.IsInTurrent(team, 0, turrentTiers);
        }
        //Is In Range of turrent
        public static bool IsInTurrent(this Vector3 pos, EntityManager.UnitTeam team, float extraRange, params TurrentTier[] turrentTiers)
        {
            if(team == EntityManager.UnitTeam.Ally)
                return Ally.Turrents.Any(x => x.IsInRange(pos, TurrentsRange) && (turrentTiers == null || turrentTiers.Any(i => i == x.GetTurrentTier())));
            if (team == EntityManager.UnitTeam.Enemy)
                return Enemy.Turrents.Any(x => x.IsInRange(pos, TurrentsRange) && (turrentTiers == null || turrentTiers.Any(i => i == x.GetTurrentTier())));
            return AllTurrents.Any(x => x.IsInRange(pos, TurrentsRange) && (turrentTiers == null || turrentTiers.Any(i => i == x.GetTurrentTier())));

        }

        public static void DrawRanges()
        {
            Circle.Draw(new ColorBGRA(10, 240, 10, 255), TurrentsRange, 2, Ally.Turrents.Where(x => !x.IsDead).Select(x => x.Position).ToArray());
            Circle.Draw(new ColorBGRA(240, 10, 10, 255), TurrentsRange, 5, Enemy.Turrents.Where(x => !x.IsDead).Select(x => x.Position).ToArray());
        }

        public enum TurrentTier
        {
            Outter, Inner, Inhibitor, Nexus, Spawn
        }
    }
}