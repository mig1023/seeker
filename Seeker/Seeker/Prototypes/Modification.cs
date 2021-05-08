using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Prototypes
{
    class Modification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public virtual void Do() { }
    }
}
