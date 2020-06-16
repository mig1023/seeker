using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Data
    {
        public static Dictionary<int, Paragraph> Paragraphs = new Dictionary<int, Paragraph>();

        public static Dictionary<int, Paragraph> DummyParagraphs = new Dictionary<int, Paragraph>
        {
            [1] = new Paragraph
            {
                ID = 1,
                Title = "First",
                Text = "First",
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "second" },
                    new Option { Destination = 3, Text = "third" },
                }
            },
            [2] = new Paragraph
            {
                ID = 1,
                Title = "Second",
                Text = "Second",
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "first" },
                    new Option { Destination = 3, Text = "third" },
                }
            },
            [3] = new Paragraph
            {
                ID = 1,
                Title = "Third",
                Text = "Third",
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "first" },
                    new Option { Destination = 2, Text = "second" },
                }
            }
        };
    }
}
