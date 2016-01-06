using System.Collections.Generic;
using EloBuddy;

namespace AdEvade.Data.Spells
{
    public static class SpellDatabase
    {
        public static List<SpellData> Spells = new List<SpellData>();

        static SpellDatabase()
        {
            #region Properties.Constants.AllChampions

            Spells.Add(
                new SpellData
                {
                    CharName = Config.Constants.AllChampions,
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "SummonerSnowball",
                    Name = "Poro Throw",
                    ProjectileSpeed = 1300,
                    Radius = 60,
                    Range = 1600,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.Q,
                    SpellName = "summonersnowball",
                    ExtraSpellNames = new[] {"summonerporothrow",},
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions},

                });

            #endregion Properties.Constants.AllChampions

            #region Aatrox

            Spells.Add(
                new SpellData
                {
                    CharName = "Aatrox",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "AatroxQ",
                    ProjectileSpeed = 2000,
                    Radius = 285,
                    Range = 650,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "AatroxQ",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Aatrox",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "AatroxEConeMissile",
                    Name = "Blade of Torment",
                    ProjectileSpeed = 1250,
                    Radius = 100,
                    Range = 1075,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "AatroxE",
                    SpellType = SpellType.Line,

                });

            #endregion Aatrox

            #region Ahri

            Spells.Add(
                new SpellData
                {
                    CharName = "Ahri",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "AhriOrbMissile",
                    Name = "Orb of Deception",
                    ProjectileSpeed = 1750,
                    Radius = 100,
                    Range = 925,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "AhriOrbofDeception",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ahri",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "AhriSeduceMissile",
                    Name = "Charm",
                    ProjectileSpeed = 1550,
                    Radius = 60,
                    Range = 1000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "AhriSeduce",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ahri",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "AhriOrbReturn",
                    Name = "Orb of Deception Back",
                    ProjectileSpeed = 915,
                    Radius = 100,
                    Range = 925,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "AhriOrbReturn",
                    SpellType = SpellType.Line,

                });

            #endregion Ahri

            #region Alistar

            Spells.Add(
                new SpellData
                {
                    CharName = "Alistar",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Pulverize",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 365,
                    Range = 365,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.Q,
                    SpellName = "Pulverize",
                    SpellType = SpellType.Circular,

                });

            #endregion Alistar

            #region Amumu

            Spells.Add(
                new SpellData
                {
                    CharName = "Amumu",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "CurseoftheSadMummy",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 560,
                    Range = 0,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "CurseoftheSadMummy",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Amumu",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "SadMummyBandageToss",
                    Name = "Bandage Toss",
                    ProjectileSpeed = 2000,
                    Radius = 80,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "BandageToss",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Amumu

            #region Anivia

            Spells.Add(
                new SpellData
                {
                    CharName = "Anivia",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "FlashFrostSpell",
                    Name = "Flash Frost",
                    ProjectileSpeed = 850,
                    Radius = 110,
                    Range = 1250,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "FlashFrostSpell",
                    SpellType = SpellType.Line,

                });

            #endregion Anivia

            #region Annie

            Spells.Add(
                new SpellData
                {
                    Angle = 25,
                    CharName = "Annie",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Incinerate",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 80,
                    Range = 625,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "Incinerate",
                    SpellType = SpellType.Cone,

                });

            //RIP tibbers flash dodge
            /*
            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                Dangerlevel = SpellDangerLevel.Extreme,
                name = "InfernalGuardian",
                radius = 290,
                range = 600,
                spellDelay = 250,
                SpellKey = SpellSlot.R,
                spellName = "InfernalGuardian",
                spellType = SpellType.Circular,

            });*/

            #endregion Annie

            #region Ashe

            Spells.Add(
                new SpellData
                {
                    CharName = "Ashe",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Enchanted Arrow",
                    ProjectileSpeed = 1600,
                    Radius = 130,
                    Range = 25000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "EnchantedCrystalArrow",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions},
                });

            Spells.Add(
                new SpellData
                {
                    Angle = 5,
                    CharName = "Ashe",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "VolleyAttack",
                    Name = "Volley",
                    ProjectileSpeed = 1500,
                    Radius = 20,
                    Range = 1200,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "Volley",
                    SpellType = SpellType.Line,
                    IsSpecial = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},
                });

            #endregion Ashe

            #region Azir

            Spells.Add(
                new SpellData
                {
                    CharName = "Azir",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "AzirQ",
                    ProjectileSpeed = 1000,
                    Radius = 80,
                    Range = 800,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "AzirQ",
                    SpellType = SpellType.Line,
                    UsePackets = true,
                    IsSpecial = true,

                });

            #endregion Azir

            #region Bard

            Spells.Add(
                new SpellData
                {
                    CharName = "Bard",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "BardQ",
                    MissileName = "BardQMissile",
                    ProjectileSpeed = 1600,
                    Radius = 60,
                    Range = 950,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "BardQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Bard",
                    Dangerlevel = SpellDangerLevel.High,
                    SpellName = "BardR",
                    MissileName = "BardR",
                    ProjectileSpeed = 2100,
                    Radius = 350,
                    Range = 3400,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellType = SpellType.Circular,

                });

            #endregion Bard

            #region Blitzcrank

            Spells.Add(
                new SpellData
                {
                    CharName = "Blitzcrank",
                    Dangerlevel = SpellDangerLevel.High,
                    ExtraDelay = 75,
                    MissileName = "RocketGrabMissile",
                    Name = "Rocket Grab",
                    ProjectileSpeed = 1800,
                    Radius = 70,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "RocketGrab",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Blitzcrank",
                    Dangerlevel = SpellDangerLevel.High,
                    ExtraDelay = 75,
                    Name = "StaticField",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 600,
                    Range = 0,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "StaticField",
                    SpellType = SpellType.Circular,

                });

            #endregion Blitzcrank

            #region Brand

            Spells.Add(
                new SpellData
                {
                    CharName = "Brand",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "BrandBlazeMissile",
                    Name = "BrandBlaze",
                    ProjectileSpeed = 1600,
                    Radius = 60,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "BrandBlaze",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Brand",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Pillar of Flame",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 250,
                    Range = 1100,
                    SpellDelay = 850,
                    SpellKey = SpellSlot.W,
                    SpellName = "BrandFissure",
                    SpellType = SpellType.Circular,

                });

            #endregion Brand

            #region Braum

            Spells.Add(
                new SpellData
                {
                    CharName = "Braum",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "braumrmissile",
                    Name = "GlacialFissure",
                    ProjectileSpeed = 1400,
                    Radius = 115,
                    Range = 1250,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "BraumRWrapper",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Braum",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "BraumQMissile",
                    Name = "BraumQ",
                    ProjectileSpeed = 1700,
                    //spellDelay = 30000,
                    SpellDelay = 250,
                    Radius = 100,
                    Range = 1025,
                    SpellKey = SpellSlot.Q,
                    SpellName = "BraumQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Braum

            #region Caitlyn

            Spells.Add(
                new SpellData
                {
                    CharName = "Caitlyn",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Piltover Peacemaker",
                    ProjectileSpeed = 2200,
                    Radius = 90,
                    Range = 1300,
                    SpellDelay = 625,
                    SpellKey = SpellSlot.Q,
                    SpellName = "CaitlynPiltoverPeacemaker",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Caitlyn",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "CaitlynEntrapmentMissile",
                    Name = "Caitlyn Entrapment",
                    ProjectileSpeed = 2000,
                    Radius = 80,
                    Range = 950,
                    SpellDelay = 125,
                    SpellKey = SpellSlot.E,
                    SpellName = "CaitlynEntrapment",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Caitlyn

            #region Cassiopeia

            Spells.Add(
                new SpellData
                {
                    Angle = 40,
                    CharName = "Cassiopeia",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "CassiopeiaPetrifyingGaze",
                    Name = "CassiopeiaPetrifyingGaze",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 80,
                    Range = 825,
                    SpellDelay = 600,
                    SpellKey = SpellSlot.R,
                    SpellName = "CassiopeiaPetrifyingGaze",
                    SpellType = SpellType.Cone,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Cassiopeia",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "CassiopeiaNoxiousBlast",
                    Name = "CassiopeiaNoxiousBlast",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 200,
                    Range = 850,
                    SpellDelay = 750,
                    SpellKey = SpellSlot.Q,
                    SpellName = "CassiopeiaNoxiousBlast",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Cassiopeia",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "CassiopeiaMiasma",
                    Radius = 220,
                    Range = 850,
                    SpellDelay = 250,
                    ProjectileSpeed = 2500,
                    SpellKey = SpellSlot.W,
                    SpellName = "CassiopeiaMiasma",
                    SpellType = SpellType.Circular,

                });

            #endregion Cassiopeia

            #region Chogath

            Spells.Add(
                new SpellData
                {
                    Angle = 30,
                    CharName = "Chogath",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "FeralScream",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 20,
                    Range = 650,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "FeralScream",
                    SpellType = SpellType.Cone,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Chogath",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Rupture",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 250,
                    Range = 950,
                    SpellDelay = 1200,
                    SpellKey = SpellSlot.Q,
                    SpellName = "Rupture",
                    SpellType = SpellType.Circular,
                    ExtraDrawHeight = 45,
                });

            #endregion Chogath

            #region Corki

            Spells.Add(
                new SpellData
                {
                    CharName = "Corki",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "MissileBarrageMissile2",
                    Name = "Missile Barrage big",
                    ProjectileSpeed = 2000,
                    Radius = 40,
                    Range = 1500,
                    SpellDelay = 175,
                    SpellKey = SpellSlot.R,
                    SpellName = "MissileBarrage2",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Corki",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "PhosphorusBombMissile",
                    Name = "Phosphorus Bomb",
                    ProjectileSpeed = 1000,
                    Radius = 270,
                    Range = 825,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "PhosphorusBomb",
                    SpellType = SpellType.Circular,
                    ExtraDrawHeight = 110,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Corki",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "MissileBarrageMissile",
                    Name = "Missile Barrage",
                    ProjectileSpeed = 2000,
                    Radius = 40,
                    Range = 1300,
                    SpellDelay = 175,
                    SpellKey = SpellSlot.R,
                    SpellName = "MissileBarrage",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Corki

            #region Darius

            Spells.Add(
                new SpellData
                {
                    Angle = 25,
                    CharName = "Darius",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "DariusAxeGrabCone",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 20,
                    Range = 570,
                    SpellDelay = 320,
                    SpellKey = SpellSlot.E,
                    SpellName = "DariusAxeGrabCone",
                    SpellType = SpellType.Cone,

                });

            #endregion Darius

            #region Diana

            Spells.Add(
                new SpellData
                {
                    CharName = "Diana",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "DianaArc",
                    ProjectileSpeed = 1400,
                    Radius = 50,
                    Range = 850,
                    FixedRange = true,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "DianaArc",
                    SpellType = SpellType.Arc,
                    HasEndExplosion = true,
                    SecondaryRadius = 195,
                    ExtraEndTime = 250,
                });

            #endregion Diana

            #region DrMundo

            Spells.Add(
                new SpellData
                {
                    CharName = "DrMundo",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "InfectedCleaverMissile",
                    Name = "Infected Cleaver",
                    ProjectileSpeed = 2000,
                    Radius = 60,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "InfectedCleaverMissileCast",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion DrMundo

            #region Draven

            Spells.Add(
                new SpellData
                {
                    CharName = "Draven",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "DravenR",
                    Name = "DravenR",
                    ProjectileSpeed = 2000,
                    Radius = 160,
                    Range = 25000,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "DravenRCast",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Draven",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "DravenDoubleShotMissile",
                    Name = "Stand Aside",
                    ProjectileSpeed = 1400,
                    Radius = 130,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "DravenDoubleShot",
                    SpellType = SpellType.Line,

                });

            #endregion Draven

            #region Ekko

            Spells.Add(
                new SpellData
                {
                    CharName = "Ekko",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "EkkoQ",
                    MissileName = "ekkoqmis",
                    ProjectileSpeed = 1650,
                    Radius = 60,
                    Range = 950,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "EkkoQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions},
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ekko",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "EkkoW",
                    Radius = 375,
                    Range = 1600,
                    SpellDelay = 3750,
                    SpellKey = SpellSlot.W,
                    SpellName = "EkkoW",
                    SpellType = SpellType.Circular,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ekko",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "EkkoR",
                    Radius = 375,
                    Range = 1600,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "EkkoR",
                    SpellType = SpellType.Circular,
                    IsSpecial = true,
                });

            #endregion Ekko

            #region Elise

            Spells.Add(
                new SpellData
                {
                    CharName = "Elise",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Cocoon",
                    ProjectileSpeed = 1600,
                    Radius = 70,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "EliseHumanE",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Elise

            #region Evelynn

            Spells.Add(
                new SpellData
                {
                    CharName = "Evelynn",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "EvelynnR",
                    Radius = 350,
                    Range = 650,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "EvelynnR",
                    SpellType = SpellType.Circular,

                });

            #endregion Evelynn

            #region Ezreal

            Spells.Add(
                new SpellData
                {
                    CharName = "Ezreal",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "EzrealMysticShotMissile",
                    Name = "Mystic Shot",
                    ProjectileSpeed = 2000,
                    Radius = 60,
                    Range = 1200,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "EzrealMysticShot",
                    ExtraSpellNames = new[] {"ezrealmysticshotwrapper",},
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ezreal",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Trueshot Barrage",
                    ProjectileSpeed = 2000,
                    Radius = 160,
                    Range = 20000,
                    SpellDelay = 1000,
                    SpellKey = SpellSlot.R,
                    SpellName = "EzrealTrueshotBarrage",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ezreal",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "EzrealEssenceFluxMissile",
                    Name = "Essence Flux",
                    ProjectileSpeed = 1600,
                    Radius = 80,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "EzrealEssenceFlux",
                    SpellType = SpellType.Line,

                });

            //Testing purpose
            /*Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                Dangerlevel = SpellDangerLevel.Low,
                name = "Essence Flux2",
                radius = 250,
                range = 1050,
                spellDelay = 825,
                SpellKey = SpellSlot.W,
                spellName = "EzrealEssenceFlux",
                spellType = SpellType.Circular,

            });*/

            #endregion Ezreal

            #region Fiora

            Spells.Add(
                new SpellData
                {
                    CharName = "Fiora",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "FioraWMissile",
                    Name = "FioraW",
                    ProjectileSpeed = 3200,
                    Radius = 70,
                    Range = 750,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.W,
                    SpellName = "FioraW",
                    SpellType = SpellType.Line,

                });

            #endregion Fiora

            #region Fizz

            Spells.Add(
                new SpellData
                {
                    CharName = "Fizz",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "FizzPiercingStrike",
                    ProjectileSpeed = 1400,
                    Radius = 150,
                    Range = 550,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.E,
                    SpellName = "FizzPiercingStrike",
                    SpellType = SpellType.Line,
                    IsSpecial = true,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Fizz",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "FizzMarinerDoomMissile",
                    Name = "Fizz ULT",
                    ProjectileSpeed = 1350,
                    Radius = 120,
                    Range = 1275,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "FizzMarinerDoom",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions,},
                    //hasEndExplosion = true,
                    SecondaryRadius = 250,
                    UseEndPosition = true,
                    //extraEndTime = 1000,

                });

            #endregion Fizz

            #region Galio

            Spells.Add(
                new SpellData
                {
                    CharName = "Galio",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "GalioRighteousGust",
                    ProjectileSpeed = 1300,
                    Radius = 160,
                    Range = 1280,
                    SpellKey = SpellSlot.E,
                    SpellName = "GalioRighteousGust",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Galio",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "GalioResoluteSmite",
                    ProjectileSpeed = 1200,
                    Radius = 235,
                    Range = 1040,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "GalioResoluteSmite",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Galio",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "GalioIdolOfDurand",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 600,
                    Range = 0,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "GalioIdolOfDurand",
                    SpellType = SpellType.Circular,

                });

            #endregion Galio

            #region Gnar

            Spells.Add(
                new SpellData
                {
                    CharName = "Gnar",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Boulder Toss",
                    ProjectileSpeed = 2000,
                    Radius = 90,
                    Range = 1150,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "gnarbigq",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Gnar",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "GnarUlt",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 500,
                    Range = 0,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "GnarR",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Gnar",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Wallop",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 100,
                    Range = 600,
                    SpellDelay = 600,
                    SpellKey = SpellSlot.W,
                    SpellName = "gnarbigw",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Gnar",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Boomerang Throw",
                    ProjectileSpeed = 2400,
                    Radius = 60,
                    Range = 1185,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "GnarQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            /*Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                Dangerlevel = SpellDangerLevel.Normal,
                name = "GnarE",
                spellName = "GnarE",
                range = 475,
                spellDelay = 0,
                radius = 150,
                fixedRange = true,
                projectileSpeed = 900,
                SpellKey = SpellSlot.E,
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                Dangerlevel = SpellDangerLevel.Normal,
                name = "GnarBigE",
                spellName = "gnarbige",
                range = 475,
                spellDelay = 0,
                radius = 100,
                fixedRange = true,
                projectileSpeed = 800,
                SpellKey = SpellSlot.E,
                spellType = SpellType.Circular,
            });*/

            #endregion Gnar

            #region Gragas

            Spells.Add(
                new SpellData
                {
                    CharName = "Gragas",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Barrel Roll",
                    ProjectileSpeed = 1000,
                    Radius = 250,
                    Range = 975,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "GragasQ",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Gragas",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "GragasE",
                    ProjectileSpeed = 1200,
                    Radius = 200,
                    Range = 950,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.E,
                    SpellName = "GragasE",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Gragas",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "GragasExplosiveCask",
                    ProjectileSpeed = 1750,
                    Radius = 350,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "GragasR",
                    SpellType = SpellType.Circular,

                });

            #endregion Gragas

            #region Graves

            Spells.Add(
                new SpellData
                {
                    Angle = 18,
                    CharName = "Graves",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "GravesQLineMis",
                    Name = "Buckshot",
                    ProjectileSpeed = 3000,
                    Radius = 20,
                    Range = 1025,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "GravesQLineSpell",
                    SpellType = SpellType.Cone,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Graves",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "GravesChargeShotShot",
                    Name = "Collateral Damage",
                    ProjectileSpeed = 2100,
                    Radius = 100,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "GravesChargeShot",
                    SpellType = SpellType.Line,
                    HasEndExplosion = true,
                    SecondaryRadius = 300,

                });

            #endregion Graves

            #region Hecarim

            Spells.Add(
                new SpellData
                {
                    CharName = "Hecarim",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "HecarimUlt",
                    ProjectileSpeed = 1100,
                    Radius = 300,
                    Range = 1500,
                    SpellDelay = 10,
                    SpellKey = SpellSlot.R,
                    SpellName = "HecarimUlt",
                    SpellType = SpellType.Circular,

                });

            #endregion Hecarim

            #region Heimerdinger

            Spells.Add(
                new SpellData
                {
                    CharName = "Heimerdinger",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "HeimerdingerESpell",
                    Name = "HeimerdingerE",
                    ProjectileSpeed = 1750,
                    Radius = 135,
                    Range = 925,
                    SpellDelay = 325,
                    SpellKey = SpellSlot.E,
                    SpellName = "HeimerdingerE",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Heimerdinger",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "HeimerdingerWAttack2",
                    Name = "HeimerdingerW",
                    ProjectileSpeed = 1800,
                    Radius = 70,
                    Range = 1500,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "Heimerdingerwm",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Heimerdinger

            #region Irelia

            Spells.Add(
                new SpellData
                {
                    CharName = "Irelia",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "ireliatranscendentbladesspell",
                    Name = "IreliaTranscendentBlades",
                    ProjectileSpeed = 1600,
                    Radius = 120,
                    Range = 1200,
                    SpellKey = SpellSlot.R,
                    SpellDelay = 0,
                    SpellName = "IreliaTranscendentBlades",
                    SpellType = SpellType.Line,
                    UsePackets = true,
                    DefaultOff = true,
                });

            #endregion Irelia

            #region Illaoi

            Spells.Add(
                new SpellData
                {
                    CharName = "Illaoi",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "illaoiemis",
                    Name = "IllaoiQ",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 100,
                    Range = 850,
                    SpellDelay = 750,
                    SpellKey = SpellSlot.Q,
                    SpellName = "IllaoiQ",
                    SpellType = SpellType.Line,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Illaoi",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "IllaoiE",
                    Name = "IllaoiE",
                    ProjectileSpeed = 1900,
                    Radius = 75,
                    Range = 950,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.E,
                    SpellName = "IllaoiE",
                    SpellType = SpellType.Line,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Illaoi",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "",
                    Name = "IllaoiR",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 450,
                    Range = 0,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "IllaoiR",
                    SpellType = SpellType.Circular,
                });

            #endregion Illaoi

            #region Janna

            Spells.Add(
                new SpellData
                {
                    CharName = "Janna",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "HowlingGaleSpell",
                    Name = "HowlingGaleSpell",
                    ProjectileSpeed = 900,
                    Radius = 120,
                    Range = 1700,
                    SpellKey = SpellSlot.Q,
                    SpellName = "JannaQ",
                    SpellType = SpellType.Line,
                    UsePackets = true,

                });

            #endregion Janna

            #region JarvanIV

            Spells.Add(
                new SpellData
                {
                    CharName = "JarvanIV",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "JarvanIVDragonStrike",
                    ProjectileSpeed = 2000,
                    Radius = 80,
                    Range = 845,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "JarvanIVDragonStrike",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "JarvanIV",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "JarvanIVEQ",
                    ProjectileSpeed = 1450,
                    Radius = 120,
                    Range = 845,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "JarvanIVEQ",
                    SpellType = SpellType.Line,
                    UseEndPosition = true,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "JarvanIV",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "JarvanIVDemacianStandard",
                    Name = "JarvanIVDemacianStandard",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 175,
                    Range = 860,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.E,
                    SpellName = "JarvanIVDemacianStandard",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "JarvanIV",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "JarvanIVCataclysm",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 350,
                    Range = 825,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.R,
                    SpellName = "JarvanIVCataclysm",
                    SpellType = SpellType.Circular,

                });

            #endregion JarvanIV

            #region Jayce

            Spells.Add(
                new SpellData
                {
                    CharName = "Jayce",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "JayceShockBlastWallMis",
                    Name = "JayceShockBlastCharged",
                    ProjectileSpeed = 2350,
                    Radius = 70,
                    Range = 1170,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "JayceShockBlastWall",
                    SpellType = SpellType.Line,
                    UsePackets = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},
                    HasEndExplosion = true,
                    SecondaryRadius = 250,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Jayce",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "JayceShockBlastMis",
                    Name = "JayceShockBlast",
                    ProjectileSpeed = 1450,
                    Radius = 70,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "jayceshockblast",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},
                    HasEndExplosion = true,
                    SecondaryRadius = 175,

                });

            #endregion Jayce

            #region Jinx

            Spells.Add(
                new SpellData
                {
                    CharName = "Jinx",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "JinxR",
                    ProjectileSpeed = 1700, //accelerates to 2600
                    Radius = 140,
                    Range = 25000,
                    SpellDelay = 600,
                    SpellKey = SpellSlot.R,
                    SpellName = "JinxR",
                    SpellType = SpellType.Line,
                    // CollisionObjects = new[] { CollisionObjectType.EnemyChampions, },
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Jinx",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "JinxWMissile",
                    Name = "Zap",
                    ProjectileSpeed = 3300,
                    Radius = 60,
                    Range = 1500,
                    SpellDelay = 600,
                    SpellKey = SpellSlot.W,
                    SpellName = "JinxW",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Jinx

            #region Kalista

            Spells.Add(
                new SpellData
                {
                    CharName = "Kalista",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "kalistamysticshotmistrue",
                    Name = "KalistaQ",
                    ProjectileSpeed = 2000,
                    Radius = 70,
                    Range = 1200,
                    SpellDelay = 350,
                    SpellKey = SpellSlot.Q,
                    SpellName = "KalistaMysticShot",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Kalista

            #region Karma

            Spells.Add(
                new SpellData
                {
                    CharName = "Karma",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "KarmaQMissile",
                    Name = "KarmaQ",
                    ProjectileSpeed = 1700,
                    Radius = 90,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "KarmaQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Karma",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "KarmaQMissileMantra",
                    Name = "KarmaQMantra",
                    ProjectileSpeed = 1700,
                    Radius = 90,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "KarmaQMantra",
                    SpellType = SpellType.Line,
                    UsePackets = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Karma

            #region Karthus

            Spells.Add(
                new SpellData
                {
                    CharName = "Karthus",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "Lay Waste",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 190,
                    Range = 875,
                    SpellDelay = 900,
                    SpellKey = SpellSlot.Q,
                    SpellName = "KarthusLayWasteA1",
                    SpellType = SpellType.Circular,
                    ExtraSpellNames =
                        new[]
                        {
                            "karthuslaywastea2", "karthuslaywastea3", "karthuslaywastedeada1", "karthuslaywastedeada2",
                            "karthuslaywastedeada3"
                        },

                });

            #endregion Karthus

            #region Kassadin

            Spells.Add(
                new SpellData
                {
                    CharName = "Kassadin",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "RiftWalk",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 270,
                    Range = 700,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "RiftWalk",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    Angle = 40,
                    CharName = "Kassadin",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "ForcePulse",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 20,
                    Range = 700,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "ForcePulse",
                    SpellType = SpellType.Cone,

                });

            #endregion Kassadin

            #region Kennen

            Spells.Add(
                new SpellData
                {
                    CharName = "Kennen",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "KennenShurikenHurlMissile1",
                    Name = "Thundering Shuriken",
                    ProjectileSpeed = 1700,
                    Radius = 50,
                    Range = 1175,
                    SpellDelay = 180,
                    SpellKey = SpellSlot.Q,
                    SpellName = "KennenShurikenHurlMissile1",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Kennen

            #region Khazix

            Spells.Add(
                new SpellData
                {
                    CharName = "Khazix",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "KhazixWMissile",
                    Name = "KhazixW",
                    ProjectileSpeed = 1700,
                    Radius = 70,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "KhazixW",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    Angle = 22,
                    CharName = "Khazix",
                    Dangerlevel = SpellDangerLevel.Low,
                    IsThreeWay = true,
                    Name = "khazixwlong",
                    ProjectileSpeed = 1700,
                    Radius = 70,
                    Range = 1025,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "khazixwlong",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    Angle = 22,
                    CharName = "Khazix",
                    Dangerlevel = SpellDangerLevel.Low,
                    IsThreeWay = true,
                    Name = "KhazixE",
                    ProjectileSpeed = 1500,
                    Radius = 300,
                    Range = 800,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "KhazixE",
                    SpellType = SpellType.Circular,

                });

            #endregion Khazix

            #region KogMaw

            Spells.Add(
                new SpellData
                {
                    CharName = "KogMaw",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "Caustic Spittle",
                    ProjectileSpeed = 1650,
                    Radius = 70,
                    Range = 1125,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "KogMawQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "KogMaw",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "KogMawVoidOozeMissile",
                    Name = "KogMawVoidOoze",
                    ProjectileSpeed = 1400,
                    Radius = 120,
                    Range = 1360,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "KogMawVoidOoze",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "KogMaw",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Living Artillery",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 235,
                    Range = 2200,
                    SpellDelay = 1100,
                    SpellKey = SpellSlot.R,
                    SpellName = "KogMawLivingArtillery",
                    SpellType = SpellType.Circular,

                });

            #endregion KogMaw

            #region Leblanc

            Spells.Add(
                new SpellData
                {
                    CharName = "Leblanc",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Ethereal Chains R",
                    ProjectileSpeed = 1600,
                    Radius = 70,
                    Range = 960,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "LeblancSoulShackleM",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Leblanc",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Ethereal Chains",
                    ProjectileSpeed = 1600,
                    Radius = 70,
                    Range = 960,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "LeblancSoulShackle",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Leblanc",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "LeblancSlideM",
                    ProjectileSpeed = 1600,
                    Radius = 250,
                    Range = 725,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "LeblancSlideM",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Leblanc",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "LeblancSlide",
                    ProjectileSpeed = 1600,
                    Radius = 250,
                    Range = 725,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "LeblancSlide",
                    SpellType = SpellType.Circular,

                });

            #endregion Leblanc

            #region LeeSin

            Spells.Add(
                new SpellData
                {
                    CharName = "LeeSin",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Sonic Wave",
                    ProjectileSpeed = 1800,
                    Radius = 60,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "BlindMonkQOne",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "LeeSin",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "LeeSinR",
                    ProjectileSpeed = 2000,
                    Radius = 150,
                    Range = 700,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "BlindMonkR",
                    SpellType = SpellType.Line,

                });

            #endregion LeeSin

            #region Leona

            Spells.Add(
                new SpellData
                {
                    CharName = "Leona",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "LeonaSolarFlare",
                    Name = "Leona Solar Flare",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 300,
                    Range = 1200,
                    SpellDelay = 1000,
                    SpellKey = SpellSlot.R,
                    SpellName = "LeonaSolarFlare",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Leona",
                    Dangerlevel = SpellDangerLevel.High,
                    ExtraDistance = 65,
                    MissileName = "LeonaZenithBladeMissile",
                    Name = "Zenith Blade",
                    ProjectileSpeed = 2000,
                    Radius = 70,
                    Range = 975,
                    SpellDelay = 200,
                    SpellKey = SpellSlot.E,
                    SpellName = "LeonaZenithBlade",
                    SpellType = SpellType.Line,

                });

            #endregion Leona

            #region Lissandra

            Spells.Add(
                new SpellData
                {
                    CharName = "Lissandra",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "LissandraW",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 450,
                    Range = 725,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "LissandraW",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lissandra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Ice Shard",
                    ProjectileSpeed = 2250,
                    Radius = 75,
                    Range = 825,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LissandraQ",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lissandra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "lissandraqshards",
                    Name = "Ice Shard",
                    ProjectileSpeed = 2250,
                    Radius = 75,
                    Range = 825,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LissandraQShards",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lissandra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "LissandraEMissile",
                    Name = "LissandraE",
                    ProjectileSpeed = 850,
                    Radius = 125,
                    Range = 1025,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LissandraE",
                    SpellType = SpellType.Circular,

                });

            #endregion Lissandra

            #region Lucian

            Spells.Add(
                new SpellData
                {
                    CharName = "Lucian",
                    Dangerlevel = SpellDangerLevel.Low,
                    DefaultOff = true,
                    Name = "LucianW",
                    ProjectileSpeed = 1600,
                    Radius = 80,
                    Range = 1000,
                    SpellDelay = 300,
                    SpellKey = SpellSlot.W,
                    SpellName = "LucianW",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lucian",
                    Dangerlevel = SpellDangerLevel.Normal,
                    //defaultOff = true,
                    IsSpecial = true,
                    Name = "LucianQ",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 65,
                    Range = 1140,
                    SpellDelay = 350,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LucianQ",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lucian",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "lucianrmissileoffhand",
                    Name = "LucianR",
                    ProjectileSpeed = 2800,
                    Radius = 110,
                    Range = 1400,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "LucianRMis",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Lucian

            #region Lulu

            Spells.Add(
                new SpellData
                {
                    CharName = "Lulu",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "LuluQMissile",
                    Name = "LuluQ",
                    ProjectileSpeed = 1450,
                    Radius = 80,
                    Range = 925,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LuluQ",
                    SpellType = SpellType.Line,
                    IsSpecial = true,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lulu",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "LuluQMissileTwo",
                    Name = "LuluQPix",
                    ProjectileSpeed = 1450,
                    Radius = 80,
                    Range = 925,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LuluQPix",
                    SpellType = SpellType.Line,
                    IsSpecial = true,

                });

            #endregion Lulu

            #region Lux

            Spells.Add(
                new SpellData
                {
                    CharName = "Lux",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "LuxLightStrikeKugel",
                    ProjectileSpeed = 1400,
                    Radius = 340,
                    Range = 1100,
                    ExtraEndTime = 1000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "LuxLightStrikeKugel",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lux",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Lux Malice Cannon",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 110,
                    Range = 3500,
                    SpellDelay = 1000,
                    SpellKey = SpellSlot.R,
                    SpellName = "LuxMaliceCannon",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Lux",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "LuxLightBindingMis",
                    Name = "Light Binding",
                    ProjectileSpeed = 1200,
                    Radius = 70,
                    Range = 1300,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "LuxLightBinding",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Lux

            #region Malphite

            Spells.Add(
                new SpellData
                {
                    CharName = "Malphite",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "UFSlash",
                    ProjectileSpeed = 2000,
                    Radius = 300,
                    Range = 1000,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.R,
                    SpellName = "UFSlash",
                    SpellType = SpellType.Circular,

                });

            #endregion Malphite

            #region Malzahar

            Spells.Add(
                new SpellData
                {
                    CharName = "Malzahar",
                    Dangerlevel = SpellDangerLevel.Normal,
                    //extraEndTime = 750,
                    //defaultOff = true,
                    IsSpecial = true,
                    IsWall = true,
                    //missileName = "AlZaharCalloftheVoidMissile",
                    Name = "AlZaharCalloftheVoid",
                    ProjectileSpeed = 1600,
                    Radius = 85,
                    Range = 900,
                    SideRadius = 400,
                    SpellDelay = 1000,
                    SpellKey = SpellSlot.Q,
                    SpellName = "AlZaharCalloftheVoid",
                    SpellType = SpellType.Line,

                });

            #endregion Malzahar

            #region MonkeyKing

            /*Spells.Add(
            new SpellData
            {
                charName = "MonkeyKing",
                Dangerlevel = SpellDangerLevel.High,
                name = "MonkeyKingSpinToWin",
                radius = 225,
                range = 300,
                spellDelay = 250,
                SpellKey = SpellSlot.R,
                spellName = "MonkeyKingSpinToWin",
                spellType = SpellType.Circular,
                defaultOff = true,
            });*/

            #endregion MonkeyKing

            #region Morgana

            Spells.Add(
                new SpellData
                {
                    CharName = "Morgana",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Dark Binding",
                    ProjectileSpeed = 1200,
                    Radius = 80,
                    Range = 1300,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "DarkBindingMissile",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Morgana

            #region Nami

            Spells.Add(
                new SpellData
                {
                    CharName = "Nami",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "NamiQ",
                    Radius = 200,
                    Range = 875,
                    SpellDelay = 1000,
                    SpellKey = SpellSlot.Q,
                    SpellName = "NamiQ",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Nami",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "NamiRMissile",
                    Name = "NamiR",
                    ProjectileSpeed = 850,
                    Radius = 250,
                    Range = 2750,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "NamiR",
                    SpellType = SpellType.Line,

                });

            #endregion Nami

            #region Nautilus

            Spells.Add(
                new SpellData
                {
                    CharName = "Nautilus",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "NautilusAnchorDragMissile",
                    Name = "Dredge Line",
                    ProjectileSpeed = 2000,
                    Radius = 90,
                    Range = 1250,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "NautilusAnchorDrag",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Nautilus

            #region Nidalee

            Spells.Add(
                new SpellData
                {
                    CharName = "Nidalee",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Javelin Toss",
                    ProjectileSpeed = 1300,
                    Radius = 60,
                    Range = 1500,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "JavelinToss",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Nidalee

            #region Nocturne

            Spells.Add(
                new SpellData
                {
                    CharName = "Nocturne",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "NocturneDuskbringer",
                    ProjectileSpeed = 1400,
                    Radius = 60,
                    Range = 1125,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "NocturneDuskbringer",
                    SpellType = SpellType.Line,

                });

            #endregion Nocturne

            #region Olaf

            Spells.Add(
                new SpellData
                {
                    CharName = "Olaf",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "olafaxethrow",
                    Name = "Undertow",
                    ProjectileSpeed = 1600,
                    Radius = 90,
                    Range = 1000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "OlafAxeThrowCast",
                    SpellType = SpellType.Line,

                });

            #endregion Olaf

            #region Orianna

            Spells.Add(
                new SpellData
                {
                    CharName = "Orianna",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "orianaizuna",
                    //HasEndExplosion = true,
                    Name = "OrianaIzunaCommand",
                    ProjectileSpeed = 1200,
                    Radius = 80,
                    SecondaryRadius = 170,
                    Range = 2000,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.Q,
                    SpellName = "OrianaIzunaCommand",
                    SpellType = SpellType.Line,
                    IsSpecial = true,
                    UseEndPosition = true,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Orianna",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "OrianaDetonateCommand",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 410,
                    Range = 0,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "OrianaDetonateCommand",
                    SpellType = SpellType.Circular,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Orianna",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "OrianaDissonanceCommand",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 250,
                    Range = 0,
                    SpellDelay = 100,
                    SpellKey = SpellSlot.W,
                    SpellName = "OrianaDissonanceCommand",
                    SpellType = SpellType.Circular,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Orianna",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "orianaredact",
                    Name = "OriannasE",
                    ProjectileSpeed = 1850,
                    Radius = 85,
                    Range = 1500,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.E,
                    SpellName = "OriannasE",
                    SpellType = SpellType.Line,
                });

            #endregion Orianna

            #region Pantheon

            Spells.Add(
                new SpellData
                {
                    Angle = 35,
                    CharName = "Pantheon",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Heartseeker",
                    Radius = 100,
                    Range = 650,
                    SpellDelay = 1000,
                    SpellKey = SpellSlot.E,
                    SpellName = "PantheonE",
                    SpellType = SpellType.Cone,

                });

            #endregion Pantheon

            #region Quinn

            Spells.Add(
                new SpellData
                {
                    CharName = "Quinn",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "QuinnQ",
                    Name = "QuinnQ",
                    ProjectileSpeed = 1550,
                    Radius = 80,
                    Range = 1050,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "QuinnQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Quinn

            #region Poppy

            Spells.Add(
                new SpellData
                {
                    CharName = "Poppy",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "PoppyQ",
                    Name = "PoppyQ",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 100,
                    Range = 430,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "PoppyQ",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Poppy",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "PoppyRMissile",
                    Name = "PoppyR",
                    ProjectileSpeed = 1600,
                    Radius = 100,
                    Range = 1200,
                    SpellKey = SpellSlot.R,
                    SpellName = "PoppyRSpell",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},
                });

            #endregion Poppy

            #region RekSai

            Spells.Add(
                new SpellData
                {
                    CharName = "RekSai",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "RekSaiQBurrowedMis",
                    Name = "RekSaiQ",
                    ProjectileSpeed = 1950,
                    Radius = 65,
                    Range = 1500,
                    SpellDelay = 125,
                    SpellKey = SpellSlot.E,
                    SpellName = "reksaiqburrowed",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion RekSai

            #region Rengar

            Spells.Add(
                new SpellData
                {
                    CharName = "Rengar",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "RengarEFinal",
                    Name = "Bola Strike",
                    ProjectileSpeed = 1500,
                    Radius = 70,
                    Range = 1000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "RengarE",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Rengar

            #region Riven

            Spells.Add(
                new SpellData
                {
                    CharName = "Riven",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "RivenLightsaberMissile",
                    Name = "WindSlash",
                    ProjectileSpeed = 1600,
                    Radius = 20,
                    Range = 1100,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "rivenizunablade",
                    SpellType = SpellType.Cone,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Riven",
                    Dangerlevel = SpellDangerLevel.Normal,
                    DefaultOff = true,
                    Name = "RivenW",
                    ProjectileSpeed = 1500,
                    Radius = 280,
                    Range = 0,
                    SpellDelay = 267,
                    SpellKey = SpellSlot.W,
                    SpellName = "RivenMartyr",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Riven",
                    Dangerlevel = SpellDangerLevel.Low,
                    DefaultOff = true,
                    Name = "RivenQ",
                    ProjectileSpeed = 1500,
                    Radius = 225,
                    Range = 275,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "RivenTriCleave",
                    SpellType = SpellType.Circular,

                });

            #endregion Riven

            #region Rumble

            Spells.Add(
                new SpellData
                {
                    CharName = "Rumble",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "RumbleGrenadeMissile",
                    Name = "RumbleGrenade",
                    ProjectileSpeed = 2000,
                    Radius = 90,
                    Range = 950,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "RumbleGrenade",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Rumble",
                    Dangerlevel = SpellDangerLevel.Low,
                    MissileName = "RumbleCarpetBombMissile",
                    Name = "RumbleR",
                    ProjectileSpeed = 1600,
                    Radius = 200,
                    Range = 1200,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "RumbleCarpetBombM",
                    SpellType = SpellType.Line,

                });

            #endregion Rumble

            #region Ryze

            Spells.Add(
                new SpellData
                {
                    CharName = "Ryze",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "RyzeQ",
                    Name = "RyzeQ",
                    ProjectileSpeed = 1700,
                    Radius = 60,
                    Range = 900,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "RyzeQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Ryze


            #region Sejuani

            Spells.Add(
                new SpellData
                {
                    CharName = "Sejuani",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "SejuaniGlacialPrison",
                    Name = "SejuaniR",
                    ProjectileSpeed = 1600,
                    Radius = 110,
                    Range = 1200,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "SejuaniGlacialPrisonCast",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Sejuani",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "",
                    Name = "SejuaniQ",
                    ProjectileSpeed = 1600,
                    Radius = 120,
                    Range = 900,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.Q,
                    SpellName = "SejuaniArcticAssault",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Sejuani

            #region Shen

            Spells.Add(
                new SpellData
                {
                    CharName = "Shen",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "ShadowDash",
                    ProjectileSpeed = 1250,
                    Radius = 75,
                    Range = 700,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.E,
                    SpellName = "ShenShadowDash",
                    SpellType = SpellType.Line,

                });

            #endregion Shen

            #region Shyvana

            Spells.Add(
                new SpellData
                {
                    CharName = "Shyvana",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "ShyvanaFireball",
                    ProjectileSpeed = 1700,
                    Radius = 60,
                    Range = 950,
                    SpellKey = SpellSlot.E,
                    SpellName = "ShyvanaFireball",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Shyvana",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "ShyvanaTransformCast",
                    ProjectileSpeed = 1100,
                    Radius = 160,
                    Range = 1000,
                    SpellDelay = 10,
                    SpellKey = SpellSlot.R,
                    SpellName = "ShyvanaTransformCast",
                    SpellType = SpellType.Line,

                });

            #endregion Shyvana

            #region Sion

            Spells.Add(
                new SpellData
                {
                    CharName = "Sion",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "SionEMissile",
                    Name = "SionE",
                    ProjectileSpeed = 1800,
                    Radius = 80,
                    Range = 800,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "SionE",
                    SpellType = SpellType.Line,
                    IsSpecial = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Sion",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "SionR",
                    ProjectileSpeed = 1000,
                    Radius = 120,
                    Range = 800,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "SionR",
                    SpellType = SpellType.Line,
                    IsSpecial = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions},
                });

            #endregion Sion

            #region Sivir

            Spells.Add(
                new SpellData
                {
                    CharName = "Sivir",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "SivirQMissile",
                    Name = "Boomerang Blade",
                    ProjectileSpeed = 1350,
                    Radius = 100,
                    Range = 1275,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "SivirQ",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Sivir",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "SivirQMissileReturn",
                    Name = "Boomerang Blade Return",
                    ProjectileSpeed = 1350,
                    Radius = 100,
                    Range = 1275,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.Q,
                    SpellName = "SivirQReturn",
                    SpellType = SpellType.Line,

                });

            #endregion Sivir

            #region Skarner

            Spells.Add(
                new SpellData
                {
                    CharName = "Skarner",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "SkarnerFractureMissile",
                    Name = "SkarnerFracture",
                    ProjectileSpeed = 1400,
                    Radius = 60,
                    Range = 1000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "SkarnerFracture",
                    SpellType = SpellType.Line,

                });

            #endregion Skarner

            #region Sona

            Spells.Add(
                new SpellData
                {
                    CharName = "Sona",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    MissileName = "SonaR",
                    Name = "Crescendo",
                    ProjectileSpeed = 2400,
                    Radius = 150,
                    Range = 1000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "SonaR",
                    SpellType = SpellType.Line,

                });

            #endregion Sona

            #region Soraka

            Spells.Add(
                new SpellData
                {
                    CharName = "Soraka",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "SorakaQ",
                    ProjectileSpeed = 1100,
                    Radius = 260,
                    Range = 970,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "SorakaQ",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Soraka",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "SorakaE",
                    Radius = 275,
                    Range = 925,
                    SpellDelay = 1750,
                    SpellKey = SpellSlot.E,
                    SpellName = "SorakaE",
                    SpellType = SpellType.Circular,

                });

            #endregion Soraka

            #region Swain

            Spells.Add(
                new SpellData
                {
                    CharName = "Swain",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Nevermove",
                    Radius = 250,
                    Range = 900,
                    SpellDelay = 1100,
                    SpellKey = SpellSlot.W,
                    SpellName = "SwainShadowGrasp",
                    SpellType = SpellType.Circular,

                });

            #endregion Swain

            #region Syndra

            Spells.Add(
                new SpellData
                {
                    Angle = 30,
                    CharName = "Syndra",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "SyndraE",
                    UsePackets = true,
                    ProjectileSpeed = 1500,
                    Radius = 140,
                    Range = 800,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "SyndraE",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Syndra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "SyndraW",
                    ProjectileSpeed = 1450,
                    Radius = 220,
                    Range = 925,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.W,
                    SpellName = "syndrawcast",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Syndra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "SyndraQ",
                    Radius = 210,
                    Range = 800,
                    SpellDelay = 600,
                    SpellKey = SpellSlot.Q,
                    SpellName = "SyndraQ",
                    SpellType = SpellType.Circular,

                });

            #endregion Syndra

            #region TahmKench

            Spells.Add(
                new SpellData
                {
                    CharName = "TahmKench",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "tahmkenchqmissile",
                    Name = "TahmKenchQ",
                    ProjectileSpeed = 2000,
                    SpellDelay = 250,
                    Radius = 90,
                    Range = 951,
                    SpellKey = SpellSlot.Q,
                    SpellName = "TahmKenchQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion TahmKench

            #region Talon

            Spells.Add(
                new SpellData
                {
                    CharName = "Talon",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "talonrakemissileone",
                    Name = "TalonRake",
                    ProjectileSpeed = 2300,
                    Radius = 20,
                    Range = 780,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "TalonRake",
                    SpellType = SpellType.Cone,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Talon",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "talonrakemissiletwo",
                    Name = "TalonRakeReturn",
                    ProjectileSpeed = 2300,
                    Radius = 20,
                    Range = 780,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "TalonRakeReturn",
                    SpellType = SpellType.Cone,
                });

            #endregion Talon

            #region Thresh

            Spells.Add(
                new SpellData
                {
                    CharName = "Thresh",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "ThreshQMissile",
                    Name = "ThreshQ",
                    ProjectileSpeed = 1900,
                    Radius = 70,
                    Range = 1100,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "ThreshQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Thresh",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "ThreshEMissile1",
                    Name = "ThreshE",
                    ProjectileSpeed = 2000,
                    Radius = 110,
                    Range = 1075,
                    SpellDelay = 0,
                    DefaultOff = true,
                    SpellKey = SpellSlot.E,
                    SpellName = "ThreshE",
                    SpellType = SpellType.Line,
                    UsePackets = true,

                });

            #endregion Thresh

            #region TwistedFate

            Spells.Add(
                new SpellData
                {
                    Angle = 28,
                    CharName = "TwistedFate",
                    Dangerlevel = SpellDangerLevel.Normal,
                    IsThreeWay = true,
                    MissileName = "SealFateMissile",
                    Name = "Loaded Dice",
                    ProjectileSpeed = 1000,
                    Radius = 40,
                    Range = 1450,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "WildCards",
                    SpellType = SpellType.Line,
                    IsSpecial = true,
                });

            #endregion TwistedFate

            #region Urgot

            Spells.Add(
                new SpellData
                {
                    CharName = "Urgot",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "Acid Hunter",
                    ProjectileSpeed = 1600,
                    Radius = 60,
                    Range = 1000,
                    SpellDelay = 175,
                    SpellKey = SpellSlot.Q,
                    SpellName = "UrgotHeatseekingLineMissile",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Urgot",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Plasma Grenade",
                    ProjectileSpeed = 1750,
                    Radius = 250,
                    Range = 900,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "UrgotPlasmaGrenade",
                    SpellType = SpellType.Circular,

                });

            #endregion Urgot

            #region Varus

            Spells.Add(
                new SpellData
                {
                    CharName = "Varus",
                    Dangerlevel = SpellDangerLevel.Low,
                    DefaultOff = true,
                    Name = "Varus E",
                    ProjectileSpeed = 1500,
                    Radius = 235,
                    Range = 925,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "VarusE",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Varus",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "VarusQMissile",
                    Name = "VarusQMissile",
                    ProjectileSpeed = 1900,
                    Radius = 70,
                    Range = 1600,
                    SpellDelay = 0,
                    SpellKey = SpellSlot.Q,
                    SpellName = "varusq",
                    SpellType = SpellType.Line,
                    UsePackets = true,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Varus",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "VarusR",
                    ProjectileSpeed = 1950,
                    Radius = 100,
                    Range = 1200,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.R,
                    SpellName = "VarusR",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions,},

                });

            #endregion Varus

            #region Veigar

            Spells.Add(
                new SpellData
                {
                    CharName = "Veigar",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "VeigarBalefulStrike",
                    Radius = 70,
                    Range = 950,
                    SpellDelay = 250,
                    ProjectileSpeed = 1750,
                    SpellKey = SpellSlot.Q,
                    SpellName = "VeigarBalefulStrike",
                    MissileName = "VeigarBalefulStrikeMis",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Veigar",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "VeigarDarkMatter",
                    Radius = 225,
                    Range = 900,
                    SpellDelay = 1350,
                    SpellKey = SpellSlot.W,
                    SpellName = "VeigarDarkMatter",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Veigar",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "VeigarEventHorizon",
                    Radius = 425,
                    Range = 700,
                    SpellDelay = 500,
                    ExtraEndTime = 3500,
                    SpellKey = SpellSlot.E,
                    SpellName = "VeigarEventHorizon",
                    SpellType = SpellType.Circular,
                });

            #endregion Veigar

            #region Velkoz

            Spells.Add(
                new SpellData
                {
                    CharName = "Velkoz",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "VelkozE",
                    ProjectileSpeed = 1500,
                    Radius = 225,
                    Range = 950,
                    SpellKey = SpellSlot.E,
                    SpellName = "VelkozE",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Velkoz",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "VelkozW",
                    ProjectileSpeed = 1700,
                    Radius = 88,
                    Range = 1100,
                    SpellKey = SpellSlot.W,
                    SpellName = "VelkozW",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Velkoz",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "VelkozQMissileSplit",
                    ProjectileSpeed = 2100,
                    Radius = 45,
                    Range = 1100,
                    SpellKey = SpellSlot.Q,
                    SpellName = "VelkozQMissileSplit",
                    SpellType = SpellType.Line,
                    UsePackets = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Velkoz",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "VelkozQ",
                    ProjectileSpeed = 1300,
                    Radius = 50,
                    Range = 1250,
                    SpellKey = SpellSlot.Q,
                    MissileName = "VelkozQMissile",
                    SpellName = "VelkozQ",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Velkoz

            #region Vi

            Spells.Add(
                new SpellData
                {
                    CharName = "Vi",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "ViQMissile",
                    Name = "Vi-q",
                    ProjectileSpeed = 1500,
                    Radius = 90,
                    Range = 725,
                    SpellKey = SpellSlot.Q,
                    SpellName = "Vi-q",
                    SpellType = SpellType.Line,
                    UsePackets = true,
                    DefaultOff = true,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions,},

                });

            #endregion Vi

            #region Viktor

            Spells.Add(
                new SpellData
                {
                    CharName = "Viktor",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "ViktorDeathRayMissile",
                    Name = "ViktorDeathRay",
                    ProjectileSpeed = 780,
                    Radius = 80,
                    Range = 800,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "Laser",
                    ExtraMissileNames = new[] {"viktoreaugmissile",},
                    SpellType = SpellType.Line,
                    UsePackets = true,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Viktor",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "GravitonField",
                    Radius = 300,
                    Range = 625,
                    SpellDelay = 1500,
                    SpellKey = SpellSlot.W,
                    SpellName = "ViktorGravitonField",
                    SpellType = SpellType.Circular,
                    DefaultOff = true,
                });

            #endregion Viktor

            #region Vladimir

            Spells.Add(
                new SpellData
                {
                    CharName = "Vladimir",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "VladimirHemoplague",
                    Radius = 375,
                    Range = 700,
                    SpellDelay = 389,
                    SpellKey = SpellSlot.R,
                    SpellName = "VladimirHemoplague",
                    SpellType = SpellType.Circular,

                });

            #endregion Vladimir

            #region Xerath

            Spells.Add(
                new SpellData
                {
                    CharName = "Xerath",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "XerathArcaneBarrage2",
                    Radius = 280,
                    Range = 1100,
                    SpellDelay = 750,
                    SpellKey = SpellSlot.W,
                    SpellName = "XerathArcaneBarrage2",
                    SpellType = SpellType.Circular,
                    ExtraDrawHeight = 45,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Xerath",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "XerathArcanopulse2",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 70,
                    Range = 1525,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "xeratharcanopulse2",
                    ExtraSpellNames = new[] {"XerathArcanopulseChargeUp"},
                    UseEndPosition = true,
                    SpellType = SpellType.Line,
                    //isSpecial = true,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Xerath",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "XerathLocusOfPower2",
                    Radius = 200,
                    Range = 5600,
                    SpellDelay = 600,
                    SpellKey = SpellSlot.R,
                    SpellName = "xerathrmissilewrapper",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Xerath",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "XerathMageSpearMissile",
                    Name = "XerathMageSpear",
                    ProjectileSpeed = 1600,
                    SpellDelay = 200,
                    Radius = 60,
                    Range = 1125,
                    SpellKey = SpellSlot.E,
                    SpellName = "XerathMageSpear",
                    SpellType = SpellType.Line,
                    CollisionObjects = new[] {CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions},

                });

            #endregion Xerath

            #region Yasuo

            Spells.Add(
                new SpellData
                {
                    CharName = "Yasuo",
                    Dangerlevel = SpellDangerLevel.High,
                    MissileName = "yasuoq3mis",
                    Name = "Steel Tempest3",
                    ProjectileSpeed = 1200,
                    Radius = 90,
                    Range = 1025,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "yasuoq3w",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Yasuo",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Steel Tempest",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 35,
                    Range = 500,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "YasuoQW",
                    ExtraSpellNames = new[] {"yasuoq2w"},
                    SpellType = SpellType.Line,
                    DefaultOff = true,

                });

            #endregion Yasuo

            #region Zac

            #endregion Zac

            #region Zed

            Spells.Add(
                new SpellData
                {
                    CharName = "Zed",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "ZedShuriken",
                    ProjectileSpeed = 1700,
                    Radius = 50,
                    Range = 925,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "ZedQ",
                    SpellType = SpellType.Line,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Zed",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "ZedE",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 290,
                    Range = 290,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "ZedE",
                    SpellType = SpellType.Circular,
                    IsSpecial = true,
                    DefaultOff = true,
                });

            #endregion Zed

            #region Ziggs

            Spells.Add(
                new SpellData
                {
                    CharName = "Ziggs",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "ZiggsE",
                    ProjectileSpeed = 3000,
                    Radius = 235,
                    Range = 2000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "ZiggsE",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ziggs",
                    Dangerlevel = SpellDangerLevel.Low,
                    Name = "ZiggsW",
                    ProjectileSpeed = 3000,
                    Radius = 275,
                    Range = 2000,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.W,
                    SpellName = "ZiggsW",
                    SpellType = SpellType.Circular,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ziggs",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "ZiggsQ",
                    ProjectileSpeed = 1700,
                    Radius = 150,
                    Range = 850,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.Q,
                    SpellName = "ZiggsQ",
                    SpellType = SpellType.Circular,
                    IsSpecial = true,
                    NoProcess = true,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Ziggs",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "ZiggsR",
                    ProjectileSpeed = 1500,
                    Radius = 550,
                    Range = 5300,
                    SpellDelay = 1500,
                    SpellKey = SpellSlot.R,
                    SpellName = "ZiggsR",
                    SpellType = SpellType.Circular,
                });

            #endregion Ziggs

            #region Zilean

            Spells.Add(
                new SpellData
                {
                    CharName = "Zilean",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "ZileanQ",
                    ProjectileSpeed = 2000,
                    Radius = 250,
                    Range = 900,
                    SpellDelay = 300,
                    SpellKey = SpellSlot.Q,
                    SpellName = "ZileanQ",
                    SpellType = SpellType.Circular,
                });

            #endregion Zilean

            #region Zyra

            Spells.Add(
                new SpellData
                {
                    CharName = "Zyra",
                    Dangerlevel = SpellDangerLevel.High,
                    Name = "Grasping Roots",
                    ProjectileSpeed = 1400, //1150
                    Radius = 70,
                    Range = 1150,
                    SpellDelay = 250,
                    SpellKey = SpellSlot.E,
                    SpellName = "ZyraGraspingRoots",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Zyra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    MissileName = "ZyraPassiveDeathMissile",
                    Name = "Zyra Passive",
                    ProjectileSpeed = 2200,
                    Radius = 70,
                    Range = 1474,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.Q,
                    SpellName = "zyrapassivedeathmanager",
                    SpellType = SpellType.Line,

                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Zyra",
                    Dangerlevel = SpellDangerLevel.Normal,
                    Name = "Deadly Bloom",
                    Radius = 260,
                    Range = 825,
                    SpellDelay = 800,
                    SpellKey = SpellSlot.Q,
                    SpellName = "ZyraQFissure",
                    SpellType = SpellType.Circular,
                });

            Spells.Add(
                new SpellData
                {
                    CharName = "Zyra",
                    Dangerlevel = SpellDangerLevel.Extreme,
                    Name = "ZyraR",
                    ProjectileSpeed = float.MaxValue,
                    Radius = 525,
                    Range = 700,
                    SpellDelay = 500,
                    SpellKey = SpellSlot.R,
                    SpellName = "ZyraBrambleZone",
                    SpellType = SpellType.Circular,

                });

            #endregion Zyra
        }
    }
}
