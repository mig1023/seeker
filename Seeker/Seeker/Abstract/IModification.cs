using System;

namespace Seeker.Abstract
{
    public interface IModification
    {
        string Name { get; set; }
        int Value { get; set; }
        string ValueString { get; set; }

        void Do();

        void Do(Abstract.ICharacter Character);
    }
}
