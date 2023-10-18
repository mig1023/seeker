using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.AlamutFortress
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
    }
}