using System.Collections.Generic;

namespace AdEvade.Config.Plugins
{
    [ConfigPlugin(Author = "coman3", Name = "Default", Version = "1.0.0.0", RecomendedChampions = new[] { "All" },
        Description = "The default config for AdEvade, Use with caution at high Elo's as it is clear that you are using an\n evade script.")]
    public class Default : ConfigPreset
    {
        public override void LoadConfig()
        {
            SetValue(ConfigValue.ExtraDetectionRange, 500);
            SetValue(ConfigValue.ExtraLineWidth, 1);
            SetValue(ConfigValue.ExtraSpellRadius, 20); //ExtraAvoidDistance
            SetValue(ConfigValue.ExtraPingBuffer, 0);
            SetValue(ConfigValue.ExtraCpaDistance, 40);
            SetValue(ConfigValue.RejectMinDistance, 10);
            SetValue(ConfigValue.MinimumComfortZone, 50);
            SetValue(ConfigValue.ExtraEvadeDistance, 0);
            SetValue(ConfigValue.EvadeMode, EvadeMode.Smooth);
            SetValue(ConfigValue.FastEvadeActivationTime, 0);
            SetValue(ConfigValue.SpellActivationTime, 10);
            SetValue(ConfigValue.TickLimiter, 0);
            SetValue(ConfigValue.SpellDetectionTime, 10);

            //Booleans
            SetValue(ConfigValue.DodgeSkillShots, true);
            SetValue(ConfigValue.DrawSkillShots, true);
            SetValue(ConfigValue.PreventDodgingNearEnemy, true);
            SetValue(ConfigValue.CalculateWindupDelay, true);
            SetValue(ConfigValue.HighPrecision, true);
            SetValue(ConfigValue.PreventDodgingUnderTower, false);
            SetValue(ConfigValue.ClickOnlyOnce, true);
            SetValue(ConfigValue.OnlyDodgeDangerous, false);
            SetValue(ConfigValue.DodgeDangerousKeysEnabled, false);
            SetValue(ConfigValue.DodgeFowSpells, true);
            SetValue(ConfigValue.DodgeCircularSpells, true);
            SetValue(ConfigValue.ActivateEvadeSpells, true);
            SetValue(ConfigValue.DodgeDangerousKey1, false);
            SetValue(ConfigValue.DodgeDangerousKey2, false);
            SetValue(ConfigValue.RecalculatePath, true);
            SetValue(ConfigValue.ContinueMovement, true);
            SetValue(ConfigValue.CheckSpellCollision, true);
            SetValue(ConfigValue.AdvancedSpellDetection, true);
            SetValue(ConfigValue.FastMovementBlock, true);
            SetValue(ConfigValue.EnableEvadeDistance, false);
            SetValue(ConfigValue.ShowDebugInfo, false);
            SetValue(ConfigValue.EnableSpellTester, false);
            SetValue(ConfigValue.DrawEvadeStatus, false);
            SetValue(ConfigValue.DrawSpellPosition, true);
            SetValue(ConfigValue.DrawEvadePosition, true);
            SetValue(ConfigValue.AutoSetPing, false);
        }
    }
}