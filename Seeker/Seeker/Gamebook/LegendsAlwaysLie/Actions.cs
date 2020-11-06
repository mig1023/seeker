using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Price { get; set; }
        public string Text { get; set; }
        public bool Spell { get; set; }

        public List<Character> Enemies { get; set; }
        public int OnlyRounds { get; set; }
        public int RoundsToWin { get; set; }
        public int AttackWounds { get; set; }
        public string ReactionWounds { get; set; }
        public string ConneryAttacks { get; set; }
        public bool GolemFight { get; set; }

        public Modification Benefit { get; set; }
        public Modification Damage { get; set; }


        public Character.SpecializationType? Specialization { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Representer()
        {
            if (Enemies != null)
            {
                List<string> enemies = new List<string>();

                foreach (Character enemy in Enemies)
                    if (enemy.Hitpoints > 0)
                        enemies.Add(String.Format("{0}\nсила {1}  жизни {2}", enemy.Name, enemy.Strength, enemy.Hitpoints));
                    else
                        enemies.Add(String.Format("{0}\nсила {1}", enemy.Name, enemy.Strength));

                return enemies;
            }

            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            return new List<string> { };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
                String.Format("Заклинаний: {0}", Character.Protagonist.Magicpoints),
                String.Format("Золото: {0}", Character.Protagonist.Gold),
                String.Format("Жизни Коннери: {0}", Character.Protagonist.ConneryHitpoints),
                String.Format("Доверие Коннери: {0}", Character.Protagonist.ConneryTrust),
            };

            return statusLines;
        }

        private bool GoodReaction(ref List<string> reaction)
        {
            int reactionLevel = (int)Math.Floor((double)Character.Protagonist.Hitpoints / 5);
            reaction.Add(String.Format("Уровнь реакции: {0} / 5 = {1}", Character.Protagonist.Hitpoints, reactionLevel));

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            reaction.Add(String.Format("Реакция: {0} ⚄ {1} {2}", reactionDice, (goodReaction ? "<=" : ">"), reactionLevel));

            return goodReaction;
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

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать с начала...";

            return false;
        }

        public bool IsButtonEnabled()
        {
            bool disabledSpecializationButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Gold < Price);
            bool disabledBySpellpoints = Spell && (Character.Protagonist.Spellpoints <= 0);
            bool disabledBySpellRepeat = Spell && Character.Protagonist.Spells.Contains(Text);

            bool disabledBySpecialization = Spell && (Text == "ВЗОР") && (Character.Protagonist.Specialization == Character.SpecializationType.Warrior);

            return !(disabledSpecializationButton || disabledByPrice || disabledBySpellpoints || disabledBySpellRepeat || disabledBySpecialization);
        }

        public List<string> Get()
        {
            if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    Character.Protagonist.Strength += 2;
                else if (Specialization == Character.SpecializationType.Wizard)
                {
                    Character.Protagonist.Spellpoints += 3;
                    Character.Protagonist.Magicpoints += 2;
                }
                else
                {
                    Character.Protagonist.Strength += 1;
                    Character.Protagonist.Spellpoints += 1;
                }
            }
            else if (Spell && (Character.Protagonist.Spellpoints >= 1))
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.Spellpoints -= 1;
            }
            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                string[] options = option.Split('|');

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                int optionCount = 0;

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        optionCount += 1;

                return (optionCount >= optionMustBe ? true : false);
            }
            else
            {
                string[] options = option.Split(',');

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        private bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight, bool connery = false)
        {
            bool enemyLost = true;

            foreach (Character e in FightEnemies)
                if (e.Hitpoints > 0)
                    enemyLost = false;

            if (!enemyLost)
                return false;
            else
            {
                fight.Add(String.Empty);

                if (connery)
                    fight.Add("BIG|GOOD|Коннери его добил, вы ПОБЕДИЛИ :)");
                else
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                return true;
            }
        }

        private List<string> LostFight(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
            return fight;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int golemRound = 4;

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    fight.Add(String.Format("{0} (жизни: {1})", enemy.Name, enemy.Hitpoints));

                    if (!String.IsNullOrEmpty(ConneryAttacks))
                    {
                        string[] bonus = ConneryAttacks.Split(',');

                        if ((bonus.Length < 2) || (round > int.Parse(bonus[1])))
                        {
                            int conneryAttack = int.Parse(bonus[0]);
                            enemy.Hitpoints -= conneryAttack;
                            fight.Add(String.Format("GOOD|{0} ранен атакой Коннери", enemy.Name, conneryAttack));

                            if (EnemyLostFight(FightEnemies, ref fight, connery: true))
                                return fight;
                        }
                    }

                    int firstHeroRoll = Game.Dice.Roll();
                    int secondHeroRoll = Game.Dice.Roll();
                    int heroHitStrength = firstHeroRoll + secondHeroRoll + Character.Protagonist.Strength;

                    fight.Add(
                        String.Format(
                            "Ваш удар: {0} ⚄ + {1} ⚄ + {2} = {3}",
                            firstHeroRoll, secondHeroRoll, Character.Protagonist.Strength, heroHitStrength
                        )
                    );

                    int firstEnemyRoll = Game.Dice.Roll();
                    int secondEnemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Strength;

                    fight.Add(
                        String.Format(
                            "Его удар: {0} ⚄ + {1} ⚄ + {2} = {3}",
                            firstEnemyRoll, secondEnemyRoll, enemy.Strength, enemyHitStrength
                        )
                    );

                    if (GolemFight && (heroHitStrength > enemyHitStrength))
                        fight.Add(String.Format("BOLD|Вы отбили все атаки", enemy.Name));
                    else if (heroHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));
                        
                        enemy.Hitpoints -= (AttackWounds > 0 ? AttackWounds : 2);

                        if (EnemyLostFight(FightEnemies, ref fight))
                            return fight;
                    }
                    else if (heroHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        if (String.IsNullOrEmpty(ReactionWounds))
                            Character.Protagonist.Hitpoints -= 2;
                        else
                        {
                            string[] wounds = ReactionWounds.Split('-');
                            int wound = int.Parse(GoodReaction(ref fight) ? wounds[0] : wounds[1]);
                            Character.Protagonist.Hitpoints -= wound;
                            fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, wound));
                        }

                        if (Character.Protagonist.Hitpoints < 0)
                            Character.Protagonist.Hitpoints = 0;

                        if (Character.Protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    if (GolemFight && (golemRound > 0))
                        golemRound -= 1;
                    else if (GolemFight)
                    {
                        golemRound = 4;

                        fight.Add("BOLD|Коннери улучил момент для удара");

                        if (!GoodReaction(ref fight))
                        {
                            fight.Add(String.Format("BAD|Вы не смогли прикрыть Коннери и он ранен", enemy.Name));
                            Character.Protagonist.ConneryHitpoints -= 2;

                            if (Character.Protagonist.ConneryHitpoints <= 0)
                                return LostFight(fight);
                        }
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен атакой Коннери", enemy.Name));
                            enemy.Hitpoints -= 1;

                            if (EnemyLostFight(FightEnemies, ref fight, connery: true))
                                return fight;
                        }
                    }

                    if ((OnlyRounds > 0) && (OnlyRounds <= round))
                    {
                        fight.Add(String.Format("BOLD|Отведённые на бой раунды истекли.", RoundsToWin));
                        return fight;
                    }

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Format("BAD|Отведённые на победу раунды истекли.", RoundsToWin));
                        return LostFight(fight);
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
