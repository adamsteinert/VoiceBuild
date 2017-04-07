MasterGrammar
	=> (Project,Verb)


VoiceActionFactory
	=> VoiceAction

VoiceAction.Execute()
	=> (outputs, user actions)


Grammars:
	OpenSolution: "open solution for PROJECT"

Actions
	OpenSolutionSimple - Looks for a .sln file in the root directory and opens it. If multiples, opens first.  Otherwise search directories until first is found using Files.Enumerate
	Logs when multiple solutions are found. Suggests solution override in configuration



User definable properties:

Defaults: 
	VsVersion


Define:
	Project Name
	Project Directory
	Repository Web page
	CI Web Page
	Override test script
	Override default solution
	Override build script
	Override VSVersion
	
	
Commands:
	Open XYZ in IE|
	
	
Build PROJECT 
checkout PROJECT on master, latest, QA
Load PROJECT in visual studio
Load PROJECT in vs code
Load PROJECT in gitKraken
Run PROJECT tests 
DOCKER up PROJECT
open explorer for PROJECT
