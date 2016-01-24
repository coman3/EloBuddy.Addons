using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy.Game
{
    public static class Heroes
    {
        public static List<AIHeroClient> Allies = GetHeroes(HeroType.Ally);

        private static List<AIHeroClient> GetHeroes(HeroType heroType)
        {
            var temp = new 
            switch (heroType)
            {
                case HeroType.Ally:

                    break;
                case HeroType.Enemy:
                    break;
                case HeroType.All:
                    break;
                case HeroType.Debug:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(heroType), heroType, null);
            }
        }

        public static List<AIHeroClient> Enemies = EntityManager.Heroes.Enemies;
        public static List<AIHeroClient> All = EntityManager.Heroes.AllHeroes;
        public static List<MyFakeHero> Debug;

        public static Vector3 GetAveragePosistion(params HeroType[] heroTypes)
        {
            return new Vector3();
        }

        public enum HeroType : byte
        {
            Ally,
            Enemy,
            All,
            Debug
        }
    }

    public class DebugHero : Hero
    {
        public DebugHero(Vector3 posistion) : base(posistion)
        {

        }

        public override float GetStat(FloatStatType statType)
        {
            
        }
    }
    public abstract class Hero
    {
        public Vector3 Posistion { get; set; }

        public abstract float GetStat(FloatStatType statType);
        
        public Hero(Vector3 posistion)
        {
            Posistion = posistion;
        }

        public enum FloatStatType
        {
            Health,
            Mana
        }
    }
}