using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
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

        static bool NextFightWithWolf = false;
        static bool NextFightWithBear = false;

        public Abstract.IModification Damage { get; set; }

        public Character.SpecializationType? Specialization { get; set; }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                $"Сила: {Character.Protagonist.Strength}",
                $"Жизни: {Character.Protagonist.Hitpoints}/{Character.Protagonist.MaxHitpoints}",
            };

            if (Character.Protagonist.Transformation > 0)
                statusLines.Add($"Обращений: {Character.Protagonist.Transformation}");

            return statusLines;
        }

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (Character.Protagonist.Spells == null)
                return null;

            foreach (string spell in Character.Protagonist.Spells)
            {
                if (String.IsNullOrEmpty(spell))
                    continue;

                if (currentSpells.ContainsKey(spell.ToLower()))
                {
                    currentSpells[spell.ToLower()] += 1;
                }
                else
                {
                    currentSpells.Add(spell.ToLower(), 1);
                }
            }

            List<string> statusLines = new List<string> { $"Золото: {Character.Protagonist.Gold}" };

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(x => x))
            {
                string spellName = char.ToUpper(spell[0]) + spell.Substring(1);
                string spellLine = $"Заклятье {spellName} - {currentSpells[spell]} шт";
                statusLines.Insert(0, spellLine);
            }

            return statusLines;
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { $"{Head}, {Price} {gold}" };
            }
            else if (ThisIsSpell)
            {
                int count = Character.Protagonist.Spells.Where(x => x == Head).Count();
                string template = count > 0 ? $" ({count} шт)" : String.Empty;
                return new List<string> { $"{Head}{template}" };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (enemy.Hitpoints > 0)
                {
                    enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  жизни {enemy.Hitpoints}");
                }
                else
                {
                    enemies.Add($"{enemy.Name}\nсила {enemy.Strength}");
                }
            }

            return enemies;
        }

        public override string ButtonText()
        {
            if (!String.IsNullOrEmpty(Button))
                return Button;

            switch (Type)
            {
                case "Fight":
                    return "Сражаться";

                case "Reaction":
                    return "Реагируйте";

                case "DiceWounds":
                    return "Кинуть кубик" + (Dices > 0 ? "и" : String.Empty);

                default:
                    return Button;
            }
        }

        public List<string> Camouflage()
        {
            Game.Option.Trigger("Camouflage");

            return new List<string> { "Вы успешно себя закамуфлировали грязью :)" };
        }

        private static int CureSpellCount() =>
            Character.Protagonist.Spells.Where(x => x.Contains("ЛЕЧЕНИЕ")).Count();

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool bySpellAdd = ThisIsSpell && (Character.Protagonist.Magicpoints <= 0) && !secondButton;
            bool bySpellRemove = ThisIsSpell && !Character.Protagonist.Spells.Contains(Head) && secondButton;
            bool byCureSpell = (Type == "CureFracture") && (CureSpellCount() < Wound);
            bool bySell = (Type == "Sell") && !Game.Option.IsTriggered(Trigger);
            bool bySpecButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (Character.Protagonist.Gold < Price);
            bool byTrigger = Game.Option.IsTriggered(OnlyOne);

            return !(bySpellAdd || bySpellRemove || byCureSpell || bySell || bySpecButton || byPrice || byTrigger || Used);
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Hitpoints, out toEndParagraph, out toEndText);

        public List<string> Get()
        {
            if (ThisIsSpell && (Character.Protagonist.Magicpoints >= 1))
            {
                Character.Protagonist.Spells.Add(Head);
                Character.Protagonist.SpellsReplica.Add(Head);
                Character.Protagonist.Magicpoints -= 1;
            }
            else if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                {
                    Character.Protagonist.Strength += 1;
                }
                else if (Specialization == Character.SpecializationType.Thrower)
                {
                    Character.Protagonist.Magicpoints += 1;
                }
                else
                {
                    Character.Protagonist.Magicpoints += 2;
                    Character.Protagonist.Transformation += 2;
                }
            }
            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
            {
                Character.Protagonist.Gold -= Price;
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
            Character.Protagonist.Spells.Remove(Head);
            Character.Protagonist.Magicpoints += 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Sell()
        {
            Used = true;
            Game.Option.Trigger(Trigger, remove: true);
            Character.Protagonist.Gold += Price;

            return new List<string> { "RELOAD" };
        }

        public List<string> Reaction()
        {
            List<string> reaction = new List<string>();

            bool goodReaction = Reactions.Good(ref reaction);

            reaction.Add(Result(goodReaction, "СРЕАГИРОВАЛИ|НЕ СРЕАГИРОВАЛИ"));

            if (goodReaction && (Benefit != null))
                Benefit.Do();

            if (!goodReaction && (Damage != null))
                Damage.Do();

            Game.Buttons.Disable(goodReaction,
                "Успели, Все в порядке, Вырвались из обители гулов, Хорошо, Ваша скорость на высоте",
                "Нет, Чуть притормозили, Не очень");

            return reaction;
        }

        public List<string> RestoreSpells()
        {
            Character.Protagonist.Spells = Character.Protagonist.Spells
                .Where(x => x == "ЛЕЧЕНИЕ")
                .ToList();

            List<string> spells = Character.Protagonist.SpellsReplica
                .Where(x => x != "ЛЕЧЕНИЕ")
                .ToList();

            Character.Protagonist.Spells.AddRange(spells);

            Character.Protagonist.Gold -= 5;

            List<string> result = new List<string>
            {
                "BIG|GOOD|Вы восстановили заклинания :)",
                "Лечилка не восстанавливается, как и было сказано"
            };

            return result;
        }
        
        public List<string> GetMagicBlade()
        {
            Game.Option.Trigger("MagicSword");
            Character.Protagonist.Gold -= 5;

            return new List<string> { "BIG|GOOD|Ваш меч теперь заколдован :)" };
        }

        public List<string> LeechFight()
        {
            List<string> fight = new List<string> { };

            int dice = Game.Dice.Roll();

            fight.Add($"На кубиках выпало: {Game.Dice.Symbol(dice)}");

            if (dice > 2)
            {
                fight.Add("BIG|GOOD|Вы раздавили пиявку :)");
            }
            else
            {
                Character.Protagonist.Hitpoints -= 2;
                fight.Add("BIG|BAD|Она прокусила сапог :(");
                fight.Add("BAD|Вы потеряли 2 жизни...");
            }

            return fight;
        }

        public List<string> Dice()
        {
            int dice = Game.Dice.Roll();
            string odd = dice % 2 == 0 ? "чёт" : "нечет";
            return new List<string> { $"BIG|На кубике выпало: {Game.Dice.Symbol(dice)} - {odd}" };
        }

        public List<string> StoneThrow()
        {
            if (Character.Protagonist.Specialization == Character.SpecializationType.Thrower)
                return new List<string> { "BIG|GOOD|Ваша специализациея является метание ножей - и вы не промахнулись :)" };

            int dice = Game.Dice.Roll();

            List<string> stoneThrow = new List<string> { };

            stoneThrow.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}");
            stoneThrow.Add(Result(dice > 4, "Вы попали|Вы промахнулись"));

            return stoneThrow;
        }

        public List<string> DicesGame()
        {
            List<string> game = new List<string> { };

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            game.Add($"На кубиках выпало: {Game.Dice.Symbol(firstDice)} и {Game.Dice.Symbol(secondDice)}");
            game.Add($"BIG|BOLD|Итого выпало: {firstDice + secondDice}");

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
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            Character.Protagonist.Hitpoints -= dices;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dices}");

            return diceCheck;
        }

        public List<string> CureRabies()
        {
            if (CureSpellCount() < 1)
                return new List<string> { "BIG|BAD|У вас нет ЛЕЧИЛКИ :(" };

            List<string> cure = new List<string> { };

            Character.Protagonist.Spells.Remove("ЛЕЧЕНИЕ");

            Game.Option.Trigger("Rabies", remove: true);
            cure.Add("BIG|GOOD|Вы успешно вылечили болезнь!");

            Character.Protagonist.Hitpoints += 3;
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
                    Character.Protagonist.Spells.Remove("ЛЕЧЕНИЕ");

                Character.Protagonist.Hitpoints += 4;
            }    
            else
            {
                if (CureSpellCount() < 1)
                    return new List<string> { "BIG|BAD|У вас нет ЛЕЧИЛКИ :(" };

                Character.Protagonist.Spells.Remove("ЛЕЧЕНИЕ");
                Character.Protagonist.Strength -= 1;
            }

            Game.Option.Trigger(OnlyOne);

            return new List<string> { "RELOAD" };
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            bool wounded = (Character.Protagonist.Hitpoints < Character.Protagonist.MaxHitpoints);
            bool inOption = Game.Buttons.ExistsInParagraph(actionText: "ЛЕЧИЛК", optionText: "ЛЕЧИЛК");

            if (wounded && (CureSpellCount() > 0) && !inOption)
                staticButtons.Add("ЛЕЧИЛКА");

            if (wounded && (Character.Protagonist.Elixir > 0))
                staticButtons.Add("ЭЛИКСИР");

            bool alreadyTransform = NextFightWithWolf || NextFightWithBear;

            if (Game.Buttons.ExistsInParagraph(actionName: "Fight") && (Character.Protagonist.Transformation > 0) && !alreadyTransform)
                staticButtons.Add("ОБРАТИТЬСЯ ВОЛКОМ");

            if (Game.Buttons.ExistsInParagraph(actionName: "Fight") && (Character.Protagonist.Transformation > 0) && !alreadyTransform)
                staticButtons.Add("ОБРАТИТЬСЯ МЕДВЕДЕМ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ЛЕЧИЛКА")
            {
                Character.Protagonist.Hitpoints += 6;
                Character.Protagonist.Spells.Remove("ЛЕЧЕНИЕ");

                return true;
            }
            else if (action == "ЭЛИКСИР")
            {
                Character.Protagonist.Hitpoints = Character.Protagonist.MaxHitpoints;
                Character.Protagonist.Elixir -= 1;

                return true;
            }
            else if (action == "ОБРАТИТЬСЯ ВОЛКОМ")
            {
                Character.Protagonist.Transformation -= 1;
                Character.Protagonist.Hitpoints -= 2;
                NextFightWithWolf = true;

                return true;
            }
            else if (action == "ОБРАТИТЬСЯ МЕДВЕДЕМ")
            {
                Character.Protagonist.Transformation -= 1;
                Character.Protagonist.Hitpoints -= 2;
                NextFightWithBear = true;

                return true;
            }

            return false;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ЗОЛОТО >=") && (level > Character.Protagonist.Gold))
                    return false;

                if (option.Contains("СИЛА >=") && (level > Character.Protagonist.Strength))
                    return false;
            }
            else if (option == "ВОЛК")
            {
                return Character.Protagonist.Transformation > 0;
            }
            else if (option == "МЕДВЕДЬ")
            {
                return (Character.Protagonist.Transformation > 0) && !Game.Option.IsTriggered("Taboo");
            }
            else if (Character.Protagonist.Spells.Contains(option))
            {
                return true;
            }
            else if (option.Contains("ВОИН") || option.Contains("МАГ") || option.Contains("МЕТАТЕЛЬ"))
            {
                string type = option.Replace("!", String.Empty);
                Character.SpecializationType spec = Constants.GetSpecializationType()[type];

                bool result = option.Contains("!") ?
                    (Character.Protagonist.Specialization != spec) : (Character.Protagonist.Specialization == spec);

                return result;
            }
            else if (option.Contains("|"))
            {
                foreach (string opt in option.Split('|'))
                {
                    if (Game.Option.IsTriggered(opt.Trim()))
                        return true;
                }

                return false;
            }
            else if (option.Contains(","))
            {
                foreach (string opt in option.Split(','))
                {
                    if (!Game.Option.IsTriggered(opt.Trim()))
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

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, woundLine = 0, wounded = 0, death = 0, roundsWin = 0, incrementWounds = 2;
            bool warriorFight = (Character.Protagonist.Specialization == Character.SpecializationType.Warrior);
            bool wolf = false, bear = false;

            List<Character> FightEnemies = new List<Character>();

            if (NextFightWithWolf)
            {
                fight.Add("BOLD|Вы принимаете этот бой перекинувшись в волка. Это уменьшает наносимый вам урон в два раза!");
                NextFightWithWolf = false;
                wolf = true;
            }
            else if (NextFightWithBear)
            {
                fight.Add("BOLD|Вы принимаете этот бой перекинувшись в медведя. Это даёт вам Силу равную 14!");
                NextFightWithBear = false;
                bear = true;
            }

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool block = EvenWound || ReactionFight;
                bool reactionFail = false;

                if ((Character.Protagonist.Specialization == Character.SpecializationType.Thrower) && (round == 1) && !block)
                {
                    fight.Add("BOLD|Вы бросаете метательные ножи");

                    int wound = (Game.Option.IsTriggered("PoisonedBlade") ? 4 : 3);

                    FightEnemies[0].Hitpoints -= wound;

                    fight.Add($"GOOD|{FightEnemies[0].Name} ранен метательными ножами и потерял {wound} жизни");
                    fight.Add(String.Empty);

                    if (Fights.EnemyLostFight(FightEnemies, ref fight, WoundsLimit, Invincible, Poison, Regeneration))
                        return fight;
                }

                foreach (Character enemy in FightEnemies)
                {
                    if ((enemy.Hitpoints <= 0) && !Invincible)
                        continue;

                    if (Regeneration && (round % 4 == 0))
                    {
                        int baseHitpoints = Enemies.Where(x => x.Name == enemy.Name).FirstOrDefault().Hitpoints;

                        if (enemy.Hitpoints < baseHitpoints)
                        {
                            enemy.Hitpoints += 1;
                            fight.Add($"BOLD|{enemy.Name} восстановил 1 жизнь");
                        }
                    }

                    if (!Invincible)
                        fight.Add($"{enemy.Name} (жизни: {enemy.Hitpoints})");

                    int protagonistStrength = (bear ? 14 : Character.Protagonist.Strength);

                    Game.Dice.DoubleRoll(out int firstProtagonistRoll, out int secondProtagonistRoll);
                    int protagonistHitStrength = firstProtagonistRoll + secondProtagonistRoll + protagonistStrength;

                    fight.Add($"Ваш удар: " +
                        $"{Game.Dice.Symbol(firstProtagonistRoll)} + " +
                        $"{Game.Dice.Symbol(secondProtagonistRoll)} + " +
                        $"{protagonistStrength} = {protagonistHitStrength}");

                    Game.Dice.DoubleRoll(out int firstEnemyRoll, out int secondEnemyRoll);
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Strength;

                    fight.Add($"Его удар: " +
                        $"{Game.Dice.Symbol(firstEnemyRoll)} + " +
                        $"{Game.Dice.Symbol(secondEnemyRoll)} + " +
                        $"{enemy.Strength} = {enemyHitStrength}");

                    if (ReactionFight)
                        reactionFail = !Reactions.Good(ref fight, showResult: true);

                    if (TailAttack && (firstEnemyRoll == secondEnemyRoll))
                    {
                        fight.Add("BAD|Аллигатор ударил хвостом! Вы теряете 5 жизней!");

                        Character.Protagonist.Hitpoints -= Fights.HitWounds(ref fight, 5, wolf);
                        TailAttack = false;

                        woundLine = 0;

                        if (Character.Protagonist.Hitpoints <= 0)
                            return Fights.LostFight(fight);
                    }
                    else if (warriorFight && (firstProtagonistRoll == secondProtagonistRoll) && (firstProtagonistRoll == 6) && (!ReactionFight || !reactionFail))
                    {
                        fight.Add("BOLD|Вы сделали 'Крыло ястреба'!");

                        enemy.Hitpoints /= 2;
                        woundLine += 1;

                        fight.Add($"GOOD|{enemy.Name} ранен на половину своих жизней");
                    }
                    else if ((protagonistHitStrength > enemyHitStrength) && (!ReactionFight || !reactionFail))
                    {
                        int woundLevel = (Fights.IsMagicBlade() ? 3 : 2);

                        if (EvenWound)
                        {
                            int woundDice = Game.Dice.Roll();

                            if (woundDice % 2 == 0)
                            {
                                enemy.Hitpoints -= woundLevel;
                                woundLine += 1;

                                fight.Add($"Бросок на пробитие: {Game.Dice.Symbol(woundDice)} - чётное");
                                fight.Add($"GOOD|{enemy.Name} ранен");
                            }
                            else
                            {
                                fight.Add($"Бросок на пробитие: {Game.Dice.Symbol(woundDice)} - нечётное");
                                fight.Add($"BAD|Вы не смогли пробить защиту {enemy.Name}");
                            }
                        }
                        else
                        {
                            fight.Add($"GOOD|{enemy.Name} ранен");

                            woundLine += 1;
                            roundsWin += 1;

                            if (Fights.IsPoisonedBlade())
                            {
                                enemy.Hitpoints -= 5;
                                fight.Add($"BOLD|Из-за яда, рана отнимает у {enemy.Name} сразу 5 жизней");
                            }
                            else
                                enemy.Hitpoints -= woundLevel;
                        }

                        if (enemy.Hitpoints <= 0)
                            death += 1;

                        if (Fights.EnemyLostFight(FightEnemies, ref fight, WoundsLimit, Invincible, Poison, Regeneration, wounded))
                            return fight;
                    }
                    else if ((protagonistHitStrength < enemyHitStrength) || reactionFail)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");

                        wounded += 1;
                        woundLine = 0;

                        if (!String.IsNullOrEmpty(ReactionWounds))
                        {
                            string[] wounds = ReactionWounds.Split('-');
                            int wound = int.Parse(Reactions.Good(ref fight, showResult: true) ? wounds[0] : wounds[1]);

                            Character.Protagonist.Hitpoints -= Fights.HitWounds(ref fight, wound, wolf);

                            if (wound > 0)
                                fight.Add($"{enemy.Name} нанёс урон: {wound}");
                            else
                                fight.Add($"GOOD|{enemy.Name} не нанёс вам урона");
                        }
                        else if (IncrementWounds)
                        {
                            fight.Add($"{enemy.Name} нанёс урон: {incrementWounds}");

                            Character.Protagonist.Hitpoints -= Fights.HitWounds(ref fight, incrementWounds, wolf);

                            incrementWounds *= 2;
                        }
                        else
                            Character.Protagonist.Hitpoints -= Fights.HitWounds(ref fight, (Wound > 0 ? Wound : 2), wolf);

                        if (Character.Protagonist.Hitpoints <= 0)
                            return Fights.LostFight(fight);
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
                        return Fights.LostFight(fight);
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
