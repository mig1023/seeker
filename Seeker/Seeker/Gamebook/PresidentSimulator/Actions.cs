using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() => new List<string>
        {
            $"Год: {Character.Protagonist.Year}",
            $"Рейтинг: {Character.Protagonist.Rating}%",
            $"Монетки: {Character.Protagonist.Money}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Лояльность бизнеса: {Character.Protagonist.BusinessLoyalty}",
            $"Лояльность армии: {Character.Protagonist.ArmyLoyalty}",
            $"Отношения с США: {Character.Protagonist.RelationWithUSA}",
            $"Отношения с СССР: {Character.Protagonist.RelationWithUSSR}",
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

                        if (!Services.LevelAvailability("РЕЙТИНГ", line, Character.Protagonist.Rating, level))
                            return false;

                        else if (!Services.LevelAvailability("ГОД", line, Character.Protagonist.Year, level))
                            return false;

                        else if (!Services.LevelAvailability("ОТНОШЕНИЯ С США", line, Character.Protagonist.RelationWithUSA, level))
                            return false;

                        else if (!Services.LevelAvailability("ОТНОШЕНИЯ С СССР", line, Character.Protagonist.RelationWithUSSR, level))
                            return false;

                        else if (!Services.LevelAvailability("МОНЕТКИ", line, Character.Protagonist.Money, level))
                            return false;

                        else if (!Services.LevelAvailability("ЛОЯЛЬНОСТЬ БИЗНЕСА", line, Character.Protagonist.BusinessLoyalty, level))
                            return false;

                        else if (!Services.LevelAvailability("ЛОЯЛЬНОСТЬ АРМИИ", line, Character.Protagonist.ArmyLoyalty, level))
                            return false;

                        else if (!Services.LevelAvailability("СИЛА ВОЙСК", line, Character.Protagonist.Army, level))
                            return false;

                        else if (!Services.LevelAvailability("СИЛА ПОВСТАНЦЕВ", line, Character.Protagonist.Rebels, level))
                            return false;

                        else if (!Services.LevelAvailability("АГРАРНАЯ РЕФОРМА", line, Character.Protagonist.AgrarianReform, level))
                            return false;
                    }
                    else if (line == "СИЛЫ ВОЙСК И ПОВСТАНЦЕВ РАВНЫ")
                    {
                        return Character.Protagonist.Army == Character.Protagonist.Rebels;
                    }
                    else if (line == "СИЛЫ ВОЙСК НЕ МЕНЬШЕ СИЛЫ ПОВСТАНЦЕВ")
                    {
                        return Character.Protagonist.Army >= Character.Protagonist.Rebels;
                    }
                    else if (line == "СИЛЫ ВОЙСК МЕНЬШЕ СИЛЫ ПОВСТАНЦЕВ")
                    {
                        return Character.Protagonist.Army < Character.Protagonist.Rebels;
                    }
                    else if (line == "СИЛЫ ВОЙСК БОЛЬШЕ СИЛЫ ПОВСТАНЦЕВ")
                    {
                        int level = Services.LevelParse(line);
                        return Character.Protagonist.Army > Character.Protagonist.Rebels + level;
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
    }
}
