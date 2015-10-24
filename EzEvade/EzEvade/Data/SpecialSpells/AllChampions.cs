using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EloBuddy;
using EloBuddy.SDK;
using EzEvade;
using EzEvade.Data;
using EzEvade.Utils;
using SharpDX;

namespace EzEvade.Data.SpecialSpells
{

    class AllChampions : IChampionPlugin
    {
        public static Dictionary<string, bool> PDict = new Dictionary<string, bool>();

        static AllChampions()
        {

        }
        public const string ChampionName = "AllChampions";
        public string GetChampionName()
        {
            return ChampionName;
        }
        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.IsThreeWay && !PDict.ContainsKey("ProcessSpell_ProcessThreeWay"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ThreeWay;
                PDict["ProcessSpell_ProcessThreeWay"] = true;
            }    
        }            

        private static void ProcessSpell_ThreeWay(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.IsThreeWay)
            {
                Vector3 endPos2 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), spellData.Angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos2, spellData, null, 0, false);

                Vector3 endPos3 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), -spellData.Angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos3, spellData, null, 0, false);
            }
        }

    }
}
