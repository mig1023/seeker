using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public bool IntuitiveSolution { get; set; }

        public void Do()
        {
            if (!String.IsNullOrEmpty(ValueString) && (Name == "Patron"))
                Character.Protagonist.Patron = ValueString;

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Weapon"))
                Character.Protagonist.AddWeapons(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Armour"))
                Character.Protagonist.AddArmour(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "FellIntoFavor"))
                Character.Protagonist.FellIntoFavor(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "FellOutOfFavor"))
                Character.Protagonist.FellIntoFavor(ValueString, fellOut: true);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Indifferent"))
                Character.Protagonist.FellIntoFavor(ValueString, indifferent: true);

            else if (Name == "DiceShame")
                Character.Protagonist.Shame += Game.Dice.Roll();

            else if (Name == "AresFavor")
            {
                if (Character.Protagonist.Patron != "Арес")
                {
                    Character.Protagonist.FellIntoFavor("Арес");
                    Character.Protagonist.Glory += 3;
                }
                else
                    Character.Protagonist.Glory += 5;
            }

            else if (Name == "ApolloDisFavor")
            {
                if (Character.Protagonist.Patron != "Аполлон")
                    Character.Protagonist.FellIntoFavor("Аполлон", fellOut: true);
                else
                    Character.Protagonist.Glory -= 2;
            }

            else if (Name == "PoseidonDisFavor")
            {
                if (Character.Protagonist.Patron != "Посейдон")
                {
                    Character.Protagonist.FellIntoFavor("Посейдон", fellOut: true);
                    Character.Protagonist.Shame += 4;
                }
                else
                    Character.Protagonist.Glory -= 6;
            }

            else if (Name == "PoseidonDisFavor2")
            {
                if (Character.Protagonist.Patron != "Посейдон")
                    Character.Protagonist.FellIntoFavor("Посейдон", fellOut: true);
                else
                    Character.Protagonist.Shame += 1;
            }

            else if (Name == "Resurrection")
            {
                if (Character.Protagonist.Resurrection > 0)
                    Character.Protagonist.Resurrection += Value;

                else if (Character.Protagonist.BroochResurrection > 0)
                    Character.Protagonist.BroochResurrection += Value;

                Character.Protagonist.Glory = 1;
                Character.Protagonist.Shame = 0;
            }

            else if (!(IntuitiveSolution && (Character.Protagonist.NoIntuitiveSolutionPenalty > 0)))
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);
                
                if (!((Name == "Glory") && (Character.Protagonist.Glory <= 0)))
                    currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
