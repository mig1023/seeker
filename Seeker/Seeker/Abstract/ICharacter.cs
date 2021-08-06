using System;

namespace Seeker.Abstract
{
    interface ICharacter
    {
        void Init();

        string Save();

        void Load(string saveLine);
    }
}
