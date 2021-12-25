using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Prototypes
{
    class Character
    {
        public string Name { get; set; }

        public virtual void Init() => Name = String.Empty;

        public virtual string Save() => String.Empty;

        public virtual void Load(string saveLine) => Game.Other.DoNothing();

        public virtual string Debug()
        {
            string propertiesList = String.Empty;

            foreach (string proterty in this.GetType().GetProperties().Select(x => x.Name).ToList())
            {
                object value = this.GetType().GetProperty(proterty).GetValue(this);

                if (value is Dictionary<string, string>)
                {
                    propertiesList += String.Format("{0}: {1}\n",
                        (value as Dictionary<string, string>).Select(x => x.Key + " = " + x.Value).ToArray());
                }
                else if (value is Dictionary<string, int>)
                {
                    propertiesList += String.Format("{0}: {1}\n",
                        (value as Dictionary<string, int>).Select(x => x.Key + " = " + x.Value).ToArray());
                }
                else if (value is List<string>)
                {
                    propertiesList += String.Format("{0}: {1}\n",
                        proterty, String.Join(",", (value as List<string>).ToArray()));
                }
                else if (value is List<bool>)
                {
                    propertiesList += String.Format("{0}: {1}\n",
                        proterty, String.Join(",", (value as List<bool>).ToArray()));
                }
                else
                {
                    propertiesList += String.Format("{0}: {1}\n",
                        proterty, value);
                }
            }

            return propertiesList;
        }
    }
}
