using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Weapon
    {
        public string Name { get; set; }

        public int Cartridges { get; set; }

        public int Magazine { get; set; }

        public int Damage { get; set; }

        public int DistanceMin { get; set; }

        public int DistanceMax { get; set; }

        public int Recharge { get; set; }

        public Weapon(string line)
        {
            var lines = line.Split(',');
            var distance = lines[3].Split('-');

            this.Name = lines[0];
            this.Magazine = int.Parse(lines[1]);
            this.Cartridges = int.Parse(lines[1]);
            this.Damage = int.Parse(lines[2]);
            this.DistanceMin = int.Parse(distance[0]);
            this.DistanceMax = int.Parse(distance[1]);
            this.Recharge = int.Parse(lines[4]);
        }

        public string Save()
        {
            string distances = $"{this.DistanceMin}-{this.DistanceMax}";
            string weapon = string.Join(",", Name, Magazine, Damage, distances, Recharge);

            return weapon;
        }

        public static Weapon ChooseWeapon(List<Weapon> weapons, int distance)
        {
            var weapon = weapons
                .Where(x => x.DistanceMax >= distance)
                .Where(x => x.DistanceMin <= distance)
                .Where(x => x.Cartridges > 0)
                .FirstOrDefault();

            return weapon;
        }
    }
}
