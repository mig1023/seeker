using System;

namespace Seeker.Gamebook.OrcsDay
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
    }
}
