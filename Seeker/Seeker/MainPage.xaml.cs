using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;
using Seeker.Gamebook;

namespace Seeker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Gamebooks();
        }

        public void Gamebooks(bool toMain = false)
        {
            Game.Data.Clean();
            PageClean();

            MainScroll.BackgroundColor = Output.Constants.BACKGROUND;

            Text.Children.Add(Output.Interface.Text("Выберите книгу:", defaultParams: true));

            Game.Data.Actions = null;

            string LastMarker = String.Empty;

            foreach (Description gamebook in List.GetSortedBooks())
            {
                Output.Splitter.Add(gamebook, ref LastMarker, ref Options);

                if (!Game.Settings.IsEnabled("WithoutStyles"))
                    Options.Children.Add(Output.Interface.GamebookImage(gamebook));

                Options.Children.Add(Output.Buttons.Gamebook(gamebook,
                    (sender, e) => Gamebook_Click(gamebook.Book)));

                Output.Disclaimer.Gamebook(gamebook, ref Options);
            }

            Output.Interface.Footer(ref Footer, Settings_Click);

            UpdateStatus();

            if (toMain)
                ScrollToTop();
        }

        private void ScrollToTop() =>
            MainScroll.ScrollToAsync(MainScroll, ScrollToPosition.Start, true);

        public void DisableMethod(string name)
        {
            foreach (View option in Options.Children)
            {
                if (!(option is Button))
                {
                    continue;
                }

                Button button = option as Button;

                if (button.Text == name)
                {
                    button.IsEnabled = false;
                    button.BackgroundColor = Color.Default;
                }
            }
        }

        private void Gamebook_Click(string gamebook)
        {
            Game.Continue.CurrentGame(gamebook);

            try
            {
                Game.Xml.GameLoad(gamebook, DisableMethod);
            }
            catch (Exception ex)
            {
                Error("Ошибка XML-кода книги:", ex.Message);
                return;
            }

            Paragraph(0);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            PageClean();

            Output.Settings.Add(ref Action);
            Options.Children.Add(Output.Buttons.ClosePage((s, args) => Gamebooks(toMain: true)));

            ScrollToTop();
        }

        private void AddAllBookmarks(Dictionary<string, string> allBookmarks)
        {
            foreach (string bookmark in allBookmarks.Keys)
            {
                string bookmarkSave = $"{Game.Data.CurrentGamebook}-{allBookmarks[bookmark]}";

                StackLayout bookmarkLine = new StackLayout { Orientation = StackOrientation.Horizontal };

                Button load = Output.Buttons.Bookmark(
                    (s, args) => Continue_Click(bookmarkSave), bookmark, bookmark: true);

                load.HorizontalOptions = LayoutOptions.FillAndExpand;

                bookmarkLine.Children.Add(load);

                Button remove = Output.Buttons.Bookmark(
                    (s, args) => BookmarkRemove_Click(bookmarkSave),
                    Output.Constants.BOOKMARK_REMOVE, bookmark: true);

                remove.WidthRequest = 35;

                bookmarkLine.Children.Add(remove);

                Action.Children.Add(bookmarkLine);
            }
        }

        private void Bookmarks_Click()
        {
            PageClean();

            Entry field = Output.Interface.BookmarkField();

            Action.Children.Add(Output.Interface.Text(Output.Constants.BOOKMARK_SAVE_HEAD, bold: true));
            Action.Children.Add(field);

            Action.Children.Add(Output.Buttons.Bookmark(
                (s, args) => BookmarkSave_Click(field.Text),
                Output.Constants.BOOKMARK_SAVE, bottomMargin: true));

            Action.Children.Add(Output.Interface.Text(Output.Constants.BOOKMARK_LOAD_HEAD, bold: true));

            Dictionary<string, string> allBookmarks = Game.Bookmarks.List(out string _);

            if (allBookmarks.Count > 0)
                AddAllBookmarks(allBookmarks);
            else
                Action.Children.Add(Output.Interface.Text("Пока ещё нет ни одной закладки"));

            Options.Children.Add(Output.Buttons.Bookmark(
                (s, args) => Paragraph(Game.Data.CurrentParagraphID, reload: true),
                Output.Constants.BACK_LINK, topMargin: true));

            ScrollToTop();
        }

        private void BookmarkSave_Click(string bookmark)
        {
            Game.Bookmarks.Save(bookmark);
            Paragraph(Game.Data.CurrentParagraphID, reload: true);
        }

        public void Error(string errorType, string message)
        {
            PageClean();

            Output.Text errText = new Output.Text
            {
                Content = errorType,
                Bold = true,
                Size = Output.Interface.TextFontSize.Big,
            };

            Text.Children.Add(Output.Interface.Text(errText));

            Output.Text errMessage = new Output.Text
            {
                Content = message,
                Size = Output.Interface.TextFontSize.Big,
            };

            Text.Children.Add(Output.Interface.Text(errMessage));

            Options.Children.Add(Output.Buttons.ClosePage((s, arg) => Gamebooks(toMain: true)));
        }

        private async void Speach(string text)
        {
            List<string> lines = text.Split(new string[] { "\\n\\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string line in lines)
            {
                List<string> phrases = line.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string phrase in phrases)
                    await TextToSpeech.SpeakAsync(phrase.Trim());
            }     
        }

        private void Availability(string option, string singleton,
            out bool onlyIf, out bool singleIf)
        {
            onlyIf = true;

            if (!String.IsNullOrEmpty(option))
            {
                Game.Data.MethodFromBook("Actions.Availability",
                    out string onlyIfLine, param: option);

                onlyIf = onlyIfLine == "True";
            }

            if (singleton == "Availability")
            {
                singleIf = onlyIf;
            }
            else if (!String.IsNullOrEmpty(singleton))
            {
                Game.Data.MethodFromBook("Actions.Availability",
                    out string singleIfLine, param: singleton);

                singleIf = singleIfLine == "True";
            }
            else
            {
                singleIf = false;
            }
        }

        public void Paragraph(int id, bool reload = false, bool loadGame = false,
            string optionName = "", List<Abstract.IModification> optionModifications = null)
        {
            if (optionModifications != null)
                foreach(Abstract.IModification modification in optionModifications)
                    modification.Do();

            bool physicalStartOfGame = (id == Game.Data.PhysicalStartParagraph);
            bool logicalStartOfGame = (id == Game.Data.Constants.GetStartParagraph());
            bool startOfGame = physicalStartOfGame || logicalStartOfGame;

            PageClean();

            if (startOfGame)
            {
                Game.Data.Clean(reStart: true);
                Game.Data.MethodFromBook("Character.Init", out string _);
            }

            Game.Data.CurrentSelectedOption = optionName;
            Game.Paragraph paragraph = null;

            if ((Game.Data.CurrentParagraphID != id) || startOfGame || loadGame)
            {
                paragraph = Game.Data.Paragraphs.Get(id, Game.Data.XmlParagraphs[id]);

                Game.Data.CurrentParagraph = paragraph;
                Game.Data.CurrentParagraphID = id;
            }
            else
            {
                paragraph = Game.Data.CurrentParagraph;
            }

            GamepageSettings();

            string text = String.Empty;

            foreach (Output.Text texts in paragraph.Texts)
            {
                Text.Children.Add(Output.Interface.TextBySelect(texts));
                text += $"{texts.Content}\\n\\n";
            }

            if (Game.Settings.IsEnabled("Audiobook"))
                Speach(text);

            if ((paragraph.Images != null) && (paragraph.Images.Count > 0))
            {
                foreach (string image in paragraph.Images.Keys.ToList())
                {
                    if (!Game.Settings.IsEnabled("WithoutStyles"))
                        Text.Children.Add(Output.Interface.IllustrationImage(image));

                    if (!String.IsNullOrEmpty(paragraph.Images[image]))
                        Text.Children.Add(Output.Interface.Text(paragraph.Images[image]));
                }
            }

            bool walkingInCircles = Game.Data.Path.Contains(id.ToString());

            if (!loadGame && !reload && !walkingInCircles)
            {
                Game.Option.Trigger(paragraph.Trigger);
                Game.Option.Trigger(paragraph.Untrigger, remove: true);

                if ((paragraph.Modification != null) && (paragraph.Modification.Count > 0))
                    foreach (Abstract.IModification modification in paragraph.Modification)
                        modification.Do();
            }

            if (!loadGame && !reload)
                Game.Data.Path.Add(id.ToString());

            bool gameOver = false;

            if ((paragraph.Actions != null) && (paragraph.Actions.Count > 0))
            {
                foreach (Abstract.IActions action in paragraph.Actions)
                {
                    if (action.Type == "Option")
                    {
                        Button button = AddOptionButton(action.Option, ref gameOver);
                        Action.Children.Add(button);
                        Game.Option.ListAdd(action.Option, button);
                        AddAftertext(ref Action, action.Option.Aftertexts);
                    }
                    else
                    {
                        StackLayout actionPlace = Output.Interface.ActionPlace();

                        foreach (View enemy in Output.Interface.Represent(action.Do(out _, "Representer")))
                            actionPlace.Children.Add(enemy);

                        if (!action.Type.Contains("-"))
                        {
                            EventHandler actionClick = (object sender, EventArgs e) => Action_Click(action, actionPlace);
                            bool enabled = action.IsButtonEnabled();
                            actionPlace.Children.Add(Output.Buttons.Action(action.ButtonText(), actionClick, enabled));
                        }
                        else
                        {
                            actionPlace.Children.Add(MultipleButtonsPlace(action, actionPlace));
                        }

                        Action.Children.Add(actionPlace);
                        AddAftertext(ref Action, action.Texts);
                    }
                }
            }

            int optionCount = 0;
            Button singleton = null;
            Game.Data.Constants.ShowDisabledOption(out bool hideAllSingletons);

            foreach (Game.Option option in paragraph.Options)
            {
                int aftertextsCount = option.Aftertexts?.Count ?? 0;
                bool mustBeVisible = OptionVisibility(aftertextsCount > 0);

                Availability(option.Availability, option.Singleton, out bool onlyIf, out bool singleIf);

                Button button = AddOptionButton(option, ref gameOver);

                if (option.Dynamic)
                {
                    Game.Option.ListAdd(option, button);
                    button.IsVisible = false;
                }
                else if (!String.IsNullOrEmpty(option.Availability) && !onlyIf && !singleIf && !mustBeVisible)
                {
                    continue;
                }
                
                Options.Children.Add(button);

                if (!String.IsNullOrEmpty(option.Input))
                    Options.Children.Add(AddInputField(option, button));

                AddAftertext(ref Options, option.Aftertexts);

                if (singleIf)
                    singleton = button;

                optionCount += 1;
            }

            if (singleton != null)
            {
                foreach (View view in Options.Children.Where(x => x != singleton))
                {
                    Button button;

                    if (view is Button)
                        button = view as Button;
                    else
                        continue;

                    button.IsEnabled = false;
                    button.BackgroundColor = Color.Default;

                    if (hideAllSingletons)
                        button.IsVisible = false;
                }
            }

            if (optionCount > 1)
                gameOver = false;

            if (physicalStartOfGame && Game.Continue.IsGameSaved())
            {
                Options.Children.Add(Output.Buttons.Additional("Продолжить предыдущую игру",
                    (sender, e) => Continue_Click(), anywayLarge: true));
            }
            else if (!startOfGame && (Game.Data.Actions != null) && !gameOver)
            {
                foreach (string buttonName in Game.Healing.List())
                    AddAdditionalButton(buttonName, HealingButton_Click);

                foreach (string buttonName in Game.Data.Actions.StaticButtons())
                    AddAdditionalButton(buttonName, StaticButton_Click);
            }

            IsThisGameOver();
            OptionFooter(id);

            if (!reload)
                ScrollToTop();

            UpdateStatus();

            Game.Option.Trigger(paragraph.LateTrigger);

            if (!startOfGame && gameOver)
            {
                Game.Continue.Remove();
            }
            else if (!startOfGame)
            {
                Game.Continue.SaveCurrentGame();
            }
        }

        private StackLayout MultipleButtonsPlace(Abstract.IActions action, StackLayout actionPlace)
        {
            string[] buttons = action.Button.Split(',');
            int buttonIndex = 0;

            StackLayout buttonPlace = Output.Interface.MultipleButtonsPlace();

            foreach (string act in action.Type.Split('-'))
            {
                EventHandler actionClick = (object sender, EventArgs e) =>
                    Action_Click(action, actionPlace, anotherAction: act.Trim());

                Button button = Output.Buttons.Action(buttons[buttonIndex], actionClick,
                    action.IsButtonEnabled(buttonIndex > 0));

                button.HorizontalOptions = LayoutOptions.FillAndExpand;
                buttonPlace.Children.Add(button);

                buttonIndex += 1;
            }

            return buttonPlace;
        }

        private bool OptionVisibility(bool aftertext)
        {
            if (aftertext)
                return true;

            int bySetting = Game.Settings.GetValue("DisabledOption");

            if (bySetting > 0)
                return bySetting == 1;

            return Game.Data.Constants.ShowDisabledOption(out bool _);
        }

        private void AddAftertext(ref StackLayout layout, List<Output.Text> texts)
        {
            if ((texts == null) || (texts.Count <= 0))
                return;

            foreach (Output.Text aftertext in texts)
                layout.Children.Add(Output.Interface.TextBySelect(aftertext));
        }

        private Entry AddInputField(Game.Option option, object button)
        {
            Game.Data.InputResponse = option.Input;
            return Output.Interface.Field(button, InputChange);
        }

        public StackLayout SystemMenu()
        {
            StackLayout systemLayout = Output.Interface.SystemMenu();

            systemLayout.Children.Add(Output.Buttons.System("Выйти",
                (object sender, EventArgs e) => System.Diagnostics.Process.GetCurrentProcess().Kill()));

            systemLayout.Children.Add(Output.Buttons.System("Закладки",
                (object sender, EventArgs e) => this.Bookmarks_Click()));

            systemLayout.Children.Add(Output.Buttons.System("На главную",
                (object sender, EventArgs e) => this.Gamebooks(toMain: true)));

            return systemLayout;
        }

        private void InputChange(object sender, EventArgs e)
        {
            Entry field = sender as Entry;

            if (field.Text.ToLower() == Game.Data.InputResponse.ToLower())
            {
                field.IsVisible = false;
                Button button = (Button)field.BindingContext;
                button.IsVisible = true;
            }
        }

        private Button AddOptionButton(Game.Option option, ref bool gameOver)
        {
            Output.Buttons.EmptyOptionTextFuse(option);

            gameOver = (option.Goto == 0);

            EventHandler optionClick = (object sender, EventArgs e) =>
                Paragraph(option.Goto, optionName: option.Text, optionModifications: option.Do);

            return Output.Buttons.Option(option, optionClick);
        }

        private void AddAdditionalButton(string name, EventHandler eventHandler) =>
            Options.Children.Add(Output.Buttons.Additional(name, eventHandler));

        private void UpdateStatus()
        {
            Status.Children.Clear();
            AdditionalStatus.Children.Clear();

            List<string> statuses = (Game.Data.Actions == null ? null : Game.Data.Actions.Status());

            if (Game.Services.ParagraphsWithoutStatuses(statuses))
            {
                StatusBorder.IsVisible = false;
                MainGrid.RowDefinitions[1].Height = 0;

                Status.IsVisible = false;
                MainGrid.RowDefinitions[2].Height = 0;
            }
            else
            {
                Status.IsVisible = true;

                MainGrid.RowDefinitions[2].Height = 30;

                Status.BackgroundColor = Color.FromHex(
                    Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar));

                foreach (Label status in Output.StatusBar.Main(statuses))
                    Status.Children.Add(status);

                string borderColor = Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBorder);

                if (!String.IsNullOrEmpty(borderColor))
                {
                    StatusBorder.BackgroundColor = Color.FromHex(borderColor);

                    StatusBorder.IsVisible = true;
                    MainGrid.RowDefinitions[1].Height = 1;
                }
            }

            List<string> addStatuses = Game.Data.Actions == null ?
                null : Game.Data.Actions.AdditionalStatus();

            bool noAdditionalStatuses = addStatuses == null || Game.Data.Constants
                .GetParagraphsWithoutStatuses()
                .Contains(Game.Data.CurrentParagraphID);

            if (noAdditionalStatuses)
            {
                AdditionalStatus.IsVisible = false;
                MainGrid.ColumnDefinitions[1].Width = 0;
            }
            else
            {
                string backgroundColor = Game.Data.Constants.GetColor(
                    Game.Data.ColorTypes.AdditionalStatus);

                MainGrid.ColumnDefinitions[1].Width = 20;

                AdditionalStatus.BackgroundColor = String.IsNullOrEmpty(backgroundColor) ?
                    Color.LightGray : Color.FromHex(backgroundColor);

                AdditionalStatus.IsVisible = true;

                List<Output.VerticalText> statusesInBar = Output.StatusBar.Additional(addStatuses);

                foreach (Output.VerticalText status in statusesInBar)
                    AdditionalStatus.Children.Add(status);
            }
        }

        private void GamepageSettings()
        {
            string backColor = Game.Data.Constants.GetColor(Game.Data.ColorTypes.Background);
            bool emptyBackColor = String.IsNullOrEmpty(backColor);
            MainScroll.BackgroundColor = emptyBackColor ? Color.White : Color.FromHex(backColor);
        }

        private bool IsThisGameOver(Abstract.IActions currentAction = null)
        {
            Abstract.IActions action = currentAction ?? Game.Data.Actions;

            if ((action == null) || !action.GameOver(out int toEndParagraph, out string toEndText))
                return false;

            if (Game.Settings.IsEnabled("Godmode"))
                return false;

            Options.Children.Clear();

            EventHandler gameoverClick = (object sender, EventArgs e) =>
                Paragraph(toEndParagraph, optionName: toEndText);

            Options.Children.Add(Output.Buttons.GameOver(toEndText, gameoverClick));

            return true;
        }

        private void OptionFooter(int id)
        {
            if (Game.Settings.IsEnabled("CheatingBack") && (Game.Data.Path.Count > 1))
                Options.Children.Add(Output.Buttons.Additional("Читерство: Назад", Back_Click));

            if (!Game.Settings.IsEnabled("SystemMenu"))
                Options.Children.Add(SystemMenu());

            if (Game.Settings.IsEnabled("Debug"))
                Options.Children.Add(Output.Debug.Information(id));
        }
               
        private void Action_Click(Abstract.IActions action, StackLayout actionPlace, string anotherAction = "")
        {
            actionPlace.Children.Clear();

            List<string> actionResult = action.Do(out bool reload, Trigger: true, action: anotherAction);

            if (reload)
            {
                Paragraph(Game.Data.CurrentParagraphID, reload: true);
            }
            else
            {
                foreach (View actionLine in Output.Interface.Actions(actionResult))
                    actionPlace.Children.Add(actionLine);

                UpdateStatus();

                if (IsThisGameOver(action))
                    OptionFooter(Game.Data.CurrentParagraphID);
            }
        }

        private void StaticButton_Click(object sender, EventArgs e)
        {
            bool reload = Game.Data.Actions.StaticAction((sender as Button).Text);

            if (reload)
                Paragraph(Game.Data.CurrentParagraphID, reload: true);
        }
        
        private void HealingButton_Click(object sender, EventArgs e)
        {
            Game.Healing.Use((sender as Button).Text);
            Paragraph(Game.Data.CurrentParagraphID, reload: true);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            int lastStep = Game.Data.Path.Count() - 1;
            Game.Data.Path.RemoveAt(lastStep);

            int returnId = int.Parse(Game.Data.Path[lastStep - 1]);

            Paragraph(returnId, reload: true);
        }

        private void BookmarkRemove_Click(string bookmark)
        {
            Game.Bookmarks.Remove(bookmark);
            Bookmarks_Click();
        }

        private void Continue_Click(string bookmark = "")
        {
            int id = 0;

            try
            {
                id = Game.Continue.Load(bookmark);
            }
            catch (Exception ex)
            {
                Error("Ошибка продолжения игры:", ex.Message);
                return;
            }

            Paragraph(id, loadGame: true);
        }

        private void PageClean()
        {
            Game.Data.InputResponse = String.Empty;

            Text.Children.Clear();
            Action.Children.Clear();
            Options.Children.Clear();
            Footer.Children.Clear();

            Game.Option.ListClean();
        }
    }
}
