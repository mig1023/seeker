using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public bool ThisIsSpell { get; set; }
        public bool Regeneration { get; set; }
        public bool EvenWound { get; set; }
        public bool ReactionFight { get; set; }
        public bool TailAttack { get; set; }
        public bool IncrementWounds { get; set; }
        public bool ThreeWoundLimit { get; set; }
        public bool Poison { get; set; }
        public bool Invincible { get; set; }
        public int OnlyRounds { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsWinToWin { get; set; }
        public int WoundsLimit { get; set; }
        public int DeathLimit { get; set; }
        public int Wound { get; set; }
        public int Dices { get; set; }
        public string ReactionWounds { get; set; }
        public string OnlyOne { get; set; }

        public Abstract.IModification Damage { get; set; }

        public Character.SpecializationType? Specialization { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", protagonist.Strength),
            String.Format("Жизни: {0}/{1}", protagonist.Hitpoints, protagonist.MaxHitpoints),
            String.Format("Обращений: {0}", protagonist.Transformation),
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (protagonist.Spells == null)
                return null;

            foreach (string spell in protagonist.Spells)
            {
                if (currentSpells.ContainsKey(spell.ToLower()))
                    currentSpells[spell.ToLower()] += 1;
                else
                    currentSpells.Add(spell.ToLower(), 1);
            }

            List<string> statusLines = new List<string> { String.Format("Золото: {0}", protagonist.Gold) };

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(x => x))
                statusLines.Insert(0, String.Format("Заклятье {0} - {1} шт", char.ToUpper(spell[0]) + spell.Substring(1), currentSpells[spell]));

            return statusLines;
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Other.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, gold) };
            }
            else if (ThisIsSpell)
            {
                int count = protagonist.Spells.Where(x => x == Text).Count();
                return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" ({0} шт)", count) : String.Empty)) };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (enemy.Hitpoints > 0)
                    enemies.Add(String.Format("{0}\nсила {1}  жизни {2}", enemy.Name, enemy.Strength, enemy.Hitpoints));
                else
                    enemies.Add(String.Format("{0}\nсила {1}", enemy.Name, enemy.Strength));
            }

            return enemies;
        }

        public List<string> Camouflage()
        {
            Game.Option.Trigger("Camouflage");

            return new List<string> { "Вы успешно себя закамуфлировали грязью :)" };
        }

        private int CureSpellCount() => protagonist.Spells.Where(x => x.Contains("ЛЕЧЕНИЕ")).Count();

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool bySpellAdd = ThisIsSpell && (protagonist.Magicpoints <= 0) && !secondButton;
            bool bySpellRemove = ThisIsSpell && !protagonist.Spells.Contains(Text) && secondButton;
            bool byCureSpell = (Name == "CureFracture") && (CureSpellCount() < Wound);
            bool bySell = (Name == "Sell") && !Game.Option.IsTriggered(Trigger);
            bool bySpecButton = (Specialization != null) && (protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (protagonist.Gold < Price);
            bool byTrigger = Game.Option.IsTriggered(OnlyOne);

            return !(bySpellAdd || bySpellRemove || byCureSpell || bySell || bySpecButton || byPrice || byTrigger || Used);
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Hitpoints, out toEndParagraph, out toEndText);

        public List<string> Get()
        {
            if (ThisIsSpell && (protagonist.Magicpoints >= 1))
            {
                protagonist.Spells.Add(Text);
                protagonist.SpellsReplica.Add(Text);
                protagonist.Magicpoints -= 1;
            }

            else if ((Specialization != null) && (protagonist.Specialization == Character.SpecializationType.Nope))
            {
                protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    protagonist.Strength += 1;

                else if (Specialization == Character.SpecializationType.Thrower)
                    protagonist.Magicpoints += 1;

                else
                {
                    protagonist.Magicpoints += 2;
                    protagonist.Transformation += 2;
                }
            }

            else if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;
                Used = true;
            }

            if (!String.IsNullOrEmpty(OnlyOne))
                Game.Option.Trigger(OnlyOne);

            if (Benefit != null)
                Benefit.Do();

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            protagonist.Spells.Remove(Text);
            protagonist.Magicpoints += 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Sell()
        {
            Used = true;
            Game.Option.Trigger(Trigger, remove: true);
            protagonist.Gold += Price;

            return new List<string> { "RELOAD" };
        }

        public List<string> Reaction()
        {
            List<string> reaction = new List<string>();

            bool goodReaction = GoodReaction(ref reaction);

            reaction.Add(Result(goodReaction, "СРЕАГИРОВАЛИ|НЕ СРЕАГИРОВАЛИ"));

            if (goodReaction && (Benefit != null))
                Benefit.Do();

            if (!goodReaction && (Damage != null))
                Damage.Do();

            return reaction;
        }

        public List<string> RestoreSpells()
        {
            protagonist.Spells = protagonist.Spells.Where(x => x == "ЛЕЧЕНИЕ").ToList();
            protagonist.Spells.AddRange(protagonist.SpellsReplica.Where(x => x != "ЛЕЧЕНИЕ").ToList());

            protagonist.Gold -= 5;

            return new List<string> { "BIG|GOOD|Вы восстановили заклинания :)", "Лечилка не восстанавливается, как и было сказано" };
        }
        
        public List<string> GetMagicBlade()
        {
            Game.Option.Trigger("MagicSword");
            protagonist.Gold -= 5;

            return new List<string> { "BIG|GOOD|Ваш меч теперь заколдован :)" };
        }

        public List<string> LeechFight()
        {
            List<string> fight = new List<string> { };

            int dice = Game.Dice.Roll();

            fight.Add(String.Format("На кубиках выпало: {0}", Game.Dice.Symbol(dice)));

            if (dice > 2)
                fight.Add("BIG|GOOD|Вы раздавили пиявку :)");
            else
            {
                protagonist.Hitpoints -= 2;
                fight.Add("BIG|BAD|Она прокусила сапог :(");
                fight.Add("BAD|Вы потеряли 2 жизни...");
            }

            return fight;
        }

        private bool GoodReaction(ref List<string> reaction, bool showResult = false)
        {
            int reactionLevel = (int)Math.Floor((double)protagonist.Hitpoints / 5);
            reaction.Add(String.Format("Уровнь реакции: {0} / 5 = {1}", protagonist.Hitpoints, reactionLevel));

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            reaction.Add(String.Format("Реакция: {0} {1} {2}", Game.Dice.Symbol(reactionDice), (goodReaction ? "<=" : ">"), reactionLevel));

            if (showResult)
                reaction.Add(goodReaction ? "BOLD|Реакции хватило" : "BOLD|Реакция подвела");

            return goodReaction;
        }

        public List<string> Dice()
        {
            int dice = Game.Dice.Roll();
            return new List<string> { String.Format("BIG|На кубике выпало: {0} - {1}", Game.Dice.Symbol(dice), (dice % 2 == 0 ? "чёт" : "нечет")) };
        }

        public List<string> StoneThrow()
        {
            if (protagonist.Specialization == Character.SpecializationType.Thrower)
                return new List<string> { "BIG|GOOD|Ваша специализациея является метание ножей - и вы не промахнулись :)" };

            int dice = Game.Dice.Roll();

            List<string> stoneThrow = new List<string> { };

            stoneThrow.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));
            stoneThrow.Add(Result(dice > 4, "Вы попали|Вы промахнулись"));

            return stoneThrow;
        }

        public List<string> DicesGame()
        {
            List<string> game = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            game.Add(String.Format("На кубиках выпало: {0} и {1}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice)));
            game.Add(String.Format("BIG|BOLD|Итого выпало: {0}", firstDice + secondDice));

            return game;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            protagonist.Hitpoints -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        public List<string> CureRabies()
        {
            if (CureSpellCount() < 1)
                return new List<string> { "BIG|BAD|У вас нет ЛЕЧИЛКИ :(" };

            List<string> cure = new List<string> { };

            protagonist.Spells.Remove("ЛЕЧЕНИЕ");

            Game.Option.Trigger("Rabies", remove: true);
            cure.Add("BIG|GOOD|Вы успешно вылечили болезнь!");

            protagonist.Hitpoints += 3;
            cure.Add("BOLD|Вы дополнительно получили +3 жизни.");

            return cure;
        }

        public List<string> CureFracture()
        {
            if (Wound > 1)
            {
                if (CureSpellCount() < 2)
                    return new List<string> { "BIG|BAD|У вас нет двух ЛЕЧИЛОК :(" };

                for (int i = 0; i <= 1; i++)
                    protagonist.Spells.Remove("ЛЕЧЕНИЕ");

                protagonist.Hitpoints += 4;
            }    
            else
            {
                if (CureSpellCount() < 1)
                    return new List<string> { "BIG|BAD|У вас нет ЛЕЧИЛКИ :(" };

                protagonist.Spells.Remove("ЛЕЧЕНИЕ");
                protagonist.Strength -= 1;
            }

            Game.Option.Trigger(OnlyOne);

            return new List<string> { "RELOAD" };
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            bool wounded = (protagonist.Hitpoints < protagonist.MaxHitpoints);
            bool inOption = Game.Checks.ExistsInParagraph(actionText: "ЛЕЧИЛК", optionText: "ЛЕЧИЛК");

            if (wounded && (CureSpellCount() > 0) && !inOption)
                staticButtons.Add("ЛЕЧИЛКА");

            if (wounded && (protagonist.Elixir > 0))
                staticButtons.Add("ЭЛИКСИР");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ЛЕЧИЛКА")
            {
                protagonist.Hitpoints += 6;
                protagonist.Spells.Remove("ЛЕЧЕНИЕ");

                return true;
            }
            else if (action == "ЭЛИКСИР")
            {
                protagonist.Hitpoints = protagonist.MaxHitpoints;
                protagonist.Elixir -= 1;

                return true;
            }

            return false;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Other.LevelParse(option);

                if (option.Contains("ЗОЛОТО >=") && (level > protagonist.Gold))
                    return false;

                if (option.Contains("СИЛА >=") && (level > protagonist.Strength))
                    return false;
            }
            else if (option == "ВОЛК")
            {
                return protagonist.Transformation > 0;
            }
            else if (option == "МЕДВЕДЬ")
            {
                return (protagonist.Transformation > 0) && !Game.Option.IsTriggered("Taboo");
            }
            else if (protagonist.Spells.Contains(option))
            {
                return true;
            }
            else if (option.Contains("ВОИН") || option.Contains("МАГ") || option.Contains("МЕТАТЕЛЬ"))
            {
                Character.SpecializationType spec = Constants.GetSpecializationType()[option.Replace("!", String.Empty)];
                return (option.Contains("!") ? (protagonist.Specialization != spec) : (protagonist.Specialization == spec));
            }
            else if (option.Contains("|"))
            {
                foreach (string opt in option.Split('|'))
                    if (Game.Option.IsTriggered(opt.Trim()))
                        return true;

                return false;
            }
            else if (option.Contains(","))
            {
                foreach (string opt in option.Split(','))
                {
                    if (Game.Option.IsTriggered(opt.Trim()))
                        return false;
                }

                return true;
            }
            else if (option.Contains("!"))
            {
                if (Game.Option.IsTriggered(option.Replace("!", String.Empty).Trim()))
                    return false;
            }
            else if (!Game.Option.IsTriggered(option.Trim()))
            {
                return false;
            }
            
            return true;
        }

        private void WinFightEnding(ref List<string> fight, int wounded)
        {
            if (IsPoisonedBlade())
            {
                Game.Option.Trigger("PoisonedBlade", remove: true);
            }

            if (Poison)
            {
                if (wounded > 3)
                {
                    protagonist.Hitpoints /= 2;

                    fight.Add(String.Empty);
                    fight.Add("BAD|Из-за яда вы теряете половину оставшихся жизней...");
                }

                fight.Add(String.Empty);

                if (protagonist.Specialization == Character.SpecializationType.Thrower)
                    fight.Add("BOLD|Вы смазали ядом свои метательные ножи, теперь они будут отнимать у противника не 3, а 4 жизни");
                else
                    fight.Add("BOLD|Вы смазали ядом свой меч, в следующем бою он будет отнимать у противника по 5 жизней");

                Game.Option.Trigger("PoisonedBlade");
            }
            else if (Regeneration)
            {
                int hitpointsBonus = Game.Dice.Roll();

                protagonist.Hitpoints += hitpointsBonus;

                fight.Add(String.Empty);
                fight.Add(String.Format("GOOD|Благодаря крови, попавшей на вас, вы восстановили {0} жизни", hitpointsBonus));
            }

            if (Game.Option.IsTriggered("Rabies"))
            {
                protagonist.Strength -= 1;

                fight.Add(String.Empty);
                fight.Add("BAD|Вы дополнительно теряете 1 Силу за невылеченную болезнь...");
            }
        }

        private bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight, int wounded = 0)
        {

            if (FightEnemies.Where(x => x.Hitpoints > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() > 0)
            {
                return false;
            }
            else if (Invincible)
            {
                return false;
            }
            else
            {
                fight.Add(String.Empty);
                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                WinFightEnding(ref fight, wounded);

                return true;
            }
        }

        private List<string> LostFight(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }

        private bool IsPoisonedBlade() => (Game.Option.IsTriggered("PoisonedBlade") &&
            (protagonist.Specialization != Character.SpecializationType.Thrower));

        private bool IsMagicBlade() => Game.Option.IsTriggered("MagicSword");

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, woundLine = 0, wounded = 0, death = 0, roundsWin = 0, incrementWounds = 2;
            bool warriorFight = (protagonist.Specialization == Character.SpecializationType.Warrior);

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                bool block = EvenWound || ReactionFight;
                bool reactionFail = false;

                if ((protagonist.Specialization == Character.SpecializationType.Thrower) && (round == 1) && !block)
                {
                    fight.Add("BOLD|Вы бросаете метательные ножи");

                    int wound = (Game.Option.IsTriggered("PoisonedBlade") ? 4 : 3);

                    FightEnemies[0].Hitpoints -= wound;

                    fight.Add(String.Format("GOOD|{0} ранен метательными ножами и потерял {1} жизни", FightEnemies[0].Name, wound));
                    fight.Add(String.Empty);

                    if (EnemyLostFight(FightEnemies, ref fight))
                        return fight;
                }

                foreach (Character enemy in FightEnemies)
                {
                    if ((enemy.Hitpoints <= 0) && !Invincible)
                        continue;

                    if (Regeneration && (round % 4 == 0) && (enemy.Hitpoints < Enemies.Where(x => x.Name == enemy.Name).FirstOrDefault().Hitpoints))
                    {
                        enemy.Hitpoints += 1;
                        fight.Add(String.Format("BOLD|{0} восстановил 1 жизнь", enemy.Name));
                    }

                    if (!Invincible)
                        fight.Add(String.Format("{0} (жизни: {1})", enemy.Name, enemy.Hitpoints));

                    int firstProtagonistRoll = Game.Dice.Roll();
                    int secondProtagonistRoll = Game.Dice.Roll();
                    int protagonistHitStrength = firstProtagonistRoll + secondProtagonistRoll + protagonist.Strength;

                    fight.Add(String.Format(
                        "Ваш удар: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(firstProtagonistRoll), Game.Dice.Symbol(secondProtagonistRoll),
                        protagonist.Strength, protagonistHitStrength));

                    int firstEnemyRoll = Game.Dice.Roll();
                    int secondEnemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Strength;

                    fight.Add(String.Format(
                        "Его удар: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll),
                        enemy.Strength, enemyHitStrength));

                    if (ReactionFight)
                        reactionFail = !GoodReaction(ref fight, showResult: true);

                    if (TailAttack && (firstEnemyRoll == secondEnemyRoll))
                    {
                        fight.Add("BAD|Аллигатор ударил хвостом! Вы теряете 5 жизней!");

                        protagonist.Hitpoints -= 5;
                        TailAttack = false;

                        woundLine = 0;

                        if (protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else if (warriorFight && (firstProtagonistRoll == secondProtagonistRoll) && (firstProtagonistRoll == 6) && (!ReactionFight || !reactionFail))
                    {
                        fight.Add("BOLD|Вы сделали 'Крыло ястреба'!");

                        enemy.Hitpoints /= 2;
                        woundLine += 1;

                        fight.Add(String.Format("GOOD|{0} ранен на половину своих жизней", enemy.Name));
                    }
                    else if ((protagonistHitStrength > enemyHitStrength) && (!ReactionFight || !reactionFail))
                    {
                        int woundLevel = (IsMagicBlade() ? 3 : 2);

                        if (EvenWound)
                        {
                            int woundDice = Game.Dice.Roll();

                            if (woundDice % 2 == 0)
                            {
                                enemy.Hitpoints -= woundLevel;
                                woundLine += 1;

                                fight.Add(String.Format("Бросок на пробитие: {0} - чётное", Game.Dice.Symbol(woundDice)));
                                fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));
                            }
                            else
                            {
                                fight.Add(String.Format("Бросок на пробитие: {0} - нечётное", Game.Dice.Symbol(woundDice)));
                                fight.Add(String.Format("BAD|Вы не смогли пробить защиту {0}", enemy.Name));
                            }
                        }
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                            woundLine += 1;
                            roundsWin += 1;

                            if (IsPoisonedBlade())
                            {
                                enemy.Hitpoints -= 5;
                                fight.Add(String.Format("BOLD|Из-за яда, рана отнимает у {0} сразу 5 жизней", enemy.Name));
                            }
                            else
                                enemy.Hitpoints -= woundLevel;
                        }

                        if (enemy.Hitpoints <= 0)
                            death += 1;

                        if (EnemyLostFight(FightEnemies, ref fight))
                            return fight;
                    }
                    else if ((protagonistHitStrength < enemyHitStrength) || reactionFail)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        wounded += 1;
                        woundLine = 0;

                        if (!String.IsNullOrEmpty(ReactionWounds))
                        {
                            string[] wounds = ReactionWounds.Split('-');
                            int wound = int.Parse(GoodReaction(ref fight, showResult: true) ? wounds[0] : wounds[1]);

                            protagonist.Hitpoints -= wound;

                            if (wound > 0)
                                fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, wound));
                            else
                                fight.Add(String.Format("GOOD|{0} не нанёс вам урона", enemy.Name));
                        }
                        else if (IncrementWounds)
                        {
                            fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, incrementWounds));

                            protagonist.Hitpoints -= incrementWounds;

                            incrementWounds *= 2;
                        }
                        else
                            protagonist.Hitpoints -= (Wound > 0 ? Wound : 2);

                        if (protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else
                    {
                        woundLine = 0;
                        fight.Add("BOLD|Ничья в раунде");
                    }
                        

                    if ((OnlyRounds > 0) && (OnlyRounds <= round))
                    {
                        fight.Add("BOLD|Отведённые на бой раунды истекли.");
                        return fight;
                    }

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        return LostFight(fight);
                    }

                    if ((DeathLimit > 0) && (death >= DeathLimit))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Вы убили установленное количество противников.");

                        return fight;
                    }

                    if ((RoundsWinToWin > 0) && (roundsWin >= RoundsWinToWin))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Вы нанесли противнику необходимое количество ран :)");

                        return fight;
                    }

                    if (ThreeWoundLimit && (woundLine > 2))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Вы ранили его три раза подряд!");

                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
