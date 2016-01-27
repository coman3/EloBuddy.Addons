using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy.Game
{
    public static class Minions
    {
        public static List<Vector3> DebugPosistions = new List<Vector3>();

        public static List<GameObject> Killable { get { return GetMinions(MinionType.Killable, Vector3.Zero); } }
        public static List<GameObject> Ally { get { return GetMinions(MinionType.Ally, Vector3.Zero); } }
        public static List<GameObject> Enemy { get { return GetMinions(MinionType.Enemy, Vector3.Zero); } }
        public static List<GameObject> All { get { return GetMinions(MinionType.All, Vector3.Zero); } }
        public static List<GameObject> Debug { get { return GetMinions(MinionType.Debug, Vector3.Zero); } }

        public static List<GameObject> GetMinions(MinionType type, Vector3 posFrom, float distance = -1)
        {
            var temp = new List<GameObject>();
            switch (type)
            {
                case MinionType.Ally:
                    temp.AddRange(
                        EntityManager.MinionsAndMonsters.AlliedMinions.Where(
                            x => posFrom == Vector3.Zero || x.Position.Distance(posFrom) > distance) //Select all if posFrom is Vector3.Zero otherwise select 
                            .Select(minion => new MinionGameObject(minion)));
                    break;
                case MinionType.Enemy:
                    temp.AddRange(
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                            x => posFrom == Vector3.Zero || x.Position.Distance(posFrom) > distance)
                            .Select(minion => new MinionGameObject(minion)));
                    break;
                case MinionType.All:
                    temp.AddRange(
                        EntityManager.MinionsAndMonsters.AlliedMinions.Where(
                            x => posFrom == Vector3.Zero || x.Position.Distance(posFrom) > distance)
                            .Select(minion => new MinionGameObject(minion)));
                    temp.AddRange(
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                            x => posFrom == Vector3.Zero || x.Position.Distance(posFrom) > distance)
                            .Select(minion => new MinionGameObject(minion)));
                    break;
                case MinionType.Debug:
                    temp.AddRange(DebugPosistions.Select(x => new DebugGameObject(x)));
                    break;
                case MinionType.Killable:
                    temp.AddRange(Orbwalker.LaneClearMinionsList.Select(x => new MinionGameObject(x)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("MinionType", type, null);
            }
            return temp;
        }
        public enum MinionType
        {
            Ally,
            Enemy,
            Killable,
            All,
            Debug
        }
    }
}
