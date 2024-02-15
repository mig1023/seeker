using System;

namespace Seeker.Gamebook
{
    class Links
    {
        public delegate void ProtagonistMethod();

        public delegate string StringMethod();

        public delegate void LoadMethod(string saveLine);

        public delegate void DisableMethod(string name);

        public delegate bool AvailabilityMethod(string option);

        public ProtagonistMethod Protagonist;

        public StringMethod Save;

        public LoadMethod Load;

        public StringMethod Debug;

        public AvailabilityMethod Availability;

        public Abstract.IParagraphs Paragraphs;

        public Abstract.IActions Actions;

        public Abstract.IConstants Constants;
    }
}
