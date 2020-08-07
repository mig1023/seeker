using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
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

            paragraph.OpenOption = source.OpenOption;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 660, Text = "В путь!" },
                    new Option { Destination = 659, Text = "Правила и инструкции" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 83, Text = "На Вильнёв" },
                    new Option { Destination = 321, Text = "На Каор" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Сэкономить деньги" },
                    new Option { Destination = 208, Text = "Спокойно провести ночь" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Найти где-нибудь укрытие и переждать его" },
                    new Option { Destination = 534, Text = "Поскачете дальше" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Направите Арбалета к мосту" },
                    new Option { Destination = 162, Text = "Попытаетесь найти в стороне брод или переправу" },
                    new Option { Destination = 541, Text = "Остановитесь, чтобы посмотреть, что будет дальше" },
                }
            },
            [5] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ВСАДНИК",
                                Skill = 8,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если вы убили его",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [6] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ СЛУГА",
                                Skill = 6,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ СЛУГА",
                                Skill = 6,
                                Strength = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 423, Text = "Сделаете это" },
                    new Option { Destination = 92, Text = "Не сделаете" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 167, Text = "Хотите уехать" },
                    new Option { Destination = 222, Text = "Постучите еще раз" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Поедете дальше" },
                    new Option { Destination = 542, Text = "Переночуете в лесу" },
                }
            },
            [10] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
                }
            },
            [11] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Согласитесь" },
                    new Option { Destination = 331, Text = "Постараетесь побыстрее уехать" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 223, Text = "Далее" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [15] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Остановитесь на постоялом дворе" },
                    new Option { Destination = 497, Text = "Поскачете дальше до Тура" },
                }
            },
            [16] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Поужинать в соседний трактир" },
                    new Option { Destination = 93, Text = "Искать место для ночлега" },
                }
            },
            [17] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Переночуете в гостинице" },
                    new Option { Destination = 480, Text = "Постараться уснуть неподалеку от дороги" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "На Перигё" },
                    new Option { Destination = 353, Text = "На Лимож" },
                }
            },
            [20] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -50,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Далее" },
                }
            },
            [21] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Далее" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 280, Text = "Остаётесь" },
                    new Option { Destination = 481, Text = "Поискать что-нибудь подешевле" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Далее" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 354, Text = "Свернете" },
                    new Option { Destination = 239, Text = "Поедете дальше до Пуатье" },
                }
            },
            [25] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 499, Text = "Удачливы" },
                    new Option { Destination = 546, Text = "Нет" },
                }
            },
            [26] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДВОРЯНИН",
                                Skill = 8,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если вы убили его",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Далее" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 515, Text = "Будете есть" },
                    new Option { Destination = 361, Text = "Не станете" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Выстрелить" },
                    new Option { Destination = 114, Text = "Напасть со шпагой" },
                    new Option { Destination = 551, Text = "Ждать, пока они объяснят свое странное вторжение" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 246, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 425, Text = "Выяснять, в чем дело" },
                    new Option { Destination = 331, Text = "Спокойно уедете" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Далее" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 282, Text = "Уйдете" },
                    new Option { Destination = 241, Text = "Вернетесь" },
                    new Option { Destination = 507, Text = "Есть предел вашему терпению" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 603, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [36] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "На Лимож" },
                    new Option { Destination = 323, Text = "На Пуатье" },
                }
            },
            [38] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЧЁРНАЯ ФИГУРА",
                                Skill = 8,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если вы победили своего врага",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Далее" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 426, Text = "Признаете себя арестованным" },
                    new Option { Destination = 517, Text = "Будете драться" },
                }
            },
            [40] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 800,
                    },
                    new Modification
                    {
                        Name = "Honor",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Согласитесь" },
                    new Option { Destination = 501, Text = "Откажетесь" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 552, Text = "Заехать на рынок" },
                    new Option { Destination = 363, Text = "Поторопитесь дальше" },
                }
            },
            [42] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПРОХОЖИЙ",
                                Skill = 8,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если убили его",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "Далее" },
                }
            },
            [43] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Попросить о помощи Бога" },
                    new Option { Destination = 244, Text = "Попробуете обойтись своими силами" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Постараетесь скрыться в лесу" },
                    new Option { Destination = 364, Text = "Направите коня прямо на зловещие фигуры" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [48] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 6,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Далее" },
                }
            },
            [49] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 245, Text = "Далее" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [51] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Далее" },
                }
            },
            [52] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Надо на рынок" },
                    new Option { Destination = 306, Text = "Не надо" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 389, Text = "Будете ждать" },
                    new Option { Destination = 553, Text = "Найдете хозяина и спросите" },
                }
            },
            [55] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ХОЗЯИН ДОМА",
                                Skill = 7,
                                Strength = 9,
                            },
                        },

                        Aftertext = "Если убили его, то вам нужно решить, что дальше делать.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 262, Text = "Побыстрее уехать" },
                    new Option { Destination = 575, Text = "Войти в дом и узнать, кто вылил на вас воду" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Выстрелите" },
                    new Option { Destination = 614, Text = "Обнажите шпагу" },
                    new Option { Destination = 468, Text = "Подождете, что он скажет" },
                }
            },
            [57] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 554, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 459, Text = "Если на нем выпало от 1 до 5" },
                    new Option { Destination = 264, Text = "Выпало 6" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 556, Text = "Дадите ему обогнать вас" },
                    new Option { Destination = 308, Text = "Поспешите к воротам" },
                    new Option { Destination = 616, Text = "Дадите ему бой" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [63] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 450,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 620, Text = "На рынок" },
                    new Option { Destination = 250, Text = "Выбираться из города" },
                    new Option { Destination = 557, Text = "Подождёте до следующего вечера" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 518, Text = "Путешествовали 6 дней или меньше" },
                    new Option { Destination = 431, Text = "7 дней" },
                    new Option { Destination = 45, Text = "8 или больше" },
                }
            },
            [65] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 700,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 619, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛОДОЧНИК",
                                Skill = 5,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Лодка теперь ваша.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 263, Text = "Далее" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Уже были в тюрьме" },
                    new Option { Destination = 307, Text = "Нет" },
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
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 470, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 562, Text = "Далее" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 621, Text = "Остановитесь в гостинице" },
                    new Option { Destination = 581, Text = "Переночуете в поле за городом" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [75] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ЛИГИСТ",
                                Skill = 9,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ЛИГИСТ",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [77] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -300,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 599, Text = "Должен передать письмо графу" },
                    new Option { Destination = 639, Text = "Походите еще" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 624, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 314, Text = "Далее" },
                }
            },
            [80] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 653, Text = "Далее" },
                }
            },
            [82] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Если ваша ЧЕСТЬ больше или равна 6" },
                    new Option { Destination = 525, Text = "Если меньше" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Задержитесь" },
                    new Option { Destination = 535, Text = "Пришпорите коня" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Вызовете его на поединок" },
                    new Option { Destination = 209, Text = "Двинетесь дальше" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Повернете лошадь, чтобы узнать, в чем дело" },
                    new Option { Destination = 402, Text = "Поторопитесь въехать в город" },
                }
            },
            [86] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Далее" },
                }
            },
            [88] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Удачлив" },
                    new Option { Destination = 418, Text = "Нет" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 543, Text = "Далее" },
                }
            },
            [91] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "В ту сторону, куда сидели лицом" },
                    new Option { Destination = 329, Text = "В обратную" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Хотите посмотреть, кто это будет" },
                    new Option { Destination = 332, Text = "Будете выбираться из переделки" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Остановите свой выбор на ней" },
                    new Option { Destination = 481, Text = "Поищете что-нибудь подешевле" },
                    new Option { Destination = 282, Text = "Если у вас вообще не осталось денег" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Далее" },
                }
            },
            [95] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 226, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 3,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Направо" },
                    new Option { Destination = 482, Text = "Налево" },
                }
            },
            [98] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Удачлив" },
                    new Option { Destination = 14, Text = "Нет" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Сдадитесь на милость победителя" },
                    new Option { Destination = 25, Text = "Будете драться до последнего" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Хотите сделать это" },
                    new Option { Destination = 228, Text = "Продолжите поиски хозяина" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Разрядите в него пистолет" },
                    new Option { Destination = 484, Text = "Ответите, что не ему учить вас хорошим манерам" },
                    new Option { Destination = 355, Text = "Молча спрячете оружие" },
                }
            },
            [105] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Сделаете то же, что и остальные" },
                    new Option { Destination = 284, Text = "Поедете дальше" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Далее" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Далее" },
                }
            },
            [108] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ВСАДНИК",
                                Skill = 6,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [111] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 230, Text = "Нападете на них" },
                    new Option { Destination = 550, Text = "Подождете другого, более благоприятного случая" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Возмутиться" },
                    new Option { Destination = 356, Text = "Подождать, что будет дальше" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 486, Text = "Далее" },
                }
            },
            [114] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ГРАБИТЕЛЬ",
                                Skill = 7,
                                Strength = 9,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ГРАБИТЕЛЬ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Далее" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Далее" },
                }
            },
            [116] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Объедете город стороной" },
                    new Option { Destination = 52, Text = "Въедете в него" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Далее" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "В Париж" },
                    new Option { Destination = 186, Text = "В Орлеан" },
                    new Option { Destination = 617, Text = "В Шартр" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Будете прорываться" },
                    new Option { Destination = 250, Text = "Вернетесь" },
                }
            },
            [120] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 570, Text = "Подъехать поближе" },
                    new Option { Destination = 257, Text = "Пришпорите коня и въедете в городок в другом месте" },
                }
            },
            [121] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТЮРЕМЩИК",
                                Skill = 9,
                                Strength = 6,
                            },
                        },

                        RoundsToWin = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Если вы успели убить его за 4 раунда" },
                    new Option { Destination = 187, Text = "Если нет" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 618, Text = "Хотите попробовать" },
                    new Option { Destination = 70, Text = "Спокойно поедете дальше" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 561, Text = "Согласитесь на предложение старого слуги" },
                    new Option { Destination = 640, Text = "Извинитесь и скажете, что должны торопиться" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Зайдете в дом, чтобы потребовать извинений" },
                    new Option { Destination = 619, Text = "Поедете дальше" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 576, Text = "Вы перегородили боковую улицу" },
                    new Option { Destination = 641, Text = "Не перегородили" },
                }
            },
            [127] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 237, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Свернете с дороги" },
                    new Option { Destination = 56, Text = "Поедете прямо к замку" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [130] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 562, Text = "Далее" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Далее" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 563, Text = "Поедете дальше" },
                    new Option { Destination = 349, Text = "Вернетесь на десяток лье назад" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 564, Text = "Дадите ему экю" },
                    new Option { Destination = 105, Text = "Поедете дальше" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Скажете правду" },
                    new Option { Destination = 577, Text = "Скажете, что просто потеряли его в пути" },
                }
            },
            [137] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -400,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 620, Text = "Сходить на базарную площадь" },
                    new Option { Destination = 250, Text = "Выбраться из города" },
                    new Option { Destination = 557, Text = "Ждите следующего вечера" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Далее" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 580, Text = "Выпрыгнуть в окно" },
                    new Option { Destination = 59, Text = "Дождетесь тех, кто пришел" },
                }
            },
            [140] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 638, Text = "Далее" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 621, Text = "Остановитесь в гостинице" },
                    new Option { Destination = 581, Text = "Переночуете в поле за городом" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 521, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 282, Text = "Далее" },
                }
            },
            [144] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [145] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Если вы удачливы" },
                    new Option { Destination = 566, Text = "Если же нет" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 256, Text = "Сказать правду о вашем поручении" },
                    new Option { Destination = 578, Text = "Приготовиться к пытке" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Возьметесь за такое поручение" },
                    new Option { Destination = 460, Text = "Откажетесь и попрощаетесь с баронессой" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Согласитесь на ее предложение" },
                    new Option { Destination = 642, Text = "Откажетесь" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 203, Text = "Прямо" },
                    new Option { Destination = 658, Text = "Налево" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 309, Text = "Поверите ему" },
                    new Option { Destination = 579, Text = "Не поверите" },
                }
            },
            [152] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "У вас нет времени заниматься глупостями" },
                    new Option { Destination = 216, Text = "Назовете свое имя, не слезая с лошади" },
                    new Option { Destination = 87, Text = "Последуете за лейтенантом" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Воспользоваться пистолетом" },
                    new Option { Destination = 94, Text = "Владеете искусством стрельбы из двух сразу" },
                    new Option { Destination = 333, Text = "Надеетесь на шпагу" },
                }
            },
            [155] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 413, Text = "Согласитесь" },
                    new Option { Destination = 11, Text = "Здоровый сон вполне заменит жирный кусок мяса" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Хотите что-то продать" },
                    new Option { Destination = 225, Text = "Не хотите" },
                }
            },
            [157] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Заночевать в поле" },
                    new Option { Destination = 224, Text = "Поедете дальше" },
                }
            },
            [158] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 334, Text = "Далее" },
                }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 543, Text = "Далее" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [161] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 330, Text = "Далее" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Проехать через мост" },
                    new Option { Destination = 12, Text = "Умеете плавать" },
                    new Option { Destination = 419, Text = "Попробовать пересечь ее, держась за коня" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Попытаетесь спасти его" },
                    new Option { Destination = 95, Text = "Поедете дальше" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Направо" },
                    new Option { Destination = 286, Text = "Налево" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 335, Text = "Выстрелите" },
                    new Option { Destination = 231, Text = "Достанете кинжал" },
                    new Option { Destination = 487, Text = "Обнажите шпагу" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 287, Text = "Далее" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 335, Text = "Выстрелите в нее" },
                    new Option { Destination = 488, Text = "Поедете в другую сторону" },
                    new Option { Destination = 500, Text = "Подождете, что будет дальше" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Воспользоваться услугами постоялого двора" },
                    new Option { Destination = 497, Text = "Побыстрее доехать до Тура" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 227, Text = "Выберетесь из кареты" },
                    new Option { Destination = 357, Text = "Спрячетесь за женщину" },
                    new Option { Destination = 429, Text = "Подождете, что будет дальше" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Поспешите к парижским воротам" },
                    new Option { Destination = 336, Text = "Подумать о том, что у вас нет ни денег, ни оружия" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 226, Text = "Далее" },
                }
            },
            [172] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "У вас остался перстень короля" },
                    new Option { Destination = 44, Text = "Нет" },
                }
            },
            [173] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Направо" },
                    new Option { Destination = 482, Text = "Налево" },
                }
            },
            [174] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Skill",
                        ButtonName = "Проверить ловкость",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 288, Text = "Ловкости хватило" },
                    new Option { Destination = 430, Text = "Нет" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "2 экю" },
                    new Option { Destination = 232, Text = "4 экю" },
                    new Option { Destination = 505, Text = "6 экю" },
                }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 490, Text = "Устроить побоище" },
                    new Option { Destination = 544, Text = "Сделаете вид, что ничего не произошло" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 359, Text = "Есть пистолет" },
                    new Option { Destination = 246, Text = "Нет пистолета" },
                }
            },
            [178] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 233, Text = "Въедете в аббатство" },
                    new Option { Destination = 491, Text = "Скажете монаху, что он обознался" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "Поскачете за ним" },
                    new Option { Destination = 501, Text = "Откажетесь, сославшись на недостаток времени" },
                    new Option { Destination = 338, Text = "Спросите, в чем должна заключаться эта помощь" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 265, Text = "Вызовете этого дворянина на поединок" },
                    new Option { Destination = 624, Text = "Начнете бой со всеми сразу" },
                    new Option { Destination = 471, Text = "Попытаетесь бежать" },
                }
            },
            [182] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Плеснете немного вина в свой кубок и отведаете оттуда" },
                    new Option { Destination = 625, Text = "Сделаете глоток из кубка, предназначенного для Майена" },
                    new Option { Destination = 310, Text = "Отольете немного вина в крышку и отопьете оттуда" },
                }
            },
            [184] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Далее" },
                }
            },
            [186] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Далее" },
                }
            },
            [187] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [189] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ОХРАННИК",
                                Skill = 7,
                                Strength = 9,
                            },
                        },

                        RoundsToWin = 5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 586, Text = "Если вы убили его за 5 раундов атаки" },
                    new Option { Destination = 651, Text = "Если нет" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Далее" },
                }
            },
            [191] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -3,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Далее" },
                }
            },
            [192] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 653, Text = "Далее" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Далее" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Далее" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Далее" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 658, Text = "Далее" },
                }
            },
            [201] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 633, Text = "Спуститься по лестнице вниз" },
                    new Option { Destination = 593, Text = "Вернуться обратно, пройдя по коридору прямо" },
                    new Option { Destination = 583, Text = "Направо" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Лезть прямо в пекло" },
                    new Option { Destination = 85, Text = "Съездить в Тур" },
                    new Option { Destination = 105, Text = "Подняться по реке почти до самого Невера" },
                }
            },
            [205] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Далее" },
                }
            },
            [206] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "Далее" },
                }
            },
            [207] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Поедете в «Золотую лилию»" },
                    new Option { Destination = 405, Text = "Попытаетесь найти другую гостиницу" },
                }
            },
            [209] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Далее" },
                }
            },
            [210] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Попытаетесь спастись бегством" },
                    new Option { Destination = 325, Text = "Выстрелите первым" },
                    new Option { Destination = 89, Text = "Подождете, пока они подъедут поближе" },
                }
            },
            [211] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Приблизитесь к двери и постучите" },
                    new Option { Destination = 165, Text = "Подкрадетесь к окну и заглянете в него" },
                    new Option { Destination = 167, Text = "Вновь сядете на коня и поедете дальше" },
                }
            },
            [212] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 415, Text = "Отправитесь искать трактир" },
                    new Option { Destination = 93, Text = "Отправитесь искать гостиницу для ночлега" },
                    new Option { Destination = 156, Text = "Проедете по торговым рядам" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 539, Text = "Хотите помочь ему" },
                    new Option { Destination = 9, Text = "Отправитесь дальше" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Воспользуетесь пистолетом" },
                    new Option { Destination = 171, Text = "Умеете стрелять сразу из двух, и они у вас есть" },
                    new Option { Destination = 339, Text = "Если же ни то, ни другое" },
                }
            },
            [215] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Если вы умеете плавать" },
                    new Option { Destination = 13, Text = "Если же решите доплыть до кареты, держась за лошадь" },
                    new Option { Destination = 410, Text = "Попытаться спрыгнуть к экипажу с моста" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "У вас нет времени на ерунду" },
                    new Option { Destination = 87, Text = "Последовать за ним" },
                }
            },
            [217] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВСАДНИК",
                                Skill = 8,
                                Strength = 8,
                            },
                        },

                        RoundsToWin = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Если вы убили его за 4 раунда атаки" },
                    new Option { Destination = 492, Text = "Если нет" },
                }
            },
            [218] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Подниметесь по дубовой лестнице на второй этаж" },
                    new Option { Destination = 420, Text = "Сначала осмотрите первый" },
                }
            },
            [219] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Honor",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [220] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ШЕВАЛЬЕ ДО МИШУАР",
                                Skill = 9,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если вы выйдете из схватки победителем, то можете либо убить шевалье, сорвав на нём свою злость, либо попытаться распросить его.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 290, Text = "Убить шевалье" },
                    new Option { Destination = 360, Text = "Попытаться расспросить его" },
                }
            },
            [221] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Обойтись без сна" },
                    new Option { Destination = 542, Text = "Переночуете в лесу" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 172, Text = "Далее" },
                }
            },
            [223] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 421, Text = "Удачлив" },
                    new Option { Destination = 109, Text = "Нет" },
                }
            },
            [224] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Отдадите королевский перстень" },
                    new Option { Destination = 502, Text = "Никакой король вам ничего не давал" },
                    new Option { Destination = 46, Text = "Будете прорываться силой" },
                }
            },
            [225] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 415, Text = "Поужинать в каком-нибудь трактире" },
                    new Option { Destination = 93, Text = "Попытаетесь найти гостиницу для ночлега" },
                }
            },
            [226] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Извиниться" },
                    new Option { Destination = 494, Text = "Предложить искупить кровью свою ошибку" },
                    new Option { Destination = 519, Text = "Постараться отправить его на тот свет" },
                }
            },
            [227] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 432, Text = "Удачлив" },
                    new Option { Destination = 365, Text = "Нет" },
                }
            },
            [228] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ХОЗЯИН ДОМА",
                                Skill = 8,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 503, Text = "Далее" },
                }
            },
            [229] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ БРОДЯГА",
                                Skill = 6,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ БРОДЯГА",
                                Skill = 7,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ БРОДЯГА",
                                Skill = 7,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Далее" },
                }
            },
            [230] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 495, Text = "Согласитесь быть провожатым" },
                    new Option { Destination = 248, Text = "Предпочтете ремесло грабителя" },
                }
            },
            [231] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Далее" },
                }
            },
            [232] = new Paragraph
            {



                Options = new List<Option>
                {
                    new Option { Destination = 505, Text = "Согласитесь на 6 экю" },
                    new Option { Destination = 18, Text = "Оставите идею с подкупом" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Подчинитесь" },
                    new Option { Destination = 433, Text = "Будете драться" },
                }
            },
            [234] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [235] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Телохранителем" },
                    new Option { Destination = 118, Text = "Авантюристом" },
                }
            },
            [236] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 228, Text = "Далее" },
                }
            },
            [237] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "В Тур" },
                    new Option { Destination = 204, Text = "На Вьерзон" },
                }
            },
            [238] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [239] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "Переночуете в поле" },
                    new Option { Destination = 520, Text = "Побыстрее добраться до Тура" },
                }
            },
            [240] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Далее" },
                }
            },
            [241] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [242] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Далее" },
                }
            },
            [243] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НЕИЗВЕСТНЫЙ",
                                Skill = 11,
                                Strength = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 412, Text = "Если вы победили" },
                    new Option { Destination = 521, Text = "Если он, то можете воззвать к Богу" },
                }
            },
            [244] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "На Ангулем" },
                    new Option { Destination = 508, Text = "На Шатору" },
                }
            },
            [246] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕПРЬ",
                                Skill = 10,
                                Strength = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 446, Text = "Далее" },
                }
            },
            [247] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [248] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДВОРЯНИН",
                                Skill = 9,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Далее" },
                }
            },
            [249] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВСАДНИК",
                                Skill = 8,
                                Strength = 9,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВСАДНИК",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [250] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Через ворота на Вандом" },
                    new Option { Destination = 21, Text = "Через ворота на Леман" },
                    new Option { Destination = 61, Text = "Через ворота на Блуа" },
                    new Option { Destination = 367, Text = "Через ворота на Вьерзон" },
                    new Option { Destination = 435, Text = "Отправитесь на пристань" },
                }
            },
            [251] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 512, Text = "Сделаете это" },
                    new Option { Destination = 26, Text = "Ответите, что спешите не меньше его" },
                }
            },
            [253] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 437, Text = "Остановитесь с ним поговорить" },
                    new Option { Destination = 563, Text = "Поскачете дальше" },
                }
            },
            [254] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 522, Text = "Повезло" },
                    new Option { Destination = 369, Text = "Нет" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [256] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [257] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Далее" },
                }
            },
            [258] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [259] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Получилось четное число" },
                    new Option { Destination = 626, Text = "Получилось нечетное число" },
                    new Option { Destination = 76, Text = "Одинаковые числа" },
                }
            },
            [260] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Ехать дальше" },
                    new Option { Destination = 52, Text = "Отправиться в путь с утра" },
                }
            },
            [261] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 562, Text = "Далее" },
                }
            },
            [262] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 619, Text = "Вы один" },
                    new Option { Destination = 587, Text = "Вы со спутником" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 627, Text = "На рыночную площадь" },
                    new Option { Destination = 54, Text = "Прямо в город" },
                }
            },
            [264] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [265] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛЕГИСТ",
                                Skill = 12,
                                Strength = 14,
                            },
                        },

                        WoundsToWin = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Если удалось ранить его три раза" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 638, Text = "Далее" },
                }
            },
            [267] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 628, Text = "Объехать замок с другой стороны" },
                    new Option { Destination = 128, Text = "Больше не сворачивать с дороги" },
                    new Option { Destination = 588, Text = "Побыстрее проехать странное место" },
                }
            },
            [268] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Войдете в нее" },
                    new Option { Destination = 643, Text = "Свернете" },
                }
            },
            [269] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Постучите еще раз и покажете перстень" },
                    new Option { Destination = 261, Text = "Попытаете счастья по другому адресу" },
                }
            },
            [270] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Герцог еще в столице" },
                    new Option { Destination = 74, Text = "Он уже уехал" },
                }
            },
            [271] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        Text = "ПЕРЕОДЕТЬСЯ, 7 экю",
                        ButtonName = "Переодеться",
                        Price = 700,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Если вы переоделись" },
                    new Option { Destination = 615, Text = "Уже побывали в тюрьме" },
                    new Option { Destination = 307, Text = "Вы там не были" },
                }
            },
            [272] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 302, Text = "Далее" },
                }
            },
            [273] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛЕГИСТ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 200, Text = "Далее" },
                }
            },
            [274] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "На Париж" },
                    new Option { Destination = 85, Text = "Добраться через Блуа до Тура" },
                    new Option { Destination = 105, Text = "Нанять лодку и подняться по Луаре до Невера" },
                }
            },
            [276] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Поищете черный ход" },
                    new Option { Destination = 71, Text = "Поедете по какому-нибудь другому адресу" },
                    new Option { Destination = 562, Text = "Направитесь к себе домой" },
                }
            },
            [277] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 596, Text = "На Блуа и Орлеан" },
                    new Option { Destination = 652, Text = "На Вандом" },
                }
            },
            [278] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 3,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 496, Text = "Далее" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 596, Text = "Далее" },
                }
            },
            [280] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 507, Text = "Возмутитесь" },
                    new Option { Destination = 49, Text = "Покорно возьмете ключи в четвертый раз" },
                }
            },
            [281] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [282] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 508, Text = "Напрямую, через Шатору" },
                    new Option { Destination = 163, Text = "В объезд, через Ангулем" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [284] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Подчинитесь" },
                    new Option { Destination = 438, Text = "Поедете дальше" },
                }
            },
            [285] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Согласитесь" },
                    new Option { Destination = 331, Text = "Постараетесь побыстрее уехать из Капденака" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 482, Text = "Сделаете вторую попытку добраться до Лиможа" },
                    new Option { Destination = 323, Text = "Развернете коня и постараетесь побыстрее попасть в Пуатье" },
                }
            },
            [287] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Покажете королевский перстень" },
                    new Option { Destination = 439, Text = "Направо" },
                    new Option { Destination = 370, Text = "Налево" },
                }
            },
            [288] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [289] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Далее" },
                }
            },
            [290] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 440, Text = "Далее" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [292] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Покажете королевский перстень" },
                    new Option { Destination = 439, Text = "Направо" },
                    new Option { Destination = 370, Text = "Налево" },
                }
            },
            [293] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -100000,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Отправитесь к хозяину" },
                    new Option { Destination = 331, Text = "Сядете на коня и покинете город" },
                }
            },
            [294] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Удачливы" },
                    new Option { Destination = 120, Text = "Нет" },
                }
            },
            [295] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛЕГИСТ",
                                Skill = 12,
                                Strength = 14,
                            },
                        },

                        WoundsToWin = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Далее" },
                }
            },
            [296] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 262, Text = "Далее" },
                }
            },
            [297] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "В Перигё" },
                    new Option { Destination = 353, Text = "В Люберсак" },
                }
            },
            [298] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Вы уже были в тюрьме" },
                    new Option { Destination = 307, Text = "Вы там не были" },
                }
            },
            [299] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Дать 3 экю" },
                    new Option { Destination = 455, Text = "Подождать до утра" },
                    new Option { Destination = 17, Text = "Если у вас нет денег" },
                }
            },
            [300] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [301] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ШЕВАЛЬЕ ДЕ БУАЗО",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Далее" },
                }
            },
            [302] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 653, Text = "Далее" },
                }
            },
            [303] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "DicesDoubles",
                        ButtonName = "Проверить на дубли",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 644, Text = "Выпали одинаковые числа" },
                    new Option { Destination = 196, Text = "Нет" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Поспешите в кабачок «У Франсуа»" },
                    new Option { Destination = 336, Text = "Подумаете, что у вас нет ни денег, ни оружия" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Хотите зайти" },
                    new Option { Destination = 558, Text = "Поскачете на Париж" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 113, Text = "Далее" },
                }
            },
            [308] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Удачливы" },
                    new Option { Destination = 556, Text = "Нет" },
                }
            },
            [309] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Отправиться по любому другому адресу" },
                    new Option { Destination = 562, Text = "Съездить посмотреть, что осталось от вашего дома" },
                }
            },
            [310] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 653, Text = "Далее" },
                }
            },
            [311] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [312] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Поспешите в кабачок «У Франсуа»" },
                    new Option { Destination = 336, Text = "Постараетесь раздобыть деньги и оружие" },
                }
            },
            [314] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "На Лимож" },
                    new Option { Destination = 323, Text = "На Пуатье" },
                }
            },
            [315] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Далее" },
                }
            },
            [316] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Далее" },
                }
            },
            [317] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 287, Text = "Далее" },
                }
            },
            [318] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 658, Text = "Далее" },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Далее" },
                }
            },
            [320] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = (Game.Dice.Roll(dices: 2) * -1),
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 260, Text = "Пришпорить коня" },
                    new Option { Destination = 191, Text = "Обогнете город и поскачете в Париж" },
                    new Option { Destination = 479, Text = "Попытаться наказать крестьян" },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 210, Text = "Остановитесь" },
                    new Option { Destination = 403, Text = "Постараетесь побыстрее добраться до Каора" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Примете бой" },
                    new Option { Destination = 536, Text = "Постараетесь побыстрее ускакать прочь" },
                }
            },
            [323] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 215, Text = "Попытаться спасти несчастную женщину" },
                    new Option { Destination = 86, Text = "Ваша миссия важнее и поедете дальше" },
                }
            },
            [324] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 404, Text = "Попробуете поискать место посуше" },
                    new Option { Destination = 211, Text = "Поскачете дальше" },
                }
            },
            [325] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Удачливы" },
                    new Option { Destination = 10, Text = "Нет" },
                }
            },
            [326] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 310,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Не пытались скрыться от преследователей" },
                    new Option { Destination = 166, Text = "Пытались" },
                }
            },
            [327] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "Далее" },
                }
            },
            [328] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Попытаетесь вырваться и убежать" },
                    new Option { Destination = 422, Text = "Подождете, пока вас выведут из караулки" },
                }
            },
            [329] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Выберете место поуютнее" },
                    new Option { Destination = 173, Text = "Поедете дальше" },
                }
            },
            [330] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Удачливы" },
                    new Option { Destination = 110, Text = "Нет" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 496, Text = "Далее" },
                }
            },
            [332] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [333] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ СЛУГА",
                                Skill = 6,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ СЛУГА",
                                Skill = 6,
                                Strength = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Далее" },
                }
            },
            [334] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [335] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Подойдете к двери и постучите" },
                    new Option { Destination = 504, Text = "Быстро уедете прочь" },
                }
            },
            [336] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Постараетесь как-то заработать деньги" },
                    new Option { Destination = 111, Text = "Решите, что лучше кого-нибудь ограбить" },
                }
            },
            [337] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 291, Text = "Пойти на рынок и купить вторую лошадь" },
                    new Option { Destination = 100, Text = "К воротам тюрьмы" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Согласитесь ему помочь" },
                    new Option { Destination = 501, Text = "Это не ваше дело и отправитесь своей дорогой" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 249, Text = "Обнажите шпагу и атакуете" },
                    new Option { Destination = 442, Text = "Сначала поговорите" },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Займетесь им" },
                    new Option { Destination = 101, Text = "Двинетесь дальше" },
                }
            },
            [341] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Согласитесь позавтракать" },
                    new Option { Destination = 220, Text = "Постараетесь выбить из него эти сведения" },
                    new Option { Destination = 176, Text = "Попробуете любой ценой узнать у хозяина, кто это сделал" },
                }
            },
            [342] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 223, Text = "Далее" },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 511, Text = "Попробуйте что-нибудь придумать" },
                    new Option { Destination = 443, Text = "Понадеетесь на свое мастерство" },
                }
            },
            [344] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "БРОДЯГА",
                                Skill = 8,
                                Strength = 6,
                            },
                        },

                        RoundsToWin = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 251, Text = "Если вы убили его за 4 раунда" },
                    new Option { Destination = 523, Text = "Если нет" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [347] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [348] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = -4,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -100,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [349] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 512, Text = "Пропустите его" },
                    new Option { Destination = 26, Text = "Потребуете, чтобы он уступил дорогу" },
                    new Option { Destination = 252, Text = "Посмотрите, что он будет делать" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 444, Text = "Обрежете веревку" },
                    new Option { Destination = 524, Text = "Полезете посмотреть, к чему она привязана" },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Приготовите оружие и поедете по дороге дальше" },
                    new Option { Destination = 513, Text = "Повернете коня и постараетесь объехать подозрительное место по полям" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 121, Text = "Нападете на него, чтобы завладеть оружием и бежать" },
                    new Option { Destination = 27, Text = "Посмотрите, что будет дальше" },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Далее" },
                }
            },
            [354] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 445, Text = "Далее" },
                }
            },
            [355] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [356] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 527, Text = "Откроете" },
                    new Option { Destination = 28, Text = "Притворитесь спящим" },
                }
            },
            [357] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 432, Text = "Далее" },
                }
            },
            [358] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Далее" },
                }
            },
            [359] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Skill",
                        ButtonName = "Проверить ловкость",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 288, Text = "Ловкости хватило" },
                    new Option { Destination = 430, Text = "Нет" },
                }
            },
            [360] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 440, Text = "Далее" },
                }
            },
            [361] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Путешествуете для собственного удовольствия" },
                    new Option { Destination = 256, Text = "Расскажете все как есть" },
                }
            },
            [362] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 627, Text = "Заехать на рынок" },
                    new Option { Destination = 54, Text = "Прямо в центр города" },
                }
            },
            [363] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Остановиться и зайти в этот дом, чтобы потребовать извинений" },
                    new Option { Destination = 568, Text = "Поедете дальше" },
                }
            },
            [364] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Если у вас один заряженный пистолет" },
                    new Option { Destination = 447, Text = "Два заряженных пистолета" },
                    new Option { Destination = 381, Text = "Драться" },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Согласитесь" },
                    new Option { Destination = 118, Text = "Попробуете себя в качестве авантюриста" },
                }
            },
            [367] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Согласитесь ему помочь" },
                    new Option { Destination = 501, Text = "Отправитесь в погоню за незнакомцем" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [370] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 124, Text = "Попробуете въехать через них" },
                    new Option { Destination = 439, Text = "Вернетесь назад и объедете Каор с другой стороны" },
                }
            },
            [371] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 570, Text = "Свернете к домику, чтобы разобраться" },
                    new Option { Destination = 257, Text = "Пришпорите Арбалета, чтобы побыстрее выехать из-под обстрела" },
                }
            },
            [372] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Honor",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 563, Text = "Далее" },
                }
            },
            [374] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 645, Text = "Хотите заехать на рынок" },
                    new Option { Destination = 125, Text = "Поедете дальше" },
                }
            },
            [375] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Skill",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 630, Text = "Откажетесь от помощи" },
                    new Option { Destination = 197, Text = "Попросите дать вам провожатого" },
                    new Option { Destination = 571, Text = "Попросите рекомендательное письмо" },
                }
            },
            [376] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ КАВАЛЕРИСТ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "Побыстрее оставите поляну" },
                    new Option { Destination = 451, Text = "Приготовитесь к обороне" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [379] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = -10,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Далее" },
                }
            },
            [380] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "На Лимож" },
                    new Option { Destination = 213, Text = "На Невер" },
                }
            },
            [381] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ЧЕЛОВЕК С ТОПОРОМ",
                                Skill = 8,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ЧЕЛОВЕК С ТОПОРОМ",
                                Skill = 7,
                                Strength = 6,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Далее" },
                     new Option { Destination = 381, Text = "ТЕСТ" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Развернете Арбалета поперек дороги" },
                    new Option { Destination = 127, Text = "Отъедете в сторону" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Далее" },
                }
            },
            [384] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = 400,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [385] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 452, Text = "На Вандом" },
                    new Option { Destination = 277, Text = "На Блуа" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 267, Text = "Слева" },
                    new Option { Destination = 628, Text = "Справа" },
                    new Option { Destination = 128, Text = "Не будете объезжать" },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "На Лимож" },
                    new Option { Destination = 157, Text = "На Ангулем" },
                    new Option { Destination = 573, Text = "Подниметесь к себе в комнату и ляжете спать" },
                }
            },
            [388] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 632, Text = "Удачливы" },
                    new Option { Destination = 198, Text = "Нет" },
                }
            },
            [389] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Далее" },
                }
            },
            [390] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 606, Text = "В Орлеан" },
                    new Option { Destination = 144, Text = "Пристать к берегу" },
                    new Option { Destination = 66, Text = "Убить хозяина лодки и управлять суденышком самому" },
                }
            },
            [391] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [392] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Далее" },
                }
            },
            [393] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 633, Text = "Спуститесь вниз по лесенке" },
                    new Option { Destination = 268, Text = "По коридору прямо" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Согласитесь выполнить его просьбу" },
                    new Option { Destination = 74, Text = "Откажетесь" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [396] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЧАСОВОЙ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Если вы успели расправиться с ним за 4 раунда атаки" },
                    new Option { Destination = 454, Text = "Не успели" },
                }
            },
            [397] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ШЕВАЛЬЕ ДЕ МИШУАР",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Далее" },
                }
            },
            [398] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [399] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 634, Text = "Удачливы" },
                    new Option { Destination = 474, Text = "Нет" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Ждать, пока он пройдет" },
                    new Option { Destination = 15, Text = "Ехать дальше, несмотря на непогоду" },
                }
            },
            [401] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Примете его приглашение" },
                    new Option { Destination = 341, Text = "Ответите, что у вас нет времени" },
                }
            },
            [402] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Вернетесь к лейтенанту и поговорите с ним" },
                    new Option { Destination = 174, Text = "Прорываться во что бы то ни стало" },
                }
            },
            [403] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Pursuit",
                        ButtonName = "Погоня",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Если вас не догнали" },
                    new Option { Destination = 90, Text = "Если догнали" },
                    new Option { Destination = 159, Text = "Однако если вы проиграли три раза подряд" },
                }
            },
            [404] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Далее" },
                }
            },
            [405] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Направите лошадь на солдат" },
                    new Option { Destination = 153, Text = "Вернетесь" },
                }
            },
            [408] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Далее" },
                }
            },
            [409] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Свернуть в лес" },
                    new Option { Destination = 161, Text = "Попытаетесь спрятаться за редкими деревьями" },
                    new Option { Destination = 238, Text = "Продолжите путь, не сворачивая" },
                }
            },
            [410] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 342, Text = "Удачливы" },
                    new Option { Destination = 103, Text = "Нет" },
                }
            },
            [411] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Сделаете это" },
                    new Option { Destination = 218, Text = "Предпочтете разобраться с хозяином дома" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Далее" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Решите, что вам подсыпали снотворного" },
                    new Option { Destination = 293, Text = "Спокойно доужинаете и пойдете к себе в комнату спать" },
                }
            },
            [414] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Остановитесь на первом" },
                    new Option { Destination = 18, Text = "На втором" },
                }
            },
            [415] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Пойдете искать другой" },
                    new Option { Destination = 219, Text = "Постараетесь поесть, как ни в чем не бывало" },
                    new Option { Destination = 104, Text = "Вынете из седельной кобуры пистолет" },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Позавтракать" },
                    new Option { Destination = 30, Text = "В конюшню седлать Арбалета" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [418] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
                }
            },
            [419] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Покинете дом и побыстрее уедете" },
                    new Option { Destination = 340, Text = "Подниметесь на второй этаж" },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [422] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Попытаться скрыться" },
                    new Option { Destination = 113, Text = "Безропотно сядете в карету" },
                }
            },
            [423] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 281, Text = "Удачливы" },
                    new Option { Destination = 31, Text = "Нет" },
                }
            },
            [424] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Далее" },
                }
            },
            [425] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Далее" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Далее" },
                }
            },
            [427] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Далее" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Далее" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [430] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Далее" },
                }
            },
            [431] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 529, Text = "Если наступила ночь" },
                    new Option { Destination = 130, Text = "В Париж" },
                }
            },
            [432] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ СОЛДАТ",
                                Skill = 6,
                                Strength = 6,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ СОЛДАТ",
                                Skill = 7,
                                Strength = 6,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 372, Text = "Далее" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 601, Text = "Далее" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Далее" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 390, Text = "До Блуа" },
                    new Option { Destination = 530, Text = "До Орлеана" },
                    new Option { Destination = 250, Text = "Вернуться" },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 226, Text = "Далее" },
                }
            },
            [437] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "Нет ли в Лиможе армии герцога Майенского" },
                    new Option { Destination = 132, Text = "Нет ли на дороге каких-нибудь засад" },
                }
            },
            [438] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ МОНАХ",
                                Skill = 7,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ МОНАХ",
                                Skill = 7,
                                Strength = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [439] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 133, Text = "Так и поступите" },
                    new Option { Destination = 602, Text = "Нет" },
                }
            },
            [440] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Дойти до трактира и позавтракать" },
                    new Option { Destination = 374, Text = "Поскорее отправитесь в путь" },
                }
            },
            [441] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Далее" },
                }
            },
            [442] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 376, Text = "Если у вас есть пистолет" },
                    new Option { Destination = 134, Text = "Если умеете стрелять из двух сразу," },
                    new Option { Destination = 532, Text = "Если пистолета нет" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Согласитесь" },
                    new Option { Destination = 9, Text = "Дела торопят" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Далее" },
                }
            },
            [448] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -600,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [449] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Уехать из Тура" },
                    new Option { Destination = 533, Text = "Попытайтесь разбогатеть по-другому" },
                }
            },
            [450] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 287, Text = "Далее" },
                }
            },
            [451] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОЛК",
                                Skill = 7,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВОЛЧИЦА",
                                Skill = 6,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Далее" },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 652, Text = "Далее" },
                }
            },
            [453] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 391, Text = "Заглянуть в трактир" },
                    new Option { Destination = 141, Text = "Покинуть город" },
                }
            },
            [454] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Вас послал к нему Король Генрих" },
                    new Option { Destination = 595, Text = "Покажете перстень" },
                    new Option { Destination = 635, Text = "Согласны выслушивать вопросы только от самого господина де Перигю" },
                }
            },
            [455] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "HadFoodToday",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = 3,
                    },
                    new Modification
                    {
                        Name = "Day",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Хотите погибнуть в бою" },
                    new Option { Destination = 67, Text = "Сдадитесь" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 393, Text = "Направо" },
                    new Option { Destination = 268, Text = "Налево" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 206, Text = "Далее" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Вернуться на ту же дорогу" },
                    new Option { Destination = 636, Text = "Продолжать скакать прямо" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "На Шатоден" },
                    new Option { Destination = 596, Text = "На Орлеан" },
                    new Option { Destination = 606, Text = "Подняться до Орлеана вверх по Луаре" },
                }
            },
            [461] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Далее" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Вы — граф де Монлюк, бывший приближенный Генриха III" },
                    new Option { Destination = 637, Text = "Вы — шевалье де Рево, католик и лигист из Беарна" },
                    new Option { Destination = 270, Text = "Вы — виконт де Тессе из Парижа" },
                }
            },
            [463] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 647, Text = "Удачливы" },
                    new Option { Destination = 207, Text = "Нет" },
                }
            },
            [464] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГЕРЦОГ ДЕ ЛАВЕЛЕТТ",
                                Skill = 6,
                                Strength = 14,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 318, Text = "Далее" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 597, Text = "Далее" },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Громко позовете мельника Жозефа" },
                    new Option { Destination = 75, Text = "Обойдете дом" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 563, Text = "Далее" },
                }
            },
            [468] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 598, Text = "Далее" },
                }
            },
            [469] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 648, Text = "Удачливы" },
                    new Option { Destination = 311, Text = "Нет" },
                }
            },
            [470] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Далее" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [472] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Поедете в город" },
                    new Option { Destination = 204, Text = "Спуститесь по реке до Вьерзона" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Далее" },
                }
            },
            [474] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 506, Text = "Войдете в нее" },
                    new Option { Destination = 565, Text = "Свернете" },
                }
            },
            [476] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Если вы уже были в тюрьме" },
                    new Option { Destination = 307, Text = "Если нет" },
                }
            },
            [477] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Далее" },
                }
            },
            [478] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [479] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 260, Text = "Направитесь в город переночевать" },
                    new Option { Destination = 191, Text = "Объедете его и поторопитесь в Париж" },
                }
            },
            [480] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Далее" },
                }
            },
            [481] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Возмутитесь и потребуете деньги обратно" },
                    new Option { Destination = 516, Text = "Приведете комнату в порядок и расположитесь в ней на ночлег" },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Далее" },
                }
            },
            [483] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Далее" },
                }
            },
            [484] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Выстрелите в негодяя" },
                    new Option { Destination = 344, Text = "Постараетесь достать его шпагой" },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Если у вас есть с собой заряженный пистолет" },
                    new Option { Destination = 377, Text = "Если нет" },
                }
            },
            [486] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [487] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Далее" },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Далее" },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Показать перстень незнакомцу" },
                    new Option { Destination = 34, Text = "Скажете, что не понимаете, о чем идет речь" },
                    new Option { Destination = 136, Text = "Постараться объяснить, что вы тот, кого он ищет, только без опознавательного знака" },
                }
            },
            [490] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Согласитесь" },
                    new Option { Destination = 346, Text = "Откажетесь" },
                }
            },
            [491] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 233, Text = "Въедете в аббатство, приготовившись к бою" },
                    new Option { Destination = 145, Text = "Попытаетесь прорваться через навязчивых монахов" },
                }
            },
            [492] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Удачливы" },
                    new Option { Destination = 35, Text = "Нет" },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Далее" },
                }
            },
            [494] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГРАФ МОРТОН",
                                Skill = 10,
                                Strength = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 347, Text = "Сохранить ему жизнь" },
                    new Option { Destination = 384, Text = "Прикончить свидетеля вашей глупой ошибки" },
                    new Option { Destination = 36, Text = "Если вы проиграли" },
                }
            },
            [495] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Далее" },
                }
            },
            [496] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 380, Text = "Далее" },
                }
            },
            [497] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Удачливы" },
                    new Option { Destination = 348, Text = "Нет" },
                }
            },
            [498] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 548, Text = "Уже встречались с подобным трюком" },
                    new Option { Destination = 483, Text = "Нет" },
                }
            },
            [499] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Покинуть Ангулем" },
                    new Option { Destination = 480, Text = "Попробовать освободить своего единоверца" },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 138, Text = "Подъедете поближе" },
                    new Option { Destination = 488, Text = "Объехать это место" },
                    new Option { Destination = 335, Text = "Выстрелить" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 604, Text = "Далее" },
                }
            },
            [502] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "У вас есть пистолет" },
                    new Option { Destination = 447, Text = "У вас есть два пистолета" },
                    new Option { Destination = 381, Text = "Драться" },
                }
            },
            [503] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Если кто-нибудь покидал дом после начала боя" },
                    new Option { Destination = 605, Text = "Если нет" },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 404, Text = "Далее" },
                }
            },
            [505] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "Если согласны" },
                    new Option { Destination = 18, Text = "Попытаться освободить единоверца завтра утром самостоятельно" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Далее" },
                }
            },
            [507] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 517, Text = "Будете драться" },
                    new Option { Destination = 39, Text = "Постараетесь объяснить, в чем дело" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Попытаться остановить экипаж" },
                    new Option { Destination = 140, Text = "Просто съедете с дороги" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "На рынок" },
                    new Option { Destination = 391, Text = "В трактир" },
                    new Option { Destination = 141, Text = "Постараетесь побыстрее уехать" },
                }
            },
            [510] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ УБИЙЦА",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ УБИЙЦА",
                                Skill = 8,
                                Strength = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [511] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Далее" },
                }
            },
            [512] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Далее" },
                }
            },
            [513] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 294, Text = "Далее" },
                }
            },
            [514] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 383, Text = "Повезло" },
                    new Option { Destination = 142, Text = "Нет" },
                }
            },
            [515] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Далее" },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [517] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ СОЛДАТ",
                                Skill = 7,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ СОЛДАТ",
                                Skill = 6,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Далее" },
                }
            },
            [518] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Далее" },
                }
            },
            [519] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГРАФ МОРТОН",
                                Skill = 10,
                                Strength = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 384, Text = "Далее" },
                }
            },
            [520] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [521] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Далее" },
                }
            },
            [522] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 385, Text = "Пересечь Тур" },
                    new Option { Destination = 489, Text = "Поспешите на встречу с курьером" },
                }
            },
            [523] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ОБОРВАНЕЦ",
                                Skill = 6,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ОБОРВАНЕЦ",
                                Skill = 7,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ОБОРВАНЕЦ",
                                Skill = 7,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Далее" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 623, Text = "Далее" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 380, Text = "Далее" },
                }
            },
            [527] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [528] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 131, Text = "Заехать на рынок" },
                    new Option { Destination = 387, Text = "Прислушаться к подозрительному разговору" },
                }
            },
            [529] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 144, Text = "Прикажете лодочнику пристать" },
                    new Option { Destination = 606, Text = "Поплывете до Орлеана" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 374, Text = "Далее" },
                }
            },
            [532] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ КАВАЛЕРИСТ",
                                Skill = 7,
                                Strength = 9,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ КАВАЛЕРИСТ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [533] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "СОЛДАТ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Далее" },
                }
            },
            [534] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Поскачете дальше" },
                    new Option { Destination = 106, Text = "Поищете какое-нибудь укрытие" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Согласитесь позавтракать" },
                    new Option { Destination = 220, Text = "Постараетесь выбить из него эти сведения" },
                    new Option { Destination = 176, Text = "Добиться от хозяина, кто и зачем устроил эту ловушку" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "На запад, в Перигё" },
                    new Option { Destination = 353, Text = "На север, в Люберсак" },
                }
            },
            [537] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ВСАДНИК",
                                Skill = 8,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [538] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Примете бой" },
                    new Option { Destination = 536, Text = "Постараетесь побыстрее ускакать прочь" },
                }
            },
            [539] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 177, Text = "Кинуться между вепрем и охотником" },
                    new Option { Destination = 221, Text = "Своя жизнь и данное поручение важнее" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 20, Text = "Прикажете перед отъездом подать себе завтрак" },
                    new Option { Destination = 107, Text = "Поедете натощак" },
                }
            },
            [541] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Далее" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Есть огниво" },
                    new Option { Destination = 485, Text = "Нет" },
                }
            },
            [543] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Выстрелите первым" },
                    new Option { Destination = 89, Text = "Подождете, пока они подъедут поближе" },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Согласитесь на это" },
                    new Option { Destination = 346, Text = "Откажетесь" },
                }
            },
            [545] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Далее" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [547] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Поедете теперь по турской дороге" },
                    new Option { Destination = 482, Text = "Развернетесь обратно" },
                }
            },
            [548] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Далее" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Далее" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Нападете на него" },
                    new Option { Destination = 449, Text = "Еще немного обождете" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Далее" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 363, Text = "Далее" },
                }
            },
            [553] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Попытаться как-то проникнуть в Ратушу" },
                    new Option { Destination = 455, Text = "Попросить у хозяина один из номеров и лечь спать" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Попробуете открыть дверь" },
                    new Option { Destination = 151, Text = "Поищете черный ход" },
                    new Option { Destination = 71, Text = "Поедете по другому адресу" },
                    new Option { Destination = 562, Text = "Направитесь к себе домой" },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 456, Text = "Поедете вслед за ним" },
                    new Option { Destination = 250, Text = "Вернетесь" },
                }
            },
            [557] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Фортуна с вами" },
                    new Option { Destination = 271, Text = "Она отвернулась от вас" },
                }
            },
            [558] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Поедете по ней" },
                    new Option { Destination = 428, Text = "Не станете сворачивать" },
                }
            },
            [559] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Далее" },
                }
            },
            [560] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 609, Text = "Направо" },
                    new Option { Destination = 457, Text = "Налево" },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 188, Text = "Далее" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 458, Text = "Откроете дверь своим ключом и войдете" },
                    new Option { Destination = 649, Text = "Обойдете вокруг, чтобы убедиться, что все спокойно" },
                }
            },
            [563] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "Развернете коня, чтобы вернуться в Люберсак" },
                    new Option { Destination = 396, Text = "Поедете через заставу" },
                }
            },
            [564] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 631, Text = "Пойдете по нему" },
                    new Option { Destination = 393, Text = "Нет" },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 459, Text = "Столько СИЛ вы потеряли, пытаясь скрыться" },
                    new Option { Destination = 264, Text = "Если же выпали одинаковые числа, — то 264" },
                }
            },
            [567] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 610, Text = "Если вы приехали в Тур с королевским перстнем" },
                    new Option { Destination = 146, Text = "Если без него" },
                }
            },
            [568] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Дуэль с де Мишуаром" },
                    new Option { Destination = 55, Text = "Шевалье прав и зайти в дом" },
                }
            },
            [569] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Согласитесь иметь с ней дело дальше" },
                    new Option { Destination = 460, Text = "Распрощаетесь" },
                }
            },
            [570] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Пощадите его" },
                    new Option { Destination = 398, Text = "Убьете его" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [572] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 266, Text = "Попробовать догнать ее" },
                    new Option { Destination = 140, Text = "Поедете дальше" },
                }
            },
            [573] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "На Лимож" },
                    new Option { Destination = 611, Text = "На Ангулем" },
                }
            },
            [574] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Да" },
                    new Option { Destination = 259, Text = "Нет" },
                }
            },
            [575] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОЛОДОЙ ДВОРЯНИН",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Далее" },
                }
            },
            [576] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [577] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 554, Text = "Далее" },
                }
            },
            [578] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 313, Text = "Обратиться к помощи Бога" },
                    new Option { Destination = 256, Text = "Рассказать все, как есть" },
                }
            },
            [579] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Громко позвать мельника Жозефа" },
                    new Option { Destination = 75, Text = "Осторожно обойти дом" },
                }
            },
            [580] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Повезло" },
                    new Option { Destination = 655, Text = "Нет" },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Далее" },
                }
            },
            [582] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 478, Text = "Войдете" },
                    new Option { Destination = 393, Text = "Направо" },
                    new Option { Destination = 268, Text = "Налево" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [586] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 314, Text = "Далее" },
                }
            },
            [587] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 619, Text = "Далее" },
                }
            },
            [588] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [589] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [590] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -300,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 315, Text = "Вы знаете это" },
                    new Option { Destination = 656, Text = "Нет" },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Далее" },
                }
            },
            [593] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Войдете в нее" },
                    new Option { Destination = 475, Text = "Пойдете дальше" },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Далее" },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [596] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 657, Text = "Свернете с пути на тропку" },
                    new Option { Destination = 473, Text = "Поедете дальше" },
                }
            },
            [597] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [598] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 469, Text = "Попробуете перепрыгнуть овраг" },
                    new Option { Destination = 201, Text = "Спешитесь и постараетесь аккуратно пройти по бревнам" },
                }
            },
            [599] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Далее" },
                }
            },
            [600] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Задержаться, чтобы перебить устроивших засаду" },
                    new Option { Destination = 64, Text = "Поскорее в Париж" },
                }
            },
            [601] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Далее" },
                }
            },
            [602] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Далее" },
                }
            },
            [603] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Заплатить и уехать" },
                    new Option { Destination = 461, Text = "Вино" },
                    new Option { Destination = 298, Text = "Ужин" },
                }
            },
            [604] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 260, Text = "Заедете, чтобы переночевать" },
                    new Option { Destination = 191, Text = "Обогнете город" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Спросите, какой она веры" },
                    new Option { Destination = 597, Text = "С благодарностью примете ладанку" },
                }
            },
            [606] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Ecu",
                        Value = -200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 627, Text = "На рынок" },
                    new Option { Destination = 54, Text = "В город" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Попробуете проникнуть в здание через главный вход" },
                    new Option { Destination = 299, Text = "Поищете обходные пути" },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 466, Text = "Если у вас есть отмычка" },
                    new Option { Destination = 276, Text = "Если нет" },
                }
            },
            [609] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 593, Text = "Направо" },
                    new Option { Destination = 203, Text = "Налево" },
                }
            },
            [610] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 256, Text = "Признаетесь, что вас послал Король и расскажете все как есть" },
                    new Option { Destination = 300, Text = "Будете отпираться дальше" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Вернетесь назад и выберете дорогу на Лимож" },
                    new Option { Destination = 463, Text = "Попробуете переехать речку вброд рядом с мостом" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Далее" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "Далее" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 468, Text = "Послушаете, что он хочет сказать" },
                    new Option { Destination = 301, Text = "Будете драться" },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [616] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОНЕЦ",
                                Skill = 7,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Далее" },
                }
            },
            [617] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Далее" },
                }
            },
            [618] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 303, Text = "Удачливы" },
                    new Option { Destination = 644, Text = "Нет" },
                }
            },
            [619] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Сесть на небольшое судно" },
                    new Option { Destination = 278, Text = "Доплыть почти до самого Орийака" },
                    new Option { Destination = 528, Text = "Поторопитесь, чтобы до захода солнца добраться до Перигё" },
                }
            },
            [620] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Выбираться из города" },
                    new Option { Destination = 557, Text = "Ждать вечера и пытаться встретиться с курьером" },
                }
            },
            [621] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Далее" },
                }
            },
            [622] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Вскрывать письмо" },
                    new Option { Destination = 279, Text = "Нет" },
                }
            },
            [623] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 386, Text = "Далее" },
                }
            },
            [624] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [625] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее" },
                }
            },
            [626] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [627] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [628] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 606, Text = "Спуститься по реке до Луары" },
                    new Option { Destination = 128, Text = "Вернуться на дорогу," },
                    new Option { Destination = 267, Text = "Обогнуть замок с другой стороны" },
                }
            },
            [629] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Да" },
                    new Option { Destination = 584, Text = "Нет" },
                    new Option { Destination = 81, Text = "Ленотр никогда и не был председателем парламента" },
                }
            },
            [630] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [631] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 478, Text = "Войдете в нее" },
                    new Option { Destination = 593, Text = "Идти направо" },
                    new Option { Destination = 203, Text = "Идти налево" },
                }
            },
            [632] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Далее" },
                }
            },
            [633] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Правую" },
                    new Option { Destination = 274, Text = "Левую" },
                }
            },
            [634] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 606, Text = "Вниз по Луаре в Орлеан" },
                    new Option { Destination = 56, Text = "Обратно на дорогу" },
                }
            },
            [635] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 650, Text = "Король Генрих" },
                    new Option { Destination = 305, Text = "Нищий в трактире" },
                }
            },
            [636] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [637] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 299, Text = "Далее" },
                }
            },
            [638] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "На Тур" },
                    new Option { Destination = 204, Text = "Еа Вьерзон" },
                }
            },
            [639] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "В Тура" },
                    new Option { Destination = 179, Text = "В Париж" },
                    new Option { Destination = 105, Text = "До Невера" },
                }
            },
            [640] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [641] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [642] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Далее" },
                }
            },
            [643] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Войдете в нее" },
                    new Option { Destination = 149, Text = "Свернете" },
                }
            },
            [644] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [645] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [646] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [647] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Далее" },
                }
            },
            [648] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [649] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "Далее" },
                }
            },
            [650] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [651] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [652] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПОСЛАНЕЦ ГУБЕРНАТОРА",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 476, Text = "Подчинитесь" },
                    new Option { Destination = 205, Text = "Если вы убили его" },
                }
            },
            [653] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Далее" },
                }
            },
            [654] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [655] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [656] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Далее" },
                }
            },
            [657] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МЕДВЕДЬ",
                                Skill = 1,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если вы ещё живы, возвращайтесь обратно и торопитесь в Орлеан: опасность давно уже миновала.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 473, Text = "Попытаетесь убежать" },
                    new Option { Destination = 362, Text = "В Орлеан" },
                }
            },
            [658] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [659] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Далее" },
                }
            },
            [660] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться тайному удару",
                        Text = "ТАЙНЫЙ УДАР ШПАГОЙ",
                        MeritalArt = Character.MeritalArts.SecretBlow,
                        Aftertext = "Среди фехтовальщиков и дуэлянтов всегда ходили легенды о существовании ударов, которые невозможно отразить. Они приносили настоящему мастеру верную победу в поединке. Одним из таких ударов можете овладеть вы. Независимо от того, в какой момент боя он применен и бросаете вы кубик или нет, можете сразу вычесть у своего противника 4 СИЛЫ. Но сделать это во время схватки можно только один раз. Ведь после этого враг узнает о вашем секрете и сможет парировать выпад.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться бою со шпагой и кинжалом",
                        Text = "БОЙ СО ШПАГОЙ И КИНЖАЛОМ",
                        MeritalArt = Character.MeritalArts.SwordAndDagger,
                        Aftertext = "Овладев этим искусством, вы сможете во время поединка взять в левую руку кинжал и наносить им удары по не владеющему подобным умением противнику. Конечно, для честной дуэли это не слишком-то подходит (если, разумеется, не оговорено условиями поединка), но в бою может не раз сохранить вам жизнь. Каждый раз, когда у вас на кубике выпадет четное число, считайте, что вы сумели воспользоваться кинжалом. В этом случае, если раунд атаки вы выиграли, вычтите у противника не 2, а 3 СИЛЫ; если же проиграли, то вычитаете 2 СИЛЫ не только у себя, но и у противника.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться стрельбе с двух рук",
                        Text = "СТРЕЛЬБА ИЗ ДВУХ ПИСТОЛЕТОВ",
                        MeritalArt = Character.MeritalArts.TwoPistols,
                        Aftertext = "В разделе «Огнестрельное оружие» уже говорилось о том, что выстрелить из пистолета в XVI веке было гораздо сложнее, чем в XX. Тем более, благодаря относительно большому весу оружия, одновременно прицельно выстрелить из двух пистолетов совсем не так-то просто. Однако вы можете научиться и этому искусству. При этом можете направить оружие как на одного, так и на разных врагов. В первом случае, не проверяя УДАЧУ, вы вычитаете у противника сразу 8 СИЛ. Возможно также после любого из выстрелов проверить УДАЧУ, в соответствии с правилами из раздела «Огнестрельное оружие». Конечно, убить одного человека два раза невозможно, но вам вряд ли придет это в голову. Если же вы стреляете в разных врагов, то поступайте в соответствии с теми же правилами.\n\nОбратите внимание: в том случае, если вы не владеете этим искусством, но у вас есть два пистолета, то будет возможность выстрелить из них только по очереди. Причем вряд ли такой случай представится часто: ведь после первого же выстрела чаще всего придется скрестить шпаги со своими врагами, и времени поменять пистолет уже не будет.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться фехтованию левой рукой",
                        Text = "ФЕХТОВАНИЕ ЛЕВОЙ РУКОЙ",
                        MeritalArt = Character.MeritalArts.LefthandFencing,
                        Aftertext = "Если вас ранили в правую руку, то волей-неволей лучше взять шпагу в левую. Если вы не овладели этим искусством, то придется до тех пор, пока рана не заживет, уменьшать свою ЛОВКОСТЬ, а это может быть весьма опасно для жизни. Те же правила будут действовать, когда ваш противник — левша. Неподготовленному человеку в таких случаях приходится нелегко: ведь удары приходится парировать совсем по-другому.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться плаванию",
                        Text = "ПЛАВАНИЕ",
                        MeritalArt = Character.MeritalArts.Swimming,
                        Aftertext = "В небольшом имении, где вы выросли, не было ни пруда, ни реки, поэтому плавать вы так и не научились. Но никогда не поздно наверстать упущенное. Конечно, реку можно переплыть и держась за лошадь, а того проще перейти по мосту, но кто знает, какие приключения ждут впереди.",
                    },
                },


                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "Далее" },
                    new Option { Destination = 381, Text = "ТЕСТ" },
                }
            },
        };
    }
}
