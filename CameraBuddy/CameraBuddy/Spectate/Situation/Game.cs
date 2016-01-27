using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Spectate.Data;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy.Spectate.Situation
{
    public static class Game
    {
        public static GameState GameState { get { return GetGameState(); }  }
        public static GameStateInfo GameStateInfo { get; private set; }
        private static bool _minionsHaveContacted = false;




        public static GameState GetGameState()
        {
            // Gate is up
            if (Timing.IsGateUp)
            {
                return UpdateGameStateData(GameState.GameStart,
                    new GameStateInfo(Buildings.Ally.Nexus.Position, Region.Location.None));
            }
            //Gate is down, and minions have not spawned
            if (Timing.HaveMinionsSpawned)
            {
                return UpdateGameStateData(GameState.MinionsSpawning,
                    new GameStateInfo(Player.Posistion, Region.InWhatRegion(Player.Posistion)));
            }
            //Gate is down, and minions have spawned but Jungle minions have not spawned
            if (Timing.HasJungleSpawned)
            {
                return UpdateGameStateData(GameState.JungleSpawning,
                    new GameStateInfo(Player.Posistion, Region.InWhatRegion(Player.Posistion)));
            }
            //Minions have arrived in lane
            if (
                (
                    (Player.IsLaneAssigned(Lane.Bottom, Lane.Top) && Timing.HaveTopBotMinionsContacted) ||
                    (Player.IsLaneAssigned(Lane.Middle, Lane.Jungle) && Timing.HaveMidMinionsContacted)
                    ) &&
                !_minionsHaveContacted
                )
            {
                _minionsHaveContacted = true;
                return UpdateGameStateData(GameState.FirstMinionContact,
                    new GameStateInfo(Player.Posistion, Region.InWhatRegion(Player.Posistion)));
                ;
            }
            Vector3 posistion;
            List<AIHeroClient> heroes;
            //Ally nexus turrents have been hurt 
            if (
                (Buildings.Ally.Turrents.GetTeir(Buildings.TurrentTier.Nexus)
                    .Count(x => !x.IsDead || x.HealthPercent < 100) < 2) &&
                Heros.AreGrouped(HeroType.Enemy, 1, out posistion, out heroes, 3000) &&
                posistion.Distance(Buildings.Ally.Nexus.Position) < 3500)
            {
                return UpdateGameStateData(GameState.AllyFinishing,
                    new GameStateInfo(Buildings.Ally.Nexus.Position, Region.Location.None));
            }

            if (Heros.AreGrouped(HeroType.Enemy, 3, out posistion, out heroes))
            {
                return UpdateGameStateData(GameState.EnemyGrouping, new GameStateInfo(posistion, posistion.InWhatRegion()));
            }
            if (Heros.AreGrouped(HeroType.Ally, 3, out posistion, out heroes))
            {
                return UpdateGameStateData(GameState.AllyGrouping, new GameStateInfo(posistion, posistion.InWhatRegion()));
            }
            if (_minionsHaveContacted)
            {
                return UpdateGameStateData(GameState.Laning,
                    new GameStateInfo(Player.Posistion, Player.Posistion.InWhatRegion()));
            }

            //Enemy nexus turrents have been hurt
            if (
                (Buildings.Enemy.Turrents.GetTeir(Buildings.TurrentTier.Nexus)
                    .Count(x => !x.IsDead || x.HealthPercent < 100) < 2) &&
                Heros.AreGrouped(HeroType.Ally, 2, out posistion, out heroes, 3000) &&
                posistion.Distance(Buildings.Enemy.Nexus.Position) < 3500)
            {
                return UpdateGameStateData(GameState.EnemyFinishing,
                    new GameStateInfo(Buildings.Enemy.Nexus.Position, Region.Location.None));
            }

            return GameState.FirstMinionContact;
        }

        public static GameState UpdateGameStateData(GameState state, GameStateInfo info)
        {
            GameStateInfo = info;
            return state;
        }
        public static class Ally
        {
            public static int TotalKills { get { return EntityManager.Heroes.Allies.Sum(x => x.ChampionsKilled); } }
            public static float TotalGold { get { return EntityManager.Heroes.Allies.Sum(x => x.GoldTotal); } }
        }

        public static class Enemy
        {
            public static int TotalKills { get { return EntityManager.Heroes.Enemies.Sum(x => x.ChampionsKilled); } }
            public static float TotalGold { get { return EntityManager.Heroes.Enemies.Sum(x => x.GoldTotal); } }

        }

        static Game()
        {
            GameStateInfo = new GameStateInfo(Vector3.Zero, Region.Location.None);
        }
    }

    public class GameStateInfo
    {
        public Vector3 Posistion { get; set; }
        public Region.Location Location { get; set; }
        public float GameTimeSet { get; private set; }

        public GameStateInfo(Vector3 posistion, Region.Location location)
        {
            Posistion = posistion;
            Location = location;
            GameTimeSet = EloBuddy.Game.Time;
        }
    }
    public enum GameState
    {
        //Preparation

        /// <summary>
        /// First spawn (At shop)
        /// </summary>
        GameStart, 
        /// <summary>
        /// When Minions Spawn
        /// </summary>
        MinionsSpawning, 
        /// <summary>
        /// When jungle spawns
        /// </summary>
        JungleSpawning,

        //Laning / Fighting

        /// <summary>
        /// Start of laning phase
        /// </summary>
        FirstMinionContact,
        /// <summary>
        /// Most people are in there lanes, not grouped mid / top / bot
        /// </summary>
        Laning,
        /// <summary>
        /// When the game turns to a group fight (More Allys)(people are not in there lanes are grouped at mid / top / bot)
        /// </summary>
        AllyGrouping,
        /// <summary>
        /// When the game turns to a group fight (More Enemy's)(people are not in there lanes are grouped at mid / top / bot)
        /// </summary>
        EnemyGrouping,
        /// <summary>
        /// Someone is in a base attacking a tower, inhibitor, or nexus.
        /// </summary>
        AllyFinishing,
        /// <summary>
        /// Someone is in a base attacking a tower, inhibitor, or nexus.
        /// </summary>
        EnemyFinishing 

    }
}