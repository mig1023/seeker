using System;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            bool patron = DoByValueString("Patron",
                () => Character.Protagonist.Patron = ValueString);

            bool wepon = DoByValueString("Weapon",
                () => Character.Protagonist.AddWeapons(ValueString));

            bool armour = DoByValueString("Armour",
                () => Character.Protagonist.AddArmour(ValueString));

            bool fellInto = DoByValueString("FellIntoFavor",
                () => Character.Protagonist.FellIntoFavor(ValueString));

            bool fellOut = DoByValueString("FellOutOfFavor",
                () => Character.Protagonist.FellIntoFavor(ValueString, fellOut: true));

            bool indiff = DoByValueString("Indifferent",
                () => Character.Protagonist.FellIntoFavor(ValueString, indifferent: true));

            if (patron || wepon || armour || fellInto || fellOut || indiff)
            {
                return;
            }
            else if (Name == "DiceShame")
            {
                Character.Protagonist.Shame += Game.Dice.Roll();
            }
            else if (Name == "AresFavor")
            {
                if (Character.Protagonist.Patron != "Арес")
                {
                    Character.Protagonist.FellIntoFavor("Арес");
                    Character.Protagonist.Glory += 3;
                }
                else
                {
                    Character.Protagonist.Glory += 5;
                }
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
                {
                    Character.Protagonist.Glory -= 6;
                }
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
                {
                    Character.Protagonist.Resurrection += Value;
                }
                else if (Character.Protagonist.BroochResurrection > 0)
                {
                    Character.Protagonist.BroochResurrection += Value;
                }
                    
                Character.Protagonist.Glory = 1;
                Character.Protagonist.Shame = 0;
            }
            else
            {
                if (Name.StartsWith("Intuitive"))
                {
                    bool noIntuitivePenalty = Game.Option.IsTriggered("NoIntuitivePenalty");
                    bool noIntuitiveGloryPenalty = Game.Option.IsTriggered("NoIntuitiveGloryPenalty");

                    if (noIntuitivePenalty)
                    {
                        return;
                    }
                    else if ((Name == "IntuitiveGlory") && noIntuitiveGloryPenalty)
                    {
                        return;
                    }
                    else
                    {
                        Name = Name.Replace("Intuitive", String.Empty);
                    }
                }

                int currentValue = GetProperty(Character.Protagonist, Name);

                if ((Name != "Glory") || (Character.Protagonist.Glory > 0))
                    currentValue += Value;

                SetProperty(Character.Protagonist, Name, currentValue);
            }
        }
    }
}
