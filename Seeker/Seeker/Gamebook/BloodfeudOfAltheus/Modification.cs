using System;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool IntuitiveSolution { get; set; }

        public override void Do()
        {
            Character protagonist = Character.Protagonist;

            bool patron = DoByValueString("Patron",
                () => protagonist.Patron = ValueString);

            bool wepon = DoByValueString("Weapon",
                () => protagonist.AddWeapons(ValueString));

            bool armour = DoByValueString("Armour",
                () => protagonist.AddArmour(ValueString));

            bool fellInto = DoByValueString("FellIntoFavor",
                () => protagonist.FellIntoFavor(ValueString));

            bool fellOut = DoByValueString("FellOutOfFavor",
                () => protagonist.FellIntoFavor(ValueString, fellOut: true));

            bool indiff = DoByValueString("Indifferent",
                () => protagonist.FellIntoFavor(ValueString, indifferent: true));

            if (patron || wepon || armour || fellInto || fellOut || indiff)
            {
                return;
            }
            else if (Name == "DiceShame")
            {
                protagonist.Shame += Game.Dice.Roll();
            }
            else if (Name == "AresFavor")
            {
                if (protagonist.Patron != "Арес")
                {
                    protagonist.FellIntoFavor("Арес");
                    protagonist.Glory += 3;
                }
                else
                {
                    protagonist.Glory += 5;
                }
            }
            else if (Name == "ApolloDisFavor")
            {
                if (protagonist.Patron != "Аполлон")
                    protagonist.FellIntoFavor("Аполлон", fellOut: true);
                else
                    protagonist.Glory -= 2;
            }
            else if (Name == "PoseidonDisFavor")
            {
                if (protagonist.Patron != "Посейдон")
                {
                    protagonist.FellIntoFavor("Посейдон", fellOut: true);
                    protagonist.Shame += 4;
                }
                else
                {
                    protagonist.Glory -= 6;
                }
            }
            else if (Name == "PoseidonDisFavor2")
            {
                if (protagonist.Patron != "Посейдон")
                    protagonist.FellIntoFavor("Посейдон", fellOut: true);
                else
                    protagonist.Shame += 1;
            }
            else if (Name == "Resurrection")
            {
                if (protagonist.Resurrection > 0)
                {
                    protagonist.Resurrection += Value;
                }
                else if (protagonist.BroochResurrection > 0)
                {
                    protagonist.BroochResurrection += Value;
                }
                    
                protagonist.Glory = 1;
                protagonist.Shame = 0;
            }
            else
            {
                if (IntuitiveSolution && (Value < 0) && Game.Option.IsTriggered("NoPenaltyByIntuitiveSolution"))
                    return;

                if (IntuitiveSolution && (Value < 0) && (Name == "Glory") && Game.Option.IsTriggered("NoGloryPenaltyByIntuitiveSolution"))
                    return;

                int currentValue = GetProperty(protagonist, Name);

                if ((Name != "Glory") || (protagonist.Glory > 0))
                    currentValue += Value;

                SetProperty(protagonist, Name, currentValue);
            }
        }
    }
}
