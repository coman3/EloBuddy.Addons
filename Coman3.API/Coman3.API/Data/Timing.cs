using EloBuddy;

namespace Coman3.API.Data
{
    /// <summary>
    /// Events that happen at a certain time, the game time of that event is here. (Summoners Rift)
    /// </summary>
    public static class Timing
    {
        /// <summary>
        /// The <see cref="Game.Time"/> in which the gate falls
        /// </summary>
        public const float GateDown = 35;
        /// <summary>
        /// The <see cref="Game.Time"/> in which the first jungle minion spawns
        /// </summary>
        public const float FirstJungleSpawn = 120;
        /// <summary>
        /// The <see cref="Game.Time"/> in which the dragon spawns
        /// </summary>
        public const float FirstDragonSpawn = 175;
        /// <summary>
        /// The <see cref="Game.Time"/> in which the first minion spawns
        /// </summary>
        public const float FirstMinionsSpawn = 95;

        //public const float FirstBarronSpawn = 0;
        /// <summary>
        /// The <see cref="Game.Time"/> in which the minions are separated. This means that it takes 'X'(<see cref="Game.Time"/>) to spawn a new wave of minions.
        /// </summary>
        public const float MinionSpeperation = 30; //First minion spawn to first minion spawned of next wave.
        /// <summary>
        /// The <see cref="Game.Time"/> in which the mid lane minions collide
        /// </summary>
        public const float MinionsFirstContactMid = 115;
        /// <summary>
        /// The <see cref="Game.Time"/> in which the bottom and top lane minions collide
        /// </summary>
        public const float MinionsFirstContactBotTop = 125;

        public static bool IsGateUp { get { return Game.Time < GateDown; } }
        public static bool HaveMinionsSpawned { get { return Game.Time < FirstMinionsSpawn; } }
        public static bool HasJungleSpawned { get { return Game.Time < FirstJungleSpawn; } }
        public static bool HasDragonSpawned { get { return Game.Time < FirstDragonSpawn; } }
        public static bool HaveTopBotMinionsContacted { get { return Game.Time > MinionsFirstContactBotTop; } }
        public static bool HaveMidMinionsContacted { get { return Game.Time > MinionsFirstContactMid; } }
    }
}