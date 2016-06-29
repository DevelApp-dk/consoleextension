using System;
using System.Collections.Generic;
using WindowsInput;

namespace ConsoleTester
{
    public static class ConsoleTest
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
                    foreach (string groupName in TestGroupsToRun.Keys)
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
                    Dictionary<string, Test> testGroup;
                    if (!TestGroupsToRun.TryGetValue(selectedGroupName, out testGroup))
                    {
                        GetNextInput(string.Format("Could not get {0}. Returning to group selection. Press Enter to procede.", selectedGroupName));
                        selectedGroupName = null;
                    }
                    else
                    {
                        Console.WriteLine("Use: Select an action by entering number and hit Enter.");
                        Console.WriteLine("0) Return to previous list.");
                        foreach (Test test in testGroup.Values)
                        {
                            Console.WriteLine(test.ListNumber.ToString() + ") " + test.Explanation);
                        }
                        inputLine = Console.ReadLine();
                        if (inputLine.Equals("0"))
                        {
                            selectedGroupName = null;
                        }
                        else
                        {
                            Test test = null;
                            if (testGroup.TryGetValue(inputLine, out test))
                            {
                                try
                                {
                                    test.TestToRun.Invoke();
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

        public static bool RunCommandLine(string[] args)
        {

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
                InputSimulator.SimulateTextEntry(defaultValue);
            }
            string capturedString = Console.ReadLine();
            Console.WriteLine(string.Format("Captured: {0}", capturedString));

            return capturedString;
        }

        static Dictionary<string, Dictionary<string, Test>> _testsToRun = new Dictionary<string, Dictionary<string, Test>>();

        private static int NewTestNumber(string groupName)
        {
            return TestsToRun(groupName).Count + 1;
        }
        public static void AddTest(string groupName, string explanation, Action testToRun)
        {
            Test test = new Test(NewTestNumber(groupName), explanation, testToRun);
            TestsToRun(groupName).Add(test.ListNumber.ToString(), test);
        }

        private static Dictionary<string, Dictionary<string, Test>> TestGroupsToRun
        {
            get
            {
                return _testsToRun;
            }
        }

        private static Dictionary<string, Test> TestsToRun(string groupName)
        {
            Dictionary<string, Test> testsToRun;
            if (TestGroupsToRun.ContainsKey(groupName))
            {
                if (TestGroupsToRun.TryGetValue(groupName, out testsToRun))
                {
                    return testsToRun;
                }
                else
                {
                    throw new Exception("Could not get test group out.");
                }
            }
            else
            {
                testsToRun = new Dictionary<string, Test>();
                TestGroupsToRun.Add(groupName, testsToRun);
                return testsToRun;
            }
        }

        private class Test
        {
            public Test(int listNumber, string explanation, Action testToRun)
            {
                ListNumber = listNumber;
                Explanation = explanation;
                TestToRun = testToRun;
            }

            public int ListNumber { get; private set; }

            public string Explanation { get; private set; }

            public Action TestToRun { get; private set; }
        }

    }
}
