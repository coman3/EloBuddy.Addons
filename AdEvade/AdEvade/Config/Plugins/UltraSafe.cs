using System.Collections.Generic;
using System.Drawing;
using EloBuddy;

namespace AdEvade.Config.Plugins
{
    [ConfigPlugin(Author = "coman3", Name = "Ultra Safe", Version = "1.0.0.0", RecomendedChampions = new[] { "All" },
        Description = "Use this config preset at high Elo's, it turns off many obvoius features such as FOW detection\n" +
                      "and circular evading. It also slows down reaction times and fast evade movement.\n\n" +
                      "Still use with caution as it can still be noticed, but has a very low chance. To decrease the\n" +
                      "chance of dectection increase the following values:\n" +
                      "     - Spell Activation Time (Humanizer)\n" +
                      "     - Spell Detection Time  (Humanizer)\n" +
                      "     - Extra Spell Radius  (Adv. Humanizer)\n" +
                      "     - Reaction Time   (Humanizer)\n" +
                      "     - Dodge Interval   (Humanizer)")]
    public class UltraSafe : ConfigPreset
    {
        public override void LoadConfig()
        {
            SetValue(ConfigValue.ExtraDetectionRange, 1000);
            SetValue(ConfigValue.ExtraLineWidth, 0);
            SetValue(ConfigValue.ExtraSpellRadius, 40); //ExtraAvoidDistance
            SetValue(ConfigValue.ExtraPingBuffer, 5);
            SetValue(ConfigValue.ExtraCpaDistance, 50);
            SetValue(ConfigValue.RejectMinDistance, 20);
            SetValue(ConfigValue.MinimumComfortZone, 150);
            SetValue(ConfigValue.ExtraEvadeDistance, 0);
            SetValue(ConfigValue.EvadeMode, EvadeMode.VerySmooth);
            SetValue(ConfigValue.FastEvadeActivationTime, 50);
            SetValue(ConfigValue.SpellActivationTime, 30);
            SetValue(ConfigValue.TickLimiter, 20);
            SetValue(ConfigValue.SpellDetectionTime, 40);
            SetValue(ConfigValue.ReactionTime, 50);
            SetValue(ConfigValue.DodgeInterval, 100);

            //Booleans
            SetValue(ConfigValue.DodgeSkillShots, true);
            SetValue(ConfigValue.DrawSkillShots, true);
            SetValue(ConfigValue.PreventDodgingNearEnemy, true);
            SetValue(ConfigValue.CalculateWindupDelay, true);
            SetValue(ConfigValue.HighPrecision, true);
            SetValue(ConfigValue.PreventDodgingUnderTower, true);
            SetValue(ConfigValue.ClickOnlyOnce, false);
            SetValue(ConfigValue.OnlyDodgeDangerous, true);
            SetValue(ConfigValue.DodgeDangerousKeysEnabled, false);
            SetValue(ConfigValue.DodgeFowSpells, false);
            SetValue(ConfigValue.DodgeCircularSpells, false);
            SetValue(ConfigValue.ActivateEvadeSpells, true);
            SetValue(ConfigValue.DodgeDangerousKey1, false);
            SetValue(ConfigValue.DodgeDangerousKey2, false);
            SetValue(ConfigValue.RecalculatePath, true);
            SetValue(ConfigValue.ContinueMovement, true);
            SetValue(ConfigValue.CheckSpellCollision, true);
            SetValue(ConfigValue.AdvancedSpellDetection, true);
            SetValue(ConfigValue.FastMovementBlock, true);
            SetValue(ConfigValue.EnableEvadeDistance, true);
            SetValue(ConfigValue.ShowDebugInfo, false);
            SetValue(ConfigValue.EnableSpellTester, false);
            SetValue(ConfigValue.DrawEvadeStatus, true);
            SetValue(ConfigValue.DrawSpellPosition, true);
            SetValue(ConfigValue.DrawEvadePosition, true);
            SetValue(ConfigValue.AutoSetPing, true);
        }
    }
}