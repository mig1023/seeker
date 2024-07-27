using System;

namespace Seeker.Gamebook.Keperlace
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "MPath")
            {
                var path = ValueString.Split('=');

                if (Character.Protagonist.MPaths.ContainsKey(path[0]))
                    Character.Protagonist.MPaths.Remove(path[0]);

                Character.Protagonist.MPaths.Add(path[0], int.Parse(path[1]));
            }
            else if (Name == "MRemove")
            {
                Character.Protagonist.MPaths.Remove(ValueString);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
