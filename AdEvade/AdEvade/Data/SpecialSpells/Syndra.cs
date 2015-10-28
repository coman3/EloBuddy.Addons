using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AdEvade.Data.SpecialSpells
{
    class Syndra : IChampionPlugin
    {
        static Syndra()
        {

        }
        public const string ChampionName = "Syndra";
        public string GetChampionName()
        {
            return ChampionName;
        }
        public void LoadSpecialSpell(SpellData spellData)
        {

        }
    }
}
