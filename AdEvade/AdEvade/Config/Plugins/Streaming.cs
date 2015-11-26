using System;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AdEvade.Config.Plugins
{
    [ConfigPlugin(Name = "Streaming Mode", Author = "coman3",
        Description = "Turns off all drawings. But still dodges skill shots at a very\n" +
                      " slow speed, and detection time (as human as Ultra Safe)", Version = "1.0.0.0",
        RecomendedChampions = new[] {"All"})]
    public class Streaming : ConfigPreset
    {
        

        public override void LoadConfig()
        {
            SetValue(ConfigValue.ExtraDetectionRange, 1000);
            SetValue(ConfigValue.ExtraLineWidth, 0);
            SetValue(ConfigValue.ExtraSpellRadius, 40); //ExtraAvoidDistance
            SetValue(ConfigValue.ExtraPingBuffer, 5);
            SetValue(ConfigValue.ExtraCpaDistance, 50);
            SetValue(ConfigValue.RejectMinDistance, 20);
            SetValue(ConfigValue.MinimumComfortZone, 350);
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
            SetValue(ConfigValue.DrawSkillShots, false);
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
            SetValue(ConfigValue.DrawEvadeStatus, false);
            SetValue(ConfigValue.DrawSpellPosition, false);
            SetValue(ConfigValue.DrawEvadePosition, false);
            SetValue(ConfigValue.AutoSetPing, true);
        }
    }
}