using System;

namespace Seeker.Gamebook.UnexpectedPassenger
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
        public static Paragraphs GetInstance() => StaticInstance;
    }
}
