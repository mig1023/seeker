using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Prototypes
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public string Name { get; set; }

        public bool IsProtagonist { get; set; }

        public int WayBack { get; set; }

        public virtual bool ThisIsProtagonist() =>
            IsProtagonist;

        public virtual void Init()
        {
            Name = String.Empty;
            IsProtagonist = true;
        }
            
        public virtual string Save() =>
            String.Empty;

        public virtual void Load(string saveLine) =>
            Game.Services.DoNothing();

        public virtual string Debug()
        {
            string propertiesList = String.Empty;

            foreach (string proterty in this.GetType().GetProperties().Select(x => x.Name).ToList())
            {
                object value = this.GetType().GetProperty(proterty).GetValue(this);
                string data = String.Empty;

                if (value is Dictionary<string, string>)
                {
                    Dictionary<string, string> dict = value as Dictionary<string, string>;
                    string[] dictList = dict.Select(x => x.Key + " = " + x.Value).ToArray();

                    data = String.Join(",", dictList);
                }
                else if (value is Dictionary<string, int>)
                {
                    Dictionary<string, int> dict = value as Dictionary<string, int>;
                    string[] dictList = dict.Select(x => x.Key + " = " + x.Value).ToArray();

                    data = String.Join(",", dictList);
                }
                else if (value is List<string>)
                {
                    data = String.Join(",", (value as List<string>).ToArray());
                }
                else if (value is List<bool>)
                {
                    data = String.Join(",", (value as List<bool>).ToArray());
                }
                else if (value == null)
                {
                    data = "null";
                }
                else
                {
                    data = value.ToString();
                }

                propertiesList += $"{proterty}: {data}\n";
            }

            return propertiesList;
        }
    }
}
