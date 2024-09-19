using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Weapon
    {
        public enum NextAction
        {
            Continue,
            Change,
            Recharge,
            GetCloser,
            MoveAway,
        }

        public string Name { get; set; }

        private int _cartridges;
        public int Cartridges
        {
            get => _cartridges;
            set => _cartridges = Game.Param.Setter(value);
        }

        public int Magazine { get; set; }

        public int Damage { get; set; }

        public int DistanceMin { get; set; }

        public int DistanceMax { get; set; }

        public int Recharge { get; set; }

        public int RechargeTimer { get; set; }

        public bool ColdWeapon { get => this.Recharge == 0; }

        public bool IsSuitable { get => this.Cartridges > 0 || this.Recharge == 0; }


        public Weapon(string line)
        {
            var lines = line.Split(',');
            var distance = lines[3].Split('-');

            this.Name = lines[0];
            this.Magazine = int.Parse(lines[1]);
            this.Cartridges = int.Parse(lines[1]);
            this.Damage = int.Parse(lines[2]);
            this.Recharge = int.Parse(lines[4]);

            if (distance.Length > 1)
            {
                this.DistanceMin = int.Parse(distance[0]);
                this.DistanceMax = int.Parse(distance[1]);
            }
            else
            {
                this.DistanceMin = int.Parse(distance[0]);
                this.DistanceMax = this.DistanceMin;
            }
        }

        public string Save()
        {
            string distances = $"{this.DistanceMin}-{this.DistanceMax}";
            string weapon = string.Join(",", Name, Magazine, Damage, distances, Recharge);

            return weapon;
        }

        public static NextAction ChooseWeapon(Character character, Character enemy)
        {
            int distance = character.Distance + enemy.Distance;

            if (character.CurrentWeapon?.IsSuitable ?? false)
            {
                if (!character.CurrentWeapon.ColdWeapon)
                {
                    return NextAction.Continue;
                }
                else if (distance > 0)
                {
                    return NextAction.GetCloser;
                }
            }
            else if (character.CurrentWeapon?.RechargeTimer > 0)
            {
                character.CurrentWeapon.RechargeTimer -= 1;
                return NextAction.Recharge;
            }
            else if ((character.CurrentWeapon?.Cartridges == 0) && (character.CurrentWeapon.Recharge > 0))
            {
                int dice = Game.Dice.Roll();

                if (dice % 2 == 0 || (character.Weapons.Count == 1))
                {
                    character.CurrentWeapon.RechargeTimer = character.CurrentWeapon.Recharge;
                    return NextAction.Recharge;
                }
            }

            character.CurrentWeapon = null;

            var weapon = character.Weapons
                .Where(x => x.DistanceMax >= distance)
                .Where(x => x.DistanceMin <= distance)
                .Where(x => x.IsSuitable)
                .OrderByDescending(x => x.Damage)
                .FirstOrDefault();

            if (weapon != null)
            {
                character.CurrentWeapon = weapon;
                return NextAction.Change;
            }
            else
            {
                var anyReadyWeapon = character.Weapons
                    .Where(x => x.Cartridges > 0 || x.ColdWeapon)
                    .OrderByDescending(x => x.Damage)
                    .FirstOrDefault();

                if (anyReadyWeapon == null)
                {
                    var anyWeapon = character.Weapons
                        .FirstOrDefault();

                    character.CurrentWeapon = anyWeapon;
                    return NextAction.Recharge;
                }
                else
                {
                    character.CurrentWeapon = anyReadyWeapon;

                    if (distance > anyReadyWeapon.DistanceMax)
                    {
                        return NextAction.GetCloser;
                    }
                    else
                    {
                        return NextAction.MoveAway;
                    }
                }
            }
        }
    }
}
