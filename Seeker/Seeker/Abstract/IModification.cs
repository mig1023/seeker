using System;

namespace Seeker.Abstract
{
    interface IModification
    {
        string Name { get; set; }
        int Value { get; set; }
        string ValueString { get; set; }

        void Do();
    }
}
