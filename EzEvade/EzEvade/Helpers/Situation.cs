﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EzEvade.Data;
using EzEvade.Utils;
using SharpDX;

namespace EzEvade.Helpers
{
    public static class Situation
    {
        private static AIHeroClient MyHero { get { return ObjectManager.Player; } }
        
        static Situation()
        {

        }

        public static bool IsNearEnemy(this Vector2 pos, float distance, bool alreadyNear = true)
        {
            if (Config.Config.GetData<bool>("PreventDodgingNearEnemy"))
            {
                var curDistToEnemies = GameData.HeroInfo.ServerPos2D.GetDistanceToChampions();
                var posDistToEnemies = pos.GetDistanceToChampions();
                
                if (curDistToEnemies < distance)
                {
                    if (curDistToEnemies > posDistToEnemies)
                    {
                        return true;
                    }
                }
                else
                {
                    if (posDistToEnemies < distance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
                
        public static bool IsUnderTurret(this Vector2 pos, bool checkEnemy = true)
        {
            if (!Config.Config.GetData<bool>("PreventDodgingUnderTower"))
            {
                return false;
            }

            var turretRange = 875 + GameData.HeroInfo.BoundingRadius;

            foreach (var entry in GameData.Turrets)
            {
                var turret = entry.Value;
                if (turret == null || !turret.IsValid || turret.IsDead)
                {
                    Core.DelayAction(() => GameData.Turrets.Remove(entry.Key), 1000);
                    continue;
                }

                if (checkEnemy && turret.IsAlly)
                {
                    continue;
                }

                var distToTurret = pos.Distance(turret.Position.To2D());
                if (distToTurret <= turretRange)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ShouldDodge()
        {
            if (Config.Config.Keys["DodgeSkillShots"].CurrentValue == false
                || CommonChecks()
                )
            {
                //has spellshield - sivir, noc, morgana
                //vlad pool
                //tryndamere r?
                //kayle ult buff?
                //hourglass
                //invulnerable
                //rooted
                //sion ult -> tenacity = 100?
                //stunned
                //elise e
                //zilean ulted
                //isdashing

                return false;
            }



            return true;
        }

        public static bool ShouldUseEvadeSpell()
        {
            if (Config.Config.Keys["ActivateEvadeSpells"].CurrentValue == false
                || CommonChecks()
                || AdEvade.LastWindupTime - EvadeUtils.TickCount > 0
                )
            {
                return false;
            }

            return true;
        }

        public static bool CommonChecks()
        {
            return

                AdEvade.IsChanneling
                || MyHero.IsDead
                || MyHero.IsInvulnerable
                || MyHero.IsTargetable == false
                || HasSpellShield(MyHero)
                || ChampionSpecificChecks()
                || Player.Instance.IsDashing()
                || AdEvade.HasGameEnded == true;
        }

        public static bool ChampionSpecificChecks()
        {
            return (MyHero.ChampionName == "Sion" && MyHero.HasBuff("SionR"))
                ;

            //Untargetable
            //|| (myHero.CharName == "KogMaw" && myHero.HasBuff("kogmawicathiansurprise"))
            //|| (myHero.CharName == "Karthus" && myHero.HasBuff("KarthusDeathDefiedBuff"))

            //Invulnerable
            //|| myHero.HasBuff("kalistarallyspelllock"); 
        }

        //from Evade by Esk0r
        public static bool HasSpellShield(AIHeroClient unit)
        {
            if (ObjectManager.Player.HasBuffOfType(BuffType.SpellShield))
            {
                return true;
            }

            if (ObjectManager.Player.HasBuffOfType(BuffType.SpellImmunity))
            {
                return true;
            }

            //TODO:
            ////Sivir E
            //if (unit.LastCastedSpellName() == "SivirE" && (EvadeUtils.TickCount - Evade.lastSpellCastTime) < 300)
            //{
            //    return true;
            //}

            ////Morganas E
            //if (unit.LastCastedSpellName() == "BlackShield" && (EvadeUtils.TickCount - Evade.lastSpellCastTime) < 300)
            //{
            //    return true;
            //}

            ////Nocturnes E
            //if (unit.LastCastedSpellName() == "NocturneShit" && (EvadeUtils.TickCount - Evade.lastSpellCastTime) < 300)
            //{
            //    return true;
            //}

            return false;
        }

    }
}