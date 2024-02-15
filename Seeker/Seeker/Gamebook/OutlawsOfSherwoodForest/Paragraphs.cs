using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.OutlawsOfSherwoodForest
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
        public static Paragraphs GetInstance() => StaticInstance;
    }
}