using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Abstract
{
    interface ICharacter
    {
        void Init();

        string Save();

        void Load(string saveLine);
    }
}
