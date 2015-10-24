using EloBuddy;

namespace EzEvade.EvadeSpells
{
    public delegate bool UseSpellFunc(EvadeSpellData evadeSpell, bool process = true);

    public enum CastType
    {
        Position,
        Target,
        Self,
    }

    public enum SpellTargets
    {
        AllyMinions,
        EnemyMinions,

        AllyChampions,
        EnemyChampions,

        Targetables,
    }

    public enum EvadeType
    {
        Blink,
        Dash,
        Invulnerability,
        MovementSpeedBuff,
        Shield,
        SpellShield,
        WindWall,
    }

    public class EvadeSpellData
    {
        public string CharName;
        public SpellSlot SpellKey = SpellSlot.Q;
        public int Dangerlevel = 1;
        public string SpellName;
        public string Name;
        public bool CheckSpellName = false;
        public float SpellDelay = 250;
        public float Range;
        public float Speed = 0;
        public bool FixedRange = false;
        public EvadeType EvadeType;
        public bool IsReversed = false;
        public bool BehindTarget = false;
        public bool InfrontTarget = false;
        public bool IsSummonerSpell = false;
        public bool IsItem = false;
        public ItemId ItemId = 0;
        public CastType CastType = CastType.Position;
        public SpellTargets[] SpellTargets = { };
        public UseSpellFunc UseSpellFunc = null;
        public bool IsSpecial = false;

        public EvadeSpellData()
        {

        }

        public EvadeSpellData(
            string charName,
            string name,
            SpellSlot spellKey,
            EvadeType evadeType,
            int dangerlevel
            )
        {
            CharName = charName;
            Name = name;
            SpellKey = spellKey;
            EvadeType = evadeType;
            Dangerlevel = dangerlevel;
        }
    }
}
