using System.Collections.Generic;

namespace Seeker.Gamebook.OctopusIsland
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;
    }
}
