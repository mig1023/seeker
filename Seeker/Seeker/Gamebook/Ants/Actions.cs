using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Ants
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (protagonist.EnemyHitpoints > 0)
                statusLines.Add(String.Format("{0}: {1}", protagonist.EnemyName, protagonist.EnemyHitpoints));

            if (protagonist.Defence > 0)
                statusLines.Add(String.Format("Защита: {0}", protagonist.Defence));

            statusLines.Add(String.Format("Прирост: {0}", protagonist.Increase));
            statusLines.Add(String.Format("Количество: {0}", protagonist.Quantity));

            return statusLines;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<") || oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ДАЙС =") && !protagonist.Dice[level])
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО >=") && (level > protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО <") && (level <= protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("ВРАГ >=") && (level > protagonist.EnemyHitpoints))
                            return false;

                        if (oneOption.Contains("ВРАГ <") && (level <= protagonist.EnemyHitpoints))
                            return false;

                        if (oneOption.Contains("ЗАЩИТА >=") && (level > protagonist.Defence))
                            return false;

                        if (oneOption.Contains("СТАРТ =") && (protagonist.Start != level))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public List<string> Result()
        {
            List<string> results = new List<string>();

            int currentHead = 0;

            for (int i = 0; i < Constants.Heads.Count; i++)
                if (Game.Option.IsTriggered(Constants.Heads[i]))
                    currentHead = i;

            if (currentHead < 3)
            {
                results.Add("Муравейник вырос до гигантских размеров. Высотою в шесть метров и диаметром двадцать, он попал в книгу рекордов Гиннесса.");
                results.Add("Позже группа религиозных фанатиков сожгла муравейник, мотивируя это тем, что продвинутый вид насекомых угрожает человечеству.");
            }
            else
            {
                results.Add("Став доминирующим видом в старом лесу, Формицин Ратус начал экспансию в другие места.");
                results.Add("Прогрессивный вид муравьёв проник в города и расплодился там до чудовищных размеров.");
                results.Add("Мировая экономика сократилась вдвое из - за нашествия новых паразитов.");
            }

            results.Add(String.Empty);

            int speed = 300 - protagonist.Time;

            if (speed < 100)
            {
                results.Add("Затянутое прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Мастер кликанья.");
            }
            else if (speed < 150)
            {
                results.Add("Долгое прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Дезинфектор.");
            }
            else if (speed < 180)
            {
                results.Add("Обычное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Любитель муравьёв.");
            }
            else if (speed < 200)
            {
                results.Add("Стандартное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Друг муравьёв.");
            }
            else if (speed < 210)
            {
                results.Add("Нормальное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный знаток.");
            }
            else if (speed < 220)
            {
                results.Add("Неплохое прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный эксперт.");
            }
            else if (speed < 230)
            {
                results.Add("Хорошое прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Мирмикипер.");
            }
            else if (speed < 250)
            {
                results.Add("Отличное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Энтомолог.");
            }
            else if (speed < 260)
            {
                results.Add("Прекрасное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Мирмеколог.");
            }
            else if (speed < 270)
            {
                results.Add("Изумительное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный барон.");
            }
            else if (speed < 280)
            {
                results.Add("Удивительное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный барон.");
            }
            else if (speed < 285)
            {
                results.Add("Чудесное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный оракул.");
            }
            else if (speed < 290)
            {
                results.Add("Невероятное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный небожитель.");
            }
            else
            {
                results.Add("Легендарное прохождение игры!");
                results.Add("BIG|BOLD|Ваше звание: Муравьиный бог.");
            }

            return results;
        }
    }
}
