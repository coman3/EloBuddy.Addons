using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Spectate.Situation;
using EloBuddy;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace CameraBuddy.Spectate.Data
{
    public class FightData
    {
        public List<AIHeroClient> Enemys { get; private set; }
        public List<AIHeroClient> Allies { get; private set; }
        public bool MoreEnemies { get; private set; }
        public bool MoreAllies { get; set; }

        public float TotalEnemyHealth { get; private set; }
        public float TotalEnemyMana { get; private set; }
        public int TotalEnemyLevel { get; private set; }
        public float TotalEnemyDamage { get; private set; }

        public float TotalAllyHealth { get; private set; }
        public float TotalAllyMana { get; private set; }
        public int TotalAllyLevel { get; private set; }
        public float TotalAllyDamage { get; private set; }

        public float TotalHealthDifference { get; private set; }
        public float TotalManaDifference { get; private set; }
        public float TotalLevelDifference { get; private set; }
        public float TotalDamageDifference { get; private set; }

        public FightData(List<AIHeroClient> players)
        {
            Enemys = players.GetEnemys<AIHeroClient>();
            Allies = players.GetAllies<AIHeroClient>();

            MoreEnemies = players.CountAllies() < players.CountEnemies();
            MoreAllies = !MoreEnemies;

            TotalEnemyHealth = players.GetEnemys<AIHeroClient>().Sum(x => x.Health + x.AllShield);
            TotalEnemyMana = players.GetEnemys<AIHeroClient>().Sum(x => x.Mana);
            TotalEnemyLevel = players.GetEnemys<AIHeroClient>().Sum(x => x.Level);
            TotalEnemyDamage = players.GetEnemys<AIHeroClient>().Sum(x => x.TotalAttackDamage + x.TotalMagicalDamage);
            
            TotalAllyHealth = players.GetAllies<AIHeroClient>().Sum(x => x.Health + x.AllShield);
            TotalAllyMana = players.GetAllies<AIHeroClient>().Sum(x => x.Mana);
            TotalAllyLevel = players.GetAllies<AIHeroClient>().Sum(x => x.Level);
            TotalAllyDamage = players.GetAllies<AIHeroClient>().Sum(x => x.TotalAttackDamage + x.TotalMagicalDamage);

            TotalHealthDifference = TotalAllyHealth - TotalEnemyHealth;
            TotalManaDifference = TotalAllyMana - TotalEnemyMana;
            TotalLevelDifference = TotalAllyLevel - TotalEnemyLevel;
            TotalDamageDifference = TotalAllyDamage - TotalEnemyDamage;
        }
    }
}