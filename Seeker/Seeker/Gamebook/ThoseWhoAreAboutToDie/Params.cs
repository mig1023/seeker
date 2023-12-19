﻿using System;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Params
    {
        public static bool Fail(string oneOption)
        {
            if (ParamFail("СИЛА", oneOption, Character.Protagonist.Strength))
            {
                return true;
            }
            else if (ParamFail("РЕАКЦИЯ", oneOption, Character.Protagonist.Reaction))
            {
                return true;
            }
            else if (ParamFail("ВЫНОСЛИВОСТЬ", oneOption, Character.Protagonist.Endurance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ParamFail(string paramName, string option, int param)
        {
            int level = Game.Services.LevelParse(option);

            if (option.Contains($"{paramName} >") && (level >= param))
            {
                return true;
            }
            else if (option.Contains($"{paramName} <=") && (level < param))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
