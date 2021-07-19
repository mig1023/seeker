using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

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

        public Abstract.IModification Damage { get; set; }

        public Character.SpecializationType? Specialization { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
            String.Format("Обращений: {0}", Character.Protagonist.Transformation),
            String.Format("Золото: {0}", Character.Protagonist.Gold),
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (Character.Protagonist.Spells == null)
                return null;

            foreach (string spell in Character.Protagonist.Spells)
            {
                if (currentSpells.ContainsKey(spell.ToLower()))
                    currentSpells[spell.ToLower()] += 1;
                else
                    currentSpells.Add(spell.ToLower(), 1);
            }

            if (currentSpells.Count <= 0)
                return null;

            List<string> statusLines = new List<string>();

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(x => x))
                statusLines.Add(String.Format("{0}: {1}", spell.ToUpper(), currentSpells[spell]));

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
            else if ((Name == "Get") && ThisIsSpell)
            {
                int count = Character.Protagonist.Spells.Where(x => x == Text).Count();
                return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" ({0} шт)", count) : String.Empty)) };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                if (enemy.Hitpoints > 0)
                    enemies.Add(String.Format("{0}\nсила {1}  жизни {2}", enemy.Name, enemy.Strength, enemy.Hitpoints));
                else
                    enemies.Add(String.Format("{0}\nсила {1}", enemy.Name, enemy.Strength));

            return enemies;
        }

        public override bool IsButtonEnabled()
        {
            bool bySpell = ThisIsSpell && (Character.Protagonist.Magicpoints <= 0);
            bool bySpecButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

            return !(bySpell || bySpecButton || byPrice || Used);
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Hitpoints, out toEndParagraph, out toEndText);

        public List<string> Get()
        {
            if (ThisIsSpell && (Character.Protagonist.Magicpoints >= 1))
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.SpellsReplica.Add(Text);
                Character.Protagonist.Magicpoints -= 1;
            }
            else if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    Character.Protagonist.Strength += 1;

                else if (Specialization == Character.SpecializationType.Thrower)
                    Character.Protagonist.Magicpoints += 1;

                else
                {
                    Character.Protagonist.Magicpoints += 2;
                    Character.Protagonist.Transformation += 2;
                }
            }

            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }

        public List<string> Reaction()
        {
            List<string> reaction = new List<string>();

            bool goodReaction = GoodReaction(ref reaction);

            reaction.Add(goodReaction ? "BIG|GOOD|СРЕАГИРОВАЛИ :)" : "BIG|BAD|НЕ СРЕАГИРОВАЛИ :(");

            if (goodReaction && (Benefit != null))
                Benefit.Do();

            if (!goodReaction && (Damage != null))
                Damage.Do();

            return reaction;
        }

        public List<string> RestoreSpells()
        {
            Character.Protagonist.Spells = Character.Protagonist.Spells.Where(x => x == "ЛЕЧЕНИЕ").ToList();
            Character.Protagonist.Spells.AddRange(Character.Protagonist.SpellsReplica.Where(x => x != "ЛЕЧЕНИЕ").ToList());

            Character.Protagonist.Gold -= 5;

            return new List<string> { "BIG|GOOD|Вы восстановили заклинания :)", "Лечилка не восстанавливается, как и было сказано" };
        }
        
        public List<string> GetMagicBlade()
        {
            Game.Option.Trigger("MagicSword");
            Character.Protagonist.Gold -= 5;

            return new List<string> { "BIG|GOOD|Ваш меч теперь заколдован :)" };
        }

        private bool GoodReaction(ref List<string> reaction, bool showResult = false)
        {
            int reactionLevel = (int)Math.Floor((double)Character.Protagonist.Hitpoints / 5);
            reaction.Add(String.Format("Уровнь реакции: {0} / 5 = {1}", Character.Protagonist.Hitpoints, reactionLevel));

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            reaction.Add(String.Format("Реакция: {0} {1} {2}", Game.Dice.Symbol(reactionDice), (goodReaction ? "<=" : ">"), reactionLevel));

            if (showResult)
                reaction.Add(goodReaction ? "BOLD|Реакции хватило" : "BOLD|Реакция подвела");

            return goodReaction;
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

            Character.Protagonist.Hitpoints -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        public List<string> CureRabies()
        {
            if (!Character.Protagonist.Spells.Contains("ЛЕЧЕНИЕ"))
                return new List<string> { "BIG|BAD|У вас нет ЛЕЧИЛКИ :(" };

            List<string> cure = new List<string> { };

            Game.Option.Trigger("Rabies", remove: true);
            cure.Add("BIG|GOOD|Вы успешно вылечили болезнь!");

            Character.Protagonist.Hitpoints += 3;
            cure.Add("BOLD|Вы дополнительно получили +3 жизни.");

            return cure;
        }

        private void WinFightEnding(ref List<string> fight, int wounded)
        {
            if (IsPoisonedBlade())
                Game.Option.Trigger("PoisonedBlade", remove: true);

            if (Poison)
            {
                if (wounded > 3)
                {
                    Character.Protagonist.Hitpoints /= 2;

                    fight.Add(String.Empty);
                    fight.Add("BAD|Из-за яда вы теряете половину оставшихся жизней...");
                }

                fight.Add(String.Empty);

                if (Character.Protagonist.Specialization == Character.SpecializationType.Thrower)
                    fight.Add("BOLD|Вы смазали ядом свои метательные ножи, теперь они будут отнимать у противника не 3, а 4 жизни");
                else
                    fight.Add("BOLD|Вы смазали ядом свой меч, в следующем бою он будет отнимать у противника по 5 жизней");

                Game.Option.Trigger("PoisonedBlade");
            }
            else if (Regeneration)
            {
                int hitpointsBonus = Game.Dice.Roll();

                Character.Protagonist.Hitpoints += hitpointsBonus;

                fight.Add(String.Empty);
                fight.Add(String.Format("GOOD|Благодаря крови, попавшей на вас, вы восстановили {0} жизни", hitpointsBonus));
            }

            if (Game.Data.Triggers.Contains("Rabies"))
            {
                Character.Protagonist.Strength -= 1;

                fight.Add(String.Empty);
                fight.Add("BAD|Вы дополнительно теряете 1 Силу за невылеченную болезнь...");
            }
        }

        private bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight, int wounded = 0)
        {

            if (FightEnemies.Where(x => x.Hitpoints > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() > 0)
                return false;

            else if (Invincible)
                return false;

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

        private bool IsPoisonedBlade() => (Game.Data.Triggers.Contains("PoisonedBlade") &&
            (Character.Protagonist.Specialization != Character.SpecializationType.Thrower));

        private bool IsMagicBlade() => Game.Data.Triggers.Contains("MagicSword");

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, woundLine = 0, wounded = 0, death = 0, roundsWin = 0, incrementWounds = 2;
            bool warriorFight = (Character.Protagonist.Specialization == Character.SpecializationType.Warrior);

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool block = EvenWound || ReactionFight;
                bool reactionFail = false;

                if ((Character.Protagonist.Specialization == Character.SpecializationType.Thrower) && (round == 1) && !block)
                {
                    fight.Add("BOLD|Вы бросаете метательные ножи");

                    int wound = (Game.Data.Triggers.Contains("PoisonedBlade") ? 4 : 3);

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

                    int firstHeroRoll = Game.Dice.Roll();
                    int secondHeroRoll = Game.Dice.Roll();
                    int heroHitStrength = firstHeroRoll + secondHeroRoll + Character.Protagonist.Strength;

                    fight.Add(String.Format(
                        "Ваш удар: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(firstHeroRoll), Game.Dice.Symbol(secondHeroRoll),
                        Character.Protagonist.Strength, heroHitStrength));

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

                        Character.Protagonist.Hitpoints -= 5;
                        TailAttack = false;

                        woundLine = 0;

                        if (Character.Protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else if (warriorFight && (firstHeroRoll == secondHeroRoll) && (firstHeroRoll == 6) && (!ReactionFight || !reactionFail))
                    {
                        fight.Add("BOLD|Вы сделали 'Крыло ястреба'!");

                        enemy.Hitpoints /= 2;
                        woundLine += 1;

                        fight.Add(String.Format("GOOD|{0} ранен на половину своих жизней", enemy.Name));
                    }
                    else if ((heroHitStrength > enemyHitStrength) && (!ReactionFight || !reactionFail))
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
                    else if ((heroHitStrength < enemyHitStrength) || reactionFail)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        wounded += 1;
                        woundLine = 0;

                        if (!String.IsNullOrEmpty(ReactionWounds))
                        {
                            string[] wounds = ReactionWounds.Split('-');
                            int wound = int.Parse(GoodReaction(ref fight, showResult: true) ? wounds[0] : wounds[1]);

                            Character.Protagonist.Hitpoints -= wound;

                            if (wound > 0)
                                fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, wound));
                            else
                                fight.Add(String.Format("GOOD|{0} не нанёс вам урона", enemy.Name));
                        }
                        else if (IncrementWounds)
                        {
                            fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, incrementWounds));

                            Character.Protagonist.Hitpoints -= incrementWounds;

                            incrementWounds *= 2;
                        }
                        else
                            Character.Protagonist.Hitpoints -= (Wound > 0 ? Wound : 2);

                        if (Character.Protagonist.Hitpoints <= 0)
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
