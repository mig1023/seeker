using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool IntuitiveSolution { get; set; }

        public override void Do()
        {
            Character hero = Character.Protagonist;

            bool patron = ValueStringModification("Patron", () => hero.Patron = ValueString);
            bool wepon = ValueStringModification("Weapon", () => hero.AddWeapons(ValueString));
            bool armour = ValueStringModification("Armour", () => hero.AddArmour(ValueString));
            bool fellInto = ValueStringModification("FellIntoFavor", () => hero.FellIntoFavor(ValueString));
            bool fellOut = ValueStringModification("FellOutOfFavor", () => hero.FellIntoFavor(ValueString, fellOut: true));
            bool indiff = ValueStringModification("Indifferent", () => hero.FellIntoFavor(ValueString, indifferent: true));

            if (patron || wepon || armour || fellInto || fellOut || indiff)
            {
                // nothing to do here
            }
            else if (Name == "DiceShame")
                hero.Shame += Game.Dice.Roll();

            else if (Name == "AresFavor")
            {
                if (hero.Patron != "Арес")
                {
                    hero.FellIntoFavor("Арес");
                    hero.Glory += 3;
                }
                else
                    hero.Glory += 5;
            }

            else if (Name == "ApolloDisFavor")
            {
                if (hero.Patron != "Аполлон")
                    hero.FellIntoFavor("Аполлон", fellOut: true);
                else
                    hero.Glory -= 2;
            }

            else if (Name == "PoseidonDisFavor")
            {
                if (hero.Patron != "Посейдон")
                {
                    hero.FellIntoFavor("Посейдон", fellOut: true);
                    hero.Shame += 4;
                }
                else
                    hero.Glory -= 6;
            }

            else if (Name == "PoseidonDisFavor2")
            {
                if (hero.Patron != "Посейдон")
                    hero.FellIntoFavor("Посейдон", fellOut: true);
                else
                    hero.Shame += 1;
            }

            else if (Name == "Resurrection")
            {
                if (hero.Resurrection > 0)
                    hero.Resurrection += Value;

                else if (hero.BroochResurrection > 0)
                    hero.BroochResurrection += Value;

                hero.Glory = 1;
                hero.Shame = 0;
            }

            else if (!(IntuitiveSolution && (hero.NoIntuitiveSolutionPenalty > 0)))
            {
                int currentValue = (int)hero.GetType().GetProperty(Name).GetValue(hero, null);
                
                if (!((Name == "Glory") && (hero.Glory <= 0)))
                    currentValue += Value;

                hero.GetType().GetProperty(Name).SetValue(hero, currentValue);
            }
        }
    }
}
