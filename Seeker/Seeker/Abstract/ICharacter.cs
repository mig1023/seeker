﻿using System;

namespace Seeker.Abstract
{
    public interface ICharacter
    {
        void Init();

        string Save();

        void Load(string saveLine);

        string GetName();
    }
}
