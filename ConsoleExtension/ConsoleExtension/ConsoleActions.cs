using System;
using System.Collections.Generic;

namespace ConsoleExtension
{
    public static class ConsoleActions
    {
        public static void Run()
        {
            string inputLine = string.Empty;
            bool exit = false;
            string selectedGroupName = null;
            while (!exit)
            {
                Console.Clear();
                if (string.IsNullOrWhiteSpace(selectedGroupName))
                {
                    Console.WriteLine("Use: Select a action group by entering number and hit Enter.");
                    Console.WriteLine("0) Exit");
                    Dictionary<string, string> selectionList = new Dictionary<string, string>();
                    int counter = 1;
                    foreach (string groupName in ActionGroupsToRun.Keys)
                    {
                        selectionList.Add(counter.ToString(), groupName);
                        Console.WriteLine(counter.ToString() + ") " + groupName);
                        counter += 1;
                    }
                    inputLine = Console.ReadLine();
                    if (inputLine.Equals("0"))
                    {
                        exit = true;
                    }
                    else
                    {
                        if (!selectionList.TryGetValue(inputLine, out selectedGroupName))
                        {
                            GetNextInput(string.Format("Unknown input: |{0}|. Please try again. Press Enter to procede.", inputLine));
                        }
                    }
                }
                else
                {
                    Dictionary<string, ConsoleAction> testGroup;
                    if (!ActionGroupsToRun.TryGetValue(selectedGroupName, out testGroup))
                    {
                        GetNextInput(string.Format("Could not get {0}. Returning to group selection. Press Enter to procede.", selectedGroupName));
                        selectedGroupName = null;
                    }
                    else
                    {
                        Console.WriteLine("Use: Select an action by entering number and hit Enter.");
                        Console.WriteLine("0) Return to previous list.");
                        foreach (ConsoleAction test in testGroup.Values)
                        {
                            Console.WriteLine(test.ListNumber.ToString() + ") " + test.ActionExplanation);
                        }
                        inputLine = Console.ReadLine();
                        if (inputLine.Equals("0"))
                        {
                            selectedGroupName = null;
                        }
                        else
                        {
                            ConsoleAction test = null;
                            if (testGroup.TryGetValue(inputLine, out test))
                            {
                                try
                                {
                                    test.ActionToRun.Invoke();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(string.Format("Exception cast. Type : {0}", ex.GetType().ToString()));
                                    Console.WriteLine(string.Format("Exception message: {0}", ex.Message));
                                    Console.WriteLine(string.Format("Exception stacktrace: {0}", ex.StackTrace));
                                    GetNextInput("Press Enter to end test");
                                }
                            }
                            else
                            {
                                GetNextInput(string.Format("Unknown input: |{0}|. Please try again. Press Enter to procede.", inputLine));
                            }
                        }
                    }
                }
            }
        }

        public static string GetNextInput(string inputRequest)
        {
            return GetNextInput(inputRequest, null);
        }

        public static string GetNextInput(string inputRequest, string defaultValue)
        {
            Console.WriteLine();
            if (inputRequest != null)
            {
                Console.Write(inputRequest);
            }
            if (defaultValue != null)
            {
                KeyboardTextInput.TextInput(defaultValue);
            }
            string capturedString = Console.ReadLine();
            Console.WriteLine(string.Format("Captured: {0}", capturedString));

            return capturedString;
        }

        static Dictionary<string, Dictionary<string, ConsoleAction>> _actionsToRun = new Dictionary<string, Dictionary<string, ConsoleAction>>();

        private static int NewActionNumber(string groupName)
        {
            return ActionsToRun(groupName).Count + 1;
        }
        public static void AddAction(string groupName, string actionExplanation, Action testToRun)
        {
            ConsoleAction test = new ConsoleAction(NewActionNumber(groupName), actionExplanation, testToRun);
            ActionsToRun(groupName).Add(test.ListNumber.ToString(), test);
        }

        private static Dictionary<string, Dictionary<string, ConsoleAction>> ActionGroupsToRun
        {
            get
            {
                return _actionsToRun;
            }
        }

        private static Dictionary<string, ConsoleAction> ActionsToRun(string groupName)
        {
            Dictionary<string, ConsoleAction> actionsToRun;
            if (ActionGroupsToRun.ContainsKey(groupName))
            {
                if (ActionGroupsToRun.TryGetValue(groupName, out actionsToRun))
                {
                    return actionsToRun;
                }
                else
                {
                    throw new Exception("Could not get test group out.");
                }
            }
            else
            {
                actionsToRun = new Dictionary<string, ConsoleAction>();
                ActionGroupsToRun.Add(groupName, actionsToRun);
                return actionsToRun;
            }
        }

        private class ConsoleAction
        {
            public ConsoleAction(int listNumber, string actionExplanation, Action actionToRun)
            {
                ListNumber = listNumber;
                ActionExplanation = actionExplanation;
                ActionToRun = actionToRun;
            }

            public int ListNumber { get; private set; }

            public string ActionExplanation { get; private set; }

            public Action ActionToRun { get; private set; }
        }

    }
}
