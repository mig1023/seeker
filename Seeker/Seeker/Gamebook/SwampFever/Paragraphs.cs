using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.SwampFever
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
            paragraph.RemoveTrigger = source.RemoveTrigger;
            paragraph.Image = source.Image;

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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ТЕРМИТ",
                        EnemyCombination = "5-4",

                        Aftertext = "Ментор советует применить тактику 'Снайпера' или 'Диверсанта' (так комбинации '6-3-3' и '4-1-1' принесут стопроцентную победу). Если в схватке с термитом вы одержите верх, то в качестве бонуса компания подарит вам 5 кредов. Иначе придётся начинать странствие без денег.",

                        Benefit = new Modification
                        {
                            Name = "Creds",
                            Value = 5,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Рискнёте сразиться с монстром" },
                    new Option { Destination = 100, Text = "Предпочтёте ретироваться" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "Примете предложение служебного бота" },
                    new Option { Destination = 27, Text = "Откажитесь, учитывая то, что время полёта будет длиться около тридцати дней" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Вы согласны и покидаете харвестер" },
                    new Option { Destination = 29, Text = "В противном случае" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее", OnlyIf = "2" },
                    new Option { Destination = 20, Text = "Далее", OnlyIf = "!2" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "На север - Пухлый Солончак" },
                    new Option { Destination = 150, Text = "На юго-запад - Грибная Чаща" },
                    new Option { Destination = 128, Text = "На юг - Гнилотопь" },
                    new Option { Destination = 106, Text = "На восток - Кристаллесье" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [09] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Согласитесь" },
                    new Option { Destination = 90, Text = "Откажитесь" },
                }
            },
            [10] = new Paragraph
            {
                Image = "SwampFever_SpectralBushes.jpg",

                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "По первой" },
                    new Option { Destination = 88, Text = "По второй" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 124, Text = "Рискнёте покинуть харвестер, чтобы его почистить" },
                    new Option { Destination = 136, Text = "Включив огнемёт, вы помчитесь вперёд" },
                }
            },
            [12] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "На север - Тимение Искупления" },
                    new Option { Destination = 56, Text = "На запад - Павший Колосс" },
                    new Option { Destination = 46, Text = "На юг - Запределье" },
                    new Option { Destination = 131, Text = "На восток - Край Ветров" },
                }
            },
            [15] = new Paragraph
            {
                Trigger = "11",

                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Creds",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "БЛУДЕНЬ",
                        EnemyCombination = "6-6-5-5-4-4",

                        Aftertext = "Ничья невозможна, т. к. безумный враг будет постоянно преследовать вас. Деритесь не на жизнь, а на смерть.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 138, Text = "Далее" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Покинуть Стигию, то можете оставаться в гостинице, ожидая своего фрегата" },
                    new Option { Destination = 30, Text = "Покинуть гостиницу" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Вы будете ехать дальше" },
                    new Option { Destination = 140, Text = "Остановитесь, чтобы рискнуть наладить с яти контакт" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Примите странное угощение" },
                    new Option { Destination = 132, Text = "Откажитесь" },
                }
            },
            [22] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "TrackPull",
                        ButtonName = "Гусеничный режим",
                        Aftertext = "или",
                    },
                    new Actions
                    {
                        ActionName = "PropellersPull",
                        ButtonName = "Режим амфибии",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Вы вытащили ялик" },
                    new Option { Destination = 65, Text = "Ялик утонул" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "На север - Павший Колосс" },
                    new Option { Destination = 39, Text = "На запад - Свалка" },
                    new Option { Destination = 64, Text = "На юг - Тихий Омут" },
                    new Option { Destination = 46, Text = "На восток - Запределье" },
                }
            },
            [24] = new Paragraph
            {
                Trigger = "15",

                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее" },
                }
            },
            [25] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "SellAcousticMembrane",
                        ButtonName = "Продать Акустическую Мембрану",
                    },
                     new Actions
                    {
                        ActionName = "SellLiveMucus",
                        ButtonName = "Продать Живую Слизь",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Подъехать к термитнику" },
                    new Option { Destination = 142, Text = "Выстрелить в яму" },
                    new Option { Destination = 48, Text = "Вернуться на основную дорогу" },
                }
            },
            [27] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 109, Text = "Рискнёте расплавить лёд" },
                    new Option { Destination = 147, Text = "Не будете рисковать" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Гостиница" },
                    new Option { Destination = 34, Text = "Элеватор" },
                    new Option { Destination = 40, Text = "Техцентр" },
                    new Option { Destination = 25, Text = "Обсерватория" },
                    new Option { Destination = 45, Text = "Покинете Порт Грёз" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее", OnlyIf = "19" },
                    new Option { Destination = 68, Text = "Далее", OnlyIf = "!19" },
                }
            },
            [32] = new Paragraph
            {
                Trigger = "22",

                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Помочь ликвидировать возгорание" },
                    new Option { Destination = 98, Text = "Не помогать" },
                }
            },
            [33] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 4,
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Проверка успешно пройдена" },
                    new Option { Destination = 71, Text = "Провалена" },
                }
            },
            [34] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "SellStigon",
                        ButtonName = "Продать стигон",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Pursuit",
                        ButtonName = "В погоню",
                        Aftertext = "За победу в испытании забирайте 1 кубометр стигона. При проигрыше вы сильно рассердитесь (+ 1 к Ярости).",
                    },
                },

                Trigger = "16",

                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [36] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Rate",
                        Value = 15,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [37] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ЗАГРЕБАЛО",
                        EnemyCombination = "5-4-4-4-4-4-2",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Вы избежали участи быть съеденным и одолеете врага" },
                    new Option { Destination = 103, Text = "При маловероятной ничьей чудище само отступит" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 95, Text = "Рискнёте туда заехать" },
                    new Option { Destination = 114, Text = "Не будете рисковать" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее", OnlyIf = "3" },
                    new Option { Destination = 129, Text = "Далее", OnlyIf = "!3" },
                }
            },
            [40] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Второй двигатель, 40 кредитов",
                        ButtonName = "Купить двигатель",
                        Price = 40,

                        Benefit = new Modification
                        {
                            Name = "SecondEngine",
                            Value = 1,
                        },
                    },

                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Стелс-покрытие, 20 кредитов",
                        ButtonName = "Купить покрытие",
                        Price = 20,

                        Benefit = new Modification
                        {
                            Name = "Stealth",
                            Value = 1,
                        },
                    },

                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Радар, 20 кредитов",
                        ButtonName = "Купить радар",
                        Price = 20,

                        Benefit = new Modification
                        {
                            Name = "Radar",
                            Value = 1,
                        },
                    },

                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Циркулярная пила, 25 кредитов",
                        ButtonName = "Купить пилу",
                        Price = 25,

                        Benefit = new Modification
                        {
                            Name = "CircularSaw",
                            Value = 1,
                        },
                    },

                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Реактивный огнемёт, 30 кредитов",
                        ButtonName = "Купить огнемёт",
                        Price = 30,

                        Benefit = new Modification
                        {
                            Name = "Flamethrower",
                            Value = 1,
                        },
                    },

                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Спаренная плазмопушка, 35 кредитов",
                        ButtonName = "Купить плазмопушку",
                        Price = 30,

                        Benefit = new Modification
                        {
                            Name = "PlasmaCannon",
                            Value = 1,
                        },
                    },

                    new Actions
                    {
                        ActionName = "Get",
                        Text = "Гармонизатор, 50 кредитов",
                        ButtonName = "Купить гармонизатор",
                        Price = 50,
                        Aftertext = "Гармонизатор похож на метроном. При колебании маятника он излучает пси-волны. Данное устройство упрощает ментальные проверки на 1 (+1 к цифре МП)",

                        Benefit = new Modification
                        {
                            Name = "Harmonizer",
                            Value = 1,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "На северо-запад - Чертоги Холода" },
                    new Option { Destination = 91, Text = "На запад - Тернистая Лощина" },
                    new Option { Destination = 63, Text = "На юг - Клейкое Озеро" },
                    new Option { Destination = 131, Text = "На юго-восток - Край Ветров" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее", OnlyIf = "23" },
                    new Option { Destination = 165, Text = "Далее", OnlyIf = "22, !23" },
                    new Option { Destination = 32, Text = "Далее", OnlyIf = "21, !23, !22" },
                    new Option { Destination = 83, Text = "Далее", OnlyIf = "20, !23, !22, !21" },
                    new Option { Destination = 113, Text = "Далее", OnlyIf = "!20, !23, !22, !21" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "SulfurCavity",
                        ButtonName = "Пересечь падь",
                    },
                },

                Image = "SwampFever_SulfurCavity.jpg",

                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Испытание завершилось победой" },
                    new Option { Destination = 144, Text = "Поражением" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "На севере - Медная Заводь" },
                    new Option { Destination = 6, Text = "На западе - Старый Тракт" },
                    new Option { Destination = 39, Text = "На юге - Свалка" },
                    new Option { Destination = 56, Text = "На востоке - Павший Колосс" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 135, Text = "Развернётесь, чтобы выстрелить" },
                    new Option { Destination = 81, Text = "Попробуете скрыться" },
                }
            },
            [47] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AcousticMembrane",
                        Value = 1,
                    },
                },

                Trigger = "17",

                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "На северо-восток - Органная Пещера" },
                    new Option { Destination = 60, Text = "На юго-запад - Мегапровал" },
                    new Option { Destination = 143, Text = "На юг - Биполярный Кочкарник" },
                    new Option { Destination = 128, Text = "На восток - Гнилотопь" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "На север - Роща Инвазии" },
                    new Option { Destination = 115, Text = "На запад - Органная Пещера" },
                    new Option { Destination = 125, Text = "На юг - Медная Заводь" },
                    new Option { Destination = 87, Text = "На восток - Чертоги Холода" },
                }
            },
            [50] = new Paragraph
            {
                Trigger = "10",

                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Предупредить гидронейца об опасности" },
                    new Option { Destination = 110, Text = "Выстрелить в Чернохвоста" },
                    new Option { Destination = 70, Text = "Не вмешиваться в ход событий" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее", OnlyIf = "8" },
                    new Option { Destination = 104, Text = "Далее", OnlyIf = "!8" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "На север - Свалка" },
                    new Option { Destination = 145, Text = "На запад - Гремучая Долина" },
                    new Option { Destination = 166, Text = "На юг - Гигросаванна" },
                    new Option { Destination = 64, Text = "На восток - Тихий Омут" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "На северо-запад - Тимение Искупления" },
                    new Option { Destination = 63, Text = "На запад - Клейкое Озеро" },
                    new Option { Destination = 46, Text = "На юго-запад - Запределье" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "Далее", OnlyIf = "1" },
                    new Option { Destination = 112, Text = "Далее", OnlyIf = "!1" },
                }
            },
            [57] = new Paragraph
            {
                Trigger = "6",

                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Попытаетесь отговорить жечь ценное растение" },
                    new Option { Destination = 105, Text = "Атакуете безумца" },
                    new Option { Destination = 23, Text = "Просто проедете мимо" },
                }
            },
            [58] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Rate",
                        Value = -10,
                    },
                },

                Trigger = "3",

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "На север - Кристаллесье" },
                    new Option { Destination = 128, Text = "На запад - Гнилотопь" },
                    new Option { Destination = 42, Text = "На юг - Порт Грёз" },
                    new Option { Destination = 91, Text = "На восток - Тернистая Лощина" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее", OnlyIf = "14" },
                    new Option { Destination = 134, Text = "Далее", OnlyIf = "!14" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Откажитесь" },
                    new Option { Destination = 21, Text = "Согласитесь" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее", OnlyIf = "4" },
                    new Option { Destination = 148, Text = "Далее", OnlyIf = "!4" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее", OnlyIf = "17" },
                    new Option { Destination = 121, Text = "Далее", OnlyIf = "!17" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "На север - Органная Пещера" },
                    new Option { Destination = 150, Text = "На запад - Грибная Чаща" },
                    new Option { Destination = 6, Text = "На юг - Старый Тракт" },
                    new Option { Destination = 125, Text = "На восток - Медная Заводь" },
                }
            },
            [66] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 4,
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [67] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Rate",
                        Value = 50,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "МИМИКРОИД",
                        EnemyCombination = "6-5-4-3-2",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "При победе" },
                    new Option { Destination = 52, Text = "Ретироваться" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "На север - Старый Тракт" },
                    new Option { Destination = 123, Text = "На запад - Спектральные Кущи" },
                    new Option { Destination = 145, Text = "На юг - Гремучая Долина" },
                    new Option { Destination = 39, Text = "На восток - Свалка" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Trigger = "13",

                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "На север - Рыжая Мшара" },
                    new Option { Destination = 31, Text = "На запад - Чаруса Чудес" },
                    new Option { Destination = 172, Text = "На юг - Пчелиные Марши" },
                    new Option { Destination = 46, Text = "На северо-восток - Запределье" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "На север - Биполярный Кочкарник" },
                    new Option { Destination = 60, Text = "На северо-запад - Мегапровал" },
                    new Option { Destination = 145, Text = "На юго-восток - Гремучая Долина" },
                    new Option { Destination = 44, Text = "На восток - Сернистая Падь" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Далее" },
                }
            },
            [77] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ИЗОТРОНОХОД",
                        EnemyCombination = "6-6-5-5-2",

                        Aftertext = "Ничья с последующим отступлением невозможны, т.к. гидронейская техника превосходит вашу по скорости.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "Далее" },
                }
            },
            [78] = new Paragraph
            {
                Trigger = "8",

                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [79] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ТОПИГЛОТ",
                        EnemyCombination = "4-4-4-2-2-2",

                        Aftertext = "В случае поражения вы станете обедом для озёрного монстра.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Если победа за вами" },
                    new Option { Destination = 14, Text = "Ретироваться" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "На север - Клейкое Озеро" },
                    new Option { Destination = 119, Text = "На запад - Рыжая Мшара" },
                    new Option { Destination = 64, Text = "На юго-запад - Тихий Омут" },
                    new Option { Destination = 131, Text = "На северо-восток - Край Ветров" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [83] = new Paragraph
            {
                Trigger = "21",

                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Вы можете покинуть поселение" },
                    new Option { Destination = 122, Text = "Присоединиться к протестующим" },
                }
            },
            [84] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Далее" },
                }
            },
            [85] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Creds",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "На северо-восток - Грибная Чаща" },
                    new Option { Destination = 143, Text = "На восток - Биполярный Кочкарник" },
                    new Option { Destination = 123, Text = "На юго-восток - Спектральные Кущи" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Далее", OnlyIf = "9" },
                    new Option { Destination = 28, Text = "Далее", OnlyIf = "!9" },
                }
            },
            [88] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "СВАРИТЕЛЬ",
                        EnemyCombination = "5-5-5-4-3-2",

                        Aftertext = "При поражении вы превратитесь в бульон, а ваша машина в дырявую кастрюлю.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Если вы победите" },
                    new Option { Destination = 74, Text = "Ретироваться" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "БРОНЕТАНК",
                        EnemyCombination = "6-4-3-3",

                        Aftertext = "Ничья невозможна, т.к. вы раскрыли тайну двуличного шерифа, и он будет драться до последнего. При победе вы уничтожаете врага и благополучно достигаете Порта Грёз. При поражении Патс назовёт вас мёртвым грабителем.",
                    },
                },

                Trigger = "20",

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее", OnlyIf = "5" },
                    new Option { Destination = 11, Text = "Далее", OnlyIf = "!5" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "На север - Порт Грёз" },
                    new Option { Destination = 44, Text = "На запад - Сернистая Падь" },
                    new Option { Destination = 31, Text = "На юг - Чаруса Чудес" },
                    new Option { Destination = 119, Text = "На восток - Рыжая Мшара" },
                }
            },
            [94] = new Paragraph
            {
                Trigger = "7",

                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Вытащить собственность компании" },
                    new Option { Destination = 65, Text = "Проедете мимо" },
                }
            },
            [95] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Trigger = "18",

                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "На север - Гнилотопь" },
                    new Option { Destination = 143, Text = "На запад - Биполярный Кочкарник" },
                    new Option { Destination = 44, Text = "На юг - Сернистая Падь" },
                    new Option { Destination = 42, Text = "На восток - Порт Грёз" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [98] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Rate",
                        Value = 3,
                        Multiplication = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Далее" },
                }
            },
            [99] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [100] = new Paragraph
            {
                Trigger = "12",

                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее" },
                }
            },
            [101] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Проверка успешно пройдена, то вам удалось уйти в сторону" },
                    new Option { Destination = 82, Text = "Иначе корпус харвестера сгорает, а вы, находясь в капсуле водителя, теряете сознание" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "На севере - Грибная Чаща" },
                    new Option { Destination = 60, Text = "На западе - Мегапровал" },
                    new Option { Destination = 123, Text = "На юге - Спектральные Кущи" },
                    new Option { Destination = 6, Text = "На востоке - Старый Тракт" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Подъехать к тени ближе" },
                    new Option { Destination = 41, Text = "Ретироваться" },
                }
            },
            [105] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ОГНЕПОКЛОННИК",
                        EnemyCombination = "5-5-2",

                        Aftertext = "Если вы не будете зажарены заживо и победите, то сможете забрать 1 кубометр стигона. Если стычка завершится ничьёй, у вас будет выбор: сбежать или продолжить схватку.",

                        Benefit = new Modification
                        {
                            Name = "Stigon",
                            Value = 1,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Далее" },
                }
            },
            [106] = new Paragraph
            {
                Image = "SwampFever_CrystalPath.jpg",

                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "А" },
                    new Option { Destination = 33, Text = "В" },
                    new Option { Destination = 66, Text = "С" },
                }
            },
            [107] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "УТИЛИЗАТОР",
                        EnemyCombination = "6-5-5-5-4-3",

                        Aftertext = "Если бой завершится ничьёй, то вы можете прекратить огонь и подождать дальнейших действий робота.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Прекратить огонь и подождать дальнейших действий робота" },
                    new Option { Destination = 58, Text = "Вы победили" },
                    new Option { Destination = 19, Text = "В противном случае" },
                }
            },
            [108] = new Paragraph
            {
                Trigger = "4",

                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "МЕРЗЛОБА",
                        EnemyCombination = "5-5-4-3",

                        Aftertext = "Если победа за вами, то можете забрать 2 кубометра стигона. Если бой завершился ничьёй, то у вас есть выбор: продолжить стычку или ретироваться.",

                        Benefit = new Modification
                        {
                            Name = "Stigon",
                            Value = 2,
                        },
                    },
                },

                Trigger = "9",

                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Далее" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Огнемёт" },
                    new Option { Destination = 120, Text = "Плазмопушка" },
                }
            },
            [111] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Creds",
                        Value = 50,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Далее" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "На север - Тернистая Лощина" },
                    new Option { Destination = 42, Text = "На запад - Порт Грёз" },
                    new Option { Destination = 119, Text = "На юг - Рыжая Мшара" },
                    new Option { Destination = 63, Text = "На восток - Клейкое Озеро" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "'Да, стигон есть!'" },
                    new Option { Destination = 149, Text = "'Нет, я еду пустым'" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "На север - Сернистая Падь" },
                    new Option { Destination = 123, Text = "На северо-запад - Спектральные Кущи" },
                    new Option { Destination = 167, Text = "На юг - Дикий Зыбун" },
                    new Option { Destination = 31, Text = "На восток - Чаруса Чудес" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее", OnlyIf = "10" },
                    new Option { Destination = 50, Text = "Далее", OnlyIf = "!10" },
                }
            },
            [116] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Проверка пройдена" },
                    new Option { Destination = 78, Text = "Проверка провалена" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Последуете за ними" },
                    new Option { Destination = 48, Text = "Поедете дальше по тропе" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "На север - Чертоги Холода" },
                    new Option { Destination = 125, Text = "На запад - Медная Заводь" },
                    new Option { Destination = 56, Text = "На юг - Павший Колосс" },
                    new Option { Destination = 51, Text = "На восток - Тимение Искупления" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Далее", OnlyIf = "6" },
                    new Option { Destination = 57, Text = "Далее", OnlyIf = "!6" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [121] = new Paragraph
            {
                Image = "SwampFever_QuietPool.jpg",

                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "'20-17-15-9-6-10'" },
                    new Option { Destination = 97, Text = "'20-18-11-8-6-10'" },
                }
            },
            [122] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Rate",
                        Value = 60,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее", OnlyIf = "15" },
                    new Option { Destination = 10, Text = "Далее", OnlyIf = "!15" },
                }
            },
            [124] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Успехом" },
                    new Option { Destination = 71, Text = "Провалом" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Далее", OnlyIf = "13" },
                    new Option { Destination = 59, Text = "Далее", OnlyIf = "!13" },
                }
            },
            [126] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "МУТАВОРМ",
                        EnemyCombination = "5-5-5-5-3-3",

                        Aftertext = "Если вы одержите верх, то получите в награду 100 кредов. При ничьей возникнет выбор: продолжить схватку или отступить. При поражении вас уничтожат Резак-Лучи, кроме того, тварь мутирует.",

                        Trigger = "14",

                        Benefit = new Modification
                        {
                            Name = "Creds",
                            Value = 100,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "В случае победы" },
                    new Option { Destination = 100, Text = "Отступить" },
                }
            },
            [127] = new Paragraph
            {
                Trigger = "1",

                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Вы можете наблюдать за памятником из-за кустов" },
                    new Option { Destination = 36, Text = "Подъехать к статуе вплотную, чтобы исследовать ухо" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Далее", OnlyIf = "7" },
                    new Option { Destination = 94, Text = "Далее", OnlyIf = "!7" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Отстреливаться" },
                    new Option { Destination = 89, Text = "Ждать своей участи" },
                }
            },
            [130] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "LiveMucus",
                        Value = 1,
                    },
                },

                RemoveTrigger = "13",

                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее", OnlyIf = "16" },
                    new Option { Destination = 35, Text = "Далее", OnlyIf = "!16" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [133] = new Paragraph
            {
                Image = "SwampFever_BipolarKochkarnik.jpg",

                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Первому" },
                    new Option { Destination = 76, Text = "Второму" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Далее", OnlyIf = "12" },
                    new Option { Destination = 2, Text = "Далее", OnlyIf = "!12" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "На севере - Клейкое Озеро" },
                    new Option { Destination = 119, Text = "На западе - Рыжая Мшара" },
                    new Option { Destination = 64, Text = "На юго-западе - Тихий Омут" },
                    new Option { Destination = 131, Text = "На северо-востоке - Край Ветров" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [137] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ГРОМЫХАЛО",
                        EnemyCombination = "6-6-6-6-5-5-5",

                        Aftertext = "При ничейном исходе боя монстр отступит. При поражении ваш мозг взорвётся, не выдержав деструктивного звука.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Вы одолели крикливую тварь" },
                    new Option { Destination = 73, Text = "При ничейном исходе боя монстр отступит" },
                }
            },
            [138] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Далее" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [140] = new Paragraph
            {
                Trigger = "2",

                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Круг с отрезками внутри" },
                    new Option { Destination = 99, Text = "Круг с лучами наружу" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Далее", OnlyIf = "11" },
                    new Option { Destination = 133, Text = "Далее", OnlyIf = "!11" },
                }
            },
            [144] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Успехом" },
                    new Option { Destination = 13, Text = "Провалом" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Далее", OnlyIf = "18" },
                    new Option { Destination = 38, Text = "Далее", OnlyIf = "!18" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "На север - Разноцветье" },
                    new Option { Destination = 106, Text = "На запад - Кристаллесье" },
                    new Option { Destination = 91, Text = "На юг - Тернистая Лощина" },
                    new Option { Destination = 51, Text = "На юго-восток - Тимение Искупления" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 79, Text = "Притормозите" },
                    new Option { Destination = 153, Text = "Ускоритесь" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Далее", OnlyIf = "5" },
                    new Option { Destination = 48, Text = "Далее", OnlyIf = "!5" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [152] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Trigger = "5",

                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [154] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "МУТАВОРМ",
                        EnemyCombination = "6-6-6-6-3-3",

                        Aftertext = "Если вы одолеете врага, то получите в награду 150 кредов. При ничьей возникнет выбор: продолжить схватку или ретироваться. При поражении вас взорвут.",

                        Benefit = new Modification
                        {
                            Name = "Creds",
                            Value = 150,
                        },
                    },
                },

                Trigger = "14",

                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее" },
                }
            },
            [155] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Trigger = "30",

                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Рискнёте покинуть харвестер" },
                    new Option { Destination = 31, Text = "На север - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На запад - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На восток - Пчелиные Марши" },
                }
            },
            [157] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ОГНЕХВОСТ",
                        EnemyCombination = "5-5-3-2-2",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Если победа за вами" },
                    new Option { Destination = 169, Text = "Ретироваться" },
                    new Option { Destination = 13, Text = "При поражении животное забросает машину огненными шарами" },
                }
            },
            [158] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее", OnlyIf = "26" },
                    new Option { Destination = 185, Text = "Далее", OnlyIf = "!26" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее", OnlyIf = "28" },
                    new Option { Destination = 196, Text = "Далее", OnlyIf = "!28" },
                }
            },
            [161] = new Paragraph
            {
                Image = "SwampFever_LarchThorns.jpg",

                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "На север - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На запад - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На восток - Пчелиные Марши" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 188, Text = "Больше «Чемиод»" },
                    new Option { Destination = 190, Text = "Больше «Ноплюче»" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [165] = new Paragraph
            {
                Trigger = "23",

                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Изъявите желание участвовать в состязании" },
                    new Option { Destination = 158, Text = "Предпочтёте стоять в стороне" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 171, Text = "Если есть № 24" },
                    new Option { Destination = 200, Text = "Иначе" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее", OnlyIf = "27" },
                    new Option { Destination = 177, Text = "Далее", OnlyIf = "!27" },
                }
            },
            [168] = new Paragraph
            {
                Trigger = "29",

                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Согласитесь на игру" },
                    new Option { Destination = 178, Text = "Попробуете взять 'своё' силой" },
                    new Option { Destination = 194, Text = "Проедете мимо" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "На севере - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На западе - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На востоке - Пчелиные Марши" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "На запад - Роща Инвазии" },
                    new Option { Destination = 87, Text = "На юг - Чертоги Холода" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "Если есть № 25" },
                    new Option { Destination = 179, Text = "Иначе" },

                    new Option { Destination = 156, Text = "Далее", OnlyIf = "25" },
                    new Option { Destination = 179, Text = "Далее", OnlyIf = "!25" },
                }
            },
            [172] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее", OnlyIf = "30" },
                    new Option { Destination = 199, Text = "Далее", OnlyIf = "!30" },
                }
            },
            [173] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ГРОЗДЕГЛАВ",
                        EnemyCombination = "4-4-4-4-1-1",

                        Aftertext = "При ничьей произойдёт продолжение боя. Поражение означает гибель в чавкающей пасти.",
                    },
                },

                Trigger = "26",

                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Вторая 'А'" },
                    new Option { Destination = 197, Text = "Вторая 'О'" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "На севере - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На западе - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На востоке - Пчелиные Марши" },
                }
            },
            [176] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "TugOfWar",
                        ButtonName = "Сразиться",
                    },
                },

                Image = "SwampFever_TugOfWar",

                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Вы сделали три шага назад и победили" },
                    new Option { Destination = 187, Text = "Вы сделали три шага вперёд и проиграли" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Примете с ними бой" },
                    new Option { Destination = 161, Text = "Cпрячетесь в кустах" },
                }
            },
            [178] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "ТЕХНОКРОТ",
                        EnemyCombination = "5-5-5-2-2-2-2",

                        Aftertext = "Если одержите верх, то в качестве награды получите кубометр стигона. При ничьей можете отступить или продолжить схватку. При поражении ваше безжизненное тело станет объектом инопланетного изучения.",

                        Benefit = new Modification
                        {
                            Name = "Stigon",
                            Value = 1,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                    new Option { Destination = 194, Text = "При ничьей можете отступить" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Агрегат с квадратом" },
                    new Option { Destination = 162, Text = "Агрегат с треугольником" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее", OnlyIf = "29" },
                    new Option { Destination = 168, Text = "Далее", OnlyIf = "!29" },
                }
            },
            [181] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MentalTest",
                        ButtonName = "Ментальная проверка",
                        Level = 5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Успех" },
                    new Option { Destination = 186, Text = "Провал" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "На север - Тихий Омут" },
                    new Option { Destination = 166, Text = "На запад - Гигросаванна" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "На запад - Пухлый Солончак" },
                    new Option { Destination = 106, Text = "На юг - Кристаллесье" },
                    new Option { Destination = 160, Text = "На восток - Разноцветье" },
                }
            },
            [184] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        EnemyName = "МЕРТВОКРЫЛЫ",
                        EnemyCombination = "4-4-4-4-4-4",
                        Birds = true,

                        Aftertext = "В отличие от остальных врагов попадание на дистанции не означает победу. Вы убиваете лишь одну птицу. У второй останется комбинация «444». Но если вы точно поразите их и плазмопушкой, и огнемётом, то до ближнего боя дело не дойдёт. Против обеих птиц вблизи деритесь по очереди. При поражении капсула водителя будет вскрыта, а вы заклёваны до смерти.",
                    },
                },

                Trigger = "27",

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "'Я пришёл с миром'" },
                    new Option { Destination = 164, Text = "'Я тебя понимаю'" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [187] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Rate",
                        Value = 2,
                        Division = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [188] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                }
            },
            [189] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Hunt",
                        ButtonName = "Охотиться",

                        Aftertext = "Если охота завершилась удачно, то разрезав горб жертвы, вы получаете 1 кубометр стигона. При провале Стигиносец скрывается в неизвестном направлении (больше не увидите рогатого беглеца).",
                    },
                },

                Image = "SwampFever_Hunting.jpg",


                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Fury",
                        Value = 1,
                    },
                },

                Trigger = "28",

                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Stigon",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                }
            },
            [191] = new Paragraph
            {
                RemoveTrigger = "25",

                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "На севере - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На западе - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На востоке - Пчелиные Марши" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Птицы пробегают мимо" },
                    new Option { Destination = 184, Text = "Харвестер заметен со стороны" },
                }
            },
            [193] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Creds",
                        Value = 30,
                    },
                    new Modification
                    {
                        Name = "Rate",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "На юг - Органная Пещера" },
                    new Option { Destination = 159, Text = "На восток - Роща Инвазии" },
                }
            },
            [195] = new Paragraph
            {
                Trigger = "25",

                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Далее" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Пуститесь в погоню за Стигиносцем" },
                    new Option { Destination = 170, Text = "Не станете этого делать" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Согласитесь и поедете дальше" },
                    new Option { Destination = 178, Text = "Рискнёте отобрать стигон" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "На север - Гремучая Долина" },
                    new Option { Destination = 166, Text = "На восток - Гигросаванна" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Рискнёте ли забрать гнездо" },
                    new Option { Destination = 182, Text = "Решите не трогать его" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Проучите усатого наглеца" },
                    new Option { Destination = 169, Text = "Предпочтёте не связываться" },
                }
            },

        };
    }
}
