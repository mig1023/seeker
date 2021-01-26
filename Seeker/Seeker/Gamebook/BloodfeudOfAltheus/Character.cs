﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.BloodfeudOfAltheus.Character();

        public string Name { get; set; }

        public void Init()
        {
            Name = String.Empty;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
            };
        }

        public string Save()
        {
            return String.Format("{0}", String.Empty);
        }

        public void Load(string saveLine)
        {

        }
    }
}
