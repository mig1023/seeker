using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.Genesis
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public void Do() { }
    }
}
