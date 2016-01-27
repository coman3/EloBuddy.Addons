using System.Collections.Generic;
using SharpDX;

namespace CameraBuddy.Spectate.Situation
{
    public class Jungle
    {
        public static List<JungleCamp> AllyJungleCamps { get; set; }
        public static List<JungleCamp> EnemyJungleCamps { get; set; }

        static Jungle()
        {
            AllyJungleCamps = new List<JungleCamp>();
            EnemyJungleCamps = new List<JungleCamp>();
            //TODO: Add Lists
        }
    }

    public class JungleCamp
    {
        public Vector3 Posistion { get; set; }
        public short TotalMinions { get; set; }
        public string LargeMinionName { get; set; }
        public string[] SmallMinionNames { get; set; }
        public float Size { get; set; }
        public bool IsDragon { get; set; }
        public bool IsBarron { get; set; }
        public float RespawnTime { get; set; }
    }
}