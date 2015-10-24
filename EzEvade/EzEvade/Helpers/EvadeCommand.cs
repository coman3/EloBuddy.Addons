using EloBuddy;
using EloBuddy.SDK;
using EzEvade.EvadeSpells;
using EzEvade.Utils;
using SharpDX;

namespace EzEvade.Helpers
{
    public enum EvadeOrderCommand
    {
        None,
        MoveTo,
        Attack,
        CastSpell
    }

    public class EvadeCommand
    {
        private static AIHeroClient MyHero { get { return ObjectManager.Player; } }

        public EvadeOrderCommand Order;
        public Vector2 TargetPosition;
        public Obj_AI_Base Target;
        public float Timestamp;
        public bool IsProcessed;
        public EvadeSpellData EvadeSpellData;

        public EvadeCommand()
        {
            Timestamp = EvadeUtils.TickCount;
            IsProcessed = false;
        }

        public static void MoveTo(Vector2 movePos)
        {
            if (!Situation.ShouldDodge())
            {
                return;
            }

            AdEvade.LastEvadeCommand = new EvadeCommand
            {
                Order = EvadeOrderCommand.MoveTo,
                TargetPosition = movePos,
                Timestamp = EvadeUtils.TickCount,
                IsProcessed = false
            };

            AdEvade.LastMoveToPosition = movePos;
            AdEvade.LastMoveToServerPos = MyHero.ServerPosition.To2D();

            Player.IssueOrder(GameObjectOrder.MoveTo, movePos.To3D(), false);
        }

        public static void Attack(EvadeSpellData spellData, Obj_AI_Base target)
        {
            EvadeSpell.LastSpellEvadeCommand = new EvadeCommand
            {
                Order = EvadeOrderCommand.Attack,
                Target = target,
                EvadeSpellData = spellData,
                Timestamp = EvadeUtils.TickCount,
                IsProcessed = false
            };

            Player.IssueOrder(GameObjectOrder.AttackUnit, target, false);
        }

        public static void CastSpell(EvadeSpellData spellData, Obj_AI_Base target)
        {
            EvadeSpell.LastSpellEvadeCommand = new EvadeCommand
            {
                Order = EvadeOrderCommand.CastSpell,
                Target = target,
                EvadeSpellData = spellData,
                Timestamp = EvadeUtils.TickCount,
                IsProcessed = false
            };

            MyHero.Spellbook.CastSpell(spellData.SpellKey, target, false);
        }

        public static void CastSpell(EvadeSpellData spellData, Vector2 movePos)
        {
            EvadeSpell.LastSpellEvadeCommand = new EvadeCommand
            {
                Order = EvadeOrderCommand.CastSpell,
                TargetPosition = movePos,
                EvadeSpellData = spellData,
                Timestamp = EvadeUtils.TickCount,
                IsProcessed = false
            };

            MyHero.Spellbook.CastSpell(spellData.SpellKey, movePos.To3D(), false);
        }

        public static void CastSpell(EvadeSpellData spellData)
        {
            EvadeSpell.LastSpellEvadeCommand = new EvadeCommand
            {
                Order = EvadeOrderCommand.CastSpell,
                EvadeSpellData = spellData,
                Timestamp = EvadeUtils.TickCount,
                IsProcessed = false
            };

            MyHero.Spellbook.CastSpell(spellData.SpellKey,false);
        }
    }
}
