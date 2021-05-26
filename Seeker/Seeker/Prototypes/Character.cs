using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Prototypes
{
    class Character
    {
        public string Name { get; set; }

        public virtual void Init() => Name = String.Empty;

        public virtual string Save() => String.Empty;

        public virtual void Load(string saveLine) => Game.Other.DoNothing();
    }
}
