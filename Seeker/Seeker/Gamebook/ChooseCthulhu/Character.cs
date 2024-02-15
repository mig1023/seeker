using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        private int _initiation;
        public int Initiation
        {
            get => _initiation;
            set
            {
                if (IsCursed() && (value < 1))
                    value = 1;

                _initiation = Game.Param.Setter(value, _initiation, this);
            }
        }

        public List<int> BackColor { get; set; }
        public List<int> BtnColor { get; set; }

        public override void Init()
        {
            base.Init();

            Initiation = IsCursed() ? 1 : 0;

            BackColor = new List<int> { 112, 144, 167 };
            BtnColor = new List<int> { 50, 88, 100 };
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Initiation = this.Initiation,
        };

        public override string Save()
        {
            string backColor = String.Join(",", BackColor);
            string btnColor = String.Join(",", BtnColor);

            return String.Join("|", Initiation, backColor, btnColor);
        }

        public bool IsCursed()
        {
            if (App.Current.Properties.TryGetValue("ChooseCthulhu_Cursed", out object value))
                return (value as string) == "1";
            else
                return false;
        }
           
        public void Cursed() =>
            App.Current.Properties["ChooseCthulhu_Cursed"] = "1";

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Initiation = int.Parse(save[0]);

            BackColor = save[1].Split(',').Select(x => int.Parse(x)).ToList();
            BtnColor = save[2].Split(',').Select(x => int.Parse(x)).ToList();

            IsProtagonist = true;
        }
    }
}
