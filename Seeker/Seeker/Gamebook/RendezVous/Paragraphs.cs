using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.RendezVous
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
            paragraph.LateTrigger = source.LateTrigger;

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
                    new Option { Destination = 6, Text = "Меня больше интересует аппарат" },
                    new Option { Destination = 11, Text = "Я решаю осмотреть помещение" },
                    new Option { Destination = 15, Text = "Я читаю записку" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [3] = new Paragraph
            {
                LateTrigger = "SecondQuestion",

                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!SecondQuestion" },
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "SecondQuestion" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "Я пробую заговорить с ней" },
                    new Option { Destination = 17, Text = "Я пытаюсь взять её за руку" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Далее" },
                }
            },
            [6] = new Paragraph
            {
                Trigger = "Medic",

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее", OnlyIf = "Medic" },
                    new Option { Destination = 5, Text = "Далее", OnlyIf = "!Medic" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Я сдаюсь" },
                    new Option { Destination = 23, Text = "Я останавливаю её" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "'Кто ты?'" },
                    new Option { Destination = 19, Text = "'Кто я?'" },
                    new Option { Destination = 24, Text = "'Что со мной случилось?'" },
                    new Option { Destination = 30, Text = "'Что это за корабль?'" },
                    new Option { Destination = 34, Text = "'Где записка?'" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Trigger = "Scene",

                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Постельная сцена", OnlyIf = "Scene" },
                    new Option { Destination = 20, Text = "Далее" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Состоянии своего здоровья" },
                    new Option { Destination = 22, Text = "Халате Бетти" },
                    new Option { Destination = 32, Text = "Соседней кровати" },
                    new Option { Destination = 16, Text = "Ни о чём - мне лучше отдохнуть, поспать" },
                }
            },
            [15] = new Paragraph
            {
                Trigger = "Musk",

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее", OnlyIf = "Rabbit" },
                    new Option { Destination = 38, Text = "Далее", OnlyIf = "!Rabbit" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [18] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Далее" },
                }
            },
            [19] = new Paragraph
            {
                LateTrigger = "SecondQuestion",

                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!SecondQuestion" },
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "SecondQuestion" },
                }
            },
            [20] = new Paragraph
            {
                Trigger = "Rabbit",

                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Далее" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Далее" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [24] = new Paragraph
            {
                LateTrigger = "SecondQuestion",

                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!SecondQuestion" },
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "SecondQuestion" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Медицинский аппарат" },
                    new Option { Destination = 51, Text = "Простыни на соседней кровати" },
                    new Option { Destination = 59, Text = "Зеркало" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Я немедленно меняю курс и направляю корабль к ближайшему небесному телу, подходящему для моей цели: столкновение не произойдёт мгновенно, но станет возможным через три-четыре дня" },
                    new Option { Destination = 70, Text = "Устанавливаю смену курса через десять дней: вскоре после этого корабль должен пересечься с траекторией какого-то крупного астероида или кометы" },
                    new Option { Destination = 75, Text = "Настраиваю курс так, чтобы он изменился в последний момент: сблизившись с Землёй, корабль должен разбиться на Луне" },
                }
            },
            [27] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                LateTrigger = "SecondQuestion",

                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!SecondQuestion" },
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "SecondQuestion" },
                }
            },
            [28] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 20, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                LateTrigger = "SecondQuestion",

                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!SecondQuestion" },
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "SecondQuestion" },
                }
            },
            [31] = new Paragraph
            {
                LateTrigger = "Alone",

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Затем я обвиняю её во лжи" },
                    new Option { Destination = 37, Text = "Вы знаете о том, что были один", OnlyIf = "Alone" },
                    new Option { Destination = 39, Text = "Задам ей другой вопрос" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее", OnlyIf = "Medic|Musk"  },
                    new Option { Destination = 41, Text = "Далее", OnlyIf = "!Medic, !Musk"  },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "Да" },
                    new Option { Destination = 38, Text = "Нет" },
                }
            },
            [34] = new Paragraph
            {
                LateTrigger = "SecondQuestion",

                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "Вы читали записку Милы Отарес", OnlyIf = "Musk" },
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!SecondQuestion" },
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "SecondQuestion" },
                }
            },
            [35] = new Paragraph
            {
                LateTrigger = "Alone",

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Я обвиняю её во лжи" },
                    new Option { Destination = 37, Text = "Вы знаете о том, что были один", OnlyIf = "Alone" },
                    new Option { Destination = 39, Text = "Задам ей другой вопрос" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее", OnlyIf = "Mandela" },
                    new Option { Destination = 56, Text = "Далее", OnlyIf = "Mandela" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Далее" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "'Ты знаешь Милу Отарес?'" },
                    new Option { Destination = 35, Text = "'Кто ты на самом деле и какова твоя цель?'" },
                    new Option { Destination = 45, Text = "'Ты заходила в комнату, пока я спал?'" },
                    new Option { Destination = 50, Text = "'Почему я подключён к этому аппарату?'" },
                    new Option { Destination = 55, Text = "'Это ты разбила зеркало?'" },
                    new Option { Destination = 60, Text = "'Почему ты мне доверяешь? Зачем помогаешь?'" },
                    new Option { Destination = 37, Text = "У меня больше нет вопросов" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [41] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [45] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Вы набрали 4 очка Осознания и более", OnlyIf = "ОСОЗНАНИЕ > 3" },
                    new Option { Destination = 40, Text = "Вы набрали меньше 4 очка Осознания", OnlyIf = "ОСОЗНАНИЕ <= 3" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Далее", OnlyIf = "Bruce" },
                    new Option { Destination = 77, Text = "Далее", OnlyIf = "!Bruce"  },
                }
            },
            [50] = new Paragraph
            {
                LateTrigger = "Alone",

                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Далее", OnlyIf = "Alone" },
                    new Option { Destination = 39, Text = "Далее", OnlyIf = "Alone" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 36, Text = "Далее" },
                }
            },
            [53] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "DiceCheck",
                        ButtonName = "Кинуть один кубик",
                        Dices = 1,
                        Aftertext = "или",
                    },
                    new Actions
                    {
                        ActionName = "DiceCheck",
                        ButtonName = "Кинуть два кубика",
                        Dices = 2,
                        Aftertext = "\u00A0",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Если выпало чётное число" },
                    new Option { Destination = 58, Text = "Если нечётное" },
                }
            },
            [54] = new Paragraph
            {
                Trigger = "Paragraph",

                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Перейти на 42 Параграф", OnlyIf = "Paragraph" },
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                LateTrigger = "Alone",

                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Далее", OnlyIf = "Alone" },
                    new Option { Destination = 39, Text = "Далее", OnlyIf = "Alone" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Вы набрали 4 очка Осознания и более", OnlyIf = "ОСОЗНАНИЕ > 3" },
                    new Option { Destination = 40, Text = "Вы набрали меньше 4 очка Осознания", OnlyIf = "ОСОЗНАНИЕ <= 3" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 2,
                    },
                },

                Trigger = "Mandela",

                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [60] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Я страдаю от психического расстройства, и моё восприятие затуманено" },
                    new Option { Destination = 47, Text = "Бетти, она виновница моего состояния" },
                    new Option { Destination = 57, Text = "Кроме нас на борту корабля находится ещё кто-то (или что-то)" },
                    new Option { Destination = 54, Text = "Мы с Бетти по-разному переживаем реальност" },
                    new Option { Destination = 78, Text = "На самом деле я не человек..." },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [65] = new Paragraph
            {
                Trigger = "Gonzalez",

                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Далее" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Настрою герметичный шлюз, чтобы убить её" },
                    new Option { Destination = 74, Text = "Пока ничего не буду делать" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Trigger = "Prince",

                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [73] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Awareness",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [74] = new Paragraph
            {
                Trigger = "Bruce",

                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Далее" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Попытаюсь уговорить её прекратить эксперименты" },
                    new Option { Destination = 73, Text = "Не буду" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее", OnlyIf = "Prince" },
                    new Option { Destination = 68, Text = "Далее", OnlyIf = "!Prince, Gonzalez" },
                    new Option { Destination = 79, Text = "Далее", OnlyIf = "!Prince, !Gonzalez" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
        };
    }
}
