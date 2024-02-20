using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {protagonist.Skill}",
            $"Сила: {protagonist.Strength}/{protagonist.MaxStrength}",
            $"Удар: {protagonist.Damage}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Наблюдательность: {protagonist.Skill}",
            $"Деньги: {MoneyFormat(protagonist.Money)}",
            $"Магия: {protagonist.Magic}",
        };

        private static string MoneyFormat(int ecu) =>
            String.Format("{0:f1}", (double)ecu / 10).TrimEnd('0').TrimEnd(',').Replace(',', '.');
    }
}
