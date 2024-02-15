using System;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        private int _rating;
        public int Rating
        {
            get => _rating;
            set => _rating = Game.Param.Setter(value, max: 100, _rating, this);
        }

        public int Year { get; set; }

        private int _relationWithUSA;
        public int RelationWithUSA
        {
            get => _relationWithUSA;
            set => _relationWithUSA = Game.Param.Setter(value, _relationWithUSA, this);
        }

        private int _relationWithUSSR;
        public int RelationWithUSSR
        {
            get => _relationWithUSSR;
            set => _relationWithUSSR = Game.Param.Setter(value, _relationWithUSSR, this);
        }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value, _money, this);
        }

        private int _businessLoyalty;
        public int BusinessLoyalty
        {
            get => _businessLoyalty;
            set => _businessLoyalty = Game.Param.Setter(value, max: 100, _businessLoyalty, this);
        }

        private int _armyLoyalty;
        public int ArmyLoyalty
        {
            get => _armyLoyalty;
            set => _armyLoyalty = Game.Param.Setter(value, max: 100, _armyLoyalty, this);
        }

        private int _army;
        public int Army
        {
            get => _army;
            set => _army = Game.Param.Setter(value, _army, this);
        }
        
        private int _rebels;
        public int Rebels
        {
            get => _rebels;
            set => _rebels = Game.Param.Setter(value, _rebels, this);
        }

        private int _agrarianReform;
        public int AgrarianReform
        {
            get => _agrarianReform;
            set => _agrarianReform = Game.Param.Setter(value, _agrarianReform, this);
        }

        public override void Init()
        {
            base.Init();

            Rating = 57;
            Year = 1979;
            RelationWithUSA = 6;
            RelationWithUSSR = 4;
            Money = 1;
            BusinessLoyalty = 70;
            ArmyLoyalty = 70;
            Army = 7;
            Rebels = 4;
            AgrarianReform = 0;

            Game.Option.Trigger("Повстанцы-коммунисты");
            Game.Option.Trigger("Аграрный вопрос");
            Game.Option.Trigger("AmericanOil");
            Game.Option.Trigger("Ультраправые террористы");
            Game.Option.Trigger("Сильные профсоюзы");
            Game.Option.Trigger("Наркотрафик");
            Game.Option.Trigger("Смертная казнь");
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Rating = this.Rating,
            Year = this.Year,
            RelationWithUSA = this.RelationWithUSA,
            RelationWithUSSR = this.RelationWithUSSR,
            Money = this.Money,
            BusinessLoyalty = this.BusinessLoyalty,
            ArmyLoyalty = this.ArmyLoyalty,
            Army = this.Army,
            Rebels = this.Rebels,
            AgrarianReform = this.AgrarianReform,
        };

        public override string Save() => String.Join("|",
            Rating, Year, RelationWithUSA, RelationWithUSSR, Money,
            BusinessLoyalty, ArmyLoyalty, Army, Rebels, AgrarianReform);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Rating = int.Parse(save[0]);
            Year = int.Parse(save[1]);
            RelationWithUSA = int.Parse(save[2]);
            RelationWithUSSR = int.Parse(save[3]);
            Money = int.Parse(save[4]);
            BusinessLoyalty = int.Parse(save[5]);
            ArmyLoyalty = int.Parse(save[6]);
            Army = int.Parse(save[7]);
            Rebels = int.Parse(save[8]);
            AgrarianReform = int.Parse(save[9]);

            IsProtagonist = true;
        }
    }
}
