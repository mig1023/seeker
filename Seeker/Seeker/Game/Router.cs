using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Router
    {
        private static Dictionary<string, int> Destinations = new Dictionary<string, int>();

        private static Dictionary<string, Abstract.IModification> DestinationsAccompanyingActions = new Dictionary<string, Abstract.IModification>();

        private static Dictionary<string, int> Actions = new Dictionary<string, int>();

        private static Dictionary<int, StackLayout> ActionsPlaces = new Dictionary<int, StackLayout>();

        public static void Clean()
        {
            Destinations.Clear();
            DestinationsAccompanyingActions.Clear();
            Actions.Clear();
            ActionsPlaces.Clear();
        }

        public static void AddDestination(string text, int index, Abstract.IModification modification = null)
        {
            if (modification != null)
                DestinationsAccompanyingActions.Add(text, modification);

            Destinations.Add(text, index);
        }

        public static void AddAction(string text, int index)
        {
            Actions.Add(text, index);
        }

        public static void AddActionsPlaces(int index, StackLayout stackLayout)
        {
            ActionsPlaces.Add(index, stackLayout);
        }

        public static int FindDestination(string text)
        {
            if (!Destinations.ContainsKey(text))
                return 0;

            if (DestinationsAccompanyingActions.ContainsKey(text))
                DestinationsAccompanyingActions[text].Do();

            return Destinations[text];
        }

        public static int FindAction(string text)
        {
            return Actions[text];
        }

        public static StackLayout FindActionsPlaces(int index)
        {
            return ActionsPlaces[index];
        }
    }
}
