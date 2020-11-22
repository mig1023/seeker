using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Interfaces
{
    interface ICharacter
    {
        void Init();

        string Save();

        void Load(string saveLine);
    }
}
