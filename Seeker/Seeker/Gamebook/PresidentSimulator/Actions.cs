using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            $"Год: {protagonist.Year}",
            $"Рейтинг: {protagonist.Rating}%",
            $"Монетки: {protagonist.Money}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Лояльность бизнеса: {protagonist.BusinessLoyalty}",
            $"Лояльность армии: {protagonist.ArmyLoyalty}",
            $"Отношения с США: {protagonist.RelationWithUSA}",
            $"Отношения с СССР: {protagonist.RelationWithUSSR}",
        };

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
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                int optionCount = options.Where(x => Game.Option.IsTriggered(x.Trim())).Count();

                return optionCount >= optionMustBe;
            }
            else
            {
                foreach (string line in option.Split(','))
                {
                    if (line.Contains(">") || line.Contains("<") || line.Contains("="))
                    {
                        int level = Services.LevelParse(line);

                        if (!Services.LevelAvailability("РЕЙТИНГ", line, protagonist.Rating, level))
                            return false;

                        else if (!Services.LevelAvailability("ГОД", line, protagonist.Year, level))
                            return false;

                        else if (!Services.LevelAvailability("ОТНОШЕНИЯ С США", line, protagonist.RelationWithUSA, level))
                            return false;

                        else if (!Services.LevelAvailability("ОТНОШЕНИЯ С СССР", line, protagonist.RelationWithUSSR, level))
                            return false;

                        else if (!Services.LevelAvailability("МОНЕТКИ", line, protagonist.Money, level))
                            return false;

                        else if (!Services.LevelAvailability("ЛОЯЛЬНОСТЬ БИЗНЕСА", line, protagonist.BusinessLoyalty, level))
                            return false;

                        else if (!Services.LevelAvailability("ЛОЯЛЬНОСТЬ АРМИИ", line, protagonist.ArmyLoyalty, level))
                            return false;

                        else if (!Services.LevelAvailability("СИЛА ВОЙСК", line, protagonist.Army, level))
                            return false;

                        else if (!Services.LevelAvailability("СИЛА ПОВСТАНЦЕВ", line, protagonist.Rebels, level))
                            return false;

                        else if (!Services.LevelAvailability("АГРАРНАЯ РЕФОРМА", line, protagonist.AgrarianReform, level))
                            return false;
                    }
                    else if (line == "СИЛЫ ВОЙСК И ПОВСТАНЦЕВ РАВНЫ")
                    {
                        return protagonist.Army == protagonist.Rebels;
                    }
                    else if (line == "СИЛЫ ВОЙСК НЕ МЕНЬШЕ СИЛЫ ПОВСТАНЦЕВ")
                    {
                        return protagonist.Army >= protagonist.Rebels;
                    }
                    else if (line == "СИЛЫ ВОЙСК МЕНЬШЕ СИЛЫ ПОВСТАНЦЕВ")
                    {
                        return protagonist.Army < protagonist.Rebels;
                    }
                    else if (line == "СИЛЫ ВОЙСК БОЛЬШЕ СИЛЫ ПОВСТАНЦЕВ")
                    {
                        int level = Services.LevelParse(line);
                        return protagonist.Army > protagonist.Rebels + level;
                    }
                    else if (line.Contains("!"))
                    {
                        if (Option.IsTriggered(line.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Option.IsTriggered(line.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public override List<string> TextByProperties(XmlNode text)
        {
            if ((text == null) || (text.InnerText != "[text by propertires]"))
                return null;

            int part = Option.IsTriggered("полугодие") ? 2 : 1;

            if ((protagonist.Year == 1980) && (part == 2))
            {
                part = Option.IsTriggered("Ультраправые террористы") ? 3 : 2;
            }
            else if ((protagonist.Year == 1983) && (part == 1))
            {
                part = Option.IsTriggered("Повстанцы-коммунисты") ? 1 : 3;
            }

            string yearPart = $"{protagonist.Year}-{part}";
            string yearLine = Constants.TextByYears[yearPart];
            return yearLine.Split('|').ToList();
        }
    }
}
