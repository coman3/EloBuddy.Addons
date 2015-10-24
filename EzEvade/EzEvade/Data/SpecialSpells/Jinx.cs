using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace EzEvade.Data.SpecialSpells
{
    class Jinx : IChampionPlugin
    {
        static Jinx()
        {

        }
        public const string ChampionName = "Jinx";
        public string GetChampionName()
        {
            return ChampionName;
        }
        public void LoadSpecialSpell(SpellData spellData)
        {
            /*if (spellData.spellName == "JinxWMissile")
            {
                var hero = EntityManager.Heroes.Enemies.FirstOrDefault(h => h.CharName == "Jinx");
                if (hero != null)
                {
                    GameObject.OnCreate += (obj, args) => OnCreateObj_JinxWMissile(obj, args, hero, spellData);
                }
            }*/
        }

        private static void OnCreateObj_JinxWMissile(GameObject obj, EventArgs args, AIHeroClient hero, SpellData spellData)
        {
            if (hero != null && !hero.IsVisible
                && obj.IsEnemy && obj.Name.Contains("Jinx") && obj.Name.Contains("W_Cas"))
            {
                var pos1 = hero.Position;
                var dir = (obj.Position - ObjectManager.Player.Position).Normalized();
                var pos2 = pos1 + dir * 500;

                SpellDetector.CreateSpellData(hero, pos1, pos2, spellData, null, 0);
            }
        }
    }
}
