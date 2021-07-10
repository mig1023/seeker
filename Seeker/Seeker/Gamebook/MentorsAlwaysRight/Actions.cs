using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }

        public Character.SpecializationType? Specialization { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
            String.Format("Обращений: {0}", Character.Protagonist.Transformation),
            String.Format("Золото: {0}", Character.Protagonist.Gold),
        };

        public override bool IsButtonEnabled()
        {
            bool bySpecButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

            return !(bySpecButton || byPrice);
        }

        public List<string> Get()
        {
            if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    Character.Protagonist.Strength += 1;

                else if (Specialization == Character.SpecializationType.Thrower)
                    Character.Protagonist.Magicpoints += 1;

                else
                {
                    Character.Protagonist.Magicpoints += 2;
                    Character.Protagonist.Transformation += 2;
                }
            }

            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }
    }
}
