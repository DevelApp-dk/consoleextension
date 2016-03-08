# Console Tester #

Sometimes it is useful to test from a console when it is against a standard api, system operations or on a server without debugging possibilities where Unit Testing would not make sence.

### What is this repository for? ###

* A simple static class for making tests from a console application.
* Version 0.1.0.0

### Use ###


* Use **ConsoleTest.AddTest(string groupName, string explanation, Action testToRun)** to add first a groupname for the tests, an explanation for the individual test and an Action that runs the test. Avoid placing more than 20 tests in each group as the scroll will make selections impossible to see.
F.x. 
```
#!C#
ConsoleTest.AddTest("DirectoryHelper", "Test validate local directory."
, () => TestValidateDirectory());

```
The "TestValidateDirectory()" method is assumed to be a static method reachable from the console.

* Use **ConsoleTest.GetNextInput(string inputRequest)** to request input from the user ending with use of Enter. inputRequest is the test written immidiate before the input. The method returns the string that the user have entered before the Enter.
Fx.

```
#!C#

ConsoleTest.GetNextInput("Press Enter to clean up test");

```


* Use **ConsoleTest.GetNextInput(string inputRequest, string defaultValue)** to request input from the user ending with use of Enter. inputRequest is the test written immidiate before the input. defaultValue is written as the suggestion for the input and is written as simulated keyboard input so if the user is just pressing Enter  the defaultvalue will be returned as string and if the user modifies the default value and hit Enter the modifies value will be returned as string. The method returns the string that the user have entered before the Enter.
Fx.

```
#!C#

string subDirectoryString = 
ConsoleTest.GetNextInput("Enter sub dircetory to test. End with Enter: ", "test");
```


* Use **ConsoleTest.Run()** to start running the console interface. First select the test group and then the the individual tests.

* How to run tests
Select the test group and test in the console to run the tests.

### Configuration ###

* Dependencies
There is a dependency to InputSimulator under Ms-PL License. The NuGet version is buggy and because of that the dll is included in download.

* Deployment instructions
Copy dll's to project and add reference to the library.

### Who do I talk to? ###

* Repo owner