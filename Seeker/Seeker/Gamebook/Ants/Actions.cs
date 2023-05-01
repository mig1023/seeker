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

            statusLines.Add(String.Format("Количество: {0}", protagonist.Quantity));
            statusLines.Add(String.Format("Прирост: {0}", protagonist.Increase));

            if (protagonist.Defence > 0)
                statusLines.Add(String.Format("Защита: {0}", protagonist.Defence));

            if (protagonist.EnemyHitpoints > 0)
                statusLines.Add(String.Format("{0}: {1}", protagonist.EnemyName, protagonist.EnemyHitpoints));

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

            bool queen = Game.Option.IsTriggered("Королева Антуанетта Плодовитая");
            bool prince = Game.Option.IsTriggered("Принц Мурадин Крылатый");
            bool soldier = Game.Option.IsTriggered("Солдат Руф Твердожвалый");

            if (queen || prince || soldier)
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

            string line = String.Empty;

            foreach (KeyValuePair<string, int> timelist in Constants.Rating.OrderBy(x => x.Value))
            {
                if (speed < timelist.Value)
                    break;
                else
                    line = timelist.Key;
            }

            List<string> resultLines = line.Split('!').ToList();

            results.Add(String.Format("{0}!", resultLines[0]));
            results.Add(resultLines[1]);

            return results;
        }

        public List<string> Changes()
        {
            List<string> changes = new List<string>();

            foreach (string head in Constants.Government.Keys)
                if (Game.Option.IsTriggered(head))
                    changes.Add(Constants.Government[head]);

            return changes;
        }
    }
}
