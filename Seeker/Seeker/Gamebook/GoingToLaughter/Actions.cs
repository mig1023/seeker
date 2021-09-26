﻿using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public bool Advantage { get; set; }
        public bool Disadvantage { get; set; }

        public List<string> Get()
        {
            if (Advantage)
            {
                protagonist.Advantages.Add(this.Button);
                protagonist.Balance += 1;
            }

            else if (Disadvantage)
            {
                protagonist.Disadvantages.Add(this.Button);
                protagonist.Balance -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override bool IsButtonEnabled()
        {
            if (Advantage && protagonist.Advantages.Contains(this.Button))
                return false;

            else if (Disadvantage && (protagonist.Balance == 0))
                return false;

            else if (Disadvantage && protagonist.Disadvantages.Contains(this.Button))
                return false;

            else
                return true;
        }

        public override bool CheckOnlyIf(string option)
        {
            foreach (string oneOption in option.Split(','))
            {
                if (option.Contains("!"))
                {
                    if (protagonist.Disadvantages.Contains(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!protagonist.Advantages.Contains(option) && !protagonist.Disadvantages.Contains(option))
                    return false;
            };

            return true;
        }
    }
}
