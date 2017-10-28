# Console Extension #

Sometimes it is useful to test or other actions from a console when it is against a standard api, system operations or on a server without debugging possibilities where Unit Testing would not make sence.

### What is this repository for? ###

* A simple static class for running actions from a console application.
* Version 0.2.0.0

### Use ###


* Use **ConsoleActions.AddAction(string groupName, string actionName, Action actionToRun)** to add first a groupname for the actions, an actionName for the individual actions and an Action that runs the action. Avoid placing more than 20 tests in each group as the scroll will make selections impossible to see.
F.x. 
```
#!C#
ConsoleExtension.AddAction("DirectoryHelper", "Test validate local directory."
, () => TestValidateDirectory());

```
The "TestValidateDirectory()" method is assumed to be a static method reachable from the console.

* Use **ConsoleExtension.GetNextInput(string inputRequest)** to request input from the user ending with use of Enter. inputRequest is the test written immidiate before the input. The method returns the string that the user have entered before the Enter.
Fx.

```
#!C#

ConsoleExtension.GetNextInput("Press Enter to clean up action");

```


* Use **ConsoleExtension.GetNextInput(string inputRequest, string defaultValue)** to request input from the user ending with use of Enter. inputRequest is the test written immidiate before the input. defaultValue is written as the suggestion for the input and is written as simulated keyboard input so if the user is just pressing Enter  the defaultvalue will be returned as string and if the user modifies the default value and hit Enter the modifies value will be returned as string. The method returns the string that the user have entered before the Enter.
Fx.

```
#!C#

string subDirectoryString = 
ConsoleExtension.GetNextInput("Enter sub dircetory to test. End with Enter: ", "test");
```


* Use **ConsoleExtension.Run()** to start running the console interface. First select the action group and then the the individual actions.

* How to run actions
Select the action group and action in the console to run the actions.

### Who do I talk to? ###

* Repo owner