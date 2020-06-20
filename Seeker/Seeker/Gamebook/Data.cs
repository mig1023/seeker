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
                Title = "First",
                Text = "First",
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "second" },
                    new Option { Destination = 3, Text = "third" },
                    new Option { Destination = 4, Text = "fourth" },
                }
            },
            [2] = new Paragraph
            {
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
                Title = "Third",
                Text = "Third",
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "first" },
                    new Option { Destination = 2, Text = "second" },
                }
            },
            [4] = new Paragraph
            {
                Title = "Fourth",
                Text = "Fourth",
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "back" },
                }
            }
        };
    }
}
