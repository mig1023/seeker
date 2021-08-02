using System;

namespace Seeker.Gamebook
{
    class Links
    {
        public delegate void ProtagonistMethod();

        public delegate string SaveMethod();

        public delegate void LoadMethod(string saveLine);

        public delegate bool CheckOnlyIfMethod(string option);

        public ProtagonistMethod Protagonist;

        public SaveMethod Save;

        public LoadMethod Load;

        public CheckOnlyIfMethod CheckOnlyIf;

        public Abstract.IParagraphs Paragraphs;

        public Abstract.IActions Actions;

        public Abstract.IConstants Constants;
    }
}
