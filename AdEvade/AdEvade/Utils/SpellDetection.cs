using EloBuddy;
using EloBuddy.SDK;
using AdEvade.Config;
using AdEvade.Data;

namespace AdEvade.Utils
{
    public static class SpellDetection
    {
        private static readonly AIHeroClient MyHero = ObjectManager.Player;
        public static bool IsMissileClient(this GameObject gameObject, out MissileClient missileClient)
        {
            missileClient = null;
            if (gameObject.GetType() != typeof(MissileClient) || !((MissileClient)gameObject).IsValidMissile())
                return false;
            missileClient = (MissileClient) gameObject;
            return true;
        }

        public static bool ShouldEvade(this EloBuddy.SpellData eloData, Obj_AI_Base hero, out Data.SpellData spellData)
        {
            spellData = null;
            return (hero.Team != MyHero.Team || (Config.Properties.GetData<bool>("DebugWithMySpells") && hero.IsMe))
                   && SpellDetector.OnProcessSpells.TryGetValue(eloData.Name, out spellData);
        }
        public static bool ShouldEvade(this MissileClient missile, out Data.SpellData spellData)
        {
            spellData = null;
            return (missile.SpellCaster.Team != MyHero.Team || //Evade if the spell if from the other team.
                    (Config.Properties.GetData<bool>("DebugWithMySpells") && missile.SpellCaster.IsMe) // Evade if the spell is from me, and it is enabled
                    ) && SpellDetector.OnMissileSpells.TryGetValue(missile.SData.Name, out spellData);

        }

        public static bool IsInRange(this MissileClient missile, Data.SpellData spellData)
        {
            return missile.StartPosition.Distance(MyHero.Position) <
                spellData.Range + Config.Properties.GetData<int>("ExtraDetectionRange");
        }
        public static bool IsValidEvadeSpell(this MissileClient missile, out Data.SpellData spellData)
        {
            spellData = null;
            if (missile == null) return false;
            //Check if spell is valid to continue
            if (missile.SpellCaster != null && missile.SData != null && missile.SData.Name != null)
            {
                return missile.ShouldEvade(out spellData);
            }
            return false;
        }
    }
}