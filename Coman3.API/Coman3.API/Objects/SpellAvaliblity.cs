using System;
using EloBuddy;

namespace Coman3.API.Objects
{
    /// <summary>
    /// Tracks the cooldowns of <see cref="AIHeroClient"/> spells.
    /// </summary>
    public class SpellAvaliblity
    {
        #region Spell: Q
        public bool Q
        {
            get { return QSpell.IsOnCooldown; }
        }

        public SpellDataInst QSpell
        {
            get { return Hero.Spellbook.GetSpell(SpellSlot.Q); }
        }

        public float QResetTime
        {
            get { return QSpell.CooldownExpires; }
        }

        public float QTimeLeft
        {
            get { return QResetTime - Game.Time; }
        }

        public float QCooldownPercent
        {
            get
            {
                return (QTimeLeft > 0 && Math.Abs(QSpell.Cooldown) > float.Epsilon)
                    ? 1f - (QTimeLeft / QSpell.Cooldown)
                    : 1f;
            }
        }
        #endregion

        #region Spell: W
        public bool W
        {
            get { return WSpell.IsOnCooldown; }
        }

        public SpellDataInst WSpell
        {
            get { return Hero.Spellbook.GetSpell(SpellSlot.W); }
        }

        public float WResetTime
        {
            get { return WSpell.CooldownExpires; }
        }

        public float WTimeLeft
        {
            get { return WResetTime - Game.Time; }
        }

        public float WCooldownPercent
        {
            get
            {
                return (WTimeLeft > 0 && Math.Abs(WSpell.Cooldown) > float.Epsilon)
                    ? 1f - (WTimeLeft / WSpell.Cooldown)
                    : 1f;
            }
        }
        #endregion

        #region Spell: E
        public bool E
        {
            get { return ESpell.IsOnCooldown; }
        }

        public SpellDataInst ESpell
        {
            get { return Hero.Spellbook.GetSpell(SpellSlot.E); }
        }

        public float EResetTime
        {
            get { return ESpell.CooldownExpires; }
        }

        public float ETimeLeft
        {
            get { return EResetTime - Game.Time; }
        }

        public float ECooldownPercent
        {
            get
            {
                return (ETimeLeft > 0 && Math.Abs(ESpell.Cooldown) > float.Epsilon)
                    ? 1f - (ETimeLeft / ESpell.Cooldown)
                    : 1f;
            }
        }
        #endregion

        #region Spell: R
        public bool R
        {
            get { return RSpell.IsOnCooldown; }
        }

        public SpellDataInst RSpell
        {
            get { return Hero.Spellbook.GetSpell(SpellSlot.R); }
        }

        public float RResetTime
        {
            get { return RSpell.CooldownExpires; }
        }

        public float RTimeLeft
        {
            get { return RResetTime - Game.Time; }
        }

        public float RCooldownPercent
        {
            get
            {
                return (RTimeLeft > 0 && Math.Abs(RSpell.Cooldown) > float.Epsilon)
                    ? 1f - (RTimeLeft / RSpell.Cooldown)
                    : 1f;
            }
        }
        #endregion

        #region Spell: Summoner1
        public bool Summoner1
        {
            get { return Summoner1Spell.IsOnCooldown; }
        }

        public SpellDataInst Summoner1Spell
        {
            get { return Hero.Spellbook.GetSpell(SpellSlot.Summoner1); }
        }

        public float Summoner1ResetTime
        {
            get { return Summoner1Spell.CooldownExpires; }
        }

        public float Summoner1TimeLeft
        {
            get { return Summoner1ResetTime - Game.Time; }
        }

        public float Summoner1CooldownPercent
        {
            get
            {
                return (Summoner1TimeLeft > 0 && Math.Abs(Summoner1Spell.Cooldown) > float.Epsilon)
                    ? 1f - (Summoner1TimeLeft / Summoner1Spell.Cooldown)
                    : 1f;
            }
        }
        #endregion

        #region Spell: Summoner2
        public bool Summoner2
        {
            get { return Summoner2Spell.IsOnCooldown; }
        }

        public SpellDataInst Summoner2Spell
        {
            get { return Hero.Spellbook.GetSpell(SpellSlot.Summoner2); }
        }

        public float Summoner2ResetTime
        {
            get { return Summoner2Spell.CooldownExpires; }
        }

        public float Summoner2TimeLeft
        {
            get { return Summoner2ResetTime - Game.Time; }
        }

        public float Summoner2CooldownPercent
        {
            get
            {
                return (Summoner2TimeLeft > 0 && Math.Abs(Summoner2Spell.Cooldown) > float.Epsilon)
                    ? 1f - (Summoner2TimeLeft / Summoner2Spell.Cooldown)
                    : 1f;
            }
        }
        #endregion

        public AIHeroClient Hero { get; private set; }

        /// <summary>
        /// Constructs a <see cref="SpellAvaliblity"/> object using the specified <see cref="AIHeroClient"/>
        /// </summary>
        /// <param name="hero">The <see cref="AIHeroClient"/>'s cooldowns to track</param>
        public SpellAvaliblity(AIHeroClient hero)
        {
            Hero = hero;
        }
    }
}