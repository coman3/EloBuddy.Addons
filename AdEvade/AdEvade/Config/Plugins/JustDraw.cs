using System.Collections.Generic;

namespace AdEvade.Config.Plugins
{
    [ConfigPlugin(Author = "coman3", Name = "Only Draw", Version = "1.0.0.0", RecomendedChampions = new[] { "All" },
        Description = "Disables all dodging and only draws spells. Turn on Draw evade posistion if you would like a\n sujestion on where to move to.")]
    public class JustDraw : ConfigPreset
    {
        public override void LoadConfig()
        {
            SetValue(ConfigValue.ExtraDetectionRange, 1500);
            SetValue(ConfigValue.ExtraLineWidth, 1);
            SetValue(ConfigValue.ExtraSpellRadius, 0); //ExtraAvoidDistance
            SetValue(ConfigValue.ExtraPingBuffer, 0);
            SetValue(ConfigValue.ExtraCpaDistance, 0);
            SetValue(ConfigValue.RejectMinDistance, 0);
            SetValue(ConfigValue.MinimumComfortZone, 0);
            SetValue(ConfigValue.ExtraEvadeDistance, 0);
            SetValue(ConfigValue.EvadeMode, EvadeMode.LowBan);
            SetValue(ConfigValue.FastEvadeActivationTime, 0);
            SetValue(ConfigValue.SpellActivationTime, 0);
            SetValue(ConfigValue.TickLimiter, 0);
            SetValue(ConfigValue.SpellDetectionTime, 0);

            //Booleans
            SetValue(ConfigValue.DodgeSkillShots, false);
            SetValue(ConfigValue.DrawSkillShots, true);
            SetValue(ConfigValue.PreventDodgingNearEnemy, true);
            SetValue(ConfigValue.CalculateWindupDelay, false);
            SetValue(ConfigValue.HighPrecision, false);
            SetValue(ConfigValue.PreventDodgingUnderTower, true);
            SetValue(ConfigValue.ClickOnlyOnce, false);
            SetValue(ConfigValue.OnlyDodgeDangerous, true);
            SetValue(ConfigValue.DodgeDangerousKeysEnabled, false);
            SetValue(ConfigValue.DodgeFowSpells, false);
            SetValue(ConfigValue.DodgeCircularSpells, false);
            SetValue(ConfigValue.ActivateEvadeSpells, false);
            SetValue(ConfigValue.DodgeDangerousKey1, false);
            SetValue(ConfigValue.DodgeDangerousKey2, false);
            SetValue(ConfigValue.RecalculatePath, false);
            SetValue(ConfigValue.ContinueMovement, false);
            SetValue(ConfigValue.CheckSpellCollision, false);
            SetValue(ConfigValue.AdvancedSpellDetection, false);
            SetValue(ConfigValue.FastMovementBlock, false);
            SetValue(ConfigValue.EnableEvadeDistance, false);
            SetValue(ConfigValue.ShowDebugInfo, false);
            SetValue(ConfigValue.EnableSpellTester, false);
            SetValue(ConfigValue.DrawEvadeStatus, false);
            SetValue(ConfigValue.DrawSpellPosition, true);
            SetValue(ConfigValue.DrawEvadePosition, false);
            SetValue(ConfigValue.AutoSetPing, false);
        }
    }
}