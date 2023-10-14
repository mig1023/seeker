using System.Linq;

namespace Seeker.Gamebook.Cyberpunk
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Normalization")
            {
                foreach (string param in Constants.NormalizationParams)
                {
                    if (GetProperty(Character.Protagonist, param) == 0)
                        SetProperty(Character.Protagonist, param, 30);
                }
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
