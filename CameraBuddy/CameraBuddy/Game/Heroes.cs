using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy.Game
{
    public static class Heroes
    {
        public static List<Vector3> DebugPosistions = new List<Vector3>(5); 

        public static List<GameObject> Allies = GetHeroes(HeroType.Ally);

        public static List<GameObject> Enemies = GetHeroes(HeroType.Enemy);
        public static List<GameObject> All = GetHeroes(HeroType.Ally);
        public static List<GameObject> Debug = GetHeroes(HeroType.Debug);

        public static Vector3 GetAveragePosistion(params HeroType[] heroTypes)
        {
            return new Vector3();
        }
        private static List<GameObject> GetHeroes(HeroType heroType)
        {
            var temp = new List<GameObject>();
            switch (heroType)
            {
                case HeroType.Ally:
                    temp.AddRange(EntityManager.Heroes.Allies.Select(ally => new PlayerGameObject(ally)));
                    break;
                case HeroType.Enemy:
                    temp.AddRange(EntityManager.Heroes.Enemies.Select(enemy => new PlayerGameObject(enemy)));
                    break;
                case HeroType.All:
                    temp.AddRange(EntityManager.Heroes.AllHeroes.Select(all => new PlayerGameObject(all)));
                    break;
                case HeroType.Debug:
                    temp.AddRange(DebugPosistions.Select(pos => new DebugGameObject(pos)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("HeroType", heroType, null);
            }
            return temp;
        }

        public enum HeroType : byte
        {
            Ally,
            Enemy,
            All,
            Debug
        }
    }

    public class MinionGameObject : GameObject
    {
        public Obj_AI_Minion Minion { get; set; }

        public MinionGameObject(Obj_AI_Minion minion)
        {
            Minion = minion;
        }
        public override Vector3 GetPosistion()
        {
            return Minion.Position;
        }

        public override float GetStat(FloatStatType statType)
        {
            switch (statType)
            {
                case FloatStatType.Health:
                    return Minion.Health;
                default:
                    return 0;
            }
        }
        public override bool IsAlive()
        {
            return !Minion.IsDead;
        }
    }
    public class PlayerGameObject : GameObject
    {
        public AIHeroClient HeroClient { get; set; }

        public PlayerGameObject(AIHeroClient hero)
        {
            HeroClient = hero;
        }

        public override Vector3 GetPosistion()
        {
            return HeroClient.Position;
        }

        public override bool IsAlive()
        {
            return !HeroClient.IsDead;
        }

        public override float GetStat(FloatStatType statType)
        {
            switch (statType)
            {
                case FloatStatType.Health:
                    return HeroClient.Health;
                case FloatStatType.Mana:
                    return HeroClient.Mana;
                default:
                    throw new ArgumentOutOfRangeException("StatType", statType, null);
            }
        }
        
    }
    public class DebugGameObject : GameObject
    {
        public Vector3 Posistion { get; set; }

        public DebugGameObject(Vector3 posistion)
        {
            Posistion = posistion;
        }

        public override Vector3 GetPosistion()
        {
            return Posistion;
        }

        public override float GetStat(FloatStatType statType)
        {
            return 0;
        }

        public override bool IsAlive()
        {
            return true;
        }
    }

    public abstract class GameObject
    {
        public abstract Vector3 GetPosistion();
        public abstract float GetStat(FloatStatType statType);
        public abstract bool IsAlive();
        public enum FloatStatType
        {
            Health,
            Mana
        }
    }
}