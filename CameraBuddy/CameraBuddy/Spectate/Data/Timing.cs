namespace CameraBuddy.Spectate.Data
{
    /// <summary>
    /// Events that happen at a certain time, the game time of that event is here.
    /// </summary>
    public static class Timing
    {
        public const float GateDown = 35;
        public const float FirstJungleSpawn = 120;
        public const float FirstDragonSpawn = 175;
        public const float FirstMinionsSpawn = 95;

        //public const float FirstBarronSpawn = 0;
        public const float MinionSpeperation = 30; //First minion spawn to first minion spawned of next wave.
        public const float MinionsFirstContactMid = 115;
        public const float MinionsFirstContactBotTop = 125;

        public static bool IsGateUp { get { return EloBuddy.Game.Time < GateDown; } }
        public static bool HaveMinionsSpawned { get { return EloBuddy.Game.Time < FirstMinionsSpawn; } }
        public static bool HasJungleSpawned { get { return EloBuddy.Game.Time < FirstJungleSpawn; } }
        public static bool HasDragonSpawned { get { return EloBuddy.Game.Time < FirstDragonSpawn; } }
        public static bool HaveTopBotMinionsContacted { get { return EloBuddy.Game.Time > MinionsFirstContactBotTop; } }
        public static bool HaveMidMinionsContacted { get { return EloBuddy.Game.Time > MinionsFirstContactMid; } }
    }
}