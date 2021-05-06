using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do() { }
    }
}
