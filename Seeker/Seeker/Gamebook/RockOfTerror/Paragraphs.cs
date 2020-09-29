using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.RockOfTerror
{
    class Paragraphs : Interfaces.IParagraphs
    {
        public Game.Paragraph Get(int id)
        {
            Paragraph source = Paragraph[id];

            Game.Paragraph paragraph = new Game.Paragraph();

            if (source.Options != null)
                paragraph.Options = new List<Option>(source.Options);

            if (source.Actions != null)
                paragraph.Actions = new List<Interfaces.IActions>(source.Actions);

            if (source.Modification != null)
                paragraph.Modification = new List<Interfaces.IModification>(source.Modification);

            paragraph.Trigger = source.Trigger;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "В путь!" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "По правой" },
                    new Option { Destination = 66, Text = "По левой" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Прошло более 11-ти часов", OnlyIf = "ВРЕМЯ >= 660" },
                    new Option { Destination = 79, Text = "Прошло менее 11-ти часов", OnlyIf = "ВРЕМЯ < 660" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "На восток" },
                    new Option { Destination = 84, Text = "Переплыть реку" },
                }
            },
            [6] = new Paragraph
            {
                Trigger = "CrazyOldWoman, Anna",

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Trigger = "CrucifixWithRuby",

                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Сойти с тропы и искать источник звука" },
                    new Option { Destination = 24, Text = "Идти по тропе" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "На восток" },
                    new Option { Destination = 58, Text = "На север" },
                    new Option { Destination = 59, Text = "Попить" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "Проснуться", OnlyIf = "WakeUp" },
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Искать морозник вокруг на болоте" },
                    new Option { Destination = 58, Text = "По тропе дальше" },
                }
            },
            [13] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Injury",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Попытаться спасти его" },
                    new Option { Destination = 29, Text = "Нет" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "По туннелю вперед" },
                    new Option { Destination = 11, Text = "Спуститься по лестнице" },
                }
            },
            [18] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
                }
            },
            [19] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [20] = new Paragraph
            {
                Trigger = "MarrelsMirror",

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Осмотреть усыпальницы" },
                    new Option { Destination = 49, Text = "Вернуться на перекресток" },
                }
            },
            [23] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Идти по тропинке" },
                    new Option { Destination = 4, Text = "Через болото прямо по направлению к Шрекенштейну" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Посмотреть, что скрывается под накидкой" },
                    new Option { Destination = 63, Text = "Закрыть люк и покинуть дом" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Расскажете об Анне", OnlyIf = "Anna" },
                    new Option { Destination = 15, Text = "Откажитесь" },
                    new Option { Destination = 95, Text = "Бросите факел в костер" },
                }
            },
            [28] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [31] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [32] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Сойти с тропы и пойти в направлении, откуда исходил странный писк" },
                    new Option { Destination = 33, Text = "Продолжить идти по тропе" },
                }
            },
            [33] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "К сумасшедшей старухе", OnlyIf = "CrazyOldWoman" },
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [34] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Есть бронзовый медальон" },
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [38] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [39] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Причалить и сойти на берег" },
                    new Option { Destination = 99, Text = "Подплыть к неясному силуэту" },
                    new Option { Destination = 96, Text = "Поторопитесь к берегу" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [43] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Восточного склона" },
                    new Option { Destination = 92, Text = "Западного склона" },
                }
            },
            [45] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Помочь этим людям" },
                    new Option { Destination = 43, Text = "Покинуть дом" },
                }
            },
            [49] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Расскажете об Анне", OnlyIf = "Anna" },
                    new Option { Destination = 60, Text = "Есть распятье с рубином", OnlyIf = "CrucifixWithRuby" },
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [51] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "На север" },
                    new Option { Destination = 80, Text = "На северо-восток" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Её имя Августина", OnlyIf = "Augustine" },
                    new Option { Destination = 82, Text = "Её имя Гринаила", OnlyIf = "Grinael" },
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [53] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Воспользоваться зеркалом Маррела", OnlyIf = "MarrelsMirror" },
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [54] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Trigger = "BronzeMedallion",

                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [56] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Постучать в дверь (прошло больше 4 часов)", OnlyIf = "ВРЕМЯ >= 240" },
                    new Option { Destination = 6, Text = "Постучать в дверь (прошло меньше 4 часов)", OnlyIf = "ВРЕМЯ < 240" },
                    new Option { Destination = 43, Text = "Идти дальше" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "По тропе" },
                    new Option { Destination = 9, Text = "Через кустарник" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "На восток" },
                    new Option { Destination = 38, Text = "На север" },
                }
            },
            [60] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [61] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Далее" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "На северо-восток" },
                    new Option { Destination = 58, Text = "На северо-запад" },
                }
            },
            [65] = new Paragraph
            {
                Trigger = "WakeUp",

                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "На северо-запад" },
                    new Option { Destination = 86, Text = "На север" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Вернуться на перекресток и выбрать другой путь" },
                    new Option { Destination = 22, Text = "Осмотреть склеп" },
                }
            },
            [69] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "Её имя Гринаила", OnlyIf = "Grinael" },
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 180,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Пойти по следам" },
                    new Option { Destination = 44, Text = "Не будете менять направление" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "По туннелю вперед" },
                    new Option { Destination = 11, Text = "Спуститься по лестнице" },
                }
            },
            [73] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Trigger = "Anna",

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [74] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Рассказать инквизитору правду" },
                    new Option { Destination = 27, Text = "Соврать, что простой напуганный лесоруб" },
                }
            },
            [76] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Идти дальше" },
                    new Option { Destination = 31, Text = "Вернуться на перекресток" },
                }
            },
            [77] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Осмотреть дом" },
                    new Option { Destination = 18, Text = "Через болота напрямик к горе" },
                }
            },
            [78] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Поможете" },
                    new Option { Destination = 70, Text = "Прорываться дальше к центру поляны" },
                }
            },
            [80] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "На северо-восток по правой тропе" },
                    new Option { Destination = 76, Text = "На северо-запад по левой тропе" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Взять книгу и выбираться" },
                    new Option { Destination = 93, Text = "Остаться и все же открыть книгу" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Есть бронзовый медальон" },
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [85] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [86] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "По тропе на север" },
                    new Option { Destination = 41, Text = "Свернуть на северо-восток" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "С инквизитором" },
                    new Option { Destination = 20, Text = "С демонологом" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [91] = new Paragraph
            {
                Trigger = "Augustine",

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [92] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Найти потайную дверь", OnlyIf = "SecretEntrance" },
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Trigger = "Grinael",

                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Далее" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 83, Text = "Посмотреть в зеркало" },
                    new Option { Destination = 54, Text = "Броситься в люк" },
                }
            },
            [95] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "Через поляну со змеями" },
                    new Option { Destination = 35, Text = "Обойти препятствие по болоту" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "В проход слева" },
                    new Option { Destination = 81, Text = "В проход справа" },
                }
            },
            [99] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Time",
                        Value = 30,
                    },
                },

                Trigger = "SecretEntrance",

                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Задержаться и похоронить" },
                    new Option { Destination = 71, Text = "Отправиться к горе через лес" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
        };
    }
}
